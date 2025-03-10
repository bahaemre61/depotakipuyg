using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
            radioButton1.Checked = true;
            button5.Enabled = false;
        }
        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlDataReader dr;
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;
        private void Aliciekle_Load(object? sender, EventArgs e)
        {
            string sql = "Select DISTINCT aliciAdi from alicilarr";
            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["aliciAdi"]);
            }
            conn.Close();
            griddoldur();

        }
        void griddoldur()
        {
            da = new SqlDataAdapter("Select urunID,urunAdi,urunMiktar,urunBirim,urunBirim_Fiyati from urunler ", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "urunler");
            dataGridView1.DataSource = ds.Tables[0];
        }
        void aliciGriddoldur()
        {//"Select m.musteri_unvani,u.urun_turu,u.miktar,u.birim,u.birim_fiyati from urunler u INNER JOIN musteriler m ON m.musteri_id = u.musteri_id where musteri_unvani
         // da = new SqlDataAdapter("Select a.aliciAdi,u.urunAdi,u.urunBirim,a.aliciMiktar,a.aliciBirim_Fiyati,a.aliciTarih from alicilarr a INNER JOIN urunler u ON a.urunID = u.urunID where aliciAdi ='"+comboBox1.Text+"'", conn);

            da = new SqlDataAdapter
                ("Select a.aliciAdi,u.urunAdi,u.urunBirim,a.aliciMiktar,a.aliciBirim_Fiyati,a.aliciTarih,m.musteriAdi from(( alicilarr a INNER JOIN urunler u ON a.urunID = u.urunID) INNER JOIN musteriler m ON m.musteriID = u.musteriID) where aliciAdi ='" + comboBox1.Text + "'", conn);

            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "alicilar");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            Font font = new Font("Courier New", 12); // Use a monospaced font for consistent alignment
            float fontHeight = font.GetHeight();
            int startX = 10; // Starting X position
            int startY = 10; // Starting Y position
            int cellPadding = 10; // Padding between columns

            // Calculate the maximum width for each column
            float[] columnWidths = new float[dataGridView1.Columns.Count];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                // Measure the width of the column header
                columnWidths[i] = graphic.MeasureString(dataGridView1.Columns[i].HeaderText, font).Width;

                // Measure the width of each cell in the column to find the maximum width
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[i].Value != null)
                    {
                        float cellWidth = graphic.MeasureString(row.Cells[i].Value.ToString(), font).Width;
                        if (cellWidth > columnWidths[i])
                        {
                            columnWidths[i] = cellWidth;
                        }
                    }
                }

                // Add padding to the column width
                columnWidths[i] += cellPadding;
            }

            // Print column headers
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                graphic.DrawString(dataGridView1.Columns[i].HeaderText, font, Brushes.Black, startX, startY);
                startX += (int)columnWidths[i]; // Move to the next column position
            }

            // Move to the next line for data rows
            startY += (int)fontHeight;
            startX = 10; // Reset X position for data rows

            // Print rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (row.Cells[i].Value != null)
                    {
                        // Draw the cell content
                        graphic.DrawString(row.Cells[i].Value.ToString(), font, Brushes.Black, startX, startY);
                    }

                    // Move to the next column position
                    startX += (int)columnWidths[i];
                }

                // Move to the next line for the next row
                startY += (int)fontHeight;
                startX = 10; // Reset X position for the next row
            }
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
        public void aliciEkle(int id, string Ad, DateTime Tarih, double miktar, double birimfiyati)
        {
            string sql = "INSERT INTO alicilarr (urunID,aliciAdi, aliciTarih, aliciMiktar,aliciBirim_Fiyati)" + " VALUES (@id,@ad, @tarih,@miktar,@birimfiyat)";

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
            string query = "select * from urunler where urunID ='" + Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "'";


            SqlCommand cmdd = new SqlCommand(query, conn);
            conn.Open();
            dr = cmdd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    query = dr[2].ToString();

                }
            }
            conn.Close();
            double s1 = double.Parse(query);

            double miktar = double.Parse(textBox2.Text);
            double birim_fiyati = double.Parse(textBox4.Text);
            double x = double.Parse(label8.Text);

            x += miktar * birim_fiyati;

            label8.Text = x.ToString();

            double yenimiktar;

            yenimiktar = s1 - miktar;

            void guncelleme(int id, double a)
            {
                string sql = "Update urunler Set  urunMiktar =@miktar where urunID='" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@miktar", a);

                conn.Open();

                cmd.ExecuteNonQuery();

                conn.Close();
            }

            if (comboBox1.Items.Count != 0 && comboBox1.Enabled == true)
            {
                aliciEkle(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), comboBox1.Text, DateTime.Now, double.Parse(textBox2.Text), double.Parse(textBox4.Text));
                guncelleme(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), yenimiktar);
                MessageBox.Show("Başarılı bir şekilde kaydedildi");
                griddoldur();
            }
            else
            {
                aliciEkle(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), textBox1.Text, DateTime.Now, double.Parse(textBox2.Text), double.Parse(textBox4.Text));
                guncelleme(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), yenimiktar);
                MessageBox.Show("Başarılı bir şekilde kaydedildi");
                griddoldur();

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
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

        private void button3_Click(object sender, EventArgs e) //Müşterinin Eski Aldığı Ürünleri Görüntüler
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
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                button5.Enabled = true;
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
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            button5.Enabled = false;

        }

        private void button5_Click(object sender, EventArgs e) // Yazdır Butonu
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
    }
}
