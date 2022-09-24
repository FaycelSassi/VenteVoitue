using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace VenteVoiture
{
    public partial class HomeForm : Form
    {
        Connexion con;
        public HomeForm()
        {
            InitializeComponent();
            tb_pwd.PasswordChar = '*';
            con = new Connexion();


        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private bool validate()
        {

            if (tb_email.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the email box", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tb_pwd.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the password box", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (tb_pwd.Text.Length < 5 || tb_pwd.Text.Length > 15)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                con.Columns = "Email ,UserType,id";
                con.TableName = " user ";
                con.WhereCond = "Email like '" + this.tb_email.Text.Trim() + "' and Password LIKE '" + tb_pwd.Text.Trim() + "'";

                DataTable tab = con.RequeteSelect();
                if (tab.Rows.Count > 0)
                {
                    DataRow dr = tab.Rows[0];

                    MessageBox.Show("Bonjour " + dr[0] );
                    this.Hide();
                    if (dr[1].ToString() == "2")
                    {
                        //BuyerForm f = new BuyerForm(dr[0].ToString());
                        BuyerForm f = new BuyerForm(dr[0].ToString());
                        f.Show();
                    }
                    if (dr[1].ToString() == "1")
                    {
                        //UserForm f = new UserForm(dr[0].ToString());
                        UserForm f = new UserForm(dr[0].ToString());
                        f.Show();
                    }
                    if (dr[1].ToString() == "0")
                    {
                        //UserForm f = new UserForm(dr[0].ToString());
                        AdminForm f = new AdminForm(dr[0].ToString());
                        f.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Email ou mot de passe sont érronnée");
                }

            }



        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignInForm f = new SignInForm();
            this.Hide();
            f.Show();
        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
