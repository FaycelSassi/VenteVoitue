using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VenteVoiture
{
    public partial class AdminForm : Form
    {
        string idbuy;
        Connexion con;
        Connexion conv;
        public AdminForm()
        {
            InitializeComponent();
        }

        public AdminForm(String email)
        {
            InitializeComponent();
            lb_email.Text = "Hi, " + email;
            this.con = new Connexion();
            con.Columns = "id";
            con.TableName = " user";
            con.WhereCond = " 	Email  like '" + email + "'";
            DataTable tab = con.RequeteSelect();
            if (tab.Rows.Count > 0)
            {
                DataRow dr = tab.Rows[0];
                idbuy = dr[0].ToString();
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.con = new Connexion();
            con.Columns = "UserType";
            con.TableName = "user";
            DataTable tab = con.RequeteSelect();
            int cust = 0, buy = 0;
            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataRow dr = tab.Rows[i];
                    if (dr[0].ToString()=="1")
                    {
                        cust++;
                    }else if (dr[0].ToString() == "2")
                    {
                        buy++;
                    }
                }
                tb_users.Text = tab.Rows.Count.ToString();
                tb_cust.Text = cust.ToString();
                tb_sellers.Text = buy.ToString();

            }
            this.con = new Connexion();
            con.Columns = "*";
            con.TableName = "commande";
            tab = con.RequeteSelect();
            tb_orders.Text = tab.Rows.Count.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            HomeForm f = new HomeForm();
            f.Show();
        }
    }
}
