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
    public partial class BuyerForm : Form
    {
        string idbuy;
        Connexion con;
        Connexion conv;
        public BuyerForm()
        {
            InitializeComponent();
        }

        public BuyerForm(String email)
        {
            InitializeComponent();
            lb_email.Text = "Hi, "+email;
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
        private void show()
        {
            lbvoitures.Items.Clear();
            this.con = new Connexion();
            con.Columns = "idVoiture ,NomVoiture ,Description ,Prix ,Etat ";
            con.TableName = " voiture";
            con.WhereCond = " 	idUser  like '" + idbuy + "'";
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    string item = "";
                    if (dr[4].ToString() == "1")
                    {
                        item = dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[3].ToString() + " - sold";
                    }
                    else
                    {
                        item = dr[0].ToString() + " - " + dr[1].ToString() + " - " + dr[3].ToString() + " - for sale";
                    }
                    lbvoitures.Items.Add(item);
                }
            }
        }

        private void BuyerForm_Load(object sender, EventArgs e)
        {
            show();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new Connexion();
            con.Columns = "idUser ,NomVoiture ,Description ,Prix ,Etat ";
            con.TableName = "voiture";
            con.Values = idbuy + "','" + tb_nom.Text.Trim().Trim() + "','" + rtb_desc.Text.Trim()  +"','" + tb_price.Text.Trim()  +"','" +0 + "')";
            con.RequeteInsert();
            show();
            tb_nom.Text = "";
            rtb_desc.Text = "";
            tb_price.Text = "";
            tb_etat.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            HomeForm f = new HomeForm();
            f.Show();
        }

        private void lbvoitures_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedtext = lbvoitures.SelectedItem.ToString();
            int pos = selectedtext.IndexOf(" - ");
            string id = selectedtext.Substring(0, pos);
            this.con = new Connexion();
            con.Columns = "idVoiture ,NomVoiture ,Description ,Prix ,Etat ";
            con.TableName = " voiture";
            con.WhereCond = " 	idVoiture  like '" + id + "'";
            DataTable tab = con.RequeteSelect();
            if (tab.Rows.Count > 0)
            {
                DataRow dr = tab.Rows[0];
                tb_nom.Text = dr[1].ToString();
                tb_price.Text = dr[3].ToString();
                rtb_desc.Text = dr[2].ToString();
                if (dr[4].ToString() == "1")
                {
                    tb_etat.Text = "sold ";
                }
                else
                {
                    tb_etat.Text = "for sale ";
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedtext = lbvoitures.SelectedItem.ToString();
            int pos = selectedtext.IndexOf(" - ");
            string id = selectedtext.Substring(0, pos);
            this.con = new Connexion();
            con.Columns = "NomVoiture = '" + tb_nom.Text.Trim() + "',Description = '" + rtb_desc.Text.Trim() + "',Prix = '" + tb_price.Text.Trim() + "'";
            con.TableName = " voiture";
            con.WhereCond = " 	idVoiture  like '" + id + "'";
            con.requeteupdate();
            show();
            tb_nom.Text = "";
            rtb_desc.Text = "";
            tb_price.Text = "";
            tb_etat.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedtext = lbvoitures.SelectedItem.ToString();
            int pos = selectedtext.IndexOf(" - ");
            string id = selectedtext.Substring(0, pos);
            this.con = new Connexion();
            con.TableName = " voiture";
            con.WhereCond = " idVoiture  like '" + id + "'";
            con.requetedelete();
            show();
            tb_nom.Text = "";
            rtb_desc.Text = "";
            tb_price.Text = "";
            tb_etat.Text = "";
        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            lbvoitures.Items.Clear();
            this.con = new Connexion();
            con.Columns = "IdVoiture ,NomVoiture ,Description ,Prix  ";
            con.TableName = " Voiture";
            con.WhereCond = "idUser  like '" + idbuy + "'";
            
            DataTable tab = con.RequeteSelect();
            int c = tab.Rows.Count;
            if (c > 0)
            {
                for (int i = 0; i < c; i++)
                {
                    DataRow dr = tab.Rows[i];
                    this.conv = new Connexion();
                    conv.Columns = "idVoiture ,idUser ,Date";
                    conv.TableName = " Commande";
                    conv.WhereCond = " 	idVoiture  like '" + dr[0].ToString() + "'";
                    DataTable tabv = conv.RequeteSelect();
                    int x = tabv.Rows.Count;
                    if (x > 0)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            DataRow drv = tabv.Rows[j];
                            string item = "";
                            item = dr[1].ToString() + " - " + dr[3].ToString() + " - " + drv[2].ToString() + " - " + dr[2].ToString();
                            lbvoitures.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            show();
        }

        private void lb_email_Click(object sender, EventArgs e)
        {

        }
    }
}
