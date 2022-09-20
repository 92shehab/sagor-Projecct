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
    public partial class DueMemoTruck : Form
    {
        //memoid 0,  shop name 2 , date 3, due balance
        string memoid = ""; 
        string shopname ="";
        string memodate = "";
        string duebalance = "";
        string cashreceived = "";
        string totalcash = "";

        double duecolelct;
        int dueflag; 

        SQLiteConnection sqlite_conn;
        public DueMemoTruck(string id, string name,string date, string balance, string cashreceive, string totalamount )
        {
            InitializeComponent();
            sqlite_conn = CreateConnection();
            memoid = id; 
            shopname = name;
            memodate = date;
            duebalance = balance; 
            cashreceived = cashreceive;
            totalcash = totalamount;

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

        private void DueMemo_Load(object sender, EventArgs e)
        {
            label4.Text = shopname; 
            label3.Text = memoid.ToString();
            label5.Text = memodate;
            label6.Text = duebalance.ToString();
            label8.Text = totalcash.ToString();
            label12.Text = cashreceived.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            duecolelct = Convert.ToDouble(textBox1.Text.ToString());
            double duebalanced;
            double cashr;
            double.TryParse(duebalance, out duebalanced);
            double.TryParse(cashreceived, out cashr);

            double dueb = duebalanced - duecolelct;
            cashr = cashr + duecolelct;
            if (dueb <= 0)
            {
                // 0 no due
                dueflag = 0;

            }

            else {
                // 1 due 
                dueflag = 1;
            }

            if (dueb<0) {
                dueb = 0;
            }

            dueb = Math.Round(dueb, 2);
            Debug.WriteLine(duecolelct);
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE TruckMemo SET DueBalance = '" + dueb + "', CashReceived = '" + cashr + "', DueFlag = '" + dueflag + "' WHERE MemoId = '" + memoid + "'";
            sqlite_cmd.ExecuteNonQuery();
            Debug.WriteLine("ddone");
            this.Dispose();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
