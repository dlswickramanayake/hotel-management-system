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
    public partial class formdashboard : Form
    {
        public string Username;

        public formdashboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Log Out??", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DialogResult.Yes==result)
            {
                loginform login = new loginform();
                login.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void pictureBoxclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void formdashboard_Load(object sender, EventArgs e)
        {
            timer1.Start();
            username.Text = Username;
        }

        private void MovePanel(Control btn)
        {
            panelSlide.Top = btn.Top;
            panelSlide.Height = btn.Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MovePanel(button2);

            formclient form2 = new formclient();
            form2.Username = username.Text;
            form2.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {
            formsettings settings = new formsettings();
            settings.Username = username.Text;
            settings.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {
            string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;

            //create a command
            string query = "SELECT COUNT(username) FROM admin";


            try
            {
                con.Open();

                cmd = new SqlCommand(query, con);

                //read from db
                Int32 rows_count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                con.Close();

                //Display data on the page
                labeluserdata.Text =  rows_count.ToString();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;

            //create a command
            string query = "SELECT COUNT(RoomNo) FROM Room";


            try
            {
                con.Open();

                cmd = new SqlCommand(query, con);

                //read from db
                Int32 rows_count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                con.Close();

                //Display data on the page
                labelroomdata.Text = rows_count.ToString();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd;

            //create a command
            string query = "SELECT COUNT(ClientID) FROM Reservation";


            try
            {
                con.Open();

                cmd = new SqlCommand(query, con);

                //read from db
                Int32 rows_count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                con.Close();

                //Display data on the page
                labelreservedata.Text = rows_count.ToString();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
