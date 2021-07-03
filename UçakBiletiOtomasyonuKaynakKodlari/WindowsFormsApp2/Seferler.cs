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
    public partial class Seferler : Form
    {
        public Seferler()
        {
            InitializeComponent();
            
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");
        public string kullanici;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu form3 = new AnaMenu();
            form3.kullanici = kullanici;
            form3.Show();
            this.Hide();
        }
        public string tarihayikla(string oge)
        {
            string tarih = "";
            char[] ogeler = oge.ToArray();
            tarih += ogeler[0];
            tarih += ogeler[1];
            return tarih;
        }
        private void Seferler_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Sefer ID", 150);
            listView1.Columns.Add("Sefer Tarihi", 200);
            listView1.Columns.Add("Kalkış Saati", 100);
            listView1.Columns.Add("Nereden", 50);
            listView1.Columns.Add("Nereye", 150);
            listView1.Columns.Add("Uçuş Süresi", 125);
            listView1.Columns.Add("Bilet Fiyatı", 130);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // TARIH KRITERI BAZ ALARAK LISTELEME
            listView1.Items.Clear(); //Daha onceki ListView ogeleri temizlenir.
            listView1.Refresh(); //ListView yenilenir.
            baglanti.Open(); //Veritabanı baglantisi acilir.
            SqlCommand liste = new SqlCommand(); //Liste adinda bir veritabani komutu olusturulur.
            liste.Connection = baglanti; //Olusturulan komutun baglanti adindaki veritabani baglantisi ile bag kurulur.
            liste.CommandText = "Select *From Seferbilgi"; //Ogeleri cagirmak icin komut satirinda Select'e ve cagrilacak ogeleri barindiran Table'a yer verilir.
            SqlDataReader dr; //dr adında bir Sql veri okuyucusu oluşturulur.
            dr = liste.ExecuteReader(); //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
            while (dr.Read()) //Satir satir veri okuma islemi baslatilir.
            {
                ListViewItem item = new ListViewItem(dr["ID"].ToString()); //Herhangi bir kriter baz alinmadan tum ogeler teker teker ListView'e eklenir.
                item.SubItems.Add(dr["TARIH"].ToString());
                item.SubItems.Add(dr["KALKIS"].ToString());
                item.SubItems.Add(dr["VARIS"].ToString());
                item.SubItems.Add(dr["KOLTUK"].ToString()+"/51");
                item.SubItems.Add(dr["KSAAT"].ToString());
                item.SubItems.Add(dr["VSAAT"].ToString());
                item.SubItems.Add(dr["FIYAT"].ToString());
                listView1.Items.Add(item);
            }
            baglanti.Close(); //Veritabani baglantisi kapatilir.
            listView1.Show(); //Daha onceden gizlenmis olan ListView, hazir hali ile ekrana getirilir.
        }

        private void Seferler_Load_1(object sender, EventArgs e)
        {
            listView1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TARIH KRITERI BAZ ALARAK LISTELEME
            listView1.Items.Clear(); //Daha onceki ListView ogeleri temizlenir.
            listView1.Refresh(); //ListView yenilenir.
            baglanti.Open(); //Veritabanı baglantisi acilir.
            SqlCommand liste = new SqlCommand();  //Liste adinda bir veritabani komutu olusturulur.
            liste.Connection = baglanti; //Olusturulan komutun baglanti adindaki veritabani baglantisi ile bag kurulur.
            liste.CommandText = "Select *From Seferbilgi";  //Ogeleri cagirmak icin komut satirinda Select'e ve cagrilacak ogeleri barindiran Table'a yer verilir.
            SqlDataReader dr; //dr adında bir Sql veri okuyucusu oluşturulur.
            dr = liste.ExecuteReader(); //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
            string secilitarih = "";   //secilitarih adinda bos bir string degisken olusturulur.
            string timepicker = dateTimePicker1.Text.ToString(); //DateTimePicker uzerindeki tarihi tutacak timepicker adinda bir string degisken olusturulur.
            char[] secilitarihayikla = timepicker.ToCharArray(); //timepicker ogelerinin tamami char dizisine donusturulur ve secilitarihayikla dizisinde tutulur.
            secilitarih = secilitarihayikla[0] + "" + secilitarihayikla[1]; //secilitarihayikla char dizisinin ilk iki ogesi (gun bilgisinin alinmasi icin) secilitarih degiskenine atanir. 
            secilitarih = secilitarih.Trim(); //secilitarih degiskeninde herhangi bir bosluk var ise kaldirilir (gun bilgisinin tek sayi olmasi durumu icin)
            while (dr.Read()) //Satir satir veri okuma islemi baslatilir.
            {
                string tarih = tarihayikla(dr["TARIH"].ToString()); //tarih ayikla fonksiyonu ile veritabanindaki tarih ogelerinin gun bilgisi tarih degiskenine atanir.
                if (tarih.Trim() == secilitarih.Trim()) //tarih ve secilitarihin eslesip eslesmedigi kontrol edilir.
                {
                    ListViewItem item = new ListViewItem(dr["ID"].ToString()); //eslesen ogelere ait diger veriler tek tek ListView'e eklenir.
                    item.SubItems.Add(dr["TARIH"].ToString());
                    item.SubItems.Add(dr["KALKIS"].ToString());
                    item.SubItems.Add(dr["VARIS"].ToString());
                    item.SubItems.Add(dr["KOLTUK"].ToString() + "/51");
                    item.SubItems.Add(dr["KSAAT"].ToString());
                    item.SubItems.Add(dr["VSAAT"].ToString());
                    item.SubItems.Add(dr["FIYAT"].ToString());
                    listView1.Items.Add(item);
                }
            }
            baglanti.Close(); //Veritabani baglantisi kapatilir.
            listView1.Show(); //Daha onceden gizlenmis olan ListView, hazir haliyle ekrana getirilir.
        }
    }
}
