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
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        int kontrol = 0;
        string kullaniciadi;
        string isim;
        private void button1_Click(object sender, EventArgs e)
        {

            if (kAdiBox.Text != "" && sifreBox1.Text != "") //TextBoxların boş mu dolu mu olduğu kontrol edilir. Dolu ise if içerisindeki işlemler gerçekleştirilecektir
            {
                baglanti.Open(); //Veritabanı bağlantısı açılır.
                SqlCommand cmd = new SqlCommand(); //Yeni Sql bağlantısı oluşturulur.
                cmd.Connection = baglanti; //Oluşturulan Sql bağlantısı baglanti adı verilen bağlantı sağlayıcı değişkene bağlanır.
                cmd.CommandText = "Select *From PersonelKayit"; //PersonelKayit veritabanına ulaşmak için Sql komut satırı oluşturulup Select'e ve ulaşılacak Table adına yer verilir.
                SqlDataReader dr; //dr adında bir Sql veri okuyucusu oluşturulur.
                dr = cmd.ExecuteReader(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
                while (dr.Read()) //Satır satır okuma işlemi gerçekleştirilir.
                {
                    if (dr["PersonelKullaniciAdi"].ToString() == kAdiBox.Text) //Veritabanında girilmiş olan kullanıcı adı ile eşleşebilecek bir kullanıcı adı aranır.
                    {
                        if (dr["PersonelSifre"].ToString() == sifreBox1.Text) //Kullanıcı adları eşleştiği takdirde aynı satır için şifrenin de eşleşip eşleşmeyeceği kontrol edilir.
                        {
                            isim = dr["PersonelAdi"].ToString(); //Eşleşme sağlandığında isim değişkenine (Diğer formlarda kullanılmak üzere) personel adı yazdırılır.
                            kullaniciadi = dr["PersonelKullaniciAdi"].ToString(); //kullanıcı adı değişkenine (Diğer formlarda kullanılmak üzere) personel adı yazdırılır.
                            kontrol = 1; //Girişin sağlanabilmesi için daha önce 0 olan kontrol değişkenine 1 değeri verilir.
                        }
                    }
                }
                baglanti.Close(); //Veritabanı bağlantısı kapatılır.
                if (kontrol == 1) //kontrol değişkeni 1 ise...
                {
                    AnaMenu form3 = new AnaMenu(); //form3 adında yeni bir AnaMenu değişkeni oluşturulur.
                    form3.isim = isim;    //yeni formun isim değişkenine bu formdaki isim bilgisi aktarılır.
                    form3.kullanici = kullaniciadi;  //Yeni formun kullanici degiskenine bu formdaki kullaniciadi bilgisi aktarilir.

                    string an = DateTime.Now.ToString(); //Ana ait tarih ve saat bilgisi alınır.
                    baglanti.Open(); // Veri tabanı bağlantısı açılır.
                    SqlCommand cmd1 = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti); // Rapora ekleme amacıyla
                   // cmd1 adında yeni bir Sql komutu oluşturulur. Insert ve komut satırının tamamı bu komutun icerisinde yazılır ve 2. degisken olan baglanti,
                   // baglanti saglayici olarak okunur. 
                    cmd1.Parameters.AddWithValue("@p1", kullaniciadi); //Olusturulan 1. parametreye kullaniciadi bilgisi atanir.
                    cmd1.Parameters.AddWithValue("@p2", "Giris yapildi"); //Rapor bilgisi icin yapilan islem 2. degiskene atanir
                    cmd1.Parameters.AddWithValue("@p3", an); //Yapilan islemin tarihi
                    cmd1.ExecuteNonQuery(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
                    baglanti.Close();//Veritabanı bağlantısı kapatılır.
                    form3.Show(); //form3 ekrana getirilir.
                    this.Hide(); //Acilmis olan form gizlenir.
                }
                else
                {
                    MessageBox.Show("Girilen kullanıcı adı ile herhangi bir eşleşme bulunamadı!", "Hata!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurunuz!", "Hata!");
            }
        }
    }
}
