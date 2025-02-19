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

        private void Urunekle_Load(object? sender, EventArgs e)
        {
            
        }

        public void musteriEkle(string Ad, int Tutar, DateTime Tarih)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                musteriEkle(textBox1.Text, Int32.Parse(textBox2.Text), dateTimePicker1.Value);
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
    }
}
