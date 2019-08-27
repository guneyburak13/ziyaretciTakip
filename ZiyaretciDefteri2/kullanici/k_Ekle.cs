using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZiyaretciDefteri2;

namespace ZiyaretciDefteri2.kullanici
{
    public partial class k_Ekle : Form
    {
        public k_Ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kadi = textBox1.Text.ToString();
            string sifre = textBox2.Text.ToString();
            string adsoyad = textBox3.Text.ToString();
            string yetki = comboBox1.SelectedItem.ToString();
            veritabani vt1 = new veritabani();
            vt1.baglan();
            vt1.k_Ekle(kadi, sifre, adsoyad, yetki);
            vt1.baglanti_Kes();
            MessageBox.Show("Kullanıcı Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
        }

        private void k_Ekle_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["k_Liste"]==null )
            {
                
            }
            else
            {
                k_Liste f1 = (k_Liste)Application.OpenForms["k_Liste"];
                f1.k_Listele();
            }
            
        }
    }
}
