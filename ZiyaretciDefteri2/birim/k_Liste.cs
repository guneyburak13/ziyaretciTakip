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
namespace ZiyaretciDefteri2.kullanici
{
    public partial class k_Liste : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        public int secilen=0;
        public string kadi, pass, adsoyad, yetki;
        public k_Liste()
        {
            InitializeComponent();
        }

        private void k_Liste_Load(object sender, EventArgs e)
        {
            k_Listele();
        }
        public void k_Listele()
        {
            string baglanti = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb";
            con = new OleDbConnection(baglanti);
            da = new OleDbDataAdapter("SElect  * from kullanici", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kullanici");
            dataGridView1.DataSource = ds.Tables["kullanici"];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k_Ekle frm = new k_Ekle();
            frm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            kadi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            pass = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            adsoyad = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            yetki = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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
                vt.k_Sil(secilen);
                vt.baglanti_Kes();
                k_Listele();
                MessageBox.Show("Kişi silindi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (secilen==0)
            {
                MessageBox.Show("Düzenlenecek kişiyi seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                k_Edit frm = new k_Edit();
                frm.k_Al(secilen,kadi, pass, adsoyad, yetki);
                frm.Show();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (txt_Kadi.Text=="")
            {
                k_Listele();
            }
            else {
                string kadi = txt_Kadi.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.k_Ara(kadi, dataGridView1);
                vt.baglanti_Kes();
            }
            
        }
    }
}
