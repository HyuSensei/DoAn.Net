using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Do_An_PhanTienHuy_NguyenHuuToan
{
    public partial class XuatHoaDon : Form
    {
        public XuatHoaDon()
        {
            InitializeComponent();
        }

        private void XuatHoaDon_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
