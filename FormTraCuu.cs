using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnTapLTUD2
{
    public partial class FormTraCuu : Form
    {
        ConnectDB data = new ConnectDB();

        public FormTraCuu()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Close();
        }

        private void btnTimTiep_Click(object sender, EventArgs e)
        {
            txtThongTin.Clear();
            txtThongTin.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtThongTin.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm !");
                return;
            }
            if (!radMaHD.Checked && !radMaKH.Checked)
            {
                MessageBox.Show("Bạn chưa chọn loại tìm kiếm !");
                return;
            }
            if (radMaHD.Checked)
                dgvTraCuu.DataSource = data.LoadDataByID("usp_TimBangMaHD", "@MAHD", txtThongTin.Text);

            if (radMaKH.Checked)
                dgvTraCuu.DataSource = data.LoadDataByID("usp_TimBangMaKH", "@MAKH", txtThongTin.Text);

        }
    }
}
