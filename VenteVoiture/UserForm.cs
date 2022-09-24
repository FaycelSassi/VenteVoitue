using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace VenteVoiture
{
    public partial class UserForm : Form
    {
        string idbuy;
        Connexion con;
        Connexion conv;
        public UserForm()
        {
            InitializeComponent();
        }
        public UserForm(String email)
        {
            InitializeComponent();
            lb_email.Text = "Hi, " + email;
            this.con = new Connexion();
            con.Columns = "id";
            con.TableName = " user";
            con.WhereCond = " Email  like '" + email + "'";
            DataTable tab = con.RequeteSelect();
            if (tab.Rows.Count > 0)
            {
                DataRow dr = tab.Rows[0];
                idbuy = dr[0].ToString();
            }
        }
        private void show() {
            lb.Items.Clear();
            this.con = new Connexion();
            con.Columns = "idVoiture ,NomVoiture ,Description ,Prix  ";
            con.TableName = " voiture";
            con.WhereCond = " 	Etat  like '" + 0 + "'";
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    string item = "";
                    item = dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[3].ToString() + " - " + dr[2].ToString();
                    lb.Items.Add(item);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lb.Items.Clear();
            this.con = new Connexion();
            con.Columns = "idVoiture ,NomVoiture ,Description ,Prix  ";
            con.TableName = " voiture";
            con.WhereCond = " 	Etat  like '" + 0 + "'";
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    string item = "";
                    item = dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[3].ToString() + " - " + dr[2].ToString();
                    lb.Items.Add(item);
                }
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            lb.Items.Clear();
            this.con = new Connexion();
            con.Columns = "idVoiture ,NomVoiture ,Description ,Prix  ";
            con.TableName = " voiture";
            con.WhereCond = " 	Etat  like '" + 0 + "'";
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    string item = "";
                    item = dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[3].ToString() + " - " + dr[2].ToString();
                    lb.Items.Add(item);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            HomeForm f = new HomeForm();
            f.Show();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {

            lb.Items.Clear();
            this.con = new Connexion();
            con.Columns = "idVoiture ,idUser ,Date";
            con.TableName = " Commande";
            con.WhereCond = " 	idUser  like '" + idbuy + "'";
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    this.conv = new Connexion();
                    conv.Columns = "NomVoiture ,Description ,Prix  ";
                    conv.TableName = " Voiture";
                    conv.WhereCond = " 	idVoiture  like '" + dr[0].ToString() + "'";
                    DataTable tabv = conv.RequeteSelect();
                    int x = tabv.Rows.Count;
                    if (x > 0)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            DataRow drv = tabv.Rows[j];
                            string item = "";
                            item = drv[0].ToString() + " - " + drv[2].ToString() + " - " + dr[2].ToString() + " - " + drv[1].ToString();
                            lb.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedtext = lb.SelectedItem.ToString();
            int pos = selectedtext.IndexOf(" - ");
            string id = selectedtext.Substring(0, pos);
            this.con = new Connexion();
            con.Columns = "Etat = '" + 1 + "'";
            con.TableName = " voiture";
            con.WhereCond = " 	idVoiture  like '" + id + "'";
            con.requeteupdate();
            this.con = new Connexion();
            con.Columns = "idUser ,idVoiture ,Date ";
            con.TableName = "commande";
            string date = DateTime.UtcNow.ToString("dd/MM/yyyy");
            con.Values = idbuy + "','" + id+ "','" + date+"')";
            con.RequeteInsert();
            show();
        }
    }
}
