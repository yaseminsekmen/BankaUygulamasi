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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Banka;uid=sa;pwd=1234");
        //SqlConnection baglanti = new SqlConnection("Server=AWESOME\\SQLEXPRESS;Database=Banka;Trusted_Connection=True;");
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
       

             baglanti.Open();
            SqlCommand komut = new SqlCommand("select KullaniciAdi,Sifre from Admin WHERE KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", baglanti);
            komut.Parameters.AddWithValue("@KullaniciAdi",textBox1.Text);
            komut.Parameters.AddWithValue("@Sifre", textBox2.Text);
            SqlDataReader dr;
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 git = new Form1();
                git.Show();
                this.Hide();
            
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }

            baglanti.Close();

         
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
