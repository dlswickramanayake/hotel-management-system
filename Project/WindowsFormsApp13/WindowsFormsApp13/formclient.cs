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
    public partial class formclient : Form
    {
        public string Username;

        public formclient()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30");

        public void Clear()
        {
            textBoxNIC.Clear();
            textBoxFname.Clear();
            textBoxLname.Clear();
            textBoxContact.Clear();
            textBoxEmail.Clear();
            textBoxAddress.Clear();
            tabControl1.SelectedTab = tabPage1;
        }

        public void Clear1()
        {
            textBoxNIC1.Clear();
            textBoxFname1.Clear();
            textBoxLname1.Clear();
            textBoxContact1.Clear();
            textBoxEmail1.Clear();
            textBoxAddress1.Clear();
            tabControl1.SelectedTab = tabPage3;
        }


        private void pictureBoxclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void formclient_Load(object sender, EventArgs e)
        {
            username.Text = Username;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
            date.Text = DateTime.Now.ToLongDateString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            formroom form2 = new formroom();
            form2.Username = username.Text;
            form2.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formreservation reserve = new formreservation();
            reserve.Username = username.Text;
            reserve.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formsettings settings = new formsettings();
            settings.Username = username.Text;
            settings.Show();
            this.Hide();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddClient_Click_1(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Client where NIC=@NIC", con);
            cmd.Parameters.AddWithValue("@NIC", textBoxNIC.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("NIC Already Exist!!");
                con.Close();
            }
            else
            {
                string querry = "insert into Client values ('" + textBoxNIC.Text + "','" + textBoxFname.Text + "','" + textBoxLname.Text + "','" + textBoxContact.Text + "','" + textBoxEmail.Text + "','" + textBoxAddress.Text + "')";
                SqlCommand sq = new SqlCommand(querry, con);
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
                    Clear();
                    con.Close();
                }
            }
        }

        private void btnSearchClient_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter search = new SqlDataAdapter("select * from Client where NIC='" + textBoxsSearchNIC.Text + "'", con);
            DataTable tbl1 = new DataTable();
            search.Fill(tbl1);
            dataGridView1.DataSource = tbl1;
            con.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string fname = textBoxFname1.Text;
            string nic = textBoxNIC1.Text;
            string lname = textBoxLname1.Text;
            string cn = textBoxContact1.Text;
            string email = textBoxEmail1.Text;
            string address = textBoxAddress1.Text;

            string update = "UPDATE Client SET FirstName = '" + fname +"', LastName='"+lname+"', ContactNo='"+cn+"', Email='"+email+"', Address='"+address+"' where NIC = '" +nic+ "'";
            SqlCommand sqlupdate = new SqlCommand(update, con);
            try
            {
                con.Open();
                sqlupdate.ExecuteNonQuery();
                MessageBox.Show("Details updated succesfully!!");
            }
            catch
            {
                MessageBox.Show("Failed");
            }
            finally
            {
                Clear1();
                con.Close();
            }

        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string delete = "delete from Client where NIC='" + textBoxNIC1.Text + "'";
            SqlCommand userdelete = new SqlCommand(delete, con);
            con.Open();
            userdelete.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully!!");
            Clear1();
            con.Close();
        }

        private void tabPage2_Leave(object sender, EventArgs e)
        {
            textBoxsSearchNIC.Clear();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlDataAdapter sqd = new SqlDataAdapter("select * from Client", con);
            DataTable tbl = new DataTable();
            sqd.Fill(tbl);
            dataGridView1.DataSource = tbl;
            con.Close();
        }
    }
}
