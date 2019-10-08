using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void varlıkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 git = new Form2();
            git.Show();
            this.Hide();
        }

        private void kategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 git = new Form3();
            git.Show();
            this.Hide();

        }

        private void hesapİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 git = new Form4();
            git.Show();
            this.Hide();
        }

        private void bütçeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 git = new Form5();
            git.Show();
            this.Hide();
        }

        private void bankaHesabıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 git = new Form6();
            git.Show();
            this.Hide();
        }

        private void kartİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 git = new Form7();
            git.Show();
            this.Hide();
        }

        private void günİçiİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 git = new Form8();
            git.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void detayToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void detayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form9 git = new Form9();
            git.Show();
            this.Hide();
        }
    }
}
