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
    public partial class Biletsatis : Form
    {
        public Biletsatis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        public string kullanici;
        string tarihKarsilastir(string t1,string t2)
        {
            string[] t1Ogeleri = t1.Split(' ');
            string[] t2Ogeleri = t2.Split(' ');

            if (Convert.ToInt32(t1Ogeleri[2]) > Convert.ToInt32(t2Ogeleri[2]))
            {
                return "buyuk";
            }
            else if(Convert.ToInt32(t1Ogeleri[2]) == Convert.ToInt32(t2Ogeleri[2]))
            {
                if (ay(t1Ogeleri[1]) > ay(t2Ogeleri[1]))
                {
                    return "buyuk";
                }
                else if (ay(t1Ogeleri[1]) == ay(t2Ogeleri[1]))
                {
                    if (Convert.ToInt32(t1Ogeleri[0]) >Convert.ToInt32( t2Ogeleri[0]))
                    {
                        return "buyuk";
                    }
                    else if (Convert.ToInt32(t1Ogeleri[0]) == Convert.ToInt32(t2Ogeleri[0]))
                    {
                        return "esit";
                    }
                    else
                    {
                        return "kucuk";
                    }
                }
                else
                {
                    return "kucuk";
                }
            }
            else
            {
                return "kucuk";
            }
        }
        int ay(string x)
        {
            if (x == "Ocak")
            {
                return 1;
            }
            else if (x == "Şubat")
            {
                return 2;
            }
            else if (x == "Mart")
            {
                return 3;
            }
            else if (x == "Nisan")
            {
                return 4;
            }
            else if (x == "Mayıs")
            {
                return 5;
            }
            else if (x == "Haziran")
            {
                return 6;
            }
            else if (x == "Temmuz")
            {
                return 7;
            }
            else if (x == "Ağustos")
            {
                return 8;
            }
            else if (x == "Eylül")
            {
                return 9;
            }
            else if (x == "Ekim")
            {
                return 10;
            }
            else if (x == "Kasım")
            {
                return 11;
            }
            else if (x == "Aralık")
            {
                return 12;
            }
            else
            {
                return 0;
            }
        }
        string saatKarsilastir(string s1, string s2)
        {
            string[] s1Ogeler = s1.Split(':');
            string[] s2Ogeler = s2.Split(':');
            int saat1 = Int32.Parse(s1Ogeler[0]);
            int saat2 = Int32.Parse(s2Ogeler[0]);
            int dakika1= Int32.Parse(s1Ogeler[1]);
            int dakika2= Int32.Parse(s2Ogeler[1]);
            if (saat1 > saat2)
            {
                return "buyuk";
            }
            else if (saat1 == saat2)
            {
                if (dakika1 > dakika2)
                {
                    return "buyuk";
                }
                else
                {
                    return "kucuk veya esit";
                }
            }
            else
            {
                return "kucuk veya esit";
            }
        }
        private void Listelebutton_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            string[] sID = new string[500];
            string[] sTarih = new string[500];
            string[] sKalkis = new string[500];
            string[] sVaris = new string[500];              //Veritabanından alınacak bilgiler üzerinde daha rahat işlem yapılabilmesi işin her oge icin farklı bir dizi olusturulmustur.
            string[] sKSaat = new string[500];
            string[] sVSaat = new string[500];
            string[] sFiyat = new string[500];

            listView1.Items.Clear();
            listView1.Refresh();               //Liste ogeleri temizlenmis ve yenilenmistir.
            baglanti.Open();  
            SqlCommand liste = new SqlCommand();
            liste.Connection = baglanti;                        //Veritabanı baglantisi acilir ve cagirma islemi icin Select ve Table adi iceren komut satirinin baglantisi komutla saglanir.
            liste.CommandText = "Select *From Seferbilgi";
            SqlDataReader dr;
            dr = liste.ExecuteReader();  //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
            int sayac = 0;
            while (dr.Read())
            {
                sID[sayac] = dr["ID"].ToString();
                sTarih[sayac] = dr["TARIH"].ToString();
                sKalkis[sayac] = dr["KALKIS"].ToString(); 
                sVaris[sayac] = dr["VARIS"].ToString();      //Ogeler sayac yardımıyla tek tek dizilerine aktarilir
                sKSaat[sayac] = dr["KSAAT"].ToString();
                sVSaat[sayac]= dr["VSAAT"].ToString();
                sFiyat[sayac]= dr["FIYAT"].ToString();
                sayac += 1;
            }
            
            baglanti.Close();
            if (checkBox1.Checked == true)  //Tek yon secimi yapilmis ise,
            {
                int sayac2 = 0;
                MessageBox.Show("Listede verilen fiyat bilgisi yetişkine ait koltuk fiyatıdır.\nUygun sefere ait ID bilgisini seçerek ödeme ekranına ilerleyebilirsiniz.", "Bilgilendirme");
                for (int i = 0; i < sayac; i++)
                {
                    if (sKalkis[i] == comboBox1.Text && sVaris[i] == comboBox2.Text && sTarih[i] == dateTimePicker1.Text) //Kalkis ve varis bilgisinin dizilerle eslesmesinin saglanması
                        //amaclanır. Ayrıca secilen tarihle de eslesme saglanmaya calisilmaktadir.
                    {
                        string direktID = sID[i];;
                        comboBox3.Items.Add(direktID);
                        ListViewItem item = new ListViewItem("Direkt");
                        item.SubItems.Add(sID[i]);  
                        item.SubItems.Add(sKSaat[i]);                   //Eslesme saglanmasinin ardindan bilgiler tek tek ListView ekranina eklenmektedir.
                        item.SubItems.Add(sVSaat[i]);
                        item.SubItems.Add(sFiyat[i]);
                        listView1.Items.Add(item);
                        sayac2 += 1;
                    }

                    else if (sKalkis[i] == comboBox1.Text && sVaris[i] != comboBox2.Text && sTarih[i] == dateTimePicker1.Text)
                        //Bir seferin kalkis bilgisi ve tarhi eslesiyor fakat varisi eslesmiyorsa...
                    {
                        for (int j = 0; j < sayac; j++)
                        {
                            if (sVaris[i] == sKalkis[j] && sVaris[j] == comboBox2.Text && tarihKarsilastir(sTarih[i], sTarih[j]) == "esit") 
                                //Kalkis seferine ait varis seferinin eslesmis oldugu baska bir kalkıs seferinin varisi ayni ise, aktarmali ucus islemine devam edilir. 
                            {
                                if (saatKarsilastir(sKSaat[j], sVSaat[i]) == "buyuk")   //2. seferin kalkis saatinin birinci seferin varisindan buyuk olmasına bakılır.
                                {
                                    
                                    string aktarmaID = sID[i] + " "+ sID[j];
                                    comboBox3.Items.Add(aktarmaID);
                                    int aktarmaFiyat = Convert.ToInt32(sFiyat[i]) + Convert.ToInt32(sFiyat[j]); 
                                    ListViewItem item = new ListViewItem("Aktarmalı");         //Kosullu saglayan ogeler listeye tek tek yerlestirilir.
                                    item.SubItems.Add(aktarmaID);
                                    item.SubItems.Add(sKSaat[i]);
                                    item.SubItems.Add(sVSaat[j]);
                                    item.SubItems.Add(aktarmaFiyat.ToString());
                                    listView1.Items.Add(item);
                                    sayac2 += 1;
                                }
                            }
                        }
                    }
                }
                if (sayac2 == 0)
                {
                    MessageBox.Show("Kriterlere uygun sefer bulunamadı.\nTüm seferleri Ana Menü>Güncel Seferler üzerinden görebilirsiniz!", "Bilgilendirme");
                    Biletsatis biletsatis = new Biletsatis();
                    biletsatis.kullanici = kullanici;            //veritabanından okunulan her bilgi icin sayac artmaktaydı, 0 olması eslesen bir bilgi bulunmadıgı anlamina gelmektedir.
                    biletsatis.Show();
                    this.Hide();
                }
            }
            else if (checkBox1.Checked == false) //Gidiş ve Dönüş işlemi yapılıyor ise...
            {
                if (tarihKarsilastir(dateTimePicker2.Text, dateTimePicker1.Text) == "buyuk") //2. işlem tarihinin birincisinden büyük olup olmadığı kontrol edilir.
                {
                    int sayac2 = 0;
                    MessageBox.Show("Listede verilen fiyat bilgisi yetişkine ait koltuk fiyatıdır.\nGiden uçak bilgileri listelenecek olup " +
                        "uygun sefere ait ID bilgisini seçerek dönen uçak seçimine ilerleyebilirsiniz.", "Bilgilendirme");
                    for (int i = 0; i < sayac; i++)
                    {
                        if (sKalkis[i] == comboBox1.Text && sVaris[i] == comboBox2.Text && sTarih[i] == dateTimePicker1.Text)
                        //Kalkis ve varis bilgisinin dizilerle eslesmesinin saglanması
                        //amaclanır. Ayrıca secilen tarihle de eslesme saglanmaya calisilmaktadir.
                        {
                            string direktID = sID[i];
                            comboBox3.Items.Add(direktID);
                            ListViewItem item = new ListViewItem("Direkt"); //Eslesme saglanmasinin ardindan bilgiler tek tek ListView ekranina eklenmektedir.
                            item.SubItems.Add(sID[i]);
                            item.SubItems.Add(sKSaat[i]);       
                            item.SubItems.Add(sVSaat[i]);
                            item.SubItems.Add(sFiyat[i]);
                            listView1.Items.Add(item);
                            sayac2 += 1;
                        }

                        else if (sKalkis[i] == comboBox1.Text && sVaris[i] != comboBox2.Text && checkBox1.Checked == false && sTarih[i] == dateTimePicker1.Text)
                            //Kalkis ve varis bilgisinin dizilerle eslesmesinin saglanması
                        //amaclanır. Ayrıca secilen tarihle de eslesme saglanmaya calisilmaktadir.
                        {
                            for (int j = 0; j < sayac; j++)
                            {
                                if (sVaris[i] == sKalkis[j] && sVaris[j] == comboBox2.Text && tarihKarsilastir(sTarih[i], sTarih[j]) == "esit")
                                //Kalkis seferine ait varis seferinin eslesmis oldugu baska bir kalkıs seferinin varisi ayni ise, aktarmali ucus islemine devam edilir. 
                                {
                                    if (saatKarsilastir(sKSaat[j], sVSaat[i]) == "buyuk") //2. işlem saatinin birincisinden büyük olup olmadığı kontrol edilir.
                                    {
                                        string aktarmaID = sID[i] +" "+ sID[j];
                                        comboBox3.Items.Add(aktarmaID);
                                        int aktarmaFiyat = Convert.ToInt32(sFiyat[i]) + Convert.ToInt32(sFiyat[j]);
                                        ListViewItem item = new ListViewItem("Aktarmalı");
                                        item.SubItems.Add(aktarmaID);
                                        item.SubItems.Add(sKSaat[i]);                   //Kosullu saglayan ogeler listeye tek tek yerlestirilir.
                                        item.SubItems.Add(sVSaat[j]);
                                        item.SubItems.Add(aktarmaFiyat.ToString());
                                        listView1.Items.Add(item);
                                        sayac2 += 1;
                                    }
                                }
                            }
                        }                       
                    }
                    if (sayac2 == 0)
                    {
                        MessageBox.Show("Kriterlere uygun sefer bulunamadı.\nTüm seferleri Ana Menü>Güncel Seferler üzerinden kontrol edebilirsiniz!", "Bilgilendirme");
                        Biletsatis biletsatis = new Biletsatis();
                        biletsatis.Show();       //veritabanından okunulan her bilgi icin sayac2 degeri artmaktaydı, 0 olması eslesen bir bilgi bulunmadıgı anlamina gelmektedir.
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Dönüş tarihi gidiş tarihinden sonra olmalıdır!", "Bilgilendirme");
                }
            }

            baglanti.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePicker2.Hide();
                label6.Hide();
                button1.Text = "Ödeme Ekranına İlerle";
            }
            else if (checkBox1.Checked == false)
            {
                dateTimePicker2.Show();
                label6.Show();
                button1.Text = "Dönüş Seferi Seçimi";
            }
        }
        public string tumSeferler="";

        private void button1_Click(object sender, EventArgs e)
        {
            tumSeferler += comboBox3.Text + " ";
            if (button1.Text== "Dönüş Seferi Seçimi"&&comboBox3.Text!="")
            {
                comboBox3.Items.Clear();
                listView1.Items.Clear();
                string[] sID = new string[100];
                string[] sTarih = new string[100];
                string[] sKalkis = new string[100];
                string[] sVaris = new string[100];
                string[] sKSaat = new string[100];
                string[] sVSaat = new string[100];
                string[] sFiyat = new string[100];
                listView1.Items.Clear();
                listView1.Refresh();
                baglanti.Open();
                SqlCommand liste = new SqlCommand();
                liste.Connection = baglanti;
                liste.CommandText = "Select *From Seferbilgi";
                SqlDataReader dr;
                dr = liste.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    sID[sayac] = dr["ID"].ToString();
                    sTarih[sayac] = dr["TARIH"].ToString();
                    sKalkis[sayac] = dr["KALKIS"].ToString();
                    sVaris[sayac] = dr["VARIS"].ToString();
                    sKSaat[sayac] = dr["KSAAT"].ToString();
                    sVSaat[sayac] = dr["VSAAT"].ToString();
                    sFiyat[sayac] = dr["FIYAT"].ToString();
                    sayac += 1;
                }
                baglanti.Close();

                int sayac2 = 0;
                for (int i = 0; i < sayac; i++)
                {
                    if (sKalkis[i] == comboBox2.Text && sVaris[i] == comboBox1.Text && sTarih[i] == dateTimePicker2.Text)
                    {
                        string direktID = sID[i];
                        comboBox3.Items.Add(direktID);
                        ListViewItem item = new ListViewItem("Direkt");
                        item.SubItems.Add(sID[i]);
                        item.SubItems.Add(sKSaat[i]);
                        item.SubItems.Add(sVSaat[i]);
                        item.SubItems.Add(sFiyat[i]);
                        listView1.Items.Add(item);
                        sayac2 += 1;
                    }

                    else if (sKalkis[i] == comboBox2.Text && sVaris[i] != comboBox1.Text && checkBox1.Checked == false && sTarih[i] == dateTimePicker2.Text)
                    {
                        for (int j = 0; j < sayac; j++)
                        {
                            if (sVaris[i] == sKalkis[j] && sVaris[j] == comboBox1.Text && tarihKarsilastir(sTarih[i], sTarih[j]) == "esit")
                            {
                                if (saatKarsilastir(sKSaat[j], sVSaat[i]) == "buyuk")
                                {
                                    string aktarmaID = sID[i] +" " +sID[j];
                                    comboBox3.Items.Add(aktarmaID);
                                    int aktarmaFiyat = Convert.ToInt32(sFiyat[i]) + Convert.ToInt32(sFiyat[j]);
                                    ListViewItem item = new ListViewItem("Aktarmalı");
                                    item.SubItems.Add(aktarmaID);
                                    item.SubItems.Add(sKSaat[j]);
                                    item.SubItems.Add(sVSaat[j]);
                                    item.SubItems.Add(aktarmaFiyat.ToString());
                                    listView1.Items.Add(item);
                                    sayac2 += 1;
                                }
                            }
                        }
                    }
                }
                button1.Text = "Ödeme Ekranına İlerle";
                if (sayac2 == 0)
                {
                    MessageBox.Show("Kriterlere uygun dönüş seferi bulunamadı.\nTüm seferleri Ana Menü>Güncel Seferler üzerinden görebilirsiniz!", "Bilgilendirme");
                    Biletsatis biletsatis = new Biletsatis();
                    biletsatis.kullanici = kullanici;
                    biletsatis.Show();
                    this.Hide();
                }
            }
            else if(comboBox3.Text!="")
            {
                OdemeEkrani odemeekrani = new OdemeEkrani();
                string[] seferler = tumSeferler.Split(' ');
                odemeekrani.kullanici = kullanici;
                odemeekrani.seferler = seferler;
                odemeekrani.tseferler = tumSeferler;
                odemeekrani.nereden = comboBox1.Text;
                odemeekrani.nereye = comboBox2.Text;
                if (checkBox1.Checked == false)
                {
                    odemeekrani.sefertipi = "Gidiş-Dönüş";
                }
                else if(checkBox1.Checked == true)
                {
                    odemeekrani.sefertipi = "Tek Yön";
                }
                odemeekrani.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen devam etmeden önce sefer seçimi yapınız!");
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu form3 = new AnaMenu();
            form3.kullanici = kullanici;
            form3.Show();
            this.Hide();
        }
       
        private void Biletsatis_Load(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                button1.Text = ("Dönüş Seferi Seçimi");
            }
            else
            {
                button1.Text = ("Ödeme Ekranına İlerle");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
