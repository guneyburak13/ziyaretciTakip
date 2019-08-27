using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZiyaretciDefteri2.kullanici
{
    public partial class k_Edit : Form
    {
        public int secilen = 0;
        public k_Edit()
        {
            InitializeComponent();
        }

        private void k_Edit_Load(object sender, EventArgs e)
        {

        }
        public void k_Al(int id,string kadi,string sifre,string adsoyad,string yetki)
        {
            secilen = id;
            textBox1.Text = kadi.ToString();
            textBox2.Text = sifre.ToString();
            textBox3.Text = adsoyad.ToString();
            comboBox1.SelectedItem = yetki.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                veritabani vt = new veritabani();
                vt.baglan();
                vt.k_Edit(secilen, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.SelectedItem.ToString());
                vt.baglanti_Kes();
                MessageBox.Show("Kayıt Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Hata Oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void k_Edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            k_Liste f1 = (k_Liste)Application.OpenForms["k_Liste"];
            f1.k_Listele() ;
        }
    }
}
