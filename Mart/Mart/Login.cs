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
    public partial class Login : Form
    {
        MySqlConnection connect = new MySqlConnection("database=mart;uid=root;password=;");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        MySqlCommandBuilder cb;
        DataTable dt;
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp su = new SignUp();
            su.Show();
            this.Hide();
        }
        void Execute(string query)
        {
            connect.Open();
            cmd = new MySqlCommand(query, connect);
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        DataTable FillTable(string query)
        {
            dt = new DataTable();
            da = new MySqlDataAdapter(query, connect);
            cb = new MySqlCommandBuilder(da);
            da.Fill(dt);
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable temp = new DataTable();
                temp = this.FillTable("select * from users where user_name='" + textBox1.Text + "' and password='" + textBox2.Text + "'");
                string userName = temp.Rows[0]["user_name"].ToString();
                string userID = temp.Rows[0]["user_ID"].ToString();

                Form1 fm = new Form1(userName, userID);
                fm.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                //MessageBox.Show("Wrong user name or password;");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "type here ...")
                textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox1.Text == "type here ...")
                textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }
    }
}
