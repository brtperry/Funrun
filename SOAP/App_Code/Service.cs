
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using System.Data.SqlClient;

[WebService(Namespace = "http://81.143.88.106/SOAP/", Description = "Web service functions for managing the Beaverbrooks Blackpool 10K Fun Run")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    const string cs = @"Persist Security Info=False;Integrated Security=SSPI;database=Funrun;server=.\SQLExpress;Connect Timeout=30;User ID=sa;Password=M1s0nly99";

    public Service()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public struct Addr
    {
        public string House;
        public string Street;
        public string Locality;
        public string County;
        public string Postcode;
    }
    public struct Runner
    {
        public int ID;
        public int Age;
        public int HashID;
        public bool Gender;
        public string Title;
        public string Forename;
        public string Surname;
        public string Postcode;
        public string Team;
        public string Mail;
	public Addr Address;
    }
    public struct Locales
    {
        public int HashID;
        public string Postcode;
    }

    [WebMethod(Description = "Return a batch of 21 registered runners where their locality is null and Paid has been verified.")]
    public List<Locales> GetEmptyLocales()
    {
        List<Locales> Results = new List<Locales>();

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter("Exec GetEmptyLocales", Connection))
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
                                Locales Local = new Locales();
                                Local.HashID = int.Parse(DR["HashID"].ToString());
                                Local.Postcode = DR["Postcode"].ToString();

                                Results.Add(Local);
                            }
                        }
                    }
                }
            }
        }
        return Results;
    }

    [WebMethod(Description = "Return a batch of 21 registered runners where ID, their (RaceID) is null.")]
    public List<int> GetEmptyRaceIDs()
    {
        List<int> Results = new List<int>();

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter("Exec GeHashIDForEmptyIDCodes", Connection))
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
                                try
                                {
                                    Results.Add(int.Parse(DR["HashID"].ToString()));
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
        return Results;
    }

    [WebMethod(Description = "Return the highest RaceID in the table.")]
    public int GetHighOrderRaceNumber()
    {
        int Result = -1;

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter("Exec GetHighOrderRaceNumber", Connection))
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
                                try
                                {
                                    Result = DBNull.Value.Equals(DR["HighOrder"]) ? -10 : int.Parse(DR["HighOrder"].ToString());
                                }
                                catch
                                {
                                    Result = -1;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        return Result;
    }

    [WebMethod(Description = "Return the RaceID for the runner at the index.")]
    public int GetRaceID(int HashID)
    {
        int Result = 0;

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter(string.Format("Exec GetRaceID {0}",HashID), Connection))
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
                                try
                                {
                                    if (!DBNull.Value.Equals(DR["ID"]))
                                    {
                                        Result = (int)DR["ID"];
                                    }                                    
                                }
                                catch
                                {
                                    Result = 0;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        return Result;
    }

    [WebMethod(Description = "Does the ID already exist for this runner.")]
    public bool IDExists(int ID)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter(string.Format("Exec IDExists {0}", ID), Connection))
            {
                using (DataSet DS = new DataSet())
                {
                    DAdapter.Fill(DS);

                    if (DS.Tables.Count > 0)
                        if (DS.Tables[0].Rows.Count > 0)
                            return true;
                }
            }
        }
        return false;
    }


    [WebMethod(Description = "Return a list of runner structures where their postcode is the same as the value passed.")]
    public List<Runner> GetRunnerByPostcode(string Postcode)
    {
        List<Runner> Runners = new List<Runner>();

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter(string.Format("Exec GetRunnerByPostCode '{0}'", Postcode), Connection))
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
                                try
                                {
                                    Runner R = new Runner();

                                    R.Forename = DR["Forename"].ToString();
                                    R.Surname = DR["Surname"].ToString();
                                    R.Mail = DR["Mail"].ToString();
                                    R.HashID = int.Parse(DR["HashID"].ToString());

                                    Runners.Add(R);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
        return Runners;
    }


    [WebMethod(Description = "Get a list of runner structures where their mail address is like value passed.")]
    public List<Runner> GetRunnersByMailAddress(string Mail)
    {
        List<Runner> Runners = new List<Runner>();

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter(string.Format("Exec GetRunnerByMail '{0}'", Mail), Connection))
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
                                try
                                {
                                    Runner R = new Runner();

                                    R.Forename = DR["Forename"].ToString();
                                    R.Surname = DR["Surname"].ToString();
                                    R.Mail = DR["Mail"].ToString();
                                    R.HashID = int.Parse(DR["HashID"].ToString());

                                    Runners.Add(R);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
        return Runners;
    }

    [WebMethod(Description = "Get a list of all runners.")]
    public List<Runner> GetRunners()
    {
        List<Runner> Runners = new List<Runner>();

        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlDataAdapter DAdapter = new SqlDataAdapter("Exec GetRunners", Connection))
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
                                try
                                {
                                    Runner R = new Runner();

                                    if (!DBNull.Value.Equals(DR["ID"]))
                                    {
                                        R.ID = (int)DR["ID"];
                                        Addr Address = new Addr();

                                        try
                                        {
                                            Address.House = DR["House"].ToString();
                                            Address.Street = DR["Street"].ToString();
                                            Address.Locality = DR["Locality"].ToString();
                                            Address.County = DR["County"].ToString();
                                            Address.Postcode = DR["Postcode"].ToString();
                                        }
                                        finally
                                        {
                                            R.Address = Address;
                                        }                                        
                                    }
                                        
                                    R.Age = (int)DR["Age"];
                                    R.HashID = (int)DR["HashID"];
                                    R.Gender = (bool)DR["Gender"];
                                    R.Title = DR["Title"].ToString();
                                    R.Forename = DR["Forename"].ToString();
                                    R.Surname = DR["Surname"].ToString();
                                    R.Mail = DR["Mail"].ToString();

                                    Runners.Add(R);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
        return Runners;
    }


    [WebMethod(Description = "Set the address for a web registered runner, passing Address and unique HashID.")]
    public int SetAddress(Addr Address, int Hash)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "SetRunnersAddress";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Connection = Connection;

                Command.Parameters.AddWithValue("@Street", Address.Street);
                Command.Parameters.AddWithValue("@Locality", Address.Locality);
                Command.Parameters.AddWithValue("@County", Address.County);
                Command.Parameters.AddWithValue("@HashID", Hash);

                try
                {
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                }
                catch
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    [WebMethod(Description = "Mark the registered runner as paid.")]
    public int SetPaid(int Hash)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "SetPaid";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Connection = Connection;
                Command.Parameters.AddWithValue("@HashID", Hash);

                try
                {
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                }
                catch
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    [WebMethod(Description = "Set the RaceID for a registered runner, passing a unique HashID and a new RaceID.")]
    public int SetRaceNumber(int HashID, int RaceID)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "SetRaceNumber";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Connection = Connection;

                Command.Parameters.AddWithValue("@ID", RaceID);
                Command.Parameters.AddWithValue("@HashID", HashID);

                try
                {
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                }
                catch
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    [WebMethod(Description = "Set new Runners data.")]
    public int SetRunner(Runner R, String H)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "SetRunner";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Connection = Connection;

                Command.Parameters.AddWithValue("@Title", R.Title);
                Command.Parameters.AddWithValue("@ForeName", R.Forename);
                Command.Parameters.AddWithValue("@Surname", R.Surname);
                Command.Parameters.AddWithValue("@Gender", R.Gender);
                Command.Parameters.AddWithValue("@House", H);
                Command.Parameters.AddWithValue("@ID", R.ID);
                Command.Parameters.AddWithValue("@Team", R.Team);
                Command.Parameters.AddWithValue("@Mail", R.Mail);
                Command.Parameters.AddWithValue("@Postcode", R.Postcode);
                Command.Parameters.AddWithValue("@Age", R.Age);

                try
                {
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                }
                catch
                {
                    return 1;
                }
            }
        }
        return 0;
    }


    [WebMethod(Description = "Set new Runners data.")]
    public int SetRunnerPosition(int ID, int Position, String Time)
    {
        using (SqlConnection Connection = new SqlConnection(cs))
        {
            using (SqlCommand Command = new SqlCommand())
            {
                Command.CommandText = "SetRunnerPosition";
                Command.CommandType = CommandType.StoredProcedure;
                Command.Connection = Connection;

                Command.Parameters.AddWithValue("@ID", ID);
                Command.Parameters.AddWithValue("@Position", Position);
                Command.Parameters.AddWithValue("@Time", Time);

                try
                {
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();
                    Command.Connection.Close();
                }
                catch
                {
                    return 1;
                }
            }
        }
        return 0;
    }


}
