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
    public partial class formsettings : Form
    {
        public string Username;

        public formsettings()
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

        private void formsettings_Load(object sender, EventArgs e)
        {
            username.Text = Username;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formclient client = new formclient();
            client.Username = username.Text;
            client.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formroom room = new formroom();
            room.Username = username.Text;
            room.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formreservation reserve = new formreservation();
            reserve.Username = username.Text;
            reserve.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Log Out??", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == result)
            {
                loginform login = new loginform();
                login.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formdashboard dashboard = new formdashboard();
            dashboard.Username = username.Text;
            dashboard.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "")
            {
                MessageBox.Show("Please Enter a username!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from admin where username=@username", con);
                cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("Username already exist!!");
                    con.Close();
                }
                else
                {
                    string query = "insert into admin values('" + textBoxUsername.Text + "','" + textBoxPassword.Text + "')";
                    SqlCommand sq = new SqlCommand(query, con);
                    try
                    {
                        con.Close();
                        con.Open();
                        sq.ExecuteNonQuery();
                        MessageBox.Show("Data entered successfully");
                    }
                    catch
                    {
                        MessageBox.Show("Failed");
                    }
                    finally
                    {
                        textBoxUsername.Clear();
                        textBoxPassword.Clear();
                        con.Close();
                    }
                }
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sqd = new SqlDataAdapter("select username from admin", con);
            DataTable tbl = new DataTable();
            sqd.Fill(tbl);
            dataGridView1.DataSource = tbl;
            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string update = "update admin set password='" + textBoxPassword1.Text + "' where username='" + textBoxUsername1.Text + "'";
            SqlCommand sqlupdate = new SqlCommand(update, con);
            con.Open();
            sqlupdate.ExecuteNonQuery();
            MessageBox.Show("Password updated succesfully!!");
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string delete = "delete from admin where username='" + textBoxUsername1.Text + "'";
            SqlCommand userdelete = new SqlCommand(delete, con);
            con.Open();
            userdelete.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully!!");
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
            con.Close();
        }
    }
}
