using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19_10_2024
{
    public partial class FrmBanHang : Form
    {
        Connect connect = new Connect();
        SqlConnection conn = Connect.createConnect("Data Source=DESKTOP-P137F4R;Initial Catalog=QuanLyBanHang;Integrated Security=True;");

        private Boolean checkIsntEmpty()
        {
            return !txtDate.Text.Equals("") && !txtDate.Text.Trim().Equals("") &&
                    !txtDonGia.Text.Equals("") && !txtDonGia.Text.Trim().Equals("") &&
                    !txtMKH.Text.Equals("") && !txtMKH.Text.Trim().Equals("") &&
                    !txtSL.Text.Equals("") && !txtSL.Text.Trim().Equals("")&&
                    !txtSLHD.Text.Equals("") && !txtSLHD.Text.Trim().Equals("") ;
        }

        public void fill_to_gridview()
        {
            dataGridView1.Rows.Clear();
            try
            {
                SqlDataReader da = connect.getQuery("SELECT t.SoHieuHD, t.NgayMuaBan, t.MaKH,chitiet.MaMH , chitiet.DonGia, chitiet.SoLuong FROM tblBanHang t JOIN tblChiTietBanHang chitiet ON t.SoHieuHD = chitiet.SoHieuHD", conn);
                if (da.HasRows)
                {
                    int index = 1; 
                    while (da.Read())
                    {
                        Object[] record = new object[8];
                        record[0] = index++;
                        record[1] = da.GetValue(0);
                        record[2] = da.GetValue(1);
                        record[3] = da.GetValue(2);
                        record[4] = da.GetValue(3);
                        record[5] = da.GetValue(4);
                        record[6] = da.GetValue(5);
                        record[7] = ConvertFloatToFormattedInt(float.Parse(record[6].ToString())* float.Parse(record[5].ToString())+"");
                        dataGridView1.Rows.Add(record);
                    }
                    da.Close();
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



        public FrmBanHang()
        {
            InitializeComponent();
            conn.Open();
            fill_to_gridview();
            __Enabled();
        }


        public void clearContent()
        {
            txtDate.Text = "";
            txtDonGia.Text = "";
            txtMKH.Text = "";
            txtMMH.Text = "";
            txtSL.Text = "";
            txtSLHD.Text = "";
        }


        Boolean flag = false;


        private void _Enabled()
        {
            txtDate.Enabled = true;
            txtDonGia.Enabled = true;
            txtMKH.Enabled = true;
            txtMMH.Enabled = true;
            txtSL.Enabled = true;
            txtSLHD.Enabled = true;
        }

        public void enable_all()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        public void unenable()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void __Enabled()
        {
            txtDate.Enabled = false;
            txtDonGia.Enabled = false;
            txtMKH.Enabled = false;
            txtMMH.Enabled = false;
            txtSL.Enabled = false;
            txtSLHD.Enabled = false;
        }

        public void fill_to_gridview(SqlDataReader da)
        {
            dataGridView1.Rows.Clear();
            try
            {
                if (da.HasRows)
                {
                    int index = 1;
                    while (da.Read())
                    {
                        Object[] record = new object[8];
                        record[0] = index++;
                        record[1] = da.GetValue(0);
                        record[2] = da.GetValue(1);
                        record[3] = da.GetValue(2);
                        record[4] = da.GetValue(3);
                        record[5] = da.GetValue(4);
                        record[6] = da.GetValue(5);
                        record[7] = ConvertFloatToFormattedInt(float.Parse(record[6].ToString()) * float.Parse(record[5].ToString()) + "");
                        dataGridView1.Rows.Add(record);
                    }
                    da.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!txtSLHD.Enabled)
            {
                clearContent();
                _Enabled();
                button1.Text = "Xác nhận ";
                txtMKH.Enabled = false;
                txtDate.Enabled = false;
                unenable();
                button1.Enabled = true;

            }
            else
            {

                //-----------------//
                if (!txtDonGia.Text.Equals("") && !txtDonGia.Text.Trim().Equals("") &&
                    !txtMMH.Text.Equals("") && !txtMMH.Text.Trim().Equals("") &&
                    !txtSL.Text.Equals("") && !txtSL.Text.Trim().Equals("")
                )
                {
                    if (_check())
                    {
                        
                        String query = "insert into tblChiTietBanHang values (" + txtSLHD.Text + " , " + txtMMH.Text + " , " + txtSL.Text + " , " + txtDonGia.Text + " )";
                    connect.setDb(query, conn);
                    fill_to_gridview();
                    __Enabled();
                    MessageBox.Show("Thành công");
                    button1.Text = "Thêm";
                    enable_all();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ (mã mặt hàng hoặc số hiệu hóa đơn không tồn tại !)");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Chưa đủ thông tin ");
                }
          }
        }

        public static string ConvertFloatToFormattedInt(String  number)
        {
            float val = float.Parse(number);
            int intNumber = (int)val;

       
            return intNumber.ToString("N0");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtSLHD.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtDate.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtMKH.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtMMH.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtDonGia.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtSL.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
        }


        private Boolean _check()
        {   try
            { 
                string query = "select * from tblBanHang where SoHieuHD = " + txtSLHD.Text + "";
                string query2 = "select * from tblMatHang where MaMH = " + txtMMH.Text + " ";
                var o = connect.getQuery(query, conn);
                if (!o.HasRows)
                {
                    o.Close();
                    return false;
                }
                o.Close();
                var o2 = connect.getQuery(query2, conn);
                if (!o2.HasRows)
                {
                    o2.Close();
                    return false;
                }
                
                o2.Close ();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("EX : " + ex.Message);
                return false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkIsntEmpty())
            {
                MessageBox.Show("Chọn bản ghi cần sửa ");
            }
            else
            {
                if (!flag)
                {
                    _Enabled();
                    button2.Text = "Xác nhận";
                    txtMKH.Enabled = false;
                    txtDate.Enabled = false;
                    txtSLHD.Enabled = false;
                    txtMMH.Enabled = false;
                    flag = true;
                    unenable();
                    button2.Enabled = true;
                }
                else
                {
                    if (!txtDonGia.Text.Equals("") && !txtDonGia.Text.Trim().Equals("") &&
                    !txtSL.Text.Equals("") && !txtSL.Text.Trim().Equals(""))
                    {
                        String query = "UPDATE tblChiTietBanHang SET SoLuong = " + txtSL.Text + ", DonGia = " + ((txtDonGia.Text).IndexOf(',') != -1 ? txtDonGia.Text.Substring(0, txtDonGia.Text.IndexOf(',')) : txtDonGia.Text) + " WHERE SoHieuHD = " + txtSLHD.Text + " AND MaMH = " + txtMMH.Text + "; ";
                        
                        connect.setDb(query, conn);
                        fill_to_gridview();
                        __Enabled();
                        MessageBox.Show("Thành công");
                        button2.Text = "Sửa";
                        fill_to_gridview();
                        flag = false;
                        enable_all();
                        clearContent();
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập đủ thông tin ");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!checkIsntEmpty())
            {
                MessageBox.Show("Chọn bản ghi cần xóa ");
            }
            else
            {
                string query = "DELETE FROM tblChiTietBanHang WHERE SoHieuHD = " + txtSLHD.Text + " AND MaMH = " + txtMMH.Text + "; ";
                connect.setDb(query, conn);
                MessageBox.Show("Thành công");
                clearContent();
                fill_to_gridview();
               
            }
        }
    }
}
