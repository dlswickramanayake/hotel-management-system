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
    public partial class formreservation : Form
    {
        public string Username;

        public formreservation()
        {
            InitializeComponent();
            comboBoxRoomType.SelectedIndex = 0;
            comboBoxRoomType1.SelectedIndex = 0;
        }

        public void Clear()
        {
            comboBoxRoomType.SelectedIndex = 0;
            textBoxClientID.Clear();
            dateTimePickerIn.Value = DateTime.Now;
            dateTimePickerOut.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPageAddReserve;
        }

        public void Clear1()
        {
            comboBoxRoomType1.SelectedIndex = 0;
            textBoxClientID1.Clear();
            dateTimePickerIn1.Value = DateTime.Now;
            dateTimePickerOut1.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPage1;
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

        private void formreservation_Load(object sender, EventArgs e)
        {
            username.Text = Username;
            timer1.Start();
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
            formroom room = new formroom();
            room.Username = username.Text;
            room.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formclient client = new formclient();
            client.Username = username.Text;
            client.Show();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
            date.Text = DateTime.Now.ToLongDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Reservation where ClientID=@ClientID", con);
            cmd.Parameters.AddWithValue("@ClientID", textBoxClientID.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("NIC Number Already Exists!");
                con.Close();
            }
            else
            {
                string insert = "insert into Reservation values ('"+textBoxClientID.Text+"','"+comboBoxRoomType.SelectedItem+"','"+comboBoxRoomNo.SelectedValue+"','"+dateTimePickerIn.Text+"','"+dateTimePickerOut.Text+"')";
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
                }
                finally
                {
                    Clear();
                    con.Close();
                }
            }
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomno = "select RoomNo from Room where RoomType='" + comboBoxRoomType.SelectedItem.ToString() + "' and IsFree='Yes'";
            SqlCommand sqlroomno = new SqlCommand(roomno, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlroomno;
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            comboBoxRoomNo.DataSource = tbl;
            comboBoxRoomNo.DisplayMember = "RoomNo";
            comboBoxRoomNo.ValueMember = "RoomNo";

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlDataAdapter sqd = new SqlDataAdapter("select * from Reservation", con);
            DataTable tbl1 = new DataTable();
            sqd.Fill(tbl1);
            dataGridView1.DataSource = tbl1;
            con.Close();
        }

        private void tabPageAddReserve_Enter(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            object type = comboBoxRoomType1.SelectedItem;
            object no = comboBoxRoomNo1.SelectedValue;
            string update = "UPDATE Reservation SET RoomType='" + type + "', RoomNo='" + no + "', DateIn='" + dateTimePickerIn1.Text + "', DateOut='" + dateTimePickerOut1.Text + "' WHERE ClientID='" + textBoxClientID1.Text + "'";
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

        private void comboBoxRoomType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomno1= "select RoomNo from Room where RoomType='" + comboBoxRoomType1.SelectedItem.ToString() + "' and IsFree='Yes'";
            SqlCommand sqlroomno1 = new SqlCommand(roomno1, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlroomno1;
            DataTable tbl2 = new DataTable();
            da.Fill(tbl2);
            comboBoxRoomNo1.DataSource = tbl2;
            comboBoxRoomNo1.DisplayMember = "RoomNo";
            comboBoxRoomNo1.ValueMember = "RoomNo";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM Reservation WHERE ClientID='" + textBoxClientID1.Text + "'";
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter search = new SqlDataAdapter("select * from Reservation where ClientID='" + textBoxClientIDSearch.Text + "'", con);
            DataTable tbl1 = new DataTable();
            search.Fill(tbl1);
            dataGridView1.DataSource = tbl1;
            con.Close();
        }
    }
}
