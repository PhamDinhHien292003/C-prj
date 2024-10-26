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

namespace _19_10_2024
{
    public partial class FrmTimKiemGDMB : Form
    {
        Connect connect = new Connect();
        SqlConnection conn = Connect.createConnect("Data Source=DESKTOP-P137F4R;Initial Catalog=QuanLyBanHang;Integrated Security=True;");
        public FrmTimKiemGDMB()
        {
            InitializeComponent();
            conn.Open();
            fill_to_gridview();
        }

        public void fill_to_gridview(SqlDataReader da)
        {
            if (da.HasRows)
            {
                dataGridView1.Rows.Clear();
                while (da.Read())
                {
                    Object[] record = new object[6];
                    record[0] = da.GetValue(0);
                    record[1] = da.GetValue(1);
                    record[2] = da.GetValue(2);
                    record[3] = da.GetValue(3);
                    record[4] = da.GetValue(4);
                    record[5] = da.GetValue(5);
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
                string query = @"
    SELECT 
        t.SoHieuHD, 
        t.NgayMuaBan, 
        m.TenMatHang AS tenhang, 
        chitiet.SoLuong,
        kh.MaKH,
        kh.HoTen AS hoten   
    FROM 
        tblBanHang t 
    JOIN 
        tblChiTietBanHang chitiet ON t.SoHieuHD = chitiet.SoHieuHD
    JOIN 
        tblKhachHang kh ON kh.MaKH = t.MaKH
    JOIN 
        tblMatHang m ON m.MaMH = chitiet.MaMH;";
                SqlDataReader da = connect.getQuery(query, conn);
                if (da.HasRows)
                {

                    while (da.Read())
                    {
                        Object[] record = new object[6];
                        record[0] = da.GetValue(0);
                        record[1] = da.GetValue(1);
                        record[2] = da.GetValue(2);
                        record[3] = da.GetValue(3);
                        record[4] = da.GetValue(4);
                        record[5] = da.GetValue(5);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == true)
            {
                if (!int.TryParse(textBox1.Text, out int val))
                {
                    if (textBox1.Text.Trim().Equals("") || textBox1.Text.Equals(""))
                    {
                        fill_to_gridview();
                        return;
                    }
                    MessageBox.Show("Phải nhập số");
                    return;
                }
                
                if (!textBox1.Text.Trim().Equals("") && !textBox1.Text.Equals(""))
                {
                    string query = @"SELECT 
    t.SoHieuHD, 
    t.NgayMuaBan, 
    m.TenMatHang AS tenhang, 
    chitiet.SoLuong,
    kh.MaKH,
    kh.HoTen AS hoten   
FROM 
    tblBanHang t 
JOIN 
    tblChiTietBanHang chitiet ON t.SoHieuHD = chitiet.SoHieuHD
JOIN 
    tblKhachHang kh ON kh.MaKH = t.MaKH
JOIN 
    tblMatHang m ON m.MaMH = chitiet.MaMH
WHERE 
    kh.MaKH = " + textBox1.Text + "  ";

                    fill_to_gridview(new SqlCommand(query, conn).ExecuteReader());
                    button1.Text = "Tìm kiếm";
                    textBox1.Enabled = false;
                }
                if(MessageBox.Show("Bạn muốn dừng tìm kiếm không ?" ,"Question",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
                
            
            }
            else
            {
                textBox1.Enabled = true;
                button1.Text = "Xác Nhận";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
