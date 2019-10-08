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
    public partial class Form5 : Form
    {
        public Form5()
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
            comboBox1.Update();




        }
        public void Temizle()
        {
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox1.Text = "";

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Listele("select*from Bütce");

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select bütce_id from Bütce", baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["bütce_id"]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select kategori_id from Kategori", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox2.Items.Add(he["kategori_id"]);
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Bütce(kategori_id,limit,BaslangicTarihi,BitisTarihi)values(@kategori_id,@limit,@BaslangicTarihi,@BitisTarihi)", baglanti);
            komut.Parameters.AddWithValue("@kategori_id", comboBox2.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@limit", textBox1.Text);
            komut.Parameters.AddWithValue("@BaslangicTarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@BitisTarihi", dateTimePicker2.Value);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Bütce");

            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select bütce_id from Bütce", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["bütce_id"]);
            }
            baglanti.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Bütce where bütce_id=@bütce_id", baglanti);
            komut.Parameters.AddWithValue("@bütce_id", comboBox1.SelectedItem.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Bütce");

            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select bütce_id from Bütce", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["bütce_id"]);
            }
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Listele("select*from Bütce");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'or varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);
            SqlCommand komut = new SqlCommand("select*from Bütce where bütce_id like '%" + comboBox1.SelectedItem.ToString() + "%'", baglanti);

            SqlDataAdapter aranan = new SqlDataAdapter(komut);
            DataTable doldur = new DataTable();
            aranan.Fill(doldur);
            dataGridView1.DataSource = doldur;
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Bütce set kategori_id='" + comboBox2.SelectedItem.ToString() + "',limit='" + textBox1.Text.ToString() +"',BaslangicTarihi='"+dateTimePicker1.Value.ToString() +"',BitisTarihi='"+dateTimePicker2.Value.ToString()+"'where bütce_id='" + comboBox1.SelectedItem.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
           
            Listele("select*from Bütce");
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            comboBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
        }
    }
}
