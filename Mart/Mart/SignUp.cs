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

namespace Mart
{
    public partial class SignUp : Form
    {

        MySqlConnection connect = new MySqlConnection("database=mart;uid=root;password=;");
        MySqlCommand cmd;
        public SignUp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect.Open();
            cmd = new MySqlCommand("insert into users values("+textBox4.Text+",'"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"')",connect);
            cmd.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Successfully done.");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
        }
    }
}
