using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data.SQLite;

namespace app
{


    public partial class AddShop : Form
    {
        SQLiteConnection sqlite_conn;
        public string shopid = "";
        public string shopname = "";
        public string mobilenumber = "";
        public string shopaddress = "";
        public string totalshops = "0"; 

        public AddShop()
        {
            InitializeComponent();
            sqlite_conn = CreateConnection();
            this.dataGridView1.Rows.Clear();
            ReadData(sqlite_conn); total_shops();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            shopname = this.textBox1.Text;
            mobilenumber = this.textBox2.Text;
            shopaddress = this.richTextBox1.Text;

            if (this.textBox1.Text == "")
            {
                MessageBox.Show("Enter Shop Name, Mobile Number, Address! ");
            }
            else
            {
                try
                {
                    InsertData(sqlite_conn, shopname, mobilenumber, shopaddress);
                    this.dataGridView1.Rows.Clear();
                    ReadData(sqlite_conn);
                    total_shops();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            this.textBox1.Text = null;
            this.textBox2.Text = null;
            this.richTextBox1.Text = null;

            //ReadData(sqlite_conn);

        }

        public SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        public void InsertData(SQLiteConnection conn, string shopname, string mobilenumber, string shopaddress)
        {

            SQLiteCommand sqlite_cmd_;
            sqlite_cmd_ = conn.CreateCommand();
            sqlite_cmd_.CommandText = "SELECT count(*) FROM Shop WHERE ShopName='" + shopname + "'";
            int count = Convert.ToInt32(sqlite_cmd_.ExecuteScalar());
            // 0 means false or not exist data 1 means true or exist data in data base  
            Debug.WriteLine(count);

            if (count == 0)
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Shop(ShopName, ShopMobileNumber,ShopAddress ) VALUES('" + shopname + "','" + mobilenumber + "','" + shopaddress + "') ";
                sqlite_cmd.ExecuteNonQuery();

            }
            else {

                MessageBox.Show("Shop name Already Exist");
            }
        }

        public void ReadData(SQLiteConnection conn)
        {

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Shop";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                // get username 
                //string myreader = sqlite_datareader.GetString(1);
                //Debug.WriteLine(myreader);
                dataGridView1.Rows.Insert(0, sqlite_datareader.GetValue(0), sqlite_datareader.GetString(1), sqlite_datareader.GetString(2), sqlite_datareader.GetString(3));

            }
            //conn.Close();
        }

        public void DeleteData(SQLiteConnection conn, string id)
        {

            //shopname = this.textBox1.Text;


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM Shop WHERE ShopId='" + id + "'";
            sqlite_cmd.ExecuteNonQuery();
        }

        // Delete
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete shop?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                try
                {

                    if (totalshops != "0")
                    {

                        string rowindex = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        if (rowindex != null)
                        {
                            DeleteData(sqlite_conn, rowindex);
                        }
                        else
                        {
                            MessageBox.Show("Select A Shop Name");
                        }
                        this.dataGridView1.Rows.Clear();
                        ReadData(sqlite_conn); total_shops();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (DialogResult == DialogResult.No)
            {
                //Nothing to do
            }

           
        }


        public void total_shops() {


            totalshops = dataGridView1.Rows.Count.ToString();
            label4.Text = "Total Shops : " + totalshops; 
        }

        // update
        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            ReadData(sqlite_conn); total_shops();
        }

        public void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null) {
                shopid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                shopname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                mobilenumber = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                shopaddress = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "UPDATE Shop SET ShopName = '" + shopname + "', ShopMobileNumber = '" + mobilenumber + "',ShopAddress = '" + shopaddress + "' WHERE ShopId = '" + shopid + "'";
                sqlite_cmd.ExecuteNonQuery();

                

            }
        }
    }
}
