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
    public partial class fr_taohoadon : Form
    {
        string connect = "server=" + @"DESKTOP-1VK71I1\SQLEXPRESS" + ";database=" + "DoAn.Net" + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        public fr_taohoadon()
        {
            InitializeComponent();
        }

        private void fr_taohoadon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_DoAn_NetDataSet5.CTHoaDon' table. You can move, or remove it, as needed.
            get_tensp();
            get_tenkhachhang();
        }
        public void get_tensp()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn tên sản phẩm";
                dt.Rows.InsertAt(row, 0);
                cb_tensanpham.DataSource = dt;
                cb_tensanpham.DisplayMember = "TenSanPham";
                cb_tensanpham.ValueMember = "id";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void get_tenkhachhang()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KhachHang", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[0] = 0;
                row[1] = "Chọn tên KH";
                dt.Rows.InsertAt(row, 0);
                cb_tenkhachhang.DataSource = dt;
                cb_tenkhachhang.DisplayMember = "TenKhachHang";
                cb_tenkhachhang.ValueMember = "MaKhachHang";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void dis_cthoadon()
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CTHoaDon", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gv_cthoadon.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void cl_cthoadon()
        {
            txt_mahoadon.Text = "";
            txt_masanpham.Text = "";
            cb_tensanpham.SelectedIndex = -1;
            cb_tenkhachhang.SelectedIndex = -1;
            txt_makhachhang.Text = "";
            txt_soluong.Text = "";
            txt_ngaylap.Text = "";
            txt_tongtien.Text = "";
        }
        private void bt_lammoi_Click(object sender, EventArgs e)
        {
            cl_cthoadon();
        }

        private void gv_cthoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rows_id = e.RowIndex;
            txt_mahoadon.Text = gv_cthoadon.Rows[rows_id].Cells[0].Value.ToString().Trim();
            txt_masanpham.Text = gv_cthoadon.Rows[rows_id].Cells[1].Value.ToString().Trim();
            cb_tensanpham.Text = gv_cthoadon.Rows[rows_id].Cells[2].Value.ToString().Trim();
            txt_soluong.Text = gv_cthoadon.Rows[rows_id].Cells[3].Value.ToString().Trim();
            txt_tongtien.Text = gv_cthoadon.Rows[rows_id].Cells[4].Value.ToString().Trim();
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            if (txt_mahoadon.Text != "" && txt_masanpham.Text != "" && cb_tensanpham.Text != "" && txt_soluong.Text !="" && txt_tongtien.Text!="")
            {
                try
                {
                    con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CTHoaDon(MaHoaDon,Id_SanPham,TenSanPham,SoLuong,TongTien) VALUES(@mahoadon,@masanpham,@tensanpham,@soluong,@tongtien)", con);
                    cmd.Parameters.AddWithValue("@mahoadon", txt_mahoadon.Text);
                    cmd.Parameters.AddWithValue("@masanpham", txt_masanpham.Text);
                    cmd.Parameters.AddWithValue("@tensanpham", cb_tensanpham.Text);
                    cmd.Parameters.AddWithValue("@soluong", txt_soluong.Text);
                    cmd.Parameters.AddWithValue("@tongtien", txt_tongtien.Text);
                    int row = cmd.ExecuteNonQuery();
                    {
                        if (row == 1)
                        {
                            MessageBox.Show("Thêm chi tiết hóa đơn thành công");
                        }
                        else
                        {
                            MessageBox.Show("Thêm chi tiết hóa đơn thất bại");
                        }
                    }
                    dis_cthoadon();
                    cl_cthoadon();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên!");
            }
        }

        private void cb_tenkhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KhachHang WHERE TenKhachHang=N'" + cb_tenkhachhang.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_makhachhang.Text = dt.Rows[0]["MaKhachHang"].ToString();
                }
                if (cb_tenkhachhang.Text == "Chọn nhân viên")
                {
                    txt_makhachhang.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cb_tensanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connect);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham WHERE TenSanPham=N'" + cb_tensanpham.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_masanpham.Text = dt.Rows[0]["id"].ToString();
                }
                if (cb_tenkhachhang.Text == "Chọn tên sản phẩm")
                {
                    txt_masanpham.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
