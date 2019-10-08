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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Banka;uid=sa;pwd=1234");
        //SqlConnection baglanti = new SqlConnection("Server=AWESOME\\SQLEXPRESS;Database=Banka;Trusted_Connection=True;");
        private void label4_Click(object sender, EventArgs e)
        {
            Form1 git = new Form1();
            git.Show();
            this.Hide();
        }
        public void Listele(string ulas)
        {
            SqlDataAdapter goster = new SqlDataAdapter(ulas, baglanti);
            DataTable doldur = new DataTable();
            goster.Fill(doldur);
            dataGridView1.DataSource = doldur;
            Temizle();
            comboBox1.Update();




        }
        public void Temizle()
        {
            comboBox2.Text = "";
            comboBox1.Text = "";
            comboBox5.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Operasyon(varlik_id,hesap_id,kategori_id,tarih,GecerlilikDurumu,Miktar)values(@varlik_id,@hesap_id,@kategori_id,@tarih,@GecerlilikDurumu,@Miktar)", baglanti);
            komut.Parameters.AddWithValue("@varlik_id", comboBox2.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@hesap_id", comboBox3.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@kategori_id", comboBox4.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@GecerlilikDurumu", comboBox5.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@Miktar", textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Operasyon");
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select operasyon_id from Operasyon", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["operasyon_id"]);
            }
            baglanti.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Operasyon where operasyon_id=@operasyon_id", baglanti);
            komut.Parameters.AddWithValue("@operasyon_id", comboBox1.SelectedItem.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Operasyon");
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select operasyon_id from Operasyon", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["operasyon_id"]);
            }
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Listele("select*from Operasyon");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'or varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);
            SqlCommand komut = new SqlCommand("select*from Operasyon where operasyon_id like '%" + comboBox1.SelectedItem.ToString() + "%'", baglanti);

            SqlDataAdapter aranan = new SqlDataAdapter(komut);
            DataTable doldur = new DataTable();
            aranan.Fill(doldur);
            dataGridView1.DataSource = doldur;
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            comboBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
            comboBox5.Text = dataGridView1.Rows[sectim].Cells[5].Value.ToString();
            textBox2.Text = dataGridView1.Rows[sectim].Cells[6].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Operasyon set varlik_id='" + comboBox2.SelectedItem.ToString() + "',hesap_id='" + comboBox3.SelectedItem.ToString() + "',kategori_id='" +comboBox4.SelectedItem.ToString() + "',tarih='" +dateTimePicker1.Value.ToString() + "',GecerlilikDurumu='" +comboBox5.SelectedItem.ToString()+ "',Miktar='" + textBox2.Text.ToString() + "'where operasyon_id='" + comboBox1.SelectedItem.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Operasyon");
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Listele("select*from Operasyon");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select operasyon_id from Operasyon", baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["operasyon_id"]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select hesap_id from Hesap", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox3.Items.Add(he["hesap_id"]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select varlik_id from Varlik", baglanti);
            SqlDataReader var;
            var = komut2.ExecuteReader();
            while (var.Read())
            {
                comboBox2.Items.Add(var["varlik_id"]);
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select kategori_id from Kategori", baglanti);
            SqlDataReader kat;
            kat = komut3.ExecuteReader();
            while (kat.Read())
            {
                comboBox4.Items.Add(kat["kategori_id"]);
            }
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
