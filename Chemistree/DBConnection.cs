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

        //
        // This was meant to submit element data to the element table.
        //
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

        //
        // This submits ion data to the ion table.
        //
        public bool SubmitIonDB(Ion i)
        {
            bool result = false;
            FormattableString sql = $"INSERT INTO ions (`abbreviation`, `name`, `charge`, `type`) VALUES ('{i.abbr}', '{i.name}', '{i.charge}', '{i.ionType}');";


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

        //
        //  This queries the database for a specific element by abbreviation.
        //
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

        //
        // This queries the database for a specific ion by abbreviation, charge, and type. It's used to validate the existence of the ion.
        //
        public void queryIonDB(ref Ion userIon, string ionType, ref string errorMessage)
        {
            bool result = false;
            string query = $"SELECT * FROM ions " +
                           $"WHERE abbreviation = '{userIon.abbr}'" +
                           $"AND charge = '{userIon.charge}'" +
                           $"AND type = '{ionType}'";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        userIon.name = reader["name"].ToString();
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
}
