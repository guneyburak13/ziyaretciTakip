using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace ZiyaretciDefteri2
{
    public class veritabani
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb");
        OleDbDataAdapter da;
        OleDbDataReader okuyucu;
        OleDbCommand cmd, cmd2;
        DataSet ds;
        string session;
        /*
        8. Gün
        */
        public void baglan()
        {
            try
            {
                con.Open();
            }
            catch
            {//veritabanı bağlantısı yapıldı...
                MessageBox.Show("Veritabanı Hatası!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void baglanti_Kes()
        {
            con.Close();
        }
        /*
        8. Gün
        */
        /*
        9. Gün
        */
        public void birimcek(DataSet ds, ComboBox ze_Birimi)
        {
            string sorgu = "SElect  * from birimler";
            cmd = new OleDbCommand(sorgu, con);
            ds = new DataSet();
            okuyucu = cmd.ExecuteReader();

            while (okuyucu.Read())
            {
                ze_Birimi.Items.Add(okuyucu["birim_adi"]);
            }
        }
        /*
        9. Gün
        */
        public void kartcek(DataSet ds, ComboBox kartno)
        {
            string sorgu = "Select  * from ziyaretci_karti where aktif=true";
            cmd = new OleDbCommand(sorgu, con);
            ds = new DataSet();
            okuyucu = cmd.ExecuteReader();
            while (okuyucu.Read())
            {
                kartno.Items.Add(okuyucu["kart_No"]);
            }

        }
        /*
        9. Gün
        */
        public void z_ekle(string tarih, string g_saati, string c_saati, string tc, string adSoyad, string aracPlaka, string
            sebep, string ze_adSoyad, string ze_Birim, string kart_no, string m_Ad)
        {
            int kartno = Convert.ToInt32(kart_no.ToString());
            string sorgu2 = "Insert into icerideki_ziyaretciler (z_Tarihi,z_SaatiGiris,z_SaatiCikis,z_TcNo,z_AdSoyad,z_AracPlaka,z_Sebebi,ze_AdSoyad,ze_Birimi,z_KartNo,m_AdSoyad) values ('" + tarih + "','" + g_saati + "','" + c_saati + "','" + tc + "','" + adSoyad + "','" + aracPlaka + "','" + sebep + "','" + ze_adSoyad + "','" + ze_Birim + "','" + kart_no + "','" + m_Ad + "')";
            cmd = new OleDbCommand(sorgu2, con);
            cmd.ExecuteNonQuery();
            string pasif = "update ziyaretci_karti set aktif=False where kart_No=@kart";
            cmd2 = new OleDbCommand(pasif, con);
            cmd2.Parameters.AddWithValue("@kart", kartno);
            cmd2.ExecuteNonQuery();
        }
        /*
        9. Gün
        */
        /*
        10. Gün
        */
        public void z_Cikis(int secilen)
        {
            string saat = DateTime.Now.ToShortTimeString();
            //string cikis = "insert into eski_kayitlar (ID,z_Tarihi,z_SaatiGiris,z_SaatiCikis,z_TcNo,z_AdSoyad,z_AracPlaka,z_Sebebi,ze_AdSoyad,ze_Birimi,z_KartNo) Select ID,z_Tarihi,z_SaatiGiris,z_SaatiCikis,z_TcNo,z_AdSoyad,z_AracPlaka,z_Sebebi,ze_AdSoyad,ze_Birimi,z_KartNo from icerideki_ziyaretciler where ID='1'";
            string cikis = "Insert into eski_kayitlar select * from icerideki_ziyaretciler where ID=@secilen";
            cmd = new OleDbCommand(cikis, con);
            cmd.Parameters.AddWithValue("@secilen", secilen);
            cmd.ExecuteNonQuery();
            string saatekle = "Update eski_kayitlar set z_SaatiCikis=@saat where ID=@secilen";
            cmd2 = new OleDbCommand(saatekle, con);
            cmd2.Parameters.AddWithValue("@saat", saat);
            cmd2.Parameters.AddWithValue("@secilen", secilen);
            cmd2.ExecuteNonQuery();

        }
        
        public void z_Sil(int secilen, int kartno)
        {
            string sil = "Delete from icerideki_ziyaretciler where ID=@secilen";
            cmd = new OleDbCommand(sil, con);
            cmd.Parameters.AddWithValue("@secilen", secilen);
            cmd.ExecuteNonQuery();
            string aktif = "update ziyaretci_karti set aktif=True where kart_No=@kart";
            cmd2 = new OleDbCommand(aktif, con);
            cmd2.Parameters.AddWithValue("@kart", kartno);
            cmd2.ExecuteNonQuery();
        }
        /*
        10. Gün
        */
        public bool giris(string k_adi, string sifre)
        {
            string giris = "select * from kullanici where k_adi='" + k_adi + "' and sifre='" + sifre + "'";
            cmd = new OleDbCommand(giris, con);
            okuyucu = cmd.ExecuteReader();
            if (okuyucu.Read())
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public void k_Ekle(string kadi, string sifre, string adsoyad, string yetki)
        {
            string ekle = "insert into kullanici (k_adi,sifre,Ad_Soyad,Yetki) values('" + kadi + "','" + sifre + "','" + adsoyad + "','" + yetki + "')";
            cmd = new OleDbCommand(ekle, con);
            cmd.ExecuteNonQuery();
        }
        public void k_Sil(int id)
        {
            string sil = "Delete from kullanici where ID=@id";
            cmd = new OleDbCommand(sil, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        public void k_Edit(int id, string kadi, string sifre, string adsoyad, string yetki)
        {
            string edit = "UPDATE kullanici SET k_adi='" + kadi + "' , sifre='" + sifre + "' , Ad_Soyad='" + adsoyad + "' , yetki='" + yetki + "' where ID=@id";
            cmd = new OleDbCommand(edit, con);
            cmd.Parameters.AddWithValue("@id", id);
            /*cmd.Parameters.AddWithValue("@kadi", kadi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
            cmd.Parameters.AddWithValue("@yetki", yetki);   */
            cmd.ExecuteNonQuery();
        }
        public void k_Ara(string kadi, DataGridView d1)
        {
            da = new OleDbDataAdapter("SElect  * from kullanici where k_adi LIKE '%" + kadi + "%'", con);
            ds = new DataSet();
            da.Fill(ds, "kullanici");
            d1.DataSource = ds.Tables["kullanici"];
        }
        /*
        11. Gün
        */
        public void a_Tarih(string tarih, DataGridView d1)
        {
            da = new OleDbDataAdapter("Select * from eski_kayitlar where z_Tarihi='" + tarih + "'", con);
            ds = new DataSet();
            da.Fill(ds, "eski_kayitlar");
            d1.DataSource = ds.Tables["eski_kayitlar"];
        }
        public void iki_Tarih(string t1, string t2, DataGridView d1)
        {
            da = new OleDbDataAdapter("select * from eski_kayitlar where z_Tarihi between '" + t1 + "' and '" + t2 + "' ", con);
            ds = new DataSet();
            da.Fill(ds, "eski_kayitlar");
            d1.DataSource = ds.Tables["eski_kayitlar"];
        }
        public void a_AdSoyad(string ad, DataGridView d1)
        {
            da = new OleDbDataAdapter("select * from eski_kayitlar where z_AdSoyad='" + ad + "'", con);
            ds = new DataSet();
            da.Fill(ds, "eski_kayitlar");
            d1.DataSource = ds.Tables["eski_kayitlar"];
        }
        public void m_AdSoyad(string ad, DataGridView d1)
        {
            da = new OleDbDataAdapter("select * from eski_kayitlar where m_AdSoyad='" + ad + "'", con);
            ds = new DataSet();
            da.Fill(ds, "eski_kayitlar");
            d1.DataSource = ds.Tables["eski_kayitlar"];
        }
        /*
        11. Gün
        */
        public void b_Ekle(string b_adi)
        {
            try
            {
                string ekle = "insert into birimler (birim_adi) values ('" + b_adi + "')";
                cmd = new OleDbCommand(ekle, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ekleme Sırasında Hata Oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void b_Sil(int id)
        {
            try
            {
                string sil = "delete from birimler where ID=@id";
                cmd = new OleDbCommand(sil, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ekleme Sırasında Hata Oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void b_Edit(int id, string ad)
        {
            try
            {
                string edit = "update birimler set birim_adi='" + ad + "' where ID=@id";
                cmd = new OleDbCommand(edit, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Düzenlendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Düzenleme Sırasında Hata Oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void b_Ara(string kadi, DataGridView d1)
        {
            da = new OleDbDataAdapter("SElect  * from birimler where birim_adi LIKE '%" + kadi + "%'", con);
            ds = new DataSet();
            da.Fill(ds, "birimler");
            d1.DataSource = ds.Tables["birimler"];
        }
        
    }
}
