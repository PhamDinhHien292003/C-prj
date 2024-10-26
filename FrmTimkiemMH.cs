using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19_10_2024
{


    public partial class FrmTimkiemMH : Form
    {
        Connect connect = new Connect();
        SqlConnection conn = Connect.createConnect("Data Source=DESKTOP-P137F4R;Initial Catalog=QuanLyBanHang;Integrated Security=True;");
        public FrmTimkiemMH()
        {
            InitializeComponent();
            conn.Open();
            fill_to_gridview();
            Graphics graphics = this.CreateGraphics();
            double startingPoint = (this.Width / 2) - (graphics.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            double widthOfSpace = graphics.MeasureString(" ", this.Font).Width;

            string temp = " ";
            double tempWidth = 0;

            while ((tempWidth + widthOfSpace) < startingPoint)
            {
                temp += " ";
                tempWidth += widthOfSpace;
            }

            this.Text = temp + this.Text.Trim();
        }
        

        public void fill_to_gridview(SqlDataReader da)
        {
            if (da.HasRows)
            {
                dataGridView1.Rows.Clear();
                while (da.Read())
                {
                    Object[] record = new object[3];
                    record[0] = da.GetValue(0);
                    record[1] = da.GetValue(1);
                    record[2] = da.GetValue(2);
                    dataGridView1.Rows.Add(record);
                }
                da.Close();
                groupBox2.Text = "Kết quả tìm thấy (" + dataGridView1.Rows.Count + " kết quả )";
            }
            else
            {
                MessageBox.Show("Không có bản ghi phù hợp!!");
                da.Close();
            }
        }

        public void fill_to_gridview()
        {
            dataGridView1.Rows.Clear();
            try
            {
                SqlDataReader da = connect.getQuery("Select * from tblMatHang", conn);
                if (da.HasRows)
                {
                    while (da.Read())
                    {
                        Object[] record = new object[3];
                        record[0] = da.GetValue(0);
                        record[1] = da.GetValue(1);
                        record[2] = da.GetValue(2);
                        dataGridView1.Rows.Add(record);
                    }
                    da.Close();
                    groupBox2.Text = "Kết quả tìm thấy (" + dataGridView1.Rows.Count + " kết quả )";
                }
                else
                {
                    da.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") && textBox1.Text.Trim().Equals(""))
            {
                fill_to_gridview();
                return;
            }
            if (radioButton1.Checked)
            {
                if (int.TryParse(textBox1.Text, out int n))
                {
                    String query = "select * from tblMatHang where MaMH  =" + textBox1.Text + "";
                    fill_to_gridview(new SqlCommand(query, conn).ExecuteReader());
                }
                else
                {
                    MessageBox.Show("Mã mặt hàng phải là số ");
                }
            }
            else
            {
                String query = "select * from tblMatHang where TenMatHang  like '%" + textBox1.Text + "%'";
                fill_to_gridview(new SqlCommand(query, conn).ExecuteReader());
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTimkiemMH_Load(object sender, EventArgs e)
        {
            
        }
    }



}
