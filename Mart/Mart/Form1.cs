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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

namespace Mart
{
    public partial class Form1 : Form
    {
        MySqlConnection connect = new MySqlConnection("database=mart;uid=root;password=;");
        MySqlCommand cmd;
        MySqlDataAdapter da;
        MySqlCommandBuilder cb;
        DataTable dt;
        DBConnect db = new DBConnect();
        string dateTime;
        string userName;
        string userID;

        public Form1(string userName,string userID)
        {
            this.userName = userName;
            this.userID = userID;
            
            InitializeComponent();
        }

        public Form1()
        {
            InitializeComponent();
        }                
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;

            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;

            label12.Text = "0";
            label11.Text = this.userName;
            GridUpdate();
            label16.Text = GetID("select * from customer", "customer_ID").ToString();
            textBox1.Text = GetID("select * from items", "serial_no").ToString();
            timer1.Start();
            ComboReady();
            
        }

        int GetID(string query, string tableName)
        {
            DataTable dataTable = this.FillTable(query);
            int ID;
            try { ID = Convert.ToInt32(dataTable.Rows[dataTable.Rows.Count - 1][tableName].ToString()); }
            catch { ID = 0; }
            return ++ID;         
        }

        void ComboReady()
        {
            DataTable dataTable = this.FillTable("select * from items");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                comboBox2.Items.Add(dataTable.Rows[i]["code"].ToString());
            }
            dataTable = this.FillTable("select * from users");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                comboBox3.Items.Add(dataTable.Rows[i]["user_ID"].ToString());
            }
        }

        void Execute(string query)
        {          
            try
            {
                connect.Open();
                cmd = new MySqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
                GridUpdate();
            }
            catch
            {
                MessageBox.Show("Maybe you are missing something.");
            }
        }

        DataTable FillTable(string query)
        {         
            try
            {
                dt = new DataTable();
                da = new MySqlDataAdapter(query, connect);
                cb = new MySqlCommandBuilder(da);
                da.Fill(dt);
            }
            catch
            {
                MessageBox.Show("Somwthing went Wrong!");
            }
            return dt;
        }



        void GridUpdate()
        {
            dataGridView1.DataSource = this.FillTable("select * from items");
            dataGridView2.DataSource = this.FillTable("select * from customer");
            dataGridView3.DataSource = this.FillTable("select * from sold_products");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Execute("insert into items values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')");
            textBox1.Text = GetID("select * from items", "serial_no").ToString();
            GridUpdate();
            ComboReady();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Please select the first from data drid view!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string del = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                this.Execute("delete from items where serial_no="+del);
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string serialNo = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                this.Execute("update items set serial_no='" + textBox1.Text + "',code='" + textBox2.Text + "',name='" + textBox3.Text + "',discount='" + textBox4.Text + "',price='" + textBox5.Text + "' where serial_no="+serialNo);

            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                textBox1.Text = " ";
            if (textBox2.Text == "")
                textBox2.Text = " ";
            if (textBox3.Text == "")
                textBox3.Text = " ";
            if (textBox4.Text == "")
                textBox4.Text = " ";
            if (textBox5.Text == "")
                textBox5.Text = " ";
            dataGridView1.DataSource = this.FillTable("select * from items where serial_no like '%" + textBox1.Text + "%' or code like '%" + textBox2.Text + "%' or name like '%" + textBox3.Text + "%' or Discount like '%" + textBox4.Text + "%' or price like '%" + textBox5.Text + "%'");
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTime = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss");
            label13.Text = dateTime.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = this.FillTable("Select * from sold_products where customer_id=" + textBox6.Text);
            textBox6.Text = "";
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string code = comboBox2.Text;
            DataTable dataTable = this.FillTable("select * from items where code='" + code + "'");
            float discount = Convert.ToInt32(dataTable.Rows[0]["discount"].ToString());
            if (discount == 0)
                discount = 100;
            float price = Convert.ToInt32(dataTable.Rows[0]["price"].ToString());
            price*=Convert.ToInt32(comboBox1.Text);
            float discountedPrice = price * (discount / 100);
            //MessageBox.Show("price"+price+" discount"+discount+" disprice"+discountedPrice+"");
            this.Execute("insert into sold_products values("+GetID("select * from sold_products","serial_no")+","+label16.Text+",'"+code+"',"+comboBox1.Text+","+discount+","+price+","+discountedPrice+")");
            dataTable = this.FillTable("select * from customer where customer_ID=" + label16.Text);
            float oldDiscountedPrice = float.Parse(dataTable.Rows[0]["total_price"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
            oldDiscountedPrice += discountedPrice;
            label12.Text = oldDiscountedPrice.ToString();
            this.Execute("update customer set total_price="+oldDiscountedPrice+" where customer_ID="+label16.Text);
            GridUpdate();
            dataGridView3.DataSource = this.FillTable("Select * from sold_products where customer_id="+label16.Text);
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            int discountedPrice = Convert.ToInt32(label12.Text);
            this.Execute("insert into customer values(" + label16.Text + ",'" + userName + "'," + discountedPrice + ",'" + label13.Text + "'," + userID + ")");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                this.Execute("delete from sold_products where serial_no=" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                GridUpdate();
            }
            catch
            {
                MessageBox.Show("Select the row first.");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string customer_ID = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
                this.Execute("delete from sold_products where customer_id=" + customer_ID);
                this.Execute("delete from customer where customer_id=" + customer_ID);
                GridUpdate();
            }
            catch
            {
                MessageBox.Show("Select the row first!");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string ID = label16.Text;
            string sum = label12.Text;
            dataGridView3.DataSource = FillTable("select * from sold_products where customer_id="+ID);
            label16.Text = GetID("select * from customer", "customer_id").ToString();
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("Item list " + ID + ".pdf", FileMode.Create));
            doc.Open();
            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance("pic.jpg");
            png.ScalePercent(25f);
            png.BorderColor = iTextSharp.text.BaseColor.BLACK;
            png.SetAbsolutePosition(doc.PageSize.Width - 200f - 200f, doc.PageSize.Height - 80f - 30f);
            doc.Add(png);
            List list = new List(List.UNORDERED, 10f);
            Paragraph para = new Paragraph("\n\n");
            doc.Add(para);
            list.Add("User: " + userName + "");
            list.Add("Date Time: " + dateTime + "");
            list.Add("ID Number: " + ID + "");
            list.Add("Customer Name: Respected Customer\n\n\n");
            doc.Add(list);
            PdfPTable table = new PdfPTable(dataGridView3.Columns.Count);
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
                table.AddCell(new Phrase(dataGridView3.Columns[i].HeaderText));
            table.HeaderRows = 1;

            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    if (dataGridView3[j, i].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView3[j, i].Value.ToString()));
                    }
                }
            }
            doc.Add(table);
            Paragraph para0 = new Paragraph("\n\n");
            doc.Add(para0);
            List list1 = new List(List.UNORDERED, 10f);
            Paragraph para1 = new Paragraph("\n\n");
            list1.Add("Total number of item(s) = " + label3.Text + "");
            list1.Add("Total price = " + sum + "");
            doc.Add(list1);
            para1.IndentationLeft = 40f;
            para1.Add("Thank you! :)");
            doc.Add(para1);
            doc.Close();
            System.Diagnostics.Process.Start("Item list " + ID + ".pdf");
            MessageBox.Show("Pdf Created Successfully!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = this.FillTable("Select * from sold_products where customer_id=" + label16.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = this.FillTable("Select * from customer where customer_id=" + textBox6.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //comboBox1.Items.Add("HellowWorld");
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "type here ...")
                textBox6.Text = "";
        }

        private void label17_MouseHover(object sender, EventArgs e)
        {
            label17.ForeColor = Color.Red;
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            label17.ForeColor = Color.Black;
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "type here ...")
                textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "type here ...")
                textBox2.Text = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "type here ...")
                textBox3.Text = "";
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "type here ...")
                textBox4.Text = "";
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "type here ...")
                textBox5.Text = "";
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Execute("insert into salary values("+comboBox3.Text+","+textBox7.Text+")");
            dataGridView1.DataSource = this.FillTable("select * from salary");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.FillTable("select * from salary");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                string del = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                this.Execute("delete from salary where user_id=" + del);
                dataGridView1.DataSource = this.FillTable("select * from salary");
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            GridUpdate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
