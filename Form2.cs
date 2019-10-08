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

namespace BankaUygulamasi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Banka;uid=sa;pwd=1234");
       // SqlConnection baglanti = new SqlConnection("Server=AWESOME\\SQLEXPRESS;Database=Banka;Trusted_Connection=True;");
        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void Listele(string ulas)
        {
            SqlDataAdapter goster = new SqlDataAdapter(ulas, baglanti);
            DataTable doldur = new DataTable();
            goster.Fill(doldur);
            dataGridView1.DataSource = doldur;
            Temizle();

        }
        public void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Listele("select * from Varlik");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 git = new Form1();
            git.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Varlik(VarlikAdi,Aciklama)values(@VarlikAdi,@Aciklama)", baglanti);
            komut.Parameters.AddWithValue("@VarlikAdi", textBox2.Text);
            komut.Parameters.AddWithValue("@Aciklama", textBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Varlik");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Listele("select * from Varlik");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Varlik where varlik_id=@varlik_id", baglanti);
            komut.Parameters.AddWithValue("@varlik_id", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Varlik");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text= dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Varlik set VarlikAdi='" + textBox2.Text.ToString() + "',Aciklama='" + textBox3.Text.ToString() + "'where varlik_id='"+textBox1.Text.ToString() +"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Varlik");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Varlik where VarlikAdi like '%" + textBox2.Text.ToString() + "%'", baglanti);

            SqlDataAdapter aranan = new SqlDataAdapter(komut);
            DataTable doldur = new DataTable();
            aranan.Fill(doldur);
            dataGridView1.DataSource = doldur;
            baglanti.Close();

        }
    }
}
