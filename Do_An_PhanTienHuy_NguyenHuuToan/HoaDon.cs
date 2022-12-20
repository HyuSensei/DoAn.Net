using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Do_An_PhanTienHuy_NguyenHuuToan
{
    public partial class fr_hoadon : Form
    {
        public fr_hoadon()
        {
            InitializeComponent();
        }

        private void fr_hoadon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet4.HoaDon' table. You can move, or remove it, as needed.
            this.hoaDonTableAdapter.Fill(this._DoAn_NetDataSet4.HoaDon);

        }

        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
                SqlConnection con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM HoaDon WHERE MaHoaDon LIKE N'%" + txt_timkiem.Text + "%'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_hoadon.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_taohoadon_Click(object sender, EventArgs e)
        {

        }
    }
}
