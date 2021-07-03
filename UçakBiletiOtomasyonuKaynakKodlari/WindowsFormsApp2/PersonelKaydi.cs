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
    public partial class PersonelKaydi : Form
    {
        public PersonelKaydi()
        {
            InitializeComponent();
            captcha();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");
        public string kullanici;
        private void captcha()
        {
            string[] captcha = new string[7];
            string[] karakterler = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                                    "r", "s", "t", "w", "x", "v", "z","1","2","3","4","5","6","7","8","9" };
            Random r = new Random();
            int k1;
            for (int i = 0; i < 7; i++)
            {
                k1 = r.Next(0, karakterler.Length);
                captcha[i] = karakterler[k1];
            }
            string capchatext = "";
            for (int j = 0; j < 7; j++)
            {
                capchatext += captcha[j];
            }
            label9.Text = capchatext;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            captcha();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                if (textBox5.Text == textBox6.Text)
                {
                    if (label9.Text == textBox7.Text) //Bilgiler eksiksiz girilmeli ve Captcha eşleştirilmesi sağlanmalı. Koşullar kusursuz sağlandığında personel kaydı işlemi devam etmektedir.
                    {
                        MessageBox.Show("Yeni Personel Kaydı Başarıyla Oluşturuldu!");
                        baglanti.Open(); //Veri tabanı bağlantısı açılmaktadır.
                        SqlCommand command = new SqlCommand("insert into PersonelKayit (PersonelAdi,PersonelSoyadi,PersonelKullaniciAdi,PersonelSifre) values (@p1,@p2,@p3,@p4)", baglanti);
                        //Insert ve parametrelerin yer aldıgı komutsatirinin ve baglanti ogesinin bulundugu komut satiri olusturulmustur. Insert ile veritabanına ekleme yapilir.
                        command.Parameters.AddWithValue("@p1", textBox1.Text);
                        command.Parameters.AddWithValue("@p2", textBox2.Text);  //parametrelere gerekli verilerin girisi yapilir
                        command.Parameters.AddWithValue("@p3", textBox4.Text);
                        command.Parameters.AddWithValue("@p4", textBox5.Text);
                        command.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                        baglanti.Close(); //Veritabani baglantisi kapatilir

                        string an = DateTime.Now.ToString(); //An'a ait tarih ve saat bilgisi an degiskeninde tutulur
                        baglanti.Open();//Farklı bir kullanim icin veritabani baglantisi yeniden acilir.
                        SqlCommand cmd = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti);
                        //Insert ve parametrelerin yer aldıgı komutsatirinin ve baglanti ogesinin bulundugu komut satiri olusturulmustur. Insert ile veritabanına ekleme yapilir.
                        //Yeni personel kaydının kim tarafindan olusturuldugu bilinmesi amaci ile rapor kaydedilir.
                        cmd.Parameters.AddWithValue("@p1", kullanici);
                        cmd.Parameters.AddWithValue("@p2", "Yeni personel kaydi gerceklestirildi.");    //Rapora ait verilerin girisi yapilir.
                        cmd.Parameters.AddWithValue("@p3", an);
                        cmd.ExecuteNonQuery();//Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                        baglanti.Close(); //Veritabani baglantisi kapatilir
                            
                        AnaMenu form1 = new AnaMenu();
                        form1.kullanici = kullanici;
                        form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Güvenlik kodunu yanlış girdiniz!");
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        captcha();
                    }
                }
                else
                {
                    MessageBox.Show("Girdiginiz sifreler ayni degil!");
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    captcha();
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Eksiksiz Doldurunuz!");
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                captcha();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            anaMenu.kullanici = kullanici;
            anaMenu.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.personelKayitTableAdapter.Fill(this.ucakBiletiSatisOtomasyonuVeritabaniDataSet3.PersonelKayit);
        }
    }
}
