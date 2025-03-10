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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace depotakipuyg
{
    public partial class urunduzenle : Form
    {
        public urunduzenle()
        {
            InitializeComponent();
            this.Load += Urunduzenle_Load;
        }

        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;

        private void Urunduzenle_Load(object? sender, EventArgs e)
        {
            string sql = "Select * from musteriler";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr["musteriAdi"]);
                comboBox2.Items.Add(dr["musteriAdi"]);
            }
            conn.Close();
            
        }
        void griddoldur(string musteri_unvani)
        {
            da = new SqlDataAdapter("Select m.musteriAdi,u.urunAdi,u.urunMiktar,u.urunBirim,u.urunBirim_Fiyati, m.musteriTarih from urunler u INNER JOIN musteriler m ON m.musteriID = u.musteriID where musteriAdi = '" + musteri_unvani+ "'", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void urunEkle(string tur, double miktar, string birim , double birim_fiyati)
        {
            string query = "select musteriID from musteriler where musteriAdi ='" + comboBox2.Text + "'";

            SqlCommand cmdd = new SqlCommand(query, conn);
            conn.Open();
            dr = cmdd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    query = dr[0].ToString();
                }              
            }
            conn.Close();
            int musteriID = Int32.Parse(query);

            string sql = "INSERT INTO urunler (musteriID, urunAdi, urunMiktar,urunBirim,urunBirim_Fiyati)" + " VALUES ('"+musteriID+"',@urun_turu,@miktar,@birim,@birim_fiyati)";

            SqlCommand cmd = new SqlCommand(sql, conn);


            cmd.Parameters.AddWithValue("@urun_turu", tur);

            cmd.Parameters.AddWithValue("@miktar", miktar);

            cmd.Parameters.AddWithValue("@birim", birim);

            cmd.Parameters.AddWithValue("@birim_fiyati", birim_fiyati);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                urunEkle(textBox1.Text, Double.Parse(textBox2.Text), textBox3.Text, Double.Parse(textBox4.Text));
                griddoldur(comboBox1.Text);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            else
            {
                MessageBox.Show("Müşteri seçilmeden ürün giremezsiniz.");
            }
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            griddoldur(comboBox1.Text);
            label7.Text = "";
            label9.Text = "";
            label11.Text = "";
            {
                string query = "select musteriTutar from musteriler where musteriAdi ='" + comboBox1.Text + "'";

                SqlCommand cmmdd = new SqlCommand(query, conn);
                conn.Open();
                dr = cmmdd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        query = dr[0].ToString();                       
                    }
                }
                conn.Close();
                double musteriTutar = Double.Parse(query);               

                label7.Text = musteriTutar.ToString();
            }

            //////////////////////////////////////////////////////
            

            string tutar = "select musteriAdi,musteriTutar from musteriler where musteriAdi ='" + comboBox1.Text + "'";
            string musterid = "select musteriID,musteriAdi,musteriTutar from musteriler where musteriAdi ='" + comboBox1.Text + "'";
            SqlCommand cmdd = new SqlCommand(musterid, conn);
            conn.Open();
            dr = cmdd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    musterid = dr[0].ToString();
                    tutar = dr[2].ToString();
                }
            }
            conn.Close();
            double urunTutari = Double.Parse(tutar);
            int MusteriID = Int32.Parse(musterid);
            string sql = "select musteriID,SUM(urunMiktar * urunBirim_Fiyati) AS ToplamFiyat From urunler where musteriID ='" + MusteriID+"' Group BY musteriID";          
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    sql = dr["ToplamFiyat"].ToString();                
                }
            }
            conn.Close();
            try
            {
                double girilmisurunfiyati = Double.Parse(sql);
                label9.Text = girilmisurunfiyati.ToString();
                label11.Text = (urunTutari - girilmisurunfiyati).ToString();
            }catch(Exception) {
                
               MessageBox.Show(comboBox1.Text+" Ünvanlının ürünleri düzenlenmediği için ilk önce ünvanlının ürünlerini ekleyin.");
                
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new giris().Show();
        }
    }
}
