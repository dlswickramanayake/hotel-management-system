using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp13
{
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30");


        private void pictureBoxclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, user_password;

            username = textBoxusername.Text;
            user_password = textBoxpassword.Text;

            try
            {
                string query = "select * from admin where username = '" +textBoxusername.Text+ "' and password='"+textBoxpassword.Text+ "'";

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtable = new DataTable();

                sda.Fill(dtable);

                if(dtable.Rows.Count > 0)
                {
                    username = textBoxusername.Text;
                    user_password = textBoxpassword.Text;

                    formdashboard form2 = new formdashboard();
                    form2.Username = textBoxusername.Text;

                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxusername.Clear();
                    textBoxpassword.Clear();

                    textBoxusername.Focus();
                }
            }
            catch 
            {
                MessageBox.Show("Error");
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxusername.Clear();
            textBoxpassword.Clear();
        }

        private void textBoxpassword_TextChanged(object sender, EventArgs e)
        {
            textBoxpassword.UseSystemPasswordChar = true;
        }

        private void pictureBoxShow_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxShow, "Show Password");
        }

        private void pictureBoxHide_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxHide, "Hide Password");
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            pictureBoxShow.Hide();
            textBoxpassword.UseSystemPasswordChar = false;
            pictureBoxHide.Show();
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            pictureBoxHide.Hide();
            textBoxpassword.UseSystemPasswordChar = true;
            pictureBoxShow.Show();
        }

        private void loginform_Enter(object sender, EventArgs e)
        {
            textBoxpassword.UseSystemPasswordChar = true;
        }
    }
}
