using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Data;

namespace Chemistree_GUI_V1
{
    class DBConnection
    {
        private string username = "mendeleev";
        private string password = "periodic";
        private string server = "localhost";
        private string database = "chemistree";
        private int port = 3306;
        public Element e = new Element();
        private MySqlConnection conn;

        public DBConnection()
        {

        }

        public bool ConnectToDB()
        {
            bool result = false;
            string connStr = $"server={this.server};user={this.username};database={this.database};port={this.port};password={this.password};";
            //string connStr = "server=" + this.server + ";user=" + this.username + ";database=" + this.database + ";port=" + this.port + ";password=" + this.password + ";";
            this.conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                // Handle Exception
            }
            return result;
        }

        public void CloseDB()
        {
            this.conn.Close();
        }

        public bool SubmitDB(Element e)
        {
            bool result = false;
            FormattableString sql = $"INSERT INTO elements VALUES ('{e.abbr}', '{e.name}', '{e.atomicNumber}', '{e.periodicGroup}', '{e.periodicPeriod}', '{e.electronConfiguration}');";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql.ToString(), conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = (rowsAffected > 0) ? true : false;
            }
            catch (Exception ex)
            {
                // Better handle exception
            }
            return result;
        }


        public bool SubmitIonDB(Ion i)
        {
            bool result = false;
            FormattableString sql = $"INSERT INTO ions (`abbreviation`, `name`, `charge`, `type`) VALUES ('{i.ionAbbr}', '{i.ionName}', '{i.ionCharge}', '{i.ionType}');";


            try
            {
                MySqlCommand cmd = new MySqlCommand(sql.ToString(), conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                result = (rowsAffected > 0) ? true : false;
            }
            catch (Exception ex)
            {
                // Better handle exception
                // Console.WriteLine("{0}", ex);
            }
            return result;
        }



        public (bool, Element) queryDB(string abbr)
        {
            bool result = false;
            Element e = new Element();
            //query statement for database
            string query = $"SELECT * FROM elements WHERE abbreviation = '{abbr}';";
            //Working on how to use a procedure to query the database making it more secure.

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                //Begin reading the database
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //By reading the table column name it prevents issues if more columns are added in the future.
                        e.abbr = reader["abbreviation"].ToString();
                        e.name = reader["name"].ToString();
                        e.atomicNumber = reader["atomicNumber"].ToString();
                        e.periodicGroup = reader["periodicGroup"].ToString();
                        e.periodicPeriod = reader["periodicPeriod"].ToString();
                        e.electronConfiguration = reader["electronConfiguration"].ToString();
                    }
                    result = true;
                }
                // Close the database.
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle Exception
            }
            return (result, e);
        }

        public void queryIonDB(ref Ion userIon, string ionType, ref string errorMessage)
        {
            bool result = false;
            string query = $"SELECT * FROM ions " +
                           $"WHERE abbreviation = '{userIon.ionAbbr}'" +
                           $"AND charge = '{userIon.ionCharge}'" +
                           $"AND type = '{ionType}'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        userIon.ionName = reader["name"].ToString();
                    }

                    result = true;
                }

                if (result == false)
                {
                    errorMessage = "Ion not found. The " + ionType + " you entered either doesn't exist, is in the wrong category, or perhaps you misspelled it.";
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                errorMessage = "Encountered an issue with the database. Try again." + ex;
            }
        }
    }

    class Element
    {
        public string abbr;
        public string name;
        public string atomicNumber;
        public string periodicGroup;
        public string periodicPeriod;
        public string electronConfiguration;

        public Element()
        {
            this.abbr = "";
            this.name = "";
            this.atomicNumber = "";
            this.periodicGroup = "";
            this.periodicPeriod = "";
            this.electronConfiguration = "";
        }

        public Element(string abbr, string name, string atomicNumber, string periodicGroup, string periodicPeriod, string electronConfiguration)
        {
            this.abbr = abbr;
            this.name = name;
            this.atomicNumber = atomicNumber;
            this.periodicGroup = periodicGroup;
            this.periodicPeriod = periodicPeriod;
            this.electronConfiguration = electronConfiguration;
        }
    }

    class Ion
    {
        public string ionAbbr;
        public string ionName;
        public int ionCharge;
        public string ionType;



        public Ion()
        {
            this.ionAbbr = "";
            this.ionName = "";
            this.ionCharge = 0;
            this.ionType = "";

        }

        public Ion(string ionAbbr, string ionName, int ionCharge, string ionType)
        {
            this.ionAbbr = ionAbbr;
            this.ionName = ionName;
            this.ionCharge = ionCharge;
            this.ionType = ionType;

        }
    }

    class UnicodeConverter
    {
        public UnicodeConverter() {
        }

        //
        // Converts to subscript or superscript using unicode values.
        //
        public string convertToSubscript(int n)
        {
            string subscript;

            switch (n)
            {
                case 0:
                    subscript = "\u2080";
                    break;
                case 1:
                    subscript = "\u2081";
                    break;
                case 2:
                    subscript = "\u2082";
                    break;
                case 3:
                    subscript = "\u2083";
                    break;
                case 4:
                    subscript = "\u2084";
                    break;
                case 5:
                    subscript = "\u2085";
                    break;
                case 6:
                    subscript = "\u2086";
                    break;
                case 7:
                    subscript = "\u2087";
                    break;
                case 8:
                    subscript = "\u2088";
                    break;
                case 9:
                    subscript = "\u2089";
                    break;
                default:
                    subscript = "";
                    break;
            }
            return subscript;
        }
        public string convertToSuperscript(int n)
        {
            string superscript;

            switch (n)
            {
                case 0:
                    superscript = "\u2070";
                    break;
                case 1:
                    superscript = "\u00B9";
                    break;
                case 2:
                    superscript = "\u00B2";
                    break;
                case 3:
                    superscript = "\u00B3";
                    break;
                case 4:
                    superscript = "\u2074";
                    break;
                case 5:
                    superscript = "\u2075";
                    break;
                case 6:
                    superscript = "\u2076";
                    break;
                case 7:
                    superscript = "\u2077";
                    break;
                case 8:
                    superscript = "\u2078";
                    break;
                case 9:
                    superscript = "\u2079";
                    break;
                default:
                    superscript = "";
                    break;
            }
            return superscript;
        }

    }
}
