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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server=.;Database=Banka;uid=sa;pwd=1234");
        //SqlConnection baglanti = new SqlConnection("Server=AWESOME\\SQLEXPRESS;Database=Banka;Trusted_Connection=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            Listele("select tipi,VarlikAdi,o.GecerlilikDurumu from Operasyon o inner join Varlik v on o.varlik_id=v.varlik_id inner join Kategori k on o.kategori_id=k.kategori_id");
        }
        public void Listele(string ulas)
        {
            SqlDataAdapter goster = new SqlDataAdapter(ulas, baglanti);
            DataTable doldur = new DataTable();
            goster.Fill(doldur);
            dataGridView1.DataSource = doldur;

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 git = new Form1();
            git.Show();
            this.Hide();

        }
    }
}
