using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    const string ConnectionString = "Persist Security Info=False;Integrated Security=SSPI;database=Funrun;server=.\\SQLExpress;Connect Timeout=30;User ID=sa;Password=M1s0nly99";
    //const string ConnectionString = "Persist Security Info=False;Integrated Security=SSPI;database=Funrun;server=.\\SQLExpress;Connect Timeout=30;User ID=sa;Password=m1s0nly";

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string location = "paypal.html";

        try
        {
            bool successful = false;

            Register(ref successful);

            if (successful)
            {
                int age = int.Parse(txtAge.Text);

                if (age >= 16)
                {
                    location = "paypal16.html";
                }
            }
            else
            {
                location = "exception.html";
            }
        }
        catch (Exception)
        {
            location = "exception.html";
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "blaster", "self.parent.location='" + location + "';", true);
        }
    }

    void Register(ref bool success)
    {
        using (SqlConnection mc = new SqlConnection(ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("AddWebRunner", mc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@HNN", SqlDbType.VarChar).Value = Server.HtmlEncode(txtHouseNumber.Text);
                cmd.Parameters.Add("@Postcode", SqlDbType.VarChar).Value = Server.HtmlEncode(txtPostcode.Text);
                cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Server.HtmlEncode(ddlTitle.Text);
                cmd.Parameters.Add("@Forename", SqlDbType.VarChar).Value = Server.HtmlEncode(txtForename.Text);
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = Server.HtmlEncode(txtSurname.Text);
                cmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = Server.HtmlEncode(txtMail.Text);
                cmd.Parameters.Add("@Team", SqlDbType.VarChar).Value = string.IsNullOrEmpty(txtTeam.Text) ? "" : Server.HtmlEncode(txtTeam.Text);
                cmd.Parameters.Add("@Age", SqlDbType.Int).Value = int.Parse(txtAge.Text);

                /*
 
                 determine gender
 
                */
                bool Male = false;

                if (Radio1.Checked)
                {
                    Male = true;
                }

                cmd.Parameters.Add("@Gender", SqlDbType.Bit).Value = Male;

                try
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    success = true;
                }
                catch
                {
                    throw;
                }
            }
        }
    }

}