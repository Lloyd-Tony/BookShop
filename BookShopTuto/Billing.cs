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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection("YOURDATEBASE");
        private void populate()
        {
            Con.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            try
            {
                Con.Open();
                string query = "update BookTb1 set BQty=" + newQty + " where BId = " + key + " ";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int n = 0,GrdTotal = 0;
        private void AddtoBillBtn_Click(object sender, EventArgs e)
        {
            if(QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("库存不足！！");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                GrdTotal = GrdTotal + total;
                TotalLbl.Text = "$" + GrdTotal;
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出本系统？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        int key = 0, stock = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = "1";
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (BTitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (BillDGV.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("购物车为空");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BillTb1 values('" + UserNameLbl.Text + "','" + GrdTotal + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("订单信息保存成功！！");
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }
        int prodid, prodqty, prodprice, total, pos = 60;
        string prodname;

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = Login.UserName;
        }

        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("牛犇书店", new Font("幼圆", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("编号  产品  价格  数量  总计", new Font("幼圆", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column7"].Value);
                prodname = "" + row.Cells["Column8"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column9"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column10"].Value);
                total = Convert.ToInt32(row.Cells["Column11"].Value);
                e.Graphics.DrawString("" + prodid, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("订单总额：￥" + GrdTotal, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("*********牛犇书店*********", new Font("幼圆", 10, FontStyle.Bold), Brushes.Crimson, new Point(40, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        

        private void Reset()
        {
            BTitleTb.Text = "";
            PriceTb.Text = "";
            QtyTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
