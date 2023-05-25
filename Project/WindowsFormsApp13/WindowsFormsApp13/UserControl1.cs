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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Database\Login_New.mdf;Integrated Security=True;Connect Timeout=30");

        public void Clear()
        {
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            tabControl1.SelectedTab = tabPage1;
        }

        private void Clear1()
        {
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBoxUsername.Text=="")
            {
                MessageBox.Show("Please Enter a username!!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from Login_New where username=@username", con);
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
                    string query = "insert into Login_New values('"+textBoxUsername.Text+"','"+textBoxPassword.Text+"')";
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

        private void tabControl1_Leave(object sender, EventArgs e)
        {

        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            Clear();
        }

        private void tabPage2_Leave(object sender, EventArgs e)
        {
            textBoxSearchUser.Clear();
        }

        private void tabPage3_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sqd = new SqlDataAdapter("select username from Login_New",con);
            DataTable tbl = new DataTable();
            sqd.Fill(tbl);
            dataGridView1.DataSource = tbl;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string update = "update Login_New set user_password='" + textBoxPassword1.Text + "' where user='" + textBoxUsername1.Text+ "'";
            SqlCommand sqlupdate = new SqlCommand(update,con);
            con.Open();
            sqlupdate.ExecuteNonQuery();
            MessageBox.Show("Password updated succesfully!!");
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string delete = "delete from Login_New where username='" + textBoxUsername1.Text + "'";
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
