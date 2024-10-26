using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19_10_2024
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void quảnLíKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( this.MdiChildren.Length == 0)
            {
                FrmKhachHang childForm = new FrmKhachHang();
                childForm.MdiParent = this;
                childForm.Dock  = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }

            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !", "Thông báo");
            }

        }

        private void thôngTinMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                FrmMatHang childForm = new FrmMatHang();
                childForm.MdiParent = this;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !", "Thông báo");
            }
        }

        private void chiTiếtBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                FrmBanHang childForm = new FrmBanHang();
                childForm.MdiParent = this;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !", "Thông báo");
            }
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void tìmKiếmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                FrmTimkiemKH childForm = new FrmTimkiemKH();
                childForm.MdiParent = this;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !", "Thông báo");
            }
        }

        private void tìmKiếmMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                FrmTimkiemMH childForm = new FrmTimkiemMH();
                childForm.MdiParent = this;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !", "Thông báo");
            }
        }

        private void tìmKiếmThôngTinBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                FrmTimKiemGDMB childForm = new FrmTimKiemGDMB();
                childForm.MdiParent = this;
                childForm.Dock = DockStyle.Fill;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Show();
            }
            else
            {
                MessageBox.Show("Đang mở 1 màn hình , Vui lòng tắt nếu muốn dùng chức năng khác !","Thông báo");
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close  (); 
        }
    }
}
