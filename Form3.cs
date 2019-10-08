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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Banka;uid=sa;pwd=1234");
       // SqlConnection baglanti = new SqlConnection("Server=AWESOME\\SQLEXPRESS;Database=Banka;Trusted_Connection=True;");
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
            comboBox1.Text = "";
            textBox2.Text = "";
           
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Listele("select*from Kategori");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select varlik_id from Varlik", baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["varlik_id"]);
            }
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Kategori(varlik_id,tipi)values(@varlik_id,@tipi)", baglanti);
            komut.Parameters.AddWithValue("@varlik_id", comboBox1.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@tipi", textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Kategori");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Kategori where kategori_id=@kategori_id", baglanti);
            komut.Parameters.AddWithValue("@kategori_id",textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Kategori");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Kategori where varlik_id like '%" + comboBox1.SelectedItem.ToString() + "%'", baglanti);

            SqlDataAdapter aranan = new SqlDataAdapter(komut);
            DataTable doldur = new DataTable();
            aranan.Fill(doldur);
            dataGridView1.DataSource = doldur;
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Kategori set varlik_id='" + comboBox1.SelectedItem.ToString() + "',tipi='" + textBox2.Text.ToString() + "'where kategori_id='" + textBox1.Text.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Kategori");
        }
    }
}
