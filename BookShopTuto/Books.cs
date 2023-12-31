﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookShopTuto
{
    public partial class Books : Form
    {

        public Books()
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

        private void Filter()
        {
            Con.Open();
            string query = "select * from BookTb1 where BCat = '"+CatCbSearchCb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BAutTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {
                MessageBox.Show("信息缺失！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into BookTb1 values('"+BTitleTb.Text+"','"+BAutTb.Text+"','"+BCatCb.SelectedItem.ToString()+"','"+QtyTb.Text+"','"+PriceTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息保存成功！！");
                    Con.Close();
                    populate();
                    Reset();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出本系统？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CatCbSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            CatCbSearchCb.SelectedIndex = -1;
        }

        private void Reset()
        {
            BTitleTb.Text = "";
            BAutTb.Text = "";
            BCatCb.SelectedIndex = -1;
            QtyTb.Text = "";
            PriceTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int key = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAutTb.Text = BookDGV.SelectedRows[0].Cells[2].Value.ToString();
            BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            PriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            if(BTitleTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("信息缺失！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BookTb1 where BId = " + key + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息删除成功！！");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BAutTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {
                MessageBox.Show("信息缺失！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BookTb1 set BTitle='"+BTitleTb.Text+"',BAuthor='"+BAutTb.Text+"',BCat='"+BCatCb.SelectedItem.ToString()+"',BQty='"+QtyTb.Text+"',BPrice='"+PriceTb.Text+"' where BId = "+key+" ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("书籍信息更新成功！！");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

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
    }
}
