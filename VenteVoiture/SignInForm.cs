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
    public partial class SignInForm : Form
    {
        Connexion con;
        public SignInForm()
        {
            con = new Connexion();
            InitializeComponent();
            tb_pwd.PasswordChar = '*';
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
                MessageBox.Show("Week password", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(!(rd_buyer.Checked)&& !(rd_customer.Checked))
            {
                MessageBox.Show("Make sure to select role", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                int t = 0;
                if (rd_buyer.Checked)
                {
                    t = 2;
                    con.Columns = "*";
                    con.TableName = "user";
                    con.WhereCond = "Email like '" + tb_email.Text.Trim()  + "'";
                    DataTable tab = con.RequeteSelect();
                    if (tab.Rows.Count > 0)
                    {
                        MessageBox.Show(tb_email.Text+" already exists" , "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        con = new Connexion();
                        con.Columns = "Email, Password, userType";
                        con.TableName = "user";
                        con.Values = tb_email.Text.Trim() + "','" + tb_pwd.Text.Trim() + "','" + t + "')";
                        con.RequeteInsert();
                        this.Hide();
                        HomeForm f = new HomeForm();
                        f.Show();
                    }
                }
                else if (rd_customer.Checked)
                {
                    t = 1;
                    con.Columns = "Email ,userType";
                    con.TableName = " user ";
                    con.WhereCond = "Email like '" + this.tb_email.Text.Trim() + "'";
                    DataTable tab = con.RequeteSelect();
                    if (tab.Rows.Count > 0)
                    {
                        MessageBox.Show("Account already exists", "Alert!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        con = new Connexion();
                        con.Columns = "Email, Password, userType";
                        con.TableName = "user";
                        con.Values = tb_email.Text.Trim() + "','" + tb_pwd.Text.Trim() + "','" + t + "')";
                        con.RequeteInsert();
                        this.Hide();
                        HomeForm f = new HomeForm();
                        f.Show();
                    }
                }
                

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            HomeForm f = new HomeForm();
            f.Show();
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SignInForm_Load(object sender, EventArgs e)
        {

        }

        private void SignInForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
