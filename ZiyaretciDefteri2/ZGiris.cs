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


namespace ZiyaretciDefteri2
{
    /*
        8. Gün
        */    
    public partial class ZGiris : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbDataReader okuyucu;
        OleDbCommand cmd;
        DataSet ds;
        public bool tiklandi = false;
        public string ad;
        
        public ZGiris()
        {
            InitializeComponent();
        }
        public void kartcek()
        {
            string sorgu = "SElect  * from ziyaretci_karti where aktif=true";
            string baglanti = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=../db.mdb";
            con = new OleDbConnection(baglanti);
            con.Open();
            cmd = new OleDbCommand(sorgu, con);
            ds = new DataSet();
            okuyucu = cmd.ExecuteReader();

            while (okuyucu.Read())
            {
                kartno.Items.Add(okuyucu["kart_No"]);
            }
            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void ad_Al(string ad2)
        {
            ad = ad2.ToString();
        }

        private void ZGiris_Load(object sender, EventArgs e)
        {

            z_Tarihi.Text = DateTime.Now.ToString("dd/MM/yyyy");
            z_Giris.Text = DateTime.Now.ToShortTimeString();
            veritabani vt = new veritabani();
            vt.baglan();
            vt.birimcek(ds,ze_Birimi);
            vt.kartcek(ds, kartno);
            vt.baglanti_Kes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string tarih,g_saati,c_saati,tc,adSoyad,aracPlaka,sebep,ze_adSoyad,ze_Birim,kart_no;           
            tarih = z_Tarihi.Text.ToString(); 
            g_saati = z_Giris.Text.ToString();
            c_saati = "";
            tc = z_TC.Text.ToString();
            adSoyad = z_AdSoyad.Text.ToString();
            aracPlaka = z_Plaka.Text.ToString();
            sebep = z_Sebebi.Text.ToString();
            ze_adSoyad = ze_AdSoyad.Text.ToString();
            ze_Birim = ze_Birimi.Text.ToString();
            kart_no = kartno.Text.ToString();
            if (z_TC.Text == "" || z_AdSoyad.Text == "" || z_Sebebi.Text == "" || ze_AdSoyad.Text == "" || ze_Birimi.Text == "")
            {
                MessageBox.Show("Boş alan bırakmayınız!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (z_TC.TextLength<11)//tcnin 11 hadeneden az olması hata kontrolü
            {
                MessageBox.Show("Tc Kimlik Numarası 11 haneden küçük olamaz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (kartno.Text=="")
            {
                MessageBox.Show("Kart Seçmediniz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                veritabani vt = new veritabani();
                ds = new DataSet();
                vt.baglan();
                vt.z_ekle(tarih, g_saati, c_saati, tc, adSoyad, aracPlaka, sebep, ze_adSoyad, ze_Birim, kart_no,ad);
                MessageBox.Show("Kayıt Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                vt.baglanti_Kes();
                z_TC.Clear(); z_Plaka.Clear(); z_Sebebi.Clear(); ze_AdSoyad.Clear();
                z_AdSoyad.Clear();
            }
            
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ZGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Anaform f1 = (Anaform)Application.OpenForms["Anaform"];
            f1.gunluk_kayit();

        }

        private void z_TC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);//TC nin sadece sayı olması
        }
        /*
        8. Gün
        */
    }
}
