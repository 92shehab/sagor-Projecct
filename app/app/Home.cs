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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Shops = new Shops();
            Shops.ShowDialog();
            Shops = null;
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Trucks = new Trucks();
            Trucks.ShowDialog();
            Trucks = null;
            this.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
