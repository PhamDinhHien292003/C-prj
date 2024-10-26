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
    public partial class FrmMatHang : Form
    {

        Connect connect = new Connect();
        SqlConnection conn = Connect.createConnect("Data Source=DESKTOP-P137F4R;Initial Catalog=QuanLyBanHang;Integrated Security=True;");
        public FrmMatHang()
        {
            InitializeComponent();
            conn.Open();
            fill_to_gridview();
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
            button1.Enabled=false;
            button2.Enabled=false;
            button3.Enabled=false;
            button4.Enabled=false;
            button5.Enabled=false;

        }



        public void fill_to_gridview(SqlDataReader da)
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

        public void clearContent()
        {
            txtDVT.Text = "";
            txtMatHang.Text = "";
            txtTenMH.Text = "";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (!txtMatHang.Enabled)
            {
                unenable();
                txtMatHang.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                if (txtMatHang.Text.Equals("") || txtMatHang.Text.Trim().Equals(""))
                {
                    fill_to_gridview();
                    txtMatHang.Enabled = false;
                    enable_all();
 
                }
                else
                {
                    if (int.TryParse(txtMatHang.Text, out int val))
                    {
                        String query = "select * from tblMatHang where MaMH  = " + txtMatHang.Text + "";
                        fill_to_gridview(new SqlCommand(query, conn).ExecuteReader());
                        txtMatHang.Enabled = false;
                        enable_all();
                    }
                    else
                    {
          
                            MessageBox.Show("Mặt hàng phải nhập số nguyên ", "Thông báo");
              
                    }

                }
                clearContent();
            }
        }

        private void _Enabled()
        {
            txtTenMH.Enabled = true;
            txtMatHang.Enabled = true;
            txtDVT.Enabled = true;
           

        }

        private void __Enabled()
        {
            txtTenMH.Enabled = false;
            txtMatHang.Enabled = false;
            txtDVT.Enabled = false;
        }

        private Boolean checkIsntEmpty()
        {
            return !txtTenMH.Text.Equals("") && !txtMatHang.Text.Trim().Equals("") &&
                    !txtDVT.Text.Equals("") ;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!txtTenMH.Enabled)
            {
                clearContent();
                unenable() ;
                _Enabled();
                txtMatHang.Enabled = false;
                button1.Text = "Xác nhận ";
                button1.Enabled = true;

            }
            else
            {
                if (!txtTenMH.Text.Equals("") &&
                    !txtDVT.Text.Equals("")){
                    String query = "insert into tblMatHang values (N'" + txtTenMH.Text + "' , N'" + txtDVT.Text + "' )";
                    connect.setDb(query, conn);
                    fill_to_gridview();
                    __Enabled();
                    MessageBox.Show("Thành công");
                    button1.Text = "Thêm";
                    enable_all();
                    clearContent();
                }
                else
                {
                    MessageBox.Show(txtTenMH.Text + " " + txtDVT.Text);

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtMatHang.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtTenMH.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtDVT.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkIsntEmpty())
            {
                MessageBox.Show("Chọn bản ghi cần xóa ");
            }
            else
            {
                string query = "delete from tblMatHang where MaMH = '" + txtMatHang.Text + "'";
                connect.setDb(query, conn);
                MessageBox.Show("Thành công");
                clearContent();
                fill_to_gridview();
                txtMatHang.Enabled = false;
            }
        }
        Boolean flag = false;
        private void button3_Click(object sender, EventArgs e)
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
                    txtMatHang.Enabled = false;
                    button3.Text = "Xác nhận";
                    unenable();
                    button3.Enabled = true;
                    flag = true;
                }
                else
                {
                    if (checkIsntEmpty())
                    {
                        String query = "update tblMatHang set TenMatHang = N'" + txtTenMH.Text + "',DVT = N'"+txtDVT.Text + "' where MaMH = '"+txtMatHang.Text+"'";
                        connect.setDb(query, conn);
                        fill_to_gridview();
                        __Enabled();
                        MessageBox.Show("Thành công");
                        button3.Text = "Sửa";
                        enable_all();
                        fill_to_gridview();
                        flag = false;
                        clearContent();
                    }
                    else
                    {
                        MessageBox.Show("Chưa nhập đủ thông tin ");
                    }
                }
            }
        }
    }
}
