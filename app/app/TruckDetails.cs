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
    public partial class TruckDetails : Form
    {

        SQLiteConnection sqlite_conn;
        public int shopid;
        //public int memoid1;
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
        double quantity = 1, unitprice = 0, totalproductprice = 0, truckcost = 0, othercost = 0, totacost = 0, cashreceived = 0, duebalance = 0;
        int status = 0;


        public int Special = 0;
        public int Popular = 0;
        public int OPC = 0;

        // 0= no due 1 = due 
        int dueflag = 0;

        int firsttime = 0;

        public TruckDetails()
        {
            InitializeComponent();
            comboBox2.Items.Add("All");
            sqlite_conn = CreateConnection();

            ReadDataForCombobox(sqlite_conn);
            SetToday();

            comboBox3.Items.Add("By Date");
            comboBox3.Items.Add("By Month");
            comboBox3.Items.Add("By Year");

            comboBox1.Items.Add("Both");   //0
            comboBox1.Items.Add("Paid");  //1
            comboBox1.Items.Add("Due");     //2

            try
            {
                //  shop name 
                if (comboBox2.Items != null)
                {
                    comboBox2.SelectedIndex = 1;
                }
            }
            catch (Exception)
            {

                
            }

            // search by
            if (comboBox3.Items != null)
            {
                comboBox3.SelectedIndex = 0;
            }

            // transaction status 
            if (comboBox1.Items != null)
            {
                comboBox1.SelectedIndex = 0;
            }

            ReadData(sqlite_conn);

            //comboBox3.Items.Add("All Record");
           

            firsttime++;

            this.WindowState = FormWindowState.Maximized;

        }


   

        public void SetToday()
        {

            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            dateTimePicker1.Value = DateTime.Now;

            /*            string today = DateTime.Now.ToString("dd/MM/yyyy");
            label21.Text = "Today's Memo " + "( " + today + " )";*/
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {
                if (e.ColumnIndex == 15 && dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                {


                    DialogResult dr = MessageBox.Show("Are you sure you want to Paid Due Amount ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                        {

                            //MessageBox.Show(dataGridView1[2, e.RowIndex].Value.ToString());


                            try
                            {
                                this.Hide();
                                //  memoid 0,  shop name 2 , date 3, due balance , cash received, total cost 
                                var DueMemoTruck = new DueMemoTruck(dataGridView1[0, e.RowIndex].Value.ToString(), dataGridView1[2, e.RowIndex].Value.ToString(), dataGridView1[3, e.RowIndex].Value.ToString(), dataGridView1[13, e.RowIndex].Value.ToString(), dataGridView1[12, e.RowIndex].Value.ToString(), dataGridView1[11, e.RowIndex].Value.ToString());
                                DueMemoTruck.ShowDialog();
                                DueMemoTruck = null;
                                this.Show();
                                ReadData(sqlite_conn);
                            }
                            catch (Exception)
                            {

                                //throw;
                            }


                        }


                        else
                        {

                        }

                    }
                    else if (DialogResult == DialogResult.No)
                    {
                        //Nothing to do
                    }


                }
                else { }
            }
            catch (Exception)
            {

                //throw;
            }

               
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete Selected Memo ?", "Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete row from database or datagridview...
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        string id = row.Cells[0].Value.ToString();
                        MessageBox.Show("Memo : " +id.ToString()+ " Will delete ! ");
                        DeleteData(sqlite_conn, id);
                        this.dataGridView1.Rows.Clear();
                        ReadData(sqlite_conn);

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

        public void DeleteData(SQLiteConnection conn, string id)
        {

            //shopname = this.textBox1.Text;


            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM TruckMemo WHERE MemoId = '" + id + "'"; 
            sqlite_cmd.ExecuteNonQuery();
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

        

        private void ShopDetails_Load(object sender, EventArgs e)
        {
            DataGridViewColumn duecolumn = dataGridView1.Columns[14];
            duecolumn.ToolTipText =
                "Paid = 0 Due = 1";


            



        }

        // serrch by option 
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox3.SelectedIndex.ToString());

            if (comboBox3.SelectedIndex.ToString() == "0") {

                dateTimePicker1.CustomFormat = "dd-MMMM-yyyy";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;

                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.ShowUpDown = false;


            }

            if (comboBox3.SelectedIndex.ToString() == "1")
            {
                /*dateTimePicker1.CustomFormat = "MM";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;

                //dateTimePicker1.Value = DateTime.Now.Month; 

                comboBox2.SelectedValue = DateTime.Now.Month;*/

                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "MMMM yyyy";
                dateTimePicker1.ShowUpDown = true;

            }
            if (comboBox3.SelectedIndex.ToString() == "2")
            {
                /* dateTimePicker1.CustomFormat = "yyyy";
                 dateTimePicker1.Format = DateTimePickerFormat.Custom;*/

                //dateTimePicker1.Value = DateTime.Now.Month; 

                //comboBox2.SelectedValue = DateTime.Now.Year;

                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy";
                dateTimePicker1.ShowUpDown = true;

            }


            if (firsttime > 0)
            {
                try
                {
                    ReadData(sqlite_conn);
                }
                catch (Exception)
                {


                }
            }
        }

        // search by both , due, clear 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firsttime > 0)
            {
                try
                {
                    ReadData(sqlite_conn);
                }
                catch (Exception)
                {


                }
            }
        }
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firsttime>0) {
                try
                {
                    ReadData(sqlite_conn);
                }
                catch (Exception)
                {


                }
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (firsttime > 0)
            {
                try
                {
                    ReadData(sqlite_conn);
                }
                catch (Exception)
                {


                }
            }
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


            // by date
            if (comboBox3.SelectedIndex.ToString() == "0")
            {
                DateTime dateAndTime = dateTimePicker1.Value;
                memoDate = dateAndTime.ToString("dd/MM/yyyy");
            }

            // by month and year 
            if (comboBox3.SelectedIndex.ToString() == "1") {
                DateTime dateAndTime = dateTimePicker1.Value;
                memoDate = dateAndTime.ToString("MM/yyyy");

            }

            // by year 
            if (comboBox3.SelectedIndex.ToString() == "2")
            {

                DateTime dateAndTime = dateTimePicker1.Value;
                memoDate = dateAndTime.ToString("yyyy");

            }

            Debug.WriteLine(memoDate);

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();


            try
            {
                shopname = comboBox2.SelectedItem.ToString();
                //MessageBox.Show(shopname);
            }
            catch (Exception)
            {


            }

            try
            {
                status = comboBox1.SelectedIndex;
            }
            catch (Exception)
            {

                throw;
            }

            // all shop and both clear and due
            if (shopname == "All" && status ==0)
            {
               
                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE MemoDate LIKE '%" + memoDate + "%'";


            }
            // selected shop and both clear and due
            if (shopname != "All" && status == 0)
            {
                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE ShopName='" + shopname + "' AND MemoDate LIKE '%" + memoDate + "%'";
            }
            // all shop and clear
            if (shopname == "All" && status == 1)
            {

                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE MemoDate LIKE '%" + memoDate + "%' AND DueFlag='" + 0 + "' ";
            }

            // selected shop and clear
            if (shopname != "All" && status == 1)
            {
                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE ShopName='" + shopname + "' AND MemoDate LIKE '%" + memoDate + "%' AND DueFlag='" + 0 + "' ";
            }

            // all shop and due
            if (shopname == "All" && status == 2)
            {

                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE MemoDate LIKE '%" + memoDate + "%' AND DueFlag='" + 1 + "' ";
            }

            // selected shop and due
            if (shopname != "All" && status == 2)
            {
                sqlite_cmd.CommandText = "SELECT * FROM TruckMemo WHERE ShopName='" + shopname + "' AND MemoDate LIKE '%" + memoDate + "%' AND DueFlag='" + 1 + "' ";
            }

           // sqlite_cmd.CommandText = "SELECT * FROM ShopMemo WHERE MemoDate LIKE '%" + memoDate + "%'";

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            //Debug.WriteLine(sqlite_cmd.CommandText);
            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            int counter = 0;
            while (sqlite_datareader.Read())
            {
                counter += counter;


              




                dataGridView1.Rows.Insert(0, sqlite_datareader.GetValue(0), sqlite_datareader.GetValue(1), sqlite_datareader.GetString(2), sqlite_datareader.GetString(3), sqlite_datareader.GetString(4), sqlite_datareader.GetValue(5), sqlite_datareader.GetString(6), sqlite_datareader.GetValue(7), sqlite_datareader.GetValue(8), sqlite_datareader.GetValue(9), sqlite_datareader.GetValue(10), sqlite_datareader.GetValue(11), sqlite_datareader.GetValue(12), sqlite_datareader.GetValue(13), sqlite_datareader.GetValue(14));

                //memoid1 = (int)sqlite_datareader.GetValue(0); 
                //Debug.WriteLine(sqlite_datareader.GetValue(0));

                totalMemotext = totalMemotext +1;
                totalQuantitytext = totalQuantitytext + Convert.ToDouble(sqlite_datareader.GetValue(5).ToString());
                totalCosttext = totalCosttext + Convert.ToDouble(sqlite_datareader.GetValue(11).ToString());    
                totalCashreceivedtext = totalCashreceivedtext + Convert.ToDouble(sqlite_datareader.GetValue(12).ToString());




                dataGridView1.Rows[counter].Cells[5].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[8].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[9].Style.BackColor = Color.LightGoldenrodYellow;
                dataGridView1.Rows[counter].Cells[10].Style.BackColor = Color.LightGoldenrodYellow;

                dataGridView1.Rows[counter].Cells[11].Style.BackColor = Color.LightBlue;
                dataGridView1.Rows[counter].Cells[12].Style.BackColor = Color.LightGreen;
                dataGridView1.Rows[counter].Cells[13].Style.BackColor = Color.LightPink;


                double due = Convert.ToDouble(sqlite_datareader.GetValue(13).ToString());
                if (due > 0)
                {
                    totalDuebalancetext = totalDuebalancetext + due;
                    dataGridView1.Rows[counter].Cells[15].Value = "Paid";

                    



                }
                else { 
                
                }

                Debug.WriteLine(sqlite_datareader.GetString(4).ToString());
                if (sqlite_datareader.GetString(6).ToString() == "Special") { Special = Special + Convert.ToInt32(sqlite_datareader.GetValue(5)); }
                if (sqlite_datareader.GetString(6).ToString() == "Popular") { Popular = Popular + Convert.ToInt32(sqlite_datareader.GetValue(5)); }
                if (sqlite_datareader.GetString(6).ToString() == "OPC") { OPC = OPC + Convert.ToInt32(sqlite_datareader.GetValue(5)); }

                //dataGridView1.Rows[Index].Visible = false;  

                
               

            }

            label7.Text = "Total Memo : " + totalMemotext;
            label4.Text = "Total Product Quantity : " + totalQuantitytext;
            label6.Text = "Total Cost : " + totalCosttext;
            label5.Text = "Total Cash Paid : " + totalCashreceivedtext;
            label8.Text = "Total Due Balance :  : " + totalDuebalancetext;

            label9.Text = "( Special : " + Special + " Popular : " + Popular + " OPC : " + OPC + " )";




        }

    }
}
