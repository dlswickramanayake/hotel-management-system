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
    public partial class formroom : Form
    {
        public string Username,Free,No,Free1;


        public formroom()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            comboBoxType1.SelectedIndex = 0;
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\TheFinalProject\Database\finalprojectdatabase.mdf;Integrated Security=True;Connect Timeout=30");

        public void Clear()
        {
            comboBoxType.SelectedIndex = 0;
            textBoxRoomNo.Clear();
            radioButtonYes.Checked = false;
            radioButtonNo.Checked = false;
            tabControlRoom.SelectedTab = tabPageAddRoom;
        }

        public void Clear1()
        {
            comboBoxType1.SelectedIndex = 0;
            textBoxRoomNo1.Clear();
            radioButtonYes1.Checked = false;
            radioButtonNo1.Checked = false;
            tabControlRoom.SelectedTab = tabPageUpdateandDeleteRoom;
        }

        private void pictureBoxclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void formroom_Load(object sender, EventArgs e)
        {
            timer1.Start();
            username.Text = Username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formclient client = new formclient();
            client.Username = username.Text;
            client.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formdashboard dashboard = new formdashboard();
            dashboard.Username = username.Text;
            dashboard.Show();
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tabPageSearchRoom_Enter(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlDataAdapter sqd = new SqlDataAdapter("select * from Room",con);
            DataTable tbl = new DataTable();
            sqd.Fill(tbl);
            dataGridView1.DataSource = tbl;
            con.Close();
        }

        private void buttonSearchRoom_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter search = new SqlDataAdapter("select * from Room where RoomNo='" +textBoxSearchRoom.Text+ "'", con);
            DataTable tbl1 = new DataTable();
            search.Fill(tbl1);
            dataGridView1.DataSource = tbl1;
            con.Close();
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPageSearchRoom_Leave(object sender, EventArgs e)
        {
            textBoxSearchRoom.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM Room WHERE RoomNo='" + textBoxRoomNo1.Text + "'";
            SqlCommand roomdelete = new SqlCommand(delete, con);
            try
            {
                con.Open();
                roomdelete.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully!!");
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

        private void tabPageUpdateandDeleteRoom_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            object rtype = comboBoxType1.SelectedItem;
            string rno = textBoxRoomNo1.Text;
            if (radioButtonYes1.Checked) 
            {
                Free1 = "Yes";
            }
            if (radioButtonNo1.Checked)
            {
                Free1 = "No";
            }
            string update = "UPDATE Room SET RoomType='" + rtype + "', IsFree='" + Free1 + "' WHERE RoomNo='" + textBoxRoomNo1.Text + "'";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(radioButtonYes.Checked)
            {
                Free = "Yes";
            }
            if(radioButtonNo.Checked)
            {
                Free = "No";
            }
            SqlCommand cmd = new SqlCommand("select * from Room where RoomNo=@RoomNo", con);
            cmd.Parameters.AddWithValue("@RoomNo", textBoxRoomNo.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Room Number Already Exists!");
                con.Close();
            }
            else
            {
                string insert = "insert into Room values ('" + textBoxRoomNo.Text + "','" + comboBoxType.SelectedItem + "','" + Free + "')";
                SqlCommand sqlinsert = new SqlCommand(insert, con);
                try
                {
                    con.Close();
                    con.Open();
                    sqlinsert.ExecuteNonQuery();
                    MessageBox.Show("Data Entered Succesfully!!");
                }
                catch
                {
                    MessageBox.Show("Failed");
                    Clear();
                }
                finally
                {
                    Clear();
                    con.Close();
                }
            }
        }
    }
}
