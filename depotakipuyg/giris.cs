namespace depotakipuyg
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new urunekle().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new urunduzenle().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new uruncikar().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new aliciekle().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new test().Show();
        }
    }
}