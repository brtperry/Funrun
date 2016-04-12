using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class averyJ8160 : System.Web.UI.Page
{
    const string cs = "Persist Security Info=False;Integrated Security=SSPI;database=Funrun;server=.\\SQLExpress;Connect Timeout=30;User ID=sa;Password=M1s0nly99";

    private string AddFieldValue(DataRow DR, string Field)
    {
        return DBNull.Value.Equals(DR[Field]) ? String.Empty : (string)DR[Field];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<string> Data = new List<string>();

            int Hi = int.Parse(Page.Request.QueryString["hi"]);
            int Lo = int.Parse(Page.Request.QueryString["lo"]);

            string SQL = string.Format("SELECT Forename, Surname, House, Street, Locality, County, Postcode, ID FROM Runners WHERE ID BETWEEN {0} AND {1} ORDER BY ID ASC",Lo,Hi);

            try
            {
                using (SqlConnection Connection = new SqlConnection(cs))
                {
                    using (SqlDataAdapter DAdapter = new SqlDataAdapter(SQL, Connection))
                    {
                        using (DataSet DS = new DataSet())
                        {
                            DAdapter.Fill(DS);

                            if (DS.Tables.Count > 0)
                            {
                                if (DS.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow DR in DS.Tables[0].Rows)
                                    {
                                        string SB;

                                        SB = string.Format("{0} {1} {2}<br />", DR["Forename"], DR["Surname"], DR["ID"].ToString());
                                        SB += string.Format("{0} {1}<br />", AddFieldValue(DR, "House"), AddFieldValue(DR, "Street"));
                                        SB += string.Format("{0}<br />", AddFieldValue(DR, "Locality"));
                                        SB += string.Format("{0}<br />", AddFieldValue(DR, "County"));
                                        SB += string.Format("{0}", AddFieldValue(DR, "Postcode"));
                                        Data.Add(SB);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ep)
            {
                //Label1.Text = ep.Message;
            }

            /***
             * 
             * Hopefully we have some data...create 3 * ( n ) table matrix
             * 
             ***/
            int x = 0;
            int y = 0;

            while (x < Data.Count)
            {
                TableRow TRow = new TableRow();

                for (int j = 0; j < 3; j++)
                {
                    TableCell TCell = new TableCell();

                    if (x >= Data.Count)
                    {
                        TCell.Text = "empty column";
                    }
                    else
                    {
                        TCell.Text = Data[x];
                        x += 1;
                    }

                    switch (y)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                            switch (j)
                            {
                                case 0: TCell.CssClass = "tdleft paddb normheight";
                                    break;
                                case 1: TCell.CssClass = "tdcenter paddb normheight";
                                    break;
                                case 2: TCell.CssClass = "tdright paddb normheight";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 4:
                            switch (j)
                            {
                                case 0: TCell.CssClass = "tdleft incheight";
                                    break;
                                case 1: TCell.CssClass = "tdcenter incheight";
                                    break;
                                case 2: TCell.CssClass = "tdright incheight";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 6:
                            switch (j)
                            {
                                case 0: TCell.CssClass = "tdleft normheight";
                                    break;
                                case 1: TCell.CssClass = "tdcenter normheight";
                                    break;
                                case 2: TCell.CssClass = "tdright normheight";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    TRow.Cells.Add(TCell);
                }
                y = y == 7 ? 0 : y += 1;
                tblAveryJ8160.Rows.Add(TRow);
            }
        }
        catch (Exception ex)
        {
            //Label1.Text = ex.Message;
        }
    }
}
