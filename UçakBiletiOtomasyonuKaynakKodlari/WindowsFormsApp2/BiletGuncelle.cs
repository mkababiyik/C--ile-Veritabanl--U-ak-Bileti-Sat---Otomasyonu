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
    public partial class BiletGuncelle : Form
    {
        public BiletGuncelle()
        {
            InitializeComponent();
        }
        public string kullanici;

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        private void button3_Click(object sender, EventArgs e)
        {
            string PNR = textBox1.Text; //Alınan PNR bilgisi PNR degiskeninde tutulur.
            if (!string.IsNullOrEmpty(PNR))
            {
                string sql = "update YolcuBilgileri set AD= '" + textBox3.Text +    
                    "',TCNO = '" + textBox5.Text + 
                    "', TELEFON = '" + maskedTextBox1.Text +
                    "', CINSIYET = '" + comboBox1.Text +
                    "', MAIL = '" + textBox6.Text +
                    "', HES = '" + textBox7.Text +
                    "', YAS = '" + comboBox5.Text +
                    "' where PNR = '" + PNR + "' ";
                //PNR nin eslendigi yerde geri kalan verilerin guncellenebilmesi icin update iceren bir komut satırı olusturulur.
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                //komutun baglantisi saglanir.
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();        //Veri baglantisi acik degil ise acilir.
                }
                cmd.ExecuteNonQuery();
                //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                baglanti.Close();

                string an = DateTime.Now.ToString(); //An'a ait tarih ve saat bilgisi an degiskeninde tutulur.
                baglanti.Open();    //Yeniden islem yapmak icin baglanti acilir.
                SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti);   //Yapilan islemin raporunun tutulmasi icin
                //Insert ve Table adi iceren komut satiriyla ve baglantisiyla Sql komutu olusturulur.
                command.Parameters.AddWithValue("@p1", kullanici);  
                command.Parameters.AddWithValue("@p2", "Bilet guncelleme islemi gerceklestirildi"); //Veriler girilir.
                command.Parameters.AddWithValue("@p3", an);
                command.ExecuteNonQuery();
                //Degerleri tekrar tekrar dondurmeden ekleme cikarma islemleri yapmak icin ExecuteNonQuery kullanilir.
                baglanti.Close(); //Baglanti sonlandirilir.

                MessageBox.Show("Güncelleme tamamlandı!","Bilgilendirme");
                textBox3.Clear();textBox5.Clear();maskedTextBox1.Clear();comboBox1.Text = "";
                textBox6.Clear();textBox7.Clear();comboBox5.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                cmd2.CommandText = "Select *From YolcuBilgileri";
                SqlDataReader dr;
                dr = cmd2.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    if (dr["PNR"].ToString() == textBox1.Text)
                    {
                        sayac = 1;
                        MessageBox.Show("Girdiginiz PNR ile eşleşme sağlandı!\nGüncellemek istediğiniz bilgileri değiştirerek düzeltme/güncelleme yapabilirsiniz!", "Başarılı!");
                        textBox3.Text = dr["AD"].ToString();
                        textBox5.Text = dr["TCNO"].ToString();
                        maskedTextBox1.Text = dr["TELEFON"].ToString();
                        comboBox1.Text = dr["CINSIYET"].ToString();
                        textBox6.Text = dr["MAIL"].ToString();
                        textBox7.Text= dr["HES"].ToString();
                        comboBox5.Text= dr["YAS"].ToString();
                    }
                }
                baglanti.Close();
                if (sayac == 0)
                {
                    MessageBox.Show("Girdiginiz PNR ile herhangi bir eşleşme sağlanamadı!\nLütfen kontrol edip yeniden deneyiniz!", "Hatalı PNR!");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            anaMenu.kullanici=kullanici;
            anaMenu.Show();
            this.Hide();
        }

        private void BiletGuncelle_Load(object sender, EventArgs e)
        {

        }
    }
}
