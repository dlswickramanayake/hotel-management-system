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
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Database\Login_New.mdf;Integrated Security=True;Connect Timeout=30");

        public void Clear()
        {
            textBoxNIC.Clear();
            textBoxFname.Clear();
            textBoxFname.Clear();
            textBoxContact.Clear();
            textBoxEmail.Clear();
            textBoxAddress.Clear();
            tabControl1.SelectedTab = tabPage1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Client where NIC=@NIC",con);
            cmd.Parameters.AddWithValue("@NIC", textBoxNIC.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                MessageBox.Show("NIC Already Exist!!");
                con.Close();
            }
            else
            {
                int contact = int.Parse(textBoxContact.Text);
                string querry = "insert into Client values ('" + textBoxNIC.Text + "','" + textBoxFname.Text + "','" + textBoxLname.Text + "','" + contact + "','" + textBoxEmail.Text + "','" + textBoxAddress.Text + "')";
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

        private void textBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNIC_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
