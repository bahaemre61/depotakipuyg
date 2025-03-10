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

namespace depotakipuyg
{
    public partial class urunekle : Form
    {
        public urunekle()
        {
            InitializeComponent();
            Load += Urunekle_Load;
        }

        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        private void Urunekle_Load(object? sender, EventArgs e)
        {
            string sql = "Select DISTINCT musteriAdi from musteriler";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["musteriAdi"]);
            }
            conn.Close();
        }

        public void musteriEkle(string Ad, double Tutar, DateTime Tarih)
        {
            string sql = "INSERT INTO musteriler (musteriAdi, musteriTutar, musteriTarih)" + " VALUES (@ad, @tutar,@tarih)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ad", Ad);

            cmd.Parameters.AddWithValue("@tutar", Tutar);

            cmd.Parameters.AddWithValue("@tarih", Tarih);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e) // Ekleme Butonu
        {
            if (comboBox1.Items.Count != 0 && comboBox1.Enabled == true && comboBox1.SelectedItem != null)
            {
                musteriEkle(comboBox1.Text, double.Parse(textBox2.Text), dateTimePicker1.Value);
                MessageBox.Show("Müşteri başarılı bir şekilde eklendi");

            }
            else if (textBox1.Text != "" && textBox2.Text != "")
            {
                musteriEkle(textBox1.Text, double.Parse(textBox2.Text), dateTimePicker1.Value);
                MessageBox.Show("Müşteri başarılı bir şekilde eklendi");
            }
            else
            {
                MessageBox.Show("Müşterinin ismini ve tutarını boş girmediğinizden ve doğru girdiğinizden emin olun");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new giris().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            new musteriDuzenle_Sil().Show();
        }
    }
}
