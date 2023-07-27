using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopTuto
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("YOURDATEBASE");

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books obj = new Books();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            DashBoard obj = new DashBoard();
            obj.Show();
            this.Hide();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(BQty) from BookTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookStockLbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(Amount) from BillTb1", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLbl.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from UserTb1", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            UserTotalLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出本系统？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
