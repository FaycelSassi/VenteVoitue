using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace VenteVoiture
{
    class Connexion
    {

        MySqlConnection con = new MySqlConnection();
        public string TableName { get; set; }

        public string Columns { get; set; }

        public string WhereCond { get; set; }

        public string Values { get; set; }

        public Connexion()
        {
            con.ConnectionString = "server=127.0.0.1;uid=root;pwd=;persistsecurityinfo=True;database=projetvoiture;Convert Zero Datetime=True";
            try
            {
                con.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Error while connecting...");
                con = null;
            }
            TableName = null;
            Columns = null;
            WhereCond = null;
            Values = null;
        }

        public DataTable RequeteSelect()
        {

            string sql = (string.IsNullOrEmpty(WhereCond)) ? "SELECT " + Columns + " FROM " + TableName : "SELECT " + Columns + " From " + TableName + " Where " + WhereCond;
            try

            {
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataAdapter ad = new MySqlDataAdapter(sql, this.con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                return ds.Tables[0];
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public bool VerifConnexion()
        {

            return (con != null) ? true : false;
        }

        public void RequeteInsert()
        {
            string sql = "INSERT INTO " + TableName + " (" + Columns + ")  VALUES('" + Values;
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter(sql, this.con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                MessageBox.Show("Added");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public int requetedelete()
        {
            string sql = "DELETE FROM " + TableName + " WHERE " + WhereCond;
            MySqlCommand cmd = new MySqlCommand(sql, this.con);
            try
            {
                int lc = cmd.ExecuteNonQuery();
                if (lc > 0)
                {
                    MessageBox.Show("Deleted", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return lc;
                }
                else
                {
                    MessageBox.Show("Error while deleting", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return lc;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public int requeteupdate()
        {
            string sql = "UPDATE " + TableName + " SET " + Columns + " WHERE " + WhereCond;
            MySqlCommand cmd = new MySqlCommand(sql, this.con);
            try
            {
                int lc = cmd.ExecuteNonQuery();
                if (lc > 0)
                {
                    MessageBox.Show("Updated", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return lc;
                }
                else
                {
                    MessageBox.Show("Error while updating", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return lc;
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static void ExecuteQuery()
        {
            string queryString =
                @"SELECT c.CategoryID, c.CategoryName
                FROM NorthwindEntities.Categories AS c";

            using (EntityConnection conn =
                new EntityConnection("name=NorthwindEntities"))
            {
                try
                {
                    conn.Open();
                    using (EntityCommand query = new EntityCommand(queryString, conn))
                    {
                        using (DbDataReader rdr =
                            query.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            while (rdr.Read())
                            {
                                Console.WriteLine("\t{0}\t{1}", rdr[0], rdr[1]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
   


    class ComboObject
    {
        public ComboObject() { }

        public int Value { get; set; }

        public string Text { get; set; }
    }

}
