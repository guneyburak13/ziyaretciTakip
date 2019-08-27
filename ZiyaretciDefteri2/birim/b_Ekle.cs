using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZiyaretciDefteri2.birim
{
    public partial class b_Ekle : Form
    {
        public b_Ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string b_adi = textBox1.Text.ToString();
            veritabani db = new veritabani();
            db.baglan();
            db.b_Ekle(b_adi);
            db.baglanti_Kes();
        }

        private void b_Ekle_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["b_Liste"] == null)
            {

            }
            else
            {
                b_Liste f1 = (b_Liste)Application.OpenForms["b_Liste"];
                f1.b_Listele();
            }
        }
    }
}
