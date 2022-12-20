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
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_hoadon()
        {
            InitializeComponent();
        }

        private void fr_hoadon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet4.HoaDon' table. You can move, or remove it, as needed.
            this.hoaDonTableAdapter.Fill(this._DoAn_NetDataSet4.HoaDon);
            cl_hoadon();
            // TODO: This line of code loads data into the '_DoAn_NetDataSet6.HoaDon' table. You can move, or remove it, as needed.

        }
        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM HoaDon WHERE MaKhachHang LIKE N'%" + txt_timkiem.Text + "%'", con);
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
            fr_taohoadon taohoadon = new fr_taohoadon();
            taohoadon.ShowDialog();
        }

        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_hoadon();
        }

        private void gv_hoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rows_id = e.RowIndex;
            txt_id.Text = gv_hoadon.Rows[rows_id].Cells[0].Value.ToString().Trim();
            lb_id.Visible = true;
        }
        public void cl_hoadon()
        {
            lb_id.Visible = false;
            txt_id.Text = "";
            txt_timkiem.Text = "";
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM HoaDon", con);
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
        private void bt_xoa_Click(object sender, EventArgs e)
        {
            if (txt_id.Text != "")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE HoaDon WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", txt_id.Text);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 1)
                    {
                        MessageBox.Show("Xóa thành công");
                    }
                    lb_id.Visible = false;
                    txt_id.Text = "";
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui chọn id cần xóa!");
            }
        }

        private void bt_xuat_Click(object sender, EventArgs e)
        {
            fr_xuathoadon xuathoadon = new fr_xuathoadon();
            xuathoadon.ShowDialog();
        }
    }
}
