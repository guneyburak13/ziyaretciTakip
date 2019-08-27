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
    public partial class b_Edit : Form
    {

        public int secilen = 0;
        public b_Edit()
        {
            InitializeComponent();
        }

        private void b_Edit_Load(object sender, EventArgs e)
        {

        }
        public void b_Al(int id,string ad)

        {
            secilen = id;
            textBox1.Text = ad.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            veritabani vt = new veritabani();
            vt.baglan();
            vt.b_Edit(secilen, textBox1.Text.ToString());
            vt.baglanti_Kes();
        }

        private void b_Edit_FormClosed(object sender, FormClosedEventArgs e)
        {
            b_Liste f1 = (b_Liste)Application.OpenForms["b_Liste"];
            f1.b_Listele();
        }
    }
}
