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
    public partial class aliciekle : Form
    {
        public aliciekle()
        {
            InitializeComponent();
            this.Load += Aliciekle_Load;
        }
        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;
        private void Aliciekle_Load(object? sender, EventArgs e)
        {
            string sql = "Select DISTINCT alici_ad from alicilar";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["alici_ad"]);
            }
            conn.Close();
            griddoldur();
           
        }
        void griddoldur()
        {
            da = new SqlDataAdapter("Select id,urun_turu,miktar,birim,birim_fiyati from urunler ", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables[0];
        }
        void aliciGriddoldur()
        {//"Select m.musteri_unvani,u.urun_turu,u.miktar,u.birim,u.birim_fiyati from urunler u INNER JOIN musteriler m ON m.musteri_id = u.musteri_id where musteri_unvani
            da = new SqlDataAdapter("Select a.alici_ad,u.urun_turu,u.birim,a.miktar,a.birim_fiyati,a.alici_tarih from alicilar a INNER JOIN urunler u ON a.id = u.id where alici_ad ='"+comboBox1.Text+"'", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "alicilar");
            dataGridView1.DataSource = ds.Tables[0];
        }
      
       

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        public void aliciEkle(int id ,string Ad,  DateTime Tarih, int miktar ,int birimfiyati)
        {
            string sql = "INSERT INTO alicilar (id,alici_ad, alici_tarih, miktar,birim_fiyati)" + " VALUES (@id,@ad, @tarih,@miktar,@birimfiyat)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Parameters.AddWithValue("@ad", Ad);

            cmd.Parameters.AddWithValue("@tarih", Tarih);

            cmd.Parameters.AddWithValue("@miktar", miktar);

            cmd.Parameters.AddWithValue("@birimfiyat", birimfiyati);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)            
        {
            string query = "select * from urunler where id ='" + Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'";
            

            SqlCommand cmdd = new SqlCommand(query, conn);
            conn.Open();
            dr = cmdd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    query = dr[3].ToString();

                }
            }
            conn.Close();
            int s1 = Int32.Parse(query);

            int miktar = Int32.Parse(textBox2.Text);
            int birim_fiyati = Int32.Parse(textBox4.Text);
            int x = Int32.Parse(label8.Text);

            x += miktar * birim_fiyati;

            label8.Text = x.ToString();

            int yenimiktar;

            yenimiktar = s1 - miktar;
      
            void guncelleme(int id,int a )
            {
                string sql = "Update urunler Set  miktar =@miktar where id='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                                
                cmd.Parameters.AddWithValue("@miktar", a);

                conn.Open();

                cmd.ExecuteNonQuery();

                conn.Close();
            }

            if (comboBox1.Items.Count != 0 && comboBox1.Enabled == true)
            {
                aliciEkle(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), comboBox1.Text, DateTime.Now, Int32.Parse(textBox2.Text), Int32.Parse(textBox4.Text));
                guncelleme(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), yenimiktar);
                MessageBox.Show("Başarılı bir şekilde kaydedildi");
                griddoldur();
            }
            else
            {
                aliciEkle(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), textBox1.Text, DateTime.Now, Int32.Parse(textBox2.Text), Int32.Parse(textBox4.Text));
                guncelleme(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), yenimiktar);
                MessageBox.Show("Başarılı bir şekilde kaydedildi");
                griddoldur();

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                textBox1.Enabled = false;
                comboBox1.Enabled = true;
                textBox1.Clear();
            }
            else
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                comboBox1.ResetText();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                comboBox1.ResetText();
            }
            else
            {
                textBox1.Enabled = false;
                comboBox1.Enabled = true;
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new giris().Show();
        }

        private void button3_Click(object sender, EventArgs e) //Müşterinin Eski Aldığı Ürüünleri Görüntüler
        {
            if (comboBox1.Text != "")
            {
                aliciGriddoldur();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Eski Müşterinizi Seçmeden işlem yapamazsınız.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            griddoldur();
            button1.Enabled = true;
           
        }
    }
}
