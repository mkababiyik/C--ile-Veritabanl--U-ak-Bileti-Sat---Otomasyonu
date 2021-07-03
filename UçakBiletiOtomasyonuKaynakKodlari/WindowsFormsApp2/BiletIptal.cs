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
    public partial class BiletIptal : Form
    {
        public BiletIptal()
        {
            InitializeComponent();
        }

        public string silinecekKisiPnr = "";
        public string silinecekKisiTc = "";
        public string[] koltuklar = new string[4];
        public string[] seferler=new string[4];
        public string kullanici;

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");
        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu form3 = new AnaMenu();
            form3.kullanici = kullanici;
            form3.Show();
            this.Hide();
        }

        private void BiletIptal_Load(object sender, EventArgs e)
        {
            

        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;              
                cmd.CommandText = "Select *From YolcuBilgileri";            //Veritabanı baglantisi acılır, cmd adında Sql komutu oluşturulur ve bu komuta icerisinde Select ve Table
                SqlDataReader dr;                                           //iceren komut satırı eklenir. dr adında bir Sql veri okuyucusu tanımlanır.
                int varSayac = 0;
                dr = cmd.ExecuteReader();          //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
                while (dr.Read())                  // Veri okuması baslatilir.
                {
                    if (dr["PNR"].ToString() == textBox1.Text)      //Pnr ile eslesme saglanirsa...
                    {                        
                        ad.Show(); seferNo.Show(); nereden.Show(); nereye.Show(); seferTipi.Show();
                        ad.Text = dr["AD"].ToString();
                        seferNo.Text = dr["SEFERID"].ToString();
                        string seferIDleri = dr["SEFERID"].ToString();
                        nereden.Text= dr["NEREDEN"].ToString();     //Bilet goruntusu uzerindeki Label'lara diger veriler aktarilir.
                        nereye.Text= dr["NEREYE"].ToString();
                        seferTipi.Text= dr["SEFERTIPI"].ToString();
                        koltuklar[0] = dr["KOLTUK"].ToString();
                        koltuklar[1] = dr["KOLTUK2"].ToString();
                        koltuklar[2] = dr["KOLTUK3"].ToString();
                        koltuklar[3] = dr["KOLTUK4"].ToString();
                        varSayac += 1;
                        silinecekKisiPnr = dr["PNR"].ToString();    //public olarak tanimlanmis silinecekKisiPnr'ye PNR atanir. (Silinme durumunda islem yapabilmek icin)
                        seferler = seferIDleri.Split(' ');          //Sefer IDleri public olarak tanimlanmis seferler dizisinde ayrı ayrı tutulur.
                        
                    }
                }
                baglanti.Close();
                if (varSayac == 0)
                {
                    MessageBox.Show("Girilen PNR No ile eşleşen bir sonuç bulunmamaktadır!", "HATA");   
                }
            }
            else
            {
                MessageBox.Show("Lütfen öncelikle bir PNR No giriniz!","Hata");
            }
        }
        public string x = "";
        private void button3_Click(object sender, EventArgs e)
        {

            if (silinecekKisiPnr != "")
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                cmd2.CommandText = "Select *From Seferbilgi";           //Veritabanı baglantisi acılır, cmd2 adında Sql komutu oluşturulur ve bu komuta icerisinde Select ve Table
                SqlDataReader dr1;                                      //iceren komut satırı eklenir. dr adında bir Sql veri okuyucusu tanımlanır.
                dr1 = cmd2.ExecuteReader();     //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
                int koltuk0 = 0;
                while (dr1.Read())          // Veri okuması baslatilir.
                {
                    if (dr1["ID"].ToString() == seferler[0])    //ilk sefer ID'si ile veri eslesmesi saglandiginda...
                    {
                        koltuk0 = Int32.Parse(dr1["KOLTUK"].ToString());    
                        koltuk0 = koltuk0 - 1;          //koltuk bilgisi alınır ve bir eksiltilir.
                    }

                }

                baglanti.Close();       //baglanti farkli sekilde yeniden kullanmak icin kapatilir.
                baglanti.Open();
                int sefersayac = 0;
                for(int j = 0; j < seferler.Length; j++)
                {
                    if (seferler[j].Length > 1)
                    {
                        sefersayac++;      //Bos olmayan sefer ogelerini saymak icin tanimlanmistir.
                    }
                }
                string sql = "update Seferbilgi set " + koltuklar[0] +" = '" + "boş" + "',KOLTUK = '" + koltuk0 +"' where ID = '" + seferler[0] + "' ";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                //sql adı verilen update ve Table bilgisinin icerdigi komut satiri olusturulur. IDnin ilk sefer ogesi ile eslesmesi saglanir.
                cmd.ExecuteNonQuery();  //Degerleri dondurmeyen fakat ekleme ve cikarim yapabilme amaciyla ExecuteNonQuery kullanilir.
                baglanti.Close();
                if (sefersayac>1)
                {
                    baglanti.Open();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = baglanti;                     // Veritabanı baglantisi acılır, cmd3 adında Sql komutu oluşturulur ve bu komuta icerisinde Select ve Table
                    cmd3.CommandText = "Select *From Seferbilgi";   //iceren komut satırı eklenir. dr adında bir Sql veri okuyucusu tanımlanır.
                    SqlDataReader dr2;                              
                    dr2 = cmd3.ExecuteReader();                     //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
                    int koltuk=0;
                    while (dr2.Read())        // Veri okuması baslatilir.
                    {       
                        if (dr2["ID"].ToString() == seferler[1])    //Ikinci sefer ID'si ile veri eslesmesi saglandiginda...
                        {
                            koltuk = Int32.Parse(dr2["KOLTUK"].ToString());
                            koltuk = koltuk - 1; //koltuk bilgisi alınır ve bir eksiltilir.
                        }
                       
                    }
                    baglanti.Close(); //baglanti farkli sekilde yeniden kullanmak icin kapatilir.
                    baglanti.Open();

                    string sql2 = "update Seferbilgi set " + koltuklar[1] + " = '" + "boş" + "',KOLTUK = '" + koltuk.ToString() + "' where ID = '" + seferler[1] + "' ";
                    SqlCommand cmd4 = new SqlCommand(sql2, baglanti);
                    //sql2 adı verilen update ve Table bilgisinin icerdigi komut satiri olusturulur. IDnin ilk sefer ogesi ile eslesmesi saglanir.
                    cmd4.ExecuteNonQuery();
                    baglanti.Close();
                }
                if (sefersayac>2)
                {
                    baglanti.Open();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = baglanti;
                    cmd3.CommandText = "Select *From Seferbilgi";
                    SqlDataReader dr2;
                    dr2 = cmd3.ExecuteReader();  //Bir veya birden fazla veri satirini okuyabilmek icin ExecuteReader kullanilir.
                    int koltuk = 0;
                    while (dr2.Read())
                    {
                        if (dr2["ID"].ToString() == seferler[2])
                        {
                            koltuk = Int32.Parse(dr2["KOLTUK"].ToString());
                            koltuk = koltuk - 1;   //koltuk bilgisi alınır ve bir eksiltilir.
                        }

                    }
                    baglanti.Close();
                    baglanti.Open();

                    string sql2 = "update Seferbilgi set " + koltuklar[2] + " = '" + "boş" + "',KOLTUK = '" + koltuk.ToString() + "' where ID = '" + seferler[2] + "' ";
                    SqlCommand cmd4 = new SqlCommand(sql2, baglanti);
                    //sql2 adı verilen update ve Table bilgisinin icerdigi komut satiri olusturulur. IDnin ucuncu sefer ogesi ile eslesmesi saglanir.
                    cmd4.ExecuteNonQuery();
                    baglanti.Close();
                }
                if (sefersayac > 3)
                {
                    baglanti.Open();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = baglanti;
                    cmd3.CommandText = "Select *From Seferbilgi";
                    SqlDataReader dr2;
                    dr2 = cmd3.ExecuteReader();
                    int koltuk = 0;
                    while (dr2.Read())
                    {
                        if (dr2["ID"].ToString() == seferler[2])
                        {
                            koltuk = Int32.Parse(dr2["KOLTUK"].ToString());
                            koltuk = koltuk - 1;
                        }

                    }
                    baglanti.Close();
                    baglanti.Open();

                    string sql2 = "update Seferbilgi set " + koltuklar[3] + " = '" + "boş" + "',KOLTUK = '" + koltuk.ToString() + "' where ID = '" + seferler[3] + "' ";
                    SqlCommand cmd4 = new SqlCommand(sql2, baglanti);
                    cmd4.ExecuteNonQuery();
                    baglanti.Close();
                }
                baglanti.Open();
                SqlCommand sil = new SqlCommand("Delete From YolcuBilgileri Where PNR=@k1", baglanti);
                sil.Parameters.AddWithValue("@k1", silinecekKisiPnr);
                sil.ExecuteNonQuery();
                MessageBox.Show("Seçili bilet silindi!","Bilgilendirme");               
                ad.Text=""; seferNo.Text = ""; nereden.Text = ""; nereye.Text = ""; seferTipi.Text = "";
                baglanti.Close();
                string an = DateTime.Now.ToString();
                baglanti.Open();
                SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti);
                command.Parameters.AddWithValue("@p1", kullanici);
                command.Parameters.AddWithValue("@p2", "Bilet silme islemi gerceklestirildi");
                command.Parameters.AddWithValue("@p3", an);
                command.ExecuteNonQuery();
                baglanti.Close();
            }

            else if(silinecekKisiTc != "")
            {
                baglanti.Open();
                SqlCommand sil = new SqlCommand("Delete From YolcuBilgileri Where TCNO=@k1", baglanti);
                sil.Parameters.AddWithValue("@k1", silinecekKisiTc);
                sil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Seçili Bilet Silindi!","Bilgilendirme");

                ad.Text = ""; seferNo.Text = ""; nereden.Text = ""; nereye.Text = ""; seferTipi.Text = "";
            }          
        }
    }
}
