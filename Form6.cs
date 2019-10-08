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
    public partial class Form6 : Form
    {
        public Form6()
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
            textBox1.Text = "";
            comboBox3.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            Listele("select*from BankaHesabi");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select banka_id from BankaHesabi", baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["banka_id"]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select hesap_id from Hesap", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox2.Items.Add(he["hesap_id"]);
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into BankaHesabi(hesap_id,Tipi,Numara)values(@hesap_id,@Tipi,@Numara)", baglanti);
            komut.Parameters.AddWithValue("@hesap_id", comboBox2.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@Tipi", comboBox3.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@Numara", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from BankaHesabi");
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select banka_id from BankaHesabi", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["banka_id"]);
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from BankaHesabi where banka_id=@banka_id", baglanti);
            komut.Parameters.AddWithValue("@banka_id", comboBox1.SelectedItem.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from BankaHesabi");
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select banka_id from BankaHesabi", baglanti);
            SqlDataReader dr;
            dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["banka_id"]);
            }
            baglanti.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'or varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);
            SqlCommand komut = new SqlCommand("select*from BankaHesabi where banka_id like '%" + comboBox1.SelectedItem.ToString() + "%'", baglanti);

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
            textBox1.Text = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update BankaHesabi set hesap_id='" + comboBox2.SelectedItem.ToString() + "',Tipi='" + comboBox3.SelectedItem.ToString() + "',Numara='"+textBox1.Text.ToString()+"'where banka_id='" + comboBox1.SelectedItem.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from BankaHesabi");
        }
    }
}
