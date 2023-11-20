using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnTapLTUD2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Xác nhận thoát ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Close();
        }

        private void nhậpThôngTinHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FormDangMo(typeof(FormThongTinHoaDon)))
            {
                FormThongTinHoaDon frm = new FormThongTinHoaDon();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FormDangMo(typeof(FormTraCuu)))
            {
                FormTraCuu frm = new FormTraCuu();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private bool FormDangMo(Type type)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == type)
                    return true;
            }
            return false;
        }
    }
}
