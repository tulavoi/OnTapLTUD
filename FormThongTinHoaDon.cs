using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnTapLTUD2
{
    public partial class FormThongTinHoaDon : Form
    {
        ConnectDB data = new ConnectDB();

        public FormThongTinHoaDon()
        {
            InitializeComponent();
        }

        private void FormThongTinHoaDon_Load(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = data.LoadData("usp_LayAllHoaDon");
            LayMaKHThemVaoComboBox();
        }

        private void LayMaKHThemVaoComboBox()
        {
            List<string> dsMaKH = new List<string>();
            data.OpenConnect();
            using (SqlCommand cmd = new SqlCommand("usp_LayAllMaKH", data.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string maKH = reader.GetString(0);
                        dsMaKH.Add(maKH);
                    }
                }
            }
            cboMaKH.Items.Clear();
            cboMaKH.Items.AddRange(dsMaKH.ToArray());
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDon.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvHoaDon.Rows[e.RowIndex];

                txtMaHD.Text = selectedRow.Cells[0]?.Value?.ToString().Trim() ?? string.Empty;

                dtpNgayLap.Value = (DateTime)selectedRow.Cells[1].Value;

                //if (selectedRow.Cells[1]?.Value is DateTime)
                //    dtpNgayLap.Value = (DateTime)selectedRow.Cells[1].Value;
                //else
                //    dtpNgayLap.Value = DateTime.Now;

                txtSoLuong.Text = selectedRow.Cells[2]?.Value?.ToString().Trim() ?? string.Empty;
                txtDonGia.Text = selectedRow.Cells[3]?.Value?.ToString().Trim() ?? string.Empty;
                txtThanhTien.Text = selectedRow.Cells[4]?.Value?.ToString().Trim() ?? string.Empty;
                cboMaKH.Text = selectedRow.Cells[5]?.Value?.ToString().Trim() ?? string.Empty;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!MaHDVaMaKHHopLe())
                return;
            using (SqlCommand cmd = new SqlCommand())
            {
                data.InitializeCommand("usp_ThemHoaDon", cmd);
                ThemThamSoVaoSqlCommand(cmd);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công !");
                    dgvHoaDon.DataSource = data.LoadData("usp_LayAllHoaDon");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ThemThamSoVaoSqlCommand(SqlCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@MAHD", txtMaHD.Text.Trim());
            cmd.Parameters.AddWithValue("@NGAYLAP", dtpNgayLap.Value);
            cmd.Parameters.AddWithValue("@SOLUONG", txtSoLuong.Text == "" ? "0" : txtSoLuong.Text);
            cmd.Parameters.AddWithValue("@DONGIA", txtDonGia.Text == "" ? "0" : txtSoLuong.Text);
            cmd.Parameters.AddWithValue("@THANHTIEN", txtThanhTien.Text);
            cmd.Parameters.AddWithValue("@MAKH", cboMaKH.Text);
        }

        private bool MaHDVaMaKHHopLe()
        {
            if (txtMaHD.Text == "")
            {
                errMaHD.SetError(txtMaHD, "Chưa nhập mã hóa đơn");
                return false;
            }
            else
                errMaHD.Clear();

            if (cboMaKH.Text == "")
            {
                errMaKH.SetError(cboMaKH, "Chưa nhập chọn mã khách hàng");
                return false;
            }
            else
                errMaKH.Clear();

            return true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSoLuong.Text, out int sl))
                errSoLuong.SetError(txtSoLuong, "Số lượng không hợp lệ");
            else
            {
                errSoLuong.Clear();
            }
        }


        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDonGia.Text, out int dg))
                errDonGia.SetError(txtDonGia, "Đơn giá không hợp lệ");
            else
            {
                errDonGia.Clear();
                TinhThanhTien();
            }
        }

        private void TinhThanhTien()
        {
            int sl = Convert.ToInt32(txtSoLuong.Text == "" ? "0" : txtSoLuong.Text);
            int dg = Convert.ToInt32(txtDonGia.Text == "" ? "0" : txtDonGia.Text);
            txtThanhTien.Text = Convert.ToString(sl * dg);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn để xóa !");
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xóa ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    data.InitializeCommand("usp_XoaHoaDon", cmd);
                    cmd.Parameters.AddWithValue("@MAHD", txtMaHD.Text.Trim());
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công !");
                        dgvHoaDon.DataSource = data.LoadData("usp_LayAllHoaDon");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!MaHDVaMaKHHopLe())
                return;
            using (SqlCommand cmd = new SqlCommand())
            {
                data.InitializeCommand("usp_SuaHoaDon", cmd);
                ThemThamSoVaoSqlCommand(cmd);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công !");
                    dgvHoaDon.DataSource = data.LoadData("usp_LayAllHoaDon");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
