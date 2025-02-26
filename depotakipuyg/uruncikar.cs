using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace depotakipuyg
{
    public partial class uruncikar : Form
    {
        public uruncikar()
        {
            InitializeComponent();
            this.Load += Uruncikar_Load;
        }

        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;

        private void Uruncikar_Load(object? sender, EventArgs e)
        {
            string sql = "Select * from musteriler";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["musteriAdi"]);
            }
            conn.Close();
        }
        void griddoldur(string musteri_unvani)
        {
            da = new SqlDataAdapter("Select u.urunID,m.musteriID, m.musteriAdi,u.urunAdi,u.urunMiktar,u.urunBirim,u.urunBirim_Fiyati from urunler u INNER JOIN musteriler m ON m.musteriID = u.musteriID where musteriAdi = '" + musteri_unvani + "'", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            griddoldur(comboBox1.Text);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //MessageBox.Show("Deponuzdaki kalan ürün mikttarını sisteme kaydediniz.");
        }
        public void urunCikarma(string urunturu, int miktar, string birim, int birim_fiyati)
        {
            string urunid = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string sql = "Update urunler Set urunAdi =@urun_turu,urunMiktar =@miktar,urunBirim=@birim,urunBirim_Fiyati=@birim_fiyati where id='" + urunid + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@urun_turu", urunturu);

            cmd.Parameters.AddWithValue("@miktar", miktar);

            cmd.Parameters.AddWithValue("@birim", birim);

            cmd.Parameters.AddWithValue("@birim_fiyati", birim_fiyati);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e) //Kaydetme Butonu
        {
            if (comboBox1.Text != "")
            { 
                urunCikarma(textBox1.Text, Int32.Parse(textBox2.Text), textBox3.Text, Int32.Parse(textBox4.Text));
                griddoldur(comboBox1.Text);
            }
            else
                MessageBox.Show("Müşteri Ünvanı seçmeden değiştirme yapamazsınız.");        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new giris().Show();
        }
    }
}
