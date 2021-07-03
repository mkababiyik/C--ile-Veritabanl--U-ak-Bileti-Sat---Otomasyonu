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
    public partial class SeferEkle : Form
    {
        public SeferEkle()
        {
            InitializeComponent();           
        }
        public string kullanici;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");
        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu form3 = new AnaMenu();
            form3.kullanici = kullanici;
            form3.Show();
            this.Hide();
        }
        public int sayiKontrol(string kelime)
        {
            int sayac1 = 0, sayac2 = 0;
            char[] kelimeOgeleri = kelime.ToCharArray();
            for (int i = 0; i < kelime.Length; i++)
            {
                if (kelimeOgeleri[i] == '0' || kelimeOgeleri[i] == '1' || kelimeOgeleri[i] == '2' || kelimeOgeleri[i] == '3' || kelimeOgeleri[i] == '4'
                    || kelimeOgeleri[i] == '5' || kelimeOgeleri[i] == '6' || kelimeOgeleri[i] == '7' || kelimeOgeleri[i] == '8' || kelimeOgeleri[i] == '9')
                {
                    sayac1++;
                }
                else
                {
                    sayac2++;
                }
            }
            if (sayac1 >= 0)
            {
                return sayac1;
            }
            else
            {
                return 0;
            }
        }
        public string tarihayikla(string oge)
        {
            string tarih = "";
            char[] ogeler = oge.ToArray();
            tarih += ogeler[0];
            tarih += ogeler[1];
            return tarih;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string suan = DateTime.Now.Day.ToString(); //Şuana ait gün bilgisi suan degiskenine atanir.
            string[] picker = dateTimePicker1.Text.ToString().Split(' ','.'); //dateTimePicker1'de seçilen tarih split kullanılarak boşluk ve noktalardan ayrılır ve öğeler
            //picker dizisinde tutulur.
            Sefer sefer = new Sefer(dateTimePicker1, comboBox1, comboBox2,maskedTextBox1,maskedTextBox2,maskedTextBox3);//sefer adı verilen struct içerisine formun ogeleri
            //aktarilir
            if (sefer.Kalkis!=""&&sefer.Varis!=""&&sefer.Fiyat!="" && sefer.VSaat != "" && sefer.KSaat != "") //Isleme devam etmek icin if icerisinde verilen degiskenler
                                                                                                              //bos olmamasi gerekmektedir.
            {
                if (sayiKontrol(maskedTextBox1.Text).ToString() != "4" || sayiKontrol(maskedTextBox2.Text).ToString()!="4" || sayiKontrol(maskedTextBox3.Text).ToString() == "0")
                    //string icerisindeki sayilari sayan sayiKontrol fonksiyonu ile saatlerin ve  bilet fiyatınin eksiksiz girildiginden emin olunur.
                {
                    MessageBox.Show("Lütfen fiyat ve saat bilgilerini eksiksiz ve doğru girdiğinizden emin olunuz!");
                }
                else
                {
                    if (picker[0] != suan)//Pickerin ilk degiskeni suan ile eslesmiyorsa (Bugune ait degil ise) islemler devam eder. (Aynı gune sefer eklenmeyecek sekilde ayarlanmistir)
                    {
                        string fiyat = sefer.Fiyat.Replace("₺", "").Trim(); //Fiyat bilgisinden ekranda gecersiz karakterden dolayı hata vermemes icin TL isareti kaldırılır.
                        baglanti.Open(); // Veritabani baglantisi acilir.
                        SqlCommand command = new SqlCommand("insert into Seferbilgi (TARIH,VSAAT,KALKIS,VARIS,KSAAT,FIYAT,KOLTUK) values (@p1,@p2,@p3,@p4,@p5,@p6,@p8)", baglanti);
                        // Alinacak parametrelerdeki bilgilerin veritabanina islenmesi icin command adında yeni bir Sql komutu oluşturulur.
                        // Insert ve komut satırının tamamı bu komutun icerisinde yazılır ve 2. degisken olan baglanti, baglanti saglayici olarak okunur. 
                        command.Parameters.AddWithValue("@p1", sefer.Tarih); 
                        command.Parameters.AddWithValue("@p2", sefer.VSaat);
                        command.Parameters.AddWithValue("@p3", sefer.Kalkis);
                        command.Parameters.AddWithValue("@p4", sefer.Varis);        //Parametreler girilir.
                        command.Parameters.AddWithValue("@p5", sefer.KSaat);
                        command.Parameters.AddWithValue("@p6", fiyat);
                        command.Parameters.AddWithValue("@p8", 0);
                        command.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                        baglanti.Close(); //Veritabani baglantisi kapatilir.
                        string an = DateTime.Now.ToString(); //An'a ait tarih ve saat bilgisi an degiskeninde tutulur.
                        baglanti.Open(); //Veritabani baglantisi farklı bir kullanim icin yeniden acilir.
                        SqlCommand command2 = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti); // Rapora ekleme amacıyla
                        // command2 adında yeni bir Sql komutu oluşturulur. Insert ve komut satırının tamamı bu komutun icerisinde yazılır ve 2. degisken olan baglanti,
                        // baglanti saglayici olarak okunur. 
                        command2.Parameters.AddWithValue("@p1", kullanici);
                        command2.Parameters.AddWithValue("@p2", "Sefer ekleme islemi gerceklestirildi."); //Rapor icin gerekli parametreler alinir. (Bu satirda yapilan islem girilmistir.)
                        command2.Parameters.AddWithValue("@p3", an);
                        command2.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                        baglanti.Close();  //Veritabani baglantisi kapatilir.
                        MessageBox.Show("Sefer başarıyla eklendi!");
                       
                    }
                    else
                    {
                        MessageBox.Show("Yeni seferin tarihi bugün ile aynı olamaz!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lutfen sefer bilgilerini eksiksiz doldurunuz!");
            }           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear(); //ListView ogeleri temizlenir.
            listView1.Refresh(); //ListView yenilenir
            baglanti.Open(); //Veritabani baglantisi kurulur.

            SqlCommand liste = new SqlCommand(); //liste adinda bir Sql komutu olusturulur.
            liste.Connection = baglanti; //komuta ait baglanti, baglantinin saglanmasi icin baglanti adindaki komutla eslestirilir.
            liste.CommandText = "Select *From Seferbilgi"; //Komut satirinda cagirma saglanmasi icin Select'e ve Table adina yer verilir.
            SqlDataReader dr; //dr adinda bir veri okuyucusu olusturulur.
            dr = liste.ExecuteReader(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
            string secilitarih = ""; //secilitarih adinda bos bir string degisken olusturulur.
            string timepicker = dateTimePicker2.Text.ToString(); // DateTimePicker uzerindeki tarihi tutacak timepicker adinda bir string degisken olusturulur.
            char[] secilitarihayikla = timepicker.ToCharArray(); //timepicker ogelerinin tamami char dizisine donusturulur ve secilitarihayikla dizisinde tutulur.
            secilitarih = secilitarihayikla[0] + "" + secilitarihayikla[1]; //secilitarihayikla char dizisinin ilk iki ogesi (gun bilgisinin alinmasi icin) secilitarih degiskenine atanir. 
            secilitarih = secilitarih.Trim(); //secilitarih degiskeninde herhangi bir bosluk var ise kaldirilir (gun bilgisinin tek sayi olmasi durumu icin)
            while (dr.Read())//Satir satir veri okuma islemi baslatilir.
            {
                string tarih = tarihayikla(dr["TARIH"].ToString()); //tarih ayikla fonksiyonu ile veritabanindaki tarih ogelerinin gun bilgisi tarih degiskenine atanir.
                if (tarih.Trim() == secilitarih.Trim()) //tarih ve secilitarihin eslesip eslesmedigi kontrol edilir.
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                    item.SubItems.Add(dr["KALKIS"].ToString());
                    item.SubItems.Add(dr["VARIS"].ToString());      //eslesen ogelere ait diger veriler tek tek ListView'e eklenir.
                    item.SubItems.Add(dr["FIYAT"].ToString());
                    listView1.Items.Add(item);
                }
            }
            baglanti.Close(); //Veritabani baglantisi kapatilir.
        }

        private void SeferEkle_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();
            Secim = MessageBox.Show("Seçili sefere ait bilgilerin tamamen silinmesini onaylıyor musunuz?\nBu işlem geri döndürülemeyecektir!",
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes) //Seferin silinmesinin onaylanmasi durumunda silme islemi devam eder.
            {
                int n;
                bool isNumeric = int.TryParse(maskedTextBox4.Text, out n);
                if (isNumeric) //Gecerli bir deger girilip girilmedigi kontrol edilir.
                {
                    baglanti.Open(); //Veritabani baglantisi acilir.
                    SqlCommand sil = new SqlCommand("Delete From Seferbilgi Where ID=@k1", baglanti); //sil adinda yeni bir Sql komutu olusturulur. Komut satirinda Delete ve Seferbilgi Table
                    //adina yer verilir. Where ise yalnizca eslesen ID ile silinmesini saglayacaktir.
                    sil.Parameters.AddWithValue("@k1", maskedTextBox4.Text); //Parametreye girilen ID atanir.
                    sil.ExecuteNonQuery(); //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                    baglanti.Close(); //Veritabani baglantisi kapatilir.
                    MessageBox.Show("Seçili ID'ye ait sefer silindi!","İşlem Tamamlandı!"); 

                    string an = DateTime.Now.ToString(); //An'a ait tarih ve saat bilgisi an degiskenine kaydedilir.
                    baglanti.Open(); //Veritabani baglantisi acilir.
                    SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti); // Rapora ekleme amacıyla
                    // command adında yeni bir Sql komutu oluşturulur. Insert ve komut satırının tamamı bu komutun icerisinde yazılır ve 2. degisken olan baglanti,
                    // baglanti saglayici olarak okunur. 
                    command.Parameters.AddWithValue("@p1", kullanici);
                    command.Parameters.AddWithValue("@p2", "Sefer islemi gerçekleştirildi"); //Rapora ait parametreler girilir.
                    command.Parameters.AddWithValue("@p3", an);
                    command.ExecuteNonQuery();  //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                    baglanti.Close(); //Veritabani baglantisi kapatilir.

                    listView1.Items.Clear(); //ListView ogeleri temizlenir.
                    listView1.Refresh(); //ListView yenilenir
                    baglanti.Open(); //Veritabani baglantisi kurulur.

                    SqlCommand liste = new SqlCommand(); //liste adinda bir Sql komutu olusturulur.
                    liste.Connection = baglanti; //komuta ait baglanti, baglantinin saglanmasi icin baglanti adindaki komutla eslestirilir.
                    liste.CommandText = "Select *From Seferbilgi"; //Komut satirinda cagirma saglanmasi icin Select'e ve Table adina yer verilir.
                    SqlDataReader dr; //dr adinda bir veri okuyucusu olusturulur.
                    dr = liste.ExecuteReader(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
                    string secilitarih = ""; //secilitarih adinda bos bir string degisken olusturulur.
                    string timepicker = dateTimePicker2.Text.ToString(); // DateTimePicker uzerindeki tarihi tutacak timepicker adinda bir string degisken olusturulur.
                    char[] secilitarihayikla = timepicker.ToCharArray(); //timepicker ogelerinin tamami char dizisine donusturulur ve secilitarihayikla dizisinde tutulur.
                    secilitarih = secilitarihayikla[0] + "" + secilitarihayikla[1]; //secilitarihayikla char dizisinin ilk iki ogesi (gun bilgisinin alinmasi icin) secilitarih degiskenine atanir. 
                    secilitarih = secilitarih.Trim(); //secilitarih degiskeninde herhangi bir bosluk var ise kaldirilir (gun bilgisinin tek sayi olmasi durumu icin)
                    while (dr.Read())//Satir satir veri okuma islemi baslatilir.
                    {
                        string tarih = tarihayikla(dr["TARIH"].ToString()); //tarih ayikla fonksiyonu ile veritabanindaki tarih ogelerinin gun bilgisi tarih degiskenine atanir.
                        if (tarih.Trim() == secilitarih.Trim()) //tarih ve secilitarihin eslesip eslesmedigi kontrol edilir.
                        {
                            ListViewItem item = new ListViewItem(dr["ID"].ToString()); 
                            item.SubItems.Add(dr["KALKIS"].ToString());
                            item.SubItems.Add(dr["VARIS"].ToString());      //eslesen ogelere ait digerv eriler tek tek ListView'e eklenir.
                            item.SubItems.Add(dr["FIYAT"].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    baglanti.Close(); //Veritabani baglantisi kapatilir.
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir sefer ID girip tekrar deneyiniz.","Hatalı Giriş");
                }
            }
            
        }

        private void SeferEkle_Load_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    class Sefer
    {
        public string Tarih;
        public string Kalkis;
        public string Varis;
        public string KSaat;
        public string VSaat;
        public string Fiyat;
        public Sefer(DateTimePicker dtp1,ComboBox cx1, ComboBox cx2, MaskedTextBox mx1,MaskedTextBox mx2,MaskedTextBox mx3)
        {
            Tarih = dtp1.Text;
            Kalkis = cx1.Text;
            Varis = cx2.Text;
            KSaat = mx1.Text;
            VSaat = mx2.Text;
            Fiyat = mx3.Text;
        }
    }
}
