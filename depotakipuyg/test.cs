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
using System.Drawing.Printing;

namespace depotakipuyg
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            this.Load += Test_Load;
        }
        SqlConnection conn = new SqlConnection(database.GetConnectionString);
        SqlCommandBuilder cmdb;
        DataSet ds;
        SqlDataAdapter da;

        private void Test_Load(object? sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            da = new SqlDataAdapter("Select aliciAdi,aliciTarih,aliciMiktar,aliciBirim_Fiyati from alicilarr ", conn);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "alicilarr");
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
