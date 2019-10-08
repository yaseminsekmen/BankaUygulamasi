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
    public partial class Form4 : Form
    {
        public Form4()
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
            comboBox1.Update();
          



        }
        public void Temizle()
        {
            comboBox2.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Listele("select*from Hesap");
         
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select varlik_id from Varlik", baglanti);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["varlik_id"]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select hesap_id from Hesap", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox1.Items.Add(he["hesap_id"]);
            }
            baglanti.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Hesap(varlik_id,GecerlilikDurumu)values(@varlik_id,@GecerlilikDurumu)", baglanti);
            komut.Parameters.AddWithValue("@varlik_id", comboBox2.SelectedItem.ToString());
            komut.Parameters.AddWithValue("@GecerlilikDurumu", comboBox3.SelectedItem.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Hesap");
            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select hesap_id from Hesap", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox1.Items.Add(he["hesap_id"]);
            }
            baglanti.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Hesap where hesap_id=@hesap_id",baglanti);
            komut.Parameters.AddWithValue("@hesap_id", comboBox1.SelectedItem.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Hesap");

            comboBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select hesap_id from Hesap", baglanti);
            SqlDataReader he;
            he = komut1.ExecuteReader();
            while (he.Read())
            {
                comboBox1.Items.Add(he["hesap_id"]);
            }
            baglanti.Close();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'or varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);
           SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'", baglanti);

            SqlDataAdapter aranan = new SqlDataAdapter(komut);
            DataTable doldur = new DataTable();
            aranan.Fill(doldur);
            dataGridView1.DataSource = doldur;
            baglanti.Close();
            //baglanti.Open();
            //// SqlCommand komut = new SqlCommand("select*from Hesap where hesap_id like '%" + comboBox1.SelectedItem.ToString() + "%'or varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);
            //SqlCommand komut1 = new SqlCommand("select*from Hesap where varlik_id like '%" + comboBox2.SelectedItem.ToString() + "%'", baglanti);

            //SqlDataAdapter ara = new SqlDataAdapter(komut1);
            //DataTable doldur1 = new DataTable();
            //ara.Fill(doldur1);
            //dataGridView1.DataSource = doldur1;
            //baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Hesap set varlik_id='" + comboBox2.SelectedItem.ToString() + "',GecerlilikDurumu='" + comboBox3.SelectedItem.ToString() + "'where hesap_id='" + comboBox1.SelectedItem.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select*from Hesap");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            comboBox1.Text = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[sectim].Cells[2].Value.ToString();



        }
    }
}
