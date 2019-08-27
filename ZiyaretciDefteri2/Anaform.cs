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
using ZiyaretciDefteri2.kullanici;
using ZiyaretciDefteri2.birim;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace ZiyaretciDefteri2
{
    public partial class Anaform : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        int secilen = 0, kartno;
        public string yetki2 = null;
        public string ad2 = null;
        public Anaform()
        {
            InitializeComponent();
        }
        /*
         8. Gün
         */
        public void eski_kayit()
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb");
                da = new OleDbDataAdapter("SElect  * from eski_kayitlar", con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "eski_kayitlar");
                dataGridView2.DataSource = ds.Tables["eski_kayitlar"];
                con.Close();
            }
            catch
            {
                MessageBox.Show("Eski Kayıtlar Çekilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void gunluk_kayit()
        {
            string baglanti = "Provider=Microsoft.ACE.Oledb.12.0;Data Source=db.mdb";
            con = new OleDbConnection(baglanti);
            da = new OleDbDataAdapter("SElect  * from icerideki_ziyaretciler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "icerideki_ziyaretciler");
            dataGridView1.DataSource = ds.Tables["icerideki_ziyaretciler"];
            con.Close();
        }
        /*
        8. Gün
        */
        public void yetki_Al(string yetki)
        {
            yetki2 = yetki.ToString();

        }
        public void isim_Al(string ad)
        {
            ad2 = ad.ToString();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            if (yetki2 == "Şef")
            {
                kullanici_Menu.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
            }
            eski_kayit();
            gunluk_kayit();
            label7.Text = ad2.ToString();
        }
        /*
        8. Gün
        */
        private void button1_Click(object sender, EventArgs e)
        {
            ZGiris frm = new ZGiris();
            frm.ad_Al(ad2);
            frm.Show();
        }
        /*
        8. Gün
        */
        /*
        10. Gün
        */
        private void button2_Click(object sender, EventArgs e)
        {
            if (secilen == 0)
            {
                MessageBox.Show("Çıkış yapılacak kişiyi seçmediniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                veritabani vt1 = new veritabani();
                vt1.baglan();
                try
                {
                    vt1.z_Cikis(secilen);
                    vt1.z_Sil(secilen, kartno);
                    MessageBox.Show("Şeçilen kişinin çıkışı yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Hata!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                gunluk_kayit();
                vt1.baglanti_Kes();
                eski_kayit();
            }
        }
        /*
        10. Gün
        */
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            kartno = Convert.ToInt32(dataGridView1.CurrentRow.Cells[10].Value);
        }
        /*
        10. Gün
        */
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab.Text == tabPage2.Text)
            {
                panel1.Visible = true;
            }
            if (tabControl1.SelectedTab.Text == tabPage1.Text)
            {
                panel1.Visible = false;
            }
        }

        private void ziyaretçiGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZGiris frm = new ZGiris();
            frm.ad_Al(ad2);
            frm.Show();
        }

        private void ziyaretçiÇıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Giris frm = new Giris();
            frm.Show();
        }

        private void kullanıcıSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k_Liste frm = new k_Liste();
            frm.Show();
        }

        private void kullanıcıEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k_Ekle frm = new k_Ekle();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Belirli Tarih")
            {
                string tarih = dateTimePicker1.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.a_Tarih(tarih, dataGridView2);
                vt.baglanti_Kes();

            }
            else if (comboBox1.SelectedItem == "İki Tarih Arası")
            {
                string t1 = dateTimePicker2.Text.ToString();
                string t2 = dateTimePicker3.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.iki_Tarih(t1, t2, dataGridView2);
                vt.baglanti_Kes();
            }
            else if (comboBox1.SelectedItem == "Ziyaretçi Ad Soyad")
            {
                string ad = textBox1.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.a_AdSoyad(ad, dataGridView2);
                vt.baglanti_Kes();
            }
            else if (comboBox1.SelectedItem == "Kayıt Yapana Göre")
            {
                string ad = textBox2.Text.ToString();
                veritabani vt = new veritabani();
                vt.baglan();
                vt.m_AdSoyad(ad, dataGridView2);
                vt.baglanti_Kes();
            }
            else
            {
                eski_kayit();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Belirli Tarih")
            {
                panel2.Visible = true;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
            }
            else if (comboBox1.SelectedItem == "İki Tarih Arası")
            {
                panel3.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
            }
            else if (comboBox1.SelectedItem == "Ziyaretçi Ad Soyad")
            {
                panel4.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                panel5.Visible = false;
            }
            else if (comboBox1.SelectedItem == "Kayıt Yapana Göre")
            {
                panel5.Visible = true;
                panel4.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eski_kayit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kullanıcıEkleToolStripMenuItem_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kullanıcıSilToolStripMenuItem_Click(sender, e);
        }

        private void yedeklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string a = DateTime.Now.ToShortDateString();
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.File.Exists(saveFileDialog1.FileName))
                    {
                        System.IO.File.Delete(saveFileDialog1.FileName);
                    }
                    System.IO.File.Copy(Application.StartupPath.ToString() + "\\db.mdb", saveFileDialog1.FileName);
                    MessageBox.Show("Yedek alma işlemi tamamlandı !");
                }
                else
                {
                    MessageBox.Show("Yedekle işlemi iptal edildi !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void yedekYüklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.File.Exists(Application.StartupPath.ToString() + "\\db.mdb"))
                {
                    System.IO.File.Delete(Application.StartupPath.ToString() + "\\db.mdb");
                }
                System.IO.File.Copy(openFileDialog1.FileName, Application.StartupPath.ToString() + "\\db.mdb");
                MessageBox.Show("Geri yükleme işlemi tamamlandı !");
                Form1_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Geri yükleme işlemi iptal edildi !");
            }

        }

        private void birimListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b_Liste frm1 = new b_Liste();
            frm1.Show();
        }

        private void birimEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b_Ekle b1 = new b_Ekle();
            b1.Show();
        }

        private void Anaform_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                button1.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                button2.PerformClick();
            }
        }

        private void Anaform_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Giris frm = new Giris();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            yazdir(dataGridView2);
        }
        public void yazdir(DataGridView d1)
        {
            PdfPTable pdfTable = new PdfPTable(d1.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
            foreach (DataGridViewColumn column in d1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText,fontNormal));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }
            foreach (DataGridViewRow row in d1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfTable.AddCell(TurkceKarakter(cell.Value.ToString()));
                }
            }
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + @"\ZiyaretciDefteri.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
                
            }
            System.Diagnostics.Process.Start(folderPath + @"\ZiyaretciDefteri.pdf");
        }
        public string TurkceKarakter(string text)
        {

            text = text.Replace("İ", "\u0130");

            text = text.Replace("ı", "\u0139");

            text = text.Replace("Ş", "\u015e");

            text = text.Replace("ş", "\u015f");

            text = text.Replace("Ğ", "\u011e");

            text = text.Replace("ğ", "\u011f");

            text = text.Replace("Ö", "\u00d6");

            text = text.Replace("ö", "\u00f6");

            text = text.Replace("ç", "\u00e7");

            text = text.Replace("Ç", "\u00c7");

            text = text.Replace("ü", "\u00fc");

            text = text.Replace("Ü", "\u00dc");

            return text;
        }
    }


}

