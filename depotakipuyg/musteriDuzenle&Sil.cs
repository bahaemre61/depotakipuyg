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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace depotakipuyg
{
    public partial class musteriDuzenle_Sil : Form
    {
        public musteriDuzenle_Sil()
        {
            InitializeComponent();
            this.Load += MusteriDuzenle_Sil_Load;
        }
        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;

        void griddoldur()
        {
            da = new SqlDataAdapter("Select * from musteriler", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "musteriler");
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void musteriDuzenle(string musteriAdi, double tutar, DateTime tarih)
        {

            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string sql = "Update musteriler Set musteriAdi =@adi , musteriTutar =@tutar, musteriTarih =@tarih where musteriID='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@adi", musteriAdi);

            cmd.Parameters.AddWithValue("@tutar", tutar);

            cmd.Parameters.AddWithValue("@tarih", tarih);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void musteriSil(int id)
        {
            string sql = "Delete from musteriler where  musteriID='" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void MusteriDuzenle_Sil_Load(object? sender, EventArgs e)
        {
            griddoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e) //Kaydetme Butonu
        {
            musteriDuzenle(textBox1.Text, Double.Parse(textBox2.Text), DateTime.Parse(dateTimePicker1.Value.ToString()));
            MessageBox.Show("Müşteri bilgileri güncellendi");
            griddoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                musteriSil(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                MessageBox.Show("Kullanıcı başarılı bir şekilde silindi.");
                griddoldur();
            }
            catch(Exception)
            {
                MessageBox.Show("Kullanıcı silinemedi.");
            }
        }
    }
}
