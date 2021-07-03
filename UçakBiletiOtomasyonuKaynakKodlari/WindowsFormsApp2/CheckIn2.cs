using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class CheckIn2 : Form
    {
        public CheckIn2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        public string yolcuPNR;
        public string seferIDleri;
        public string seciliKoltuk;
        public int seferSayisi = 0;
        public string koltukSecimi = "";
        public int islemSirasi = 0;
        public string[] idler = new string[4];

        public string kullanici;
        private void CheckIn2_Load(object sender, EventArgs e)
        {
            label26.Hide();
            label24.Hide();
            idler = seferIDleri.Split(' ');

            for (int i = 0; i < idler.Length; i++)
            {
                if (idler[i] != "")
                {
                    seferSayisi++;
                }
            }

            if (idler[islemSirasi] != "")
            {
                label26.Text = seciliKoltuk;
                label25.Text = idler[islemSirasi];

                baglanti.Open(); //Veritabanı baglantisi olusturulur.
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                cmd2.CommandText = "Select *From Seferbilgi";   //Seferbilgi Table'ı baz alınarak Select ile çağrılma sağlanması için bir komut satırı oluşturulur.
                SqlDataReader dr;
                dr = cmd2.ExecuteReader();   //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.

                while (dr.Read()) //Okuma devam ettikce...
                {
                    if (dr["ID"].ToString() == idler[islemSirasi]) //ID eslesmesi de saglanmis ise
                    {
                        if (dr["A1"].ToString() == "dolu")
                        {
                            A1.BackColor = Color.Red;
                        }
                        if (dr["A2"].ToString() == "dolu")
                        {
                            A2.BackColor = Color.Red;
                        }
                        if (dr["A3"].ToString() == "dolu")
                        {
                            A3.BackColor = Color.Red;
                        }
                        if (dr["A4"].ToString() == "dolu")
                        {
                            A4.BackColor = Color.Red;
                        }
                        if (dr["A5"].ToString() == "dolu")          //O ID'ye ait 51 Koltuk gozden gecirilir ve bu koltukların doluluk bilgisi alınır.
                        {                                           //Dolu koltuklar kırmızı ile gosterilir.
                            A5.BackColor = Color.Red;
                        }
                        if (dr["A6"].ToString() == "dolu")
                        {
                            A6.BackColor = Color.Red;
                        }
                        if (dr["A7"].ToString() == "dolu")
                        {
                            A7.BackColor = Color.Red;
                        }
                        if (dr["A8"].ToString() == "dolu")
                        {
                            A8.BackColor = Color.Red;
                        }
                        if (dr["A9"].ToString() == "dolu")
                        {
                            A9.BackColor = Color.Red;
                        }
                        if (dr["B1"].ToString() == "dolu")
                        {
                            B1.BackColor = Color.Red;
                        }
                        if (dr["B2"].ToString() == "dolu")
                        {
                            B2.BackColor = Color.Red;
                        }
                        if (dr["B3"].ToString() == "dolu")
                        {
                            B3.BackColor = Color.Red;
                        }
                        if (dr["B4"].ToString() == "dolu")
                        {
                            B4.BackColor = Color.Red;
                        }
                        if (dr["B5"].ToString() == "dolu")
                        {
                            B5.BackColor = Color.Red;
                        }
                        if (dr["B6"].ToString() == "dolu")
                        {
                            B6.BackColor = Color.Red;
                        }
                        if (dr["B7"].ToString() == "dolu")
                        {
                            B7.BackColor = Color.Red;
                        }
                        if (dr["B8"].ToString() == "dolu")
                        {
                            B8.BackColor = Color.Red;
                        }
                        if (dr["B9"].ToString() == "dolu")
                        {
                            B9.BackColor = Color.Red;
                        }
                        if (dr["C1"].ToString() == "dolu")
                        {
                            C1.BackColor = Color.Red;
                        }
                        if (dr["C2"].ToString() == "dolu")
                        {
                            C2.BackColor = Color.Red;
                        }
                        if (dr["C3"].ToString() == "dolu")
                        {
                            C3.BackColor = Color.Red;
                        }
                        if (dr["C4"].ToString() == "dolu")
                        {
                            C4.BackColor = Color.Red;
                        }
                        if (dr["C5"].ToString() == "dolu")
                        {
                            C5.BackColor = Color.Red;
                        }
                        if (dr["C6"].ToString() == "dolu")
                        {
                            C6.BackColor = Color.Red;
                        }
                        if (dr["C7"].ToString() == "dolu")
                        {
                            C7.BackColor = Color.Red;
                        }
                        if (dr["C8"].ToString() == "dolu")
                        {
                            C8.BackColor = Color.Red;
                        }
                        if (dr["C9"].ToString() == "dolu")
                        {
                            C9.BackColor = Color.Red;
                        }
                        if (dr["D1"].ToString() == "dolu")
                        {
                            D1.BackColor = Color.Red;
                        }
                        if (dr["D2"].ToString() == "dolu")
                        {
                            D2.BackColor = Color.Red;
                        }
                        if (dr["D3"].ToString() == "dolu")
                        {
                            D3.BackColor = Color.Red;
                        }
                        if (dr["D4"].ToString() == "dolu")
                        {
                            D4.BackColor = Color.Red;
                        }
                        if (dr["D5"].ToString() == "dolu")
                        {
                            D5.BackColor = Color.Red;
                        }
                        if (dr["D6"].ToString() == "dolu")
                        {
                            D6.BackColor = Color.Red;
                        }
                        if (dr["D7"].ToString()  == "dolu")
                        {
                            D7.BackColor = Color.Red;
                        }
                        if (dr["D8"].ToString() == "dolu")
                        {
                            D8.BackColor = Color.Red;
                        }
                        if (dr["E1"].ToString() == "dolu")
                        {
                            E1.BackColor = Color.Red;
                        }
                        if (dr["E2"].ToString() == "dolu")
                        {
                            E2.BackColor = Color.Red;
                        }
                        if (dr["E3"].ToString() == "dolu")
                        {
                            E3.BackColor = Color.Red;
                        }
                        if (dr["E4"].ToString() == "dolu")
                        {
                            E4.BackColor = Color.Red;
                        }
                        if (dr["E5"].ToString() == "dolu")
                        {
                            E5.BackColor = Color.Red;
                        }
                        if (dr["E6"].ToString() == "dolu")
                        {
                            E6.BackColor = Color.Red;
                        }
                        if (dr["E7"].ToString() == "dolu")
                        {
                            E7.BackColor = Color.Red;
                        }
                        if (dr["E8"].ToString() == "dolu")
                        {
                            E8.BackColor = Color.Red;
                        }
                        if (dr["F1"].ToString() == "dolu")
                        {
                            F1.BackColor = Color.Red;
                        }
                        if (dr["F2"].ToString() == "dolu")
                        {
                            F2.BackColor = Color.Red;
                        }
                        if (dr["F3"].ToString() == "dolu")
                        {
                            F3.BackColor = Color.Red;
                        }
                        if (dr["F4"].ToString() == "dolu")
                        {
                            F4.BackColor = Color.Red;
                        }
                        if (dr["F5"].ToString() == "dolu")
                        {
                            F5.BackColor = Color.Red;
                        }
                        if (dr["F6"].ToString() == "dolu")
                        {
                            F6.BackColor = Color.Red;
                        }
                        if (dr["F7"].ToString() == "dolu")
                        {
                            F7.BackColor = Color.Red;
                        }
                        if (dr["F8"].ToString() == "dolu")
                        {
                            F8.BackColor = Color.Red;
                        }
                    }
                }
                baglanti.Close();
            }         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (islemSirasi==seferSayisi-1)
            {
                string an = DateTime.Now.ToString(); 
                baglanti.Open();
                SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti);
                command.Parameters.AddWithValue("@p1", kullanici);
                command.Parameters.AddWithValue("@p2", "Check-In işlemi gerçekleştirildi");
                command.Parameters.AddWithValue("@p3", an);
                command.ExecuteNonQuery();
                baglanti.Close();
                AnaMenu anaMenu = new AnaMenu();
                anaMenu.kullanici = kullanici;
                anaMenu.Show();
                this.Hide();
            }
            if (islemSirasi != seferSayisi)
            {
                A1.BackColor = Color.LimeGreen;
                A2.BackColor = Color.LimeGreen;
                A3.BackColor = Color.LimeGreen;
                A4.BackColor = Color.LimeGreen;
                A5.BackColor = Color.LimeGreen;
                A6.BackColor = Color.LimeGreen;
                A7.BackColor = Color.LimeGreen;
                A8.BackColor = Color.LimeGreen;
                A9.BackColor = Color.LimeGreen;
                B1.BackColor = Color.LimeGreen;
                B2.BackColor = Color.LimeGreen;
                B3.BackColor = Color.LimeGreen;
                B4.BackColor = Color.LimeGreen;
                B5.BackColor = Color.LimeGreen;
                B6.BackColor = Color.LimeGreen;
                B7.BackColor = Color.LimeGreen;
                B8.BackColor = Color.LimeGreen;
                B9.BackColor = Color.LimeGreen;
                C1.BackColor = Color.LimeGreen;
                C2.BackColor = Color.LimeGreen;
                C3.BackColor = Color.LimeGreen;
                C4.BackColor = Color.LimeGreen;
                C5.BackColor = Color.LimeGreen;
                C6.BackColor = Color.LimeGreen;
                C7.BackColor = Color.LimeGreen;
                C8.BackColor = Color.LimeGreen;
                C9.BackColor = Color.LimeGreen;
                D1.BackColor = Color.LimeGreen;
                D2.BackColor = Color.LimeGreen;
                D3.BackColor = Color.LimeGreen;
                D4.BackColor = Color.LimeGreen;
                D5.BackColor = Color.LimeGreen;
                D6.BackColor = Color.LimeGreen;
                D7.BackColor = Color.LimeGreen;
                D8.BackColor = Color.LimeGreen;
                E1.BackColor = Color.LimeGreen;
                E2.BackColor = Color.LimeGreen;
                E3.BackColor = Color.LimeGreen;
                E4.BackColor = Color.LimeGreen;
                E5.BackColor = Color.LimeGreen;
                E6.BackColor = Color.LimeGreen;
                E7.BackColor = Color.LimeGreen;
                E8.BackColor = Color.LimeGreen;
                F1.BackColor = Color.LimeGreen;
                F2.BackColor = Color.LimeGreen;
                F3.BackColor = Color.LimeGreen;
                F4.BackColor = Color.LimeGreen;
                F5.BackColor = Color.LimeGreen;
                F6.BackColor = Color.LimeGreen;
                F7.BackColor = Color.LimeGreen;
                F8.BackColor = Color.LimeGreen;

                baglanti.Open(); //Veritabani baglantisi acilir.
                SqlCommand cmd = new SqlCommand(); 
                cmd.Connection = baglanti;
                cmd.CommandText = "Select *From Seferbilgi"; //Seferbilgi Table'ı baz alınarak çağrılam işlemi Select ile gerçekleştirilen bir komut satırıyla yapılır.
                SqlDataReader dr;
                dr = cmd.ExecuteReader();   //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
                while (dr.Read()) //Okuma devam ettikce...
                {
                    if (dr["ID"].ToString() == idler[islemSirasi]) //Islem sirasina gore eslesme saglayan ID'lerde...
                    {
                        if (dr[seciliKoltuk].ToString() != "dolu") //Secili koltuk dolu degil ise...
                        {
                            baglanti.Close(); //Acik veritabani baglantisi kapatilir.
                            if (baglanti.State != ConnectionState.Open) //Kapali ise acilir
                            {
                                baglanti.Open();
                            }
                            if (islemSirasi == 0) //0. islem sirasi icin...
                            {
                                string sql = "update YolcuBilgileri set KOLTUK= '" + seciliKoltuk + "' where PNR = '" + yolcuPNR + "' "; 
                                //Secili koltuk yolcu PNR sinin esit oldugu yerde YolcuBilgileri Table'ında update komutu ile guncellenir.
                                SqlCommand cmd4 = new SqlCommand(sql, baglanti);
                                cmd4.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.

                                string sql2 = "update Seferbilgi set " + seciliKoltuk + " = '" + "dolu" + "' where ID = '" + idler[islemSirasi] + "' ";
                                //Islem sirasina gore belirtilen ID'de update komutu ile secili koltuk veritabani ogelerinde aratilip dolu olarak tanimlanacaktir
                                SqlCommand cmd5 = new SqlCommand(sql2, baglanti);
                                islemSirasi++;
                                cmd5.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                                MessageBox.Show("İşlem tamamlandı!", "Başarılı!");
                                label25.Text = idler[islemSirasi];
                                break;
                            }
                            else if (islemSirasi == 1)
                            {
                                string sql = "update YolcuBilgileri set KOLTUK2= '" + seciliKoltuk + "' where PNR = '" + yolcuPNR + "' ";
                                SqlCommand cmd4 = new SqlCommand(sql, baglanti);
                                cmd4.ExecuteNonQuery();

                                string sql2 = "update Seferbilgi set "+ seciliKoltuk + " = '" + "dolu" + "' where ID = '" + idler[islemSirasi] + "' ";
                                SqlCommand cmd5 = new SqlCommand(sql2, baglanti);
                                islemSirasi++;
                                cmd5.ExecuteNonQuery();
                                MessageBox.Show((islemSirasi) + ". İşlem için koltuk seçimi tamamlandı!", "Başarılı!");
                                label25.Text = idler[islemSirasi];
                                break;
                            }
                            else if (islemSirasi == 2)
                            {
                                string sql = "update YolcuBilgileri set KOLTUK3= '" + seciliKoltuk + "' where PNR = '" + yolcuPNR + "' ";
                                SqlCommand cmd4 = new SqlCommand(sql, baglanti);
                                cmd4.ExecuteNonQuery();

                                string sql2 = "update Seferbilgi set " + seciliKoltuk + " = '" + "dolu" + "' where ID = '" + idler[islemSirasi] + "' ";
                                SqlCommand cmd5 = new SqlCommand(sql2, baglanti);
                                islemSirasi++;
                                cmd5.ExecuteNonQuery();
                                MessageBox.Show((islemSirasi) + ". İşlem için koltuk seçimi tamamlandı!", "Başarılı!");
                                label25.Text = idler[islemSirasi];
                                break;
                            }
                            else if (islemSirasi == 3)
                            {
                                string sql = "update YolcuBilgileri set KOLTUK4= '" + seciliKoltuk + "' where PNR = '" + yolcuPNR + "' ";
                                SqlCommand cmd4 = new SqlCommand(sql, baglanti);
                                cmd4.ExecuteNonQuery();

                                string sql2 = "update Seferbilgi set " + seciliKoltuk + " = '" + "dolu" + "' where ID = '" + idler[islemSirasi] + "' ";
                                SqlCommand cmd5 = new SqlCommand(sql2, baglanti);
                                islemSirasi++;
                                cmd5.ExecuteNonQuery();
                                MessageBox.Show((islemSirasi) + ". İşlem için koltuk seçimi tamamlandı!", "Başarılı!");
                                break;
                            }
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Seçili koltuk dolu olduğu için işlem gerçekleştirilememektedir.\nLütfen farklı bir koltuk seçimi yapınız!", "Hata!");
                        }
                        break;
                    }
                }
                baglanti.Close();
                baglanti.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = baglanti;
                cmd3.CommandText = "Select *From Seferbilgi";
                SqlDataReader dr2;
                dr2 = cmd3.ExecuteReader();
                if (islemSirasi != seferSayisi )
                {
                    while (dr2.Read())
                    {
                        if (dr2["ID"].ToString() == idler[islemSirasi])
                        {
                            if (dr2["A1"].ToString() == "dolu")
                            {
                                A1.BackColor = Color.Red;
                            }
                            if (dr2["A2"].ToString() == "dolu")
                            {
                                A2.BackColor = Color.Red;
                            }
                            if (dr2["A3"].ToString() == "dolu")
                            {
                                A3.BackColor = Color.Red;
                            }
                            if (dr2["A4"].ToString() == "dolu")
                            {
                                A4.BackColor = Color.Red;
                            }
                            if (dr2["A5"].ToString() == "dolu")
                            {
                                A5.BackColor = Color.Red;
                            }
                            if (dr2["A6"].ToString() == "dolu")
                            {
                                A6.BackColor = Color.Red;
                            }
                            if (dr2["A7"].ToString() == "dolu")
                            {
                                A7.BackColor = Color.Red;
                            }
                            if (dr2["A8"].ToString() == "dolu")
                            {
                                A8.BackColor = Color.Red;
                            }
                            if (dr2["A9"].ToString() == "dolu")
                            {
                                A9.BackColor = Color.Red;
                            }
                            if (dr2["B1"].ToString() == "dolu")
                            {
                                B1.BackColor = Color.Red;
                            }
                            if (dr2["B2"].ToString() == "dolu")
                            {
                                B2.BackColor = Color.Red;
                            }
                            if (dr2["B3"].ToString() == "dolu")
                            {
                                B3.BackColor = Color.Red;
                            }
                            if (dr2["B4"].ToString() == "dolu")
                            {
                                B4.BackColor = Color.Red;
                            }
                            if (dr2["B5"].ToString() == "dolu")
                            {
                                B5.BackColor = Color.Red;
                            }
                            if (dr2["B6"].ToString() == "dolu")
                            {
                                B6.BackColor = Color.Red;
                            }
                            if (dr2["B7"].ToString() == "dolu")
                            {
                                B7.BackColor = Color.Red;
                            }
                            if (dr2["B8"].ToString() == "dolu")
                            {
                                B8.BackColor = Color.Red;
                            }
                            if (dr2["B9"].ToString() == "dolu")
                            {
                                B9.BackColor = Color.Red;
                            }
                            if (dr2["C1"].ToString() == "dolu")
                            {
                                C1.BackColor = Color.Red;
                            }
                            if (dr2["C2"].ToString() == "dolu")
                            {
                                C2.BackColor = Color.Red;
                            }
                            if (dr2["C3"].ToString() == "dolu")
                            {
                                C3.BackColor = Color.Red;
                            }
                            if (dr2["C4"].ToString() == "dolu")
                            {
                                C4.BackColor = Color.Red;
                            }
                            if (dr2["C5"].ToString() == "dolu")
                            {
                                C5.BackColor = Color.Red;
                            }
                            if (dr2["C6"].ToString() == "dolu")
                            {
                                C6.BackColor = Color.Red;
                            }
                            if (dr2["C7"].ToString() == "dolu")
                            {
                                C7.BackColor = Color.Red;
                            }
                            if (dr2["C8"].ToString() == "dolu")
                            {
                                C8.BackColor = Color.Red;
                            }
                            if (dr2["C9"].ToString() == "dolu")
                            {
                                C9.BackColor = Color.Red;
                            }
                            if (dr2["D1"].ToString() == "dolu")
                            {
                                D1.BackColor = Color.Red;
                            }
                            if (dr2["D2"].ToString() == "dolu")
                            {
                                D2.BackColor = Color.Red;
                            }
                            if (dr2["D3"].ToString() == "dolu")
                            {
                                D3.BackColor = Color.Red;
                            }
                            if (dr2["D4"].ToString() == "dolu")
                            {
                                D4.BackColor = Color.Red;
                            }
                            if (dr2["D5"].ToString() == "dolu")
                            {
                                D5.BackColor = Color.Red;
                            }
                            if (dr2["D6"].ToString() == "dolu")
                            {
                                D6.BackColor = Color.Red;
                            }
                            if (dr2["D7"].ToString() == "dolu")
                            {
                                D7.BackColor = Color.Red;
                            }
                            if (dr2["D8"].ToString() == "dolu")
                            {
                                D8.BackColor = Color.Red;
                            }
                            if (dr2["E1"].ToString() == "dolu")
                            {
                                E1.BackColor = Color.Red;
                            }
                            if (dr2["E2"].ToString() == "dolu")
                            {
                                E2.BackColor = Color.Red;
                            }
                            if (dr2["E3"].ToString() == "dolu")
                            {
                                E3.BackColor = Color.Red;
                            }
                            if (dr2["E4"].ToString() == "dolu")
                            {
                                E4.BackColor = Color.Red;
                            }
                            if (dr2["E5"].ToString() == "dolu")
                            {
                                E5.BackColor = Color.Red;
                            }
                            if (dr2["E6"].ToString() == "dolu")
                            {
                                E6.BackColor = Color.Red;
                            }
                            if (dr2["E7"].ToString() == "dolu")
                            {
                                E7.BackColor = Color.Red;
                            }
                            if (dr2["E8"].ToString() == "dolu")
                            {
                                E8.BackColor = Color.Red;
                            }
                            if (dr2["F1"].ToString() == "dolu")
                            {
                                F1.BackColor = Color.Red;
                            }
                            if (dr2["F2"].ToString() == "dolu")
                            {
                                F2.BackColor = Color.Red;
                            }
                            if (dr2["F3"].ToString() == "dolu")
                            {
                                F3.BackColor = Color.Red;
                            }
                            if (dr2["F4"].ToString() == "dolu")
                            {
                                F4.BackColor = Color.Red;
                            }
                            if (dr2["F5"].ToString() == "dolu")
                            {
                                F5.BackColor = Color.Red;
                            }
                            if (dr2["F6"].ToString() == "dolu")
                            {
                                F6.BackColor = Color.Red;
                            }
                            if (dr2["F7"].ToString() == "dolu")
                            {
                                F7.BackColor = Color.Red;
                            }
                            if (dr2["F8"].ToString() == "dolu")
                            {
                                F8.BackColor = Color.Red;
                            }
                        }
                    }        
                }
            }
            baglanti.Close();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "F8";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "E8";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "D8";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A8";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "A9";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button49_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B8";
            label26.Refresh();
            label24.Show();
            label26.Show();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "B9";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C1";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C2";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C3";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C4";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C5";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C6";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C7";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C8";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            seciliKoltuk = "C9";
            label26.Text = seciliKoltuk;
            label24.Show();
            label26.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            CheckIn checkIn = new CheckIn();
            checkIn.kullanici = kullanici;
            checkIn.Show();
            this.Hide();
        }
    }
}
