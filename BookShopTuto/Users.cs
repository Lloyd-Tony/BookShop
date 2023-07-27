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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection("YOURDATEBASE");

        private void populate()
        {
            Con.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            AddTb.Text = "";
            PassTb.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || AddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("信息缺失！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into UserTb1 values('" + UnameTb.Text + "','" + PhoneTb.Text + "','" + AddTb.Text + "','" + PassTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息保存成功！！");
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

        private void picExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出本系统？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
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
                    string query = "delete from UserTb1 where UId = " + key + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息删除成功！！");
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

        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            PassTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PhoneTb.Text == "" || AddTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("信息缺失！！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTb1 set UName='" + UnameTb.Text + "',UPhone='" + PhoneTb.Text + "',UAdd='" + AddTb.Text + "',UPassword='" + PassTb.Text + "' where UId = " + key + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息更新成功！！");
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
