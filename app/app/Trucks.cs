using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public partial class Trucks : Form
    {
        public Trucks()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var AddCompany = new AddCompany();
                AddCompany.ShowDialog();
                AddCompany = null;
                this.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var TruckMemo = new TruckMemo();
                TruckMemo.ShowDialog();
                TruckMemo = null;
                this.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var TruckDetails = new TruckDetails();
                TruckDetails.ShowDialog();
                TruckDetails = null;
                this.Show();
            }
            catch (Exception)
            {

                throw;
                //MessageBox.Show(sender.ToString());
            }
        }
    }
}
