using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopTuto
{
    public partial class BookShop : Form
    {
        public BookShop()
        {
            InitializeComponent();
        }

        int startpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            Myprogress.Value = startpos;
            PercentageLbl.Text = startpos + "%";
            if(Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void BookShop_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
