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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbDataReader okuyucu;
        OleDbCommand cmd, cmd2;
        public string session,ad;
        private void button1_Click(object sender, EventArgs e)
        {
            string k_adi = kadi.Text.ToString();
            string sifre = pass.Text.ToString();
            veritabani vt1 = new veritabani();
            try
            {
                vt1.baglan();
                if (vt1.giris(k_adi, sifre) == true)
                {
                    Anaform frm = new Anaform();

                    yetki(k_adi, sifre);
                    frm.yetki_Al(session);
                    frm.isim_Al(ad);
                    frm.Show();
                    this.Hide();

                }
                else
                {
                    kadi.Clear(); pass.Clear();
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                vt1.baglanti_Kes();
            }
            catch
            {

            }
            
        }
        public void yetki(string k_adi, string sifre)
        {
            string baglanti = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb";
            con = new OleDbConnection(baglanti);
            con.Open();
            string giris = "select * from kullanici where k_adi='" + k_adi + "' and sifre='" + sifre + "'";
            cmd = new OleDbCommand(giris, con);
            okuyucu = cmd.ExecuteReader();
            if (okuyucu.Read())
            {
                session = okuyucu["Yetki"].ToString();
                ad = okuyucu["Ad_Soyad"].ToString();
            }
            else
            {

            }
            con.Close();
        }

        private void Giris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
