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
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        public string isim;
        public string kullanici;
        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = "Hosgeldiniz " + isim+"!";
            timer1.Interval = 1000;
            timer1.Enabled = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonelKaydi personelKaydi = new PersonelKaydi();
            personelKaydi.kullanici = kullanici;
            personelKaydi.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void Seferbutton_Click(object sender, EventArgs e)
        {
            Seferler seferler = new Seferler();
            seferler.kullanici = kullanici;
            seferler.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Biletsatis biletsatis = new Biletsatis();
            biletsatis.kullanici = kullanici;
            biletsatis.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GirisEkrani form1 = new GirisEkrani(); //form1 Adında bir GirisEkrani degiskeni olusturulur.
            string an = DateTime.Now.ToString(); //an degiskenine o ana ait tarih ve saat bilgisi girilir.
            baglanti.Open(); //Veritabani baglantisi acilir.
            SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti); // Rapora ekleme amacıyla
            // command adında yeni bir Sql komutu oluşturulur. Insert ve komut satırının tamamı bu komutun icerisinde yazılır ve 2. degisken olan baglanti,
             // baglanti saglayici olarak okunur. 
            command.Parameters.AddWithValue("@p1", kullanici); //p1 parametresine personel kullanici adi yazdirilir.
            command.Parameters.AddWithValue("@p2", "Cikis yapildi"); //Yapilan islem olan cikis bilgisi p2 parametresine, yani isleme yazdirilir.
            command.Parameters.AddWithValue("@p3", an); //p3 parametresine tarih ve saatin tutuldugu an degiskeni atanir.
            command.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
            baglanti.Close(); //Veritabanı baglantisi kapatilir.
            form1.Show(); //form1 ekrana getirilir
            this.Hide(); // Acili form gizlenir.

        }

        private void button5_Click(object sender, EventArgs e)
        {
            BiletIptal biletiptal = new BiletIptal();
            biletiptal.kullanici = kullanici;
            biletiptal.Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SeferEkle seferekle = new SeferEkle();
            seferekle.kullanici = kullanici;
            seferekle.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckIn checkIn = new CheckIn();
            checkIn.kullanici = kullanici;
            checkIn.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BiletGuncelle biletGuncelle = new BiletGuncelle();
            biletGuncelle.kullanici = kullanici;
            biletGuncelle.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.kullanici = kullanici;
            raporlar.Show();
            this.Hide();
        }
    }
}
