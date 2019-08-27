using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ZiyaretciDefteri2.birim
{
    public partial class b_Liste : Form
    {
        int secilen = 0;
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        string ad;
        public b_Liste()
        {
            InitializeComponent();
        }

        private void b_Liste_Load(object sender, EventArgs e)
        {
            b_Listele();
        }
        public void b_Listele()
        {
            string baglanti = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb";
            con = new OleDbConnection(baglanti);
            da = new OleDbDataAdapter("select * from birimler ", con);
            ds = new DataSet();
            da.Fill(ds, "birimler");
            dataGridView1.DataSource = ds.Tables["birimler"];
        }
        private void btn_Ekle_Click(object sender, EventArgs e)
        {
            b_Ekle b1 = new b_Ekle();
            b1.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            ad = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            if (secilen == 0)
            {
                MessageBox.Show("Silinecek kişiyi seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                veritabani vt = new veritabani();
                vt.baglan();
                vt.b_Sil(secilen);
                vt.baglanti_Kes();
                b_Listele();
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (secilen == 0)
            {
                MessageBox.Show("Düzenlenecek kişiyi seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                b_Edit frm = new b_Edit();
                frm.b_Al(secilen,ad);
                frm.Show();
            }
        }

        private void btn_Ara_Click(object sender, EventArgs e)
        {
            if (txt_Kadi.Text == "")
            {
                b_Listele();
            }
            else
            {
                string kadi = txt_Kadi.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.b_Ara(kadi, dataGridView1);
                vt.baglanti_Kes();
            }
        }
    }
}
