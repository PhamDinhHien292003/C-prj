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
    public partial class FrmKhachHang : Form
    {
        Connect connect = new Connect();
        SqlConnection conn = Connect.createConnect("Data Source=DESKTOP-P137F4R;Initial Catalog=QuanLyBanHang;Integrated Security=True;");

        private Boolean checkIsntEmpty()
        {
            return !txtTen.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtGT.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtDiachi.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtDt.Text.Equals("") && !txtTen.Text.Trim().Equals("");
        }

        public void fill_to_gridview()
        {
            dataGridView1.Rows.Clear();
            try
            {
                SqlDataReader da = connect.getQuery("Select * from tblKhachHang", conn);
                if (da.HasRows)
                {
                    while (da.Read())
                    {
                        Object[] record = new object[5];
                        record[0] = da.GetValue(0);
                        record[1] = da.GetValue(1);
                        record[2] = da.GetValue(2);
                        record[3] = da.GetValue(3);
                        record[4] = da.GetValue(4);
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



        public FrmKhachHang()
        {
            InitializeComponent();
            conn.Open();
            fill_to_gridview();

        }


        public void clearContent()
        {
            txtMa.Text = "";
            txtGT.Text = "";
            txtDiachi.Text = "";
            txtDt.Text = "";
            txtTen.Text = "";
        }


        Boolean flag = false;


        private void _Enabled()
        {
            txtTen.Enabled = true;
            txtGT.Enabled = true;
            txtDiachi.Enabled = true;
            txtDt.Enabled = true;

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
            txtTen.Enabled = false;
            txtGT.Enabled = false;
            txtDiachi.Enabled = false;
            txtDt.Enabled = false;
        }

        public void fill_to_gridview(SqlDataReader da)
        {
            try
            {
                if (da.HasRows)
                {
                    dataGridView1.Rows.Clear();
                    while (da.Read())
                    {
                        Object[] record = new object[5];
                        record[0] = da.GetValue(0);
                        record[1] = da.GetValue(1);
                        record[2] = da.GetValue(2);
                        record[3] = da.GetValue(3);
                        record[4] = da.GetValue(4);
                        dataGridView1.Rows.Add(record);
                    }
                    da.Close();
                }
                else
                {
                    MessageBox.Show("Không có bản ghi phù hợp!!");
                    da.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Phải nhập số");
            }

        }





        private void button4_Click(object sender, EventArgs e)
        {
            if (!txtMa.Enabled)
            {
                txtMa.Enabled = true;
                unenable();
                button4.Enabled = true;
            }
            else
            {
                if (txtMa.Text.Equals("") || txtMa.Text.Trim().Equals(""))
                {
                    fill_to_gridview();
                    MessageBox.Show("JE::");
                    txtMa.Enabled = false;
                    enable_all();
                    return;
                }
                else
                {
                    if (int.TryParse(txtMa.Text, out int val))
                    {
                        String query = "select * from tblKhachHang where MaKH  =" + txtMa.Text + "";
                        fill_to_gridview(new SqlCommand(query, conn).ExecuteReader());
                        enable_all();
                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng phải là số ");
                    }
                }
                clearContent();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Close();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtMa.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtTen.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtGT.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtDiachi.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtDt.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (!txtTen.Enabled)
            {
                clearContent();
                _Enabled();
                button1.Text = "Xác nhận ";
                unenable();
                button1.Enabled = true;

            }
            else
            {

                //-----------------//
                if (!txtTen.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtGT.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtDiachi.Text.Equals("") && !txtTen.Text.Trim().Equals("") &&
                    !txtDt.Text.Equals("") && !txtTen.Text.Trim().Equals("")
                )
                {
                    String query = "insert into tblKhachHang values (N'" + txtTen.Text + "' , N'" + txtGT.Text + "' , N'" + txtDiachi.Text + "' , N'" + txtDt.Text + "' )";
                    connect.setDb(query, conn);
                    fill_to_gridview();
                    __Enabled();
                    MessageBox.Show("Thành công");
                    button1.Text = "Thêm";
                    enable_all();
                }
                else
                {
                    MessageBox.Show("Chưa đủ thông tin ");
                }
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
                    flag = true;
                    unenable();
                    button2.Enabled = true;
                }
                else
                {
                    if (checkIsntEmpty())
                    {
                        String query = "update tblKhachHang set HoTen = N'" + txtTen.Text + "' , GioiTinh = N'" + txtGT.Text + "' , DiaChi = N'" + txtDiachi.Text + "' , DienThoai = N'" + txtDt.Text + "'  where MaKH = '" + txtMa.Text + "'";
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


                string query = "delete from tblKhachHang where MaKH = '" + txtMa.Text + "'";
                connect.setDb(query, conn);
                MessageBox.Show("Thành công");
                clearContent();
                fill_to_gridview();
                txtMa.Enabled = false;
            }
        }
    }
}
