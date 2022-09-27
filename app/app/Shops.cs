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
    public partial class Shops : Form
    {
        public Shops()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var AddShop = new AddShop();
                AddShop.ShowDialog();
                AddShop = null;
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
                var ShopMemo = new ShopMemo();
                ShopMemo.ShowDialog();
                ShopMemo = null;
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
                var ShopDetails = new ShopDetails();
                ShopDetails.ShowDialog();
                ShopDetails = null;
                this.Show();
            }
            catch (Exception)
            {

                throw;
                //MessageBox.Show(e.ToString());
            }
        }
    }
}
