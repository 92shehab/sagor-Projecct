using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace app
{

    public partial class Form1 : Form
    {
        SQLiteConnection sqlite_conn;


        public Form1()
        {
            InitializeComponent();
            this.Text = "SN Traders";
            this.MaximizeBox = false;
            Debug.WriteLine("Test");


            sqlite_conn = CreateConnection();
            try
            {
                CreateTable(sqlite_conn);
                

            }
            catch (Exception)
            {

                //throw;
            }

            
            Register(sqlite_conn);
            
            
            //InsertData(sqlite_conn);
            //ReadData(sqlite_conn);


            //this.WindowState = FormWindowState.Maximized;
            //label1.Anchor = AnchorStyles.Top;
        }


   

        private void button2_Click(object sender, EventArgs e)
        {


            // check user authentication 
            //string username_ = this.username.Text;
            //string password_ = this.password.Text;
            //Debug.WriteLine(username_);
            //Debug.WriteLine(password_);

            SQLiteCommand sqlite_cmd_;
            sqlite_cmd_ = sqlite_conn.CreateCommand();
            sqlite_cmd_.CommandText = "SELECT count(*) FROM Login WHERE UserName='" + username.Text + "' And Password ='" + password.Text + "'";
            int count = Convert.ToInt32(sqlite_cmd_.ExecuteScalar());
            Debug.WriteLine(count);
            if (count == 1)
            {


                try
                {
                    this.Hide();
                    var Home = new Home();
                    Home.ShowDialog();
                    Home = null;
                    this.Show();
                }
                catch (Exception)
                {

                    throw;
                }

            }


          
        }


        static SQLiteConnection CreateConnection()
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

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            string Createsqllogin = "CREATE TABLE Login(UserName VARCHAR(20), Password VARCHAR(20))";
            string Createsqlshop = "CREATE TABLE Shop(ShopId INTEGER PRIMARY KEY AUTOINCREMENT, ShopName VARCHAR(100), ShopMobileNumber VARCHAR(100), ShopAddress VARCHAR(100))";
            string CreatesqlshopMemo = "CREATE TABLE ShopMemo(MemoId INTEGER PRIMARY KEY AUTOINCREMENT, ShopId INTEGER, ShopName VARCHAR(100), MemoDate VARCHAR(100) NOT NULL, DONumber VARCHAR(100),ProductQuantity DOUBLE(100), ProductType VARCHAR(100), UnitPrice DOUBLE(100),ProductPrice DOUBLE(100), TruckCost DOUBLE(100), OtherCost DOUBLE(100), TotalCost DOUBLE(100), CashReceived DOUBLE(100), DueBalance DOUBLE(100), DueFlag INTEGER)";
            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = Createsqllogin;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = Createsqlshop;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreatesqlshopMemo;
            sqlite_cmd.ExecuteNonQuery();
            Debug.WriteLine("Table create");



            
            string company = "CREATE TABLE Company(ShopId INTEGER PRIMARY KEY AUTOINCREMENT, ShopName VARCHAR(100), ShopMobileNumber VARCHAR(100), ShopAddress VARCHAR(100))";
            string truckmemo = "CREATE TABLE TruckMemo(MemoId INTEGER PRIMARY KEY AUTOINCREMENT, ShopId INTEGER, ShopName VARCHAR(100), MemoDate VARCHAR(100) NOT NULL, DONumber VARCHAR(100),ProductQuantity DOUBLE(100), ProductType VARCHAR(100), UnitPrice DOUBLE(100),ProductPrice DOUBLE(100), TruckCost DOUBLE(100), OtherCost DOUBLE(100), TotalCost DOUBLE(100), CashReceived DOUBLE(100), DueBalance DOUBLE(100), DueFlag INTEGER)";
            sqlite_cmd = conn.CreateCommand();



            sqlite_cmd.CommandText = company;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = truckmemo;
            sqlite_cmd.ExecuteNonQuery();
            Debug.WriteLine("Table create");


        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO Login(UserName, Password) VALUES('admin', 'admin'); ";
            sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            //sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            //sqlite_cmd.ExecuteNonQuery();


            //sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            //sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Login";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                // get username 
                string myreader = sqlite_datareader.GetString(0);
                Debug.WriteLine(myreader);
            }
            //conn.Close();
        }

        static void Register(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd_;
            sqlite_cmd_ = conn.CreateCommand();
            sqlite_cmd_.CommandText = "SELECT count(*) FROM Login WHERE UserName='admin'";
            int count = Convert.ToInt32(sqlite_cmd_.ExecuteScalar());
            // 0 means false 1 means true
            Debug.WriteLine(count);
            if (count == 0)
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Login(UserName, Password) VALUES('admin', 'admin'); ";
                sqlite_cmd.ExecuteNonQuery();

            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var Home = new Home();
                Home.ShowDialog();
                Home = null;
                this.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}