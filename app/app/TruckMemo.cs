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
    public partial class TruckMemo : Form
    {


        SQLiteConnection sqlite_conn; 
        public int shopid;
        public string shopname = "";
        public string shopMobileNumber = "";
        public string shopAddress = "";
        public string doNumber = "";
        public string memoDate = ""; 
        public string productType = "";

        public int totalMemotext;
        public double totalQuantitytext;
        public double totalCosttext;
        public double totalDuebalancetext;
        public double totalCashreceivedtext;


        public int Special = 0;
        public int Popular = 0;
        public int OPC = 0; 

        double quantity = 1, unitprice = 0, totalproductprice = 0, truckcost = 0, othercost = 0, totacost = 0, cashreceived = 0, duebalance = 0 ;
        // 0= no due 1 = due 
        int dueflag = 0; 



        public TruckMemo()
        {
            InitializeComponent();
            sqlite_conn = CreateConnection();
            ReadDataForCombobox(sqlite_conn);
            ReadData(sqlite_conn);
            //dateTimePicker1.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            /* dateTimePicker1.CustomFormat = "dd-MM-yyyy";
             dateTimePicker1.Format = DateTimePickerFormat.Custom;

             dateTimePicker1.Value = DateTime.Now;*/
            SetToday();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Special");
            comboBox1.Items.Add("Popular");
            comboBox1.Items.Add("OPC");
            textBox2.Text = quantity.ToString();
            richTextBox1.Enabled = false;


            //Total Price
            label11.ForeColor = Color.FromName("Green");
            label16.ForeColor = Color.FromName("Green");
            // Due 
            label14.ForeColor = Color.FromName("Red");
            label15.ForeColor = Color.FromName("Red");
            // product Price
            label17.ForeColor = Color.FromName("Blue");
            label12.ForeColor = Color.FromName("Blue");
           /* if (comboBox1.Items != null) {
                comboBox1.SelectedIndex = 0;
            }*/
            //comboBox2.SelectedIndex = 0;

            label15.Text = "Total Cost" + " - " + " Cash Paid " ;
            label16.Text = "Product Price " + " + " + " Trruck Cost " + " + " + " Other Cost " ;
            label17.Text = "Quantity " + " * " + " Unit Price " ;

        }

        public void SetToday() {

            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dateTimePicker1.Value = DateTime.Now;

            string today = DateTime.Now.ToString("dd/MM/yyyy"); 
            label21.Text = "Today's Memo " + "( " + today + " )" ;
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
     

        public void ReadDataForCombobox(SQLiteConnection conn)
        {

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Company";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                comboBox2.Items.Add(sqlite_datareader.GetValue(1).ToString());
            }
            //conn.Close();
        }
        
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {   // qunatity decimal not allowed 
            // allows 0-9, backspace
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
       


        // quantity text change 
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                {
                    quantity = 0;
                }
                else
                {
                    quantity = Convert.ToDouble(textBox2.Text);
                }
                

                totalproductprice = quantity * unitprice;
                label12.Text =Math.Round(totalproductprice, 2).ToString();

                totacost = totalproductprice + truckcost + othercost;
                label11.Text =  Math.Round(totacost, 2).ToString();

                duebalance = totacost - cashreceived;
                if (duebalance > 0)
                {
                    dueflag = 1;
                    label14.Text = Math.Round(duebalance, 2).ToString();
                }

                else
                {
                    dueflag = 0;
                    label14.Text = 0.ToString();
                }



            }
            catch (Exception)
            {

                //MessageBox.Show("Enter valid Number");
                
            }
        }
        // unitprice text change 
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "") {
                    unitprice = 0;
                }
                else {
                    unitprice = Convert.ToDouble(textBox3.Text);
                }

                totalproductprice = quantity * unitprice;
                label12.Text = Math.Round(totalproductprice, 2).ToString();

                totacost = totalproductprice + truckcost + othercost;
                label11.Text = Math.Round(totacost, 2).ToString();

                duebalance = totacost - cashreceived;
                if (duebalance > 0)
                {
                    dueflag = 1;
                    label14.Text = Math.Round(duebalance, 2).ToString();
                }

                else
                {
                    dueflag = 0;
                    label14.Text = 0.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Enter valid Number");
            }
        }

        // truck cost text change 
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "") {
                    truckcost = 0;
                }
                else
                {
                    truckcost = Convert.ToDouble(textBox5.Text);
                }
               

                totacost = totalproductprice + truckcost + othercost;
                label11.Text =  Math.Round(totacost, 2).ToString();

                duebalance = totacost - cashreceived;
                if (duebalance > 0)
                {
                    dueflag = 1;
                    label14.Text = Math.Round(duebalance, 2).ToString();
                }

                else
                {
                    dueflag = 0;
                    label14.Text = 0.ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Enter valid Number");
            }
        }

        private void ShopMemo_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DataGridViewColumn duecolumn = dataGridView1.Columns[14];
            duecolumn.ToolTipText =
                "Paid = 0 Due = 1";
        }

        // other cost text change 
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "") {
                    othercost = 0;
                }
                else
                {
                     othercost = Convert.ToDouble(textBox6.Text);
                }
                

                totacost = totalproductprice + truckcost + othercost;
                label11.Text = Math.Round(totacost, 2).ToString();

                duebalance = totacost - cashreceived;
                if (duebalance > 0)
                {
                    dueflag = 1;
                    label14.Text = Math.Round(duebalance, 2).ToString();
                }

                else
                {
                    dueflag = 0;
                    label14.Text = 0.ToString();
                }
            }
            catch (Exception)
            {
                
                MessageBox.Show("Enter valid Number");
            }
        }
        // cash received 
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cashreceived = Convert.ToDouble(textBox7.Text);

                duebalance = totacost - cashreceived;
                if (duebalance > 0)
                {
                    // 0= no due 1 = due 
                    dueflag = 1; 
                    label14.Text = Math.Round(duebalance, 2).ToString();
                }

                else {
                    // 0= no due 1 = due 
                    dueflag = 0;
                    label14.Text = 0.ToString();
                }
                
            }
            catch (Exception)
            {
                dueflag = 0;
                label14.Text = 0.ToString();
                //MessageBox.Show("Enter valid Number");
            }
        }



        // Shop combobox selecct change 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_conn = CreateConnection();
            sqlite_cmd = sqlite_conn.CreateCommand();

            ComboBox comboBox = (ComboBox)sender;
            shopname = (string)comboBox2.SelectedItem;


            //string shopname = comboBox2.SelectedText.ToString();
            
            sqlite_cmd.CommandText = "SELECT * FROM Company where ShopName='" + shopname + "'"; 

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                shopMobileNumber = sqlite_datareader.GetValue(2).ToString();
                shopAddress = sqlite_datareader.GetValue(3).ToString();
                shopname = sqlite_datareader.GetValue(1).ToString();
                string id = sqlite_datareader.GetValue(0).ToString();
                Int32.TryParse(id, out shopid);
                richTextBox1.Text = shopAddress;
                label19.Text = shopMobileNumber;
            }
        }
        // product type select 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            productType = (string)comboBox1.SelectedItem;

        }

        // Add Memo record 
        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.TextLength.ToString() + textBox2.TextLength.ToString() + textBox3.TextLength.ToString() + textBox5.TextLength.ToString() + textBox6.TextLength.ToString() + textBox7.TextLength.ToString() );


         

            if (comboBox2.SelectedItem != null && textBox1.TextLength > 0 && textBox2.TextLength > 0 && comboBox1.SelectedItem != null && textBox3.TextLength > 0 && textBox7.TextLength > 0)
            {


                DialogResult dr = MessageBox.Show("Are you sure to insert this memo?", "Confirmation", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    DateTime dateAndTime = dateTimePicker1.Value;

                    //MessageBox.Show("Selected date is " + dateAndTime.ToString("dd/MM/yyyy"));
                    memoDate = dateAndTime.ToString("dd/MM/yyyy");
                    doNumber = textBox1.Text.ToString();



                    Debug.WriteLine(shopid + shopname + memoDate + doNumber + quantity + productType + unitprice + totalproductprice + truckcost + othercost + totacost + cashreceived + duebalance + dueflag);

                    if (duebalance < 0)
                    {
                        duebalance = 0;
                    }

                    InsertData(sqlite_conn, shopid, shopname, memoDate, doNumber, quantity, productType, unitprice, totalproductprice, truckcost, othercost, totacost, cashreceived, duebalance, dueflag);
                    this.dataGridView1.Rows.Clear();
                    ReadData(sqlite_conn);

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";

                }


                else if (DialogResult == DialogResult.No)
                {
                    //Nothing to do
                }


                



        


            }

            else {

                MessageBox.Show("Please Insert all required fields !");
            }


      

        }


        public void InsertData(SQLiteConnection conn,int shopid,string shopname,string memoDate, string doNumber,double quantity,string productType, double unitprice, double totalproductprice, double truckcost, double othercost, double totacost, double cashreceived, double duebalance,int dueflag)
        {

            SQLiteCommand sqlite_cmd_;
            sqlite_cmd_ = conn.CreateCommand();
            sqlite_cmd_.CommandText = "SELECT count(*) FROM Company WHERE ShopName='" + shopname + "'";

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO TruckMemo(ShopId,ShopName,MemoDate,DONumber,ProductQuantity,ProductType,UnitPrice,ProductPrice,TruckCost,OtherCost,TotalCost,CashReceived,DueBalance,DueFlag) VALUES('" + shopid + "','" + shopname + "','" + memoDate + "','" + doNumber + "','" + quantity + "','" + productType + "','" + unitprice + "','" + totalproductprice + "','" + truckcost + "','" + othercost + "','" + totacost + "','" + cashreceived + "','" + duebalance + "','" + dueflag + "') ";
            sqlite_cmd.ExecuteNonQuery();

        }


        public void ReadData(SQLiteConnection conn)
        {
            this.dataGridView1.Rows.Clear();

            totalMemotext = 0;
            totalQuantitytext = 0;
            totalCosttext = 0;
            totalCashreceivedtext = 0;
            totalDuebalancetext = 0;

            Special = 0;
            Popular = 0;
            OPC = 0;


            SetToday();
            DateTime dateAndTime = dateTimePicker1.Value;
            memoDate = dateAndTime.ToString("dd/MM/yyyy");


            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE MemoDate='" + memoDate + "'"; 
            
            sqlite_datareader = sqlite_cmd.ExecuteReader();


            int counter = 0;
            while (sqlite_datareader.Read())
            {

                counter += counter;
                dataGridView1.Rows.Insert(0, sqlite_datareader.GetValue(0), sqlite_datareader.GetValue(1), sqlite_datareader.GetString(2), sqlite_datareader.GetString(3), sqlite_datareader.GetString(4), sqlite_datareader.GetValue(5), sqlite_datareader.GetString(6), sqlite_datareader.GetValue(7), sqlite_datareader.GetValue(8), sqlite_datareader.GetValue(9), sqlite_datareader.GetValue(10), sqlite_datareader.GetValue(11), sqlite_datareader.GetValue(12), sqlite_datareader.GetValue(13), sqlite_datareader.GetValue(14));


                totalMemotext = totalMemotext + 1;
                totalQuantitytext = totalQuantitytext + Convert.ToDouble(sqlite_datareader.GetValue(5).ToString());
                totalCosttext = totalCosttext + Convert.ToDouble(sqlite_datareader.GetValue(11).ToString());
                totalCashreceivedtext = totalCashreceivedtext + Convert.ToDouble(sqlite_datareader.GetValue(12).ToString());
                double due = Convert.ToDouble(sqlite_datareader.GetValue(13).ToString());



                dataGridView1.Rows[counter].Cells[5].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[8].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[9].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[10].Style.BackColor = Color.LightGoldenrodYellow;

                dataGridView1.Rows[counter].Cells[11].Style.BackColor = Color.LightBlue;
                dataGridView1.Rows[counter].Cells[12].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[counter].Cells[13].Style.BackColor = Color.LightPink;



                if (due > 0)
                {
                    totalDuebalancetext = totalDuebalancetext + due;
                }
                else
                {

                }

                Debug.WriteLine(sqlite_datareader.GetString(4).ToString());
                if (sqlite_datareader.GetString(6).ToString() == "Special") { Special = Special + Convert.ToInt32(sqlite_datareader.GetValue(5)); }
                if (sqlite_datareader.GetString(6).ToString() == "Popular") { Popular = Popular + Convert.ToInt32(sqlite_datareader.GetValue(5)); }
                if (sqlite_datareader.GetString(6).ToString() == "OPC") { OPC = OPC + Convert.ToInt32(sqlite_datareader.GetValue(5)); }

            }

            label22.Text = "Total Memo : " + totalMemotext;
            label26.Text = "Total Product Quantity : " + totalQuantitytext + " ( Special : " + Special + " Popular : " + Popular + " OPC : " + OPC + " )";
            label23.Text = "Total Cost : " + totalCosttext;
            label25.Text = "Total Cash Paid : " + totalCashreceivedtext;
            label24.Text = "Total Due Balance :  : " + totalDuebalancetext;

        }

    }

}
