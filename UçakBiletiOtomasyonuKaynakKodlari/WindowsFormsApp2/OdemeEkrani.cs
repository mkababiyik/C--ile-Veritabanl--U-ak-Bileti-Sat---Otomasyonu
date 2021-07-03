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
    public partial class OdemeEkrani : Form
    {
        public OdemeEkrani()
        {
            InitializeComponent();
            maskedTextBox3.Text = "";
            textBox10.Hide();
            label12.Hide();
        }
        public string kullanici;

        public string[] seferler;
        public string tseferler;

        public string pnr;
        public string pnrler="";

        public string sefertipi;
        public int sayac = 0;
        public int fiyat = 0;
        public int odenecek;
        public string nereden;
        public string nereye;
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");
       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                maskedTextBox3.Hide();
                label1.Hide();
                textBox10.Show();
                label12.Show();
            }
            else if (checkBox1.Checked == false)
            {
                maskedTextBox3.Show();
                label1.Show();
                textBox10.Hide();
                label12.Hide();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Biletsatis biletsatis = new Biletsatis();
            biletsatis.kullanici=kullanici;
            biletsatis.Show();
            this.Hide();
        }
        int epostaKontrol(string a)
        {
            char[] b = a.ToCharArray();
            for (int i = 0; i < a.Length; i++)
            {
                if (b[i] == '@')
                {
                    return 0;
                }
            }
            return 1;
        }

        int sayiKontrol(MaskedTextBox mx)
        {
            char[] a = mx.Text.ToCharArray();
            int sayac = 0;            
            char[] b = "1234567890".ToCharArray();
            for (int i = 0; i < a.Length; i++)
            {
                for(int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])
                    {
                        sayac += 1;
                    }
                }             
            }
            return sayac;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox8.Text != "" && comboBox2.Text != "" && comboBox3.Text != ""&&(radioButton3.Checked=true||radioButton4.Checked==true))
            {
                if (sayiKontrol(maskedTextBox2) == 16)
                {
                    string an = DateTime.Now.ToString();
                    baglanti.Open();
                    SqlCommand command = new SqlCommand("insert into Raporlar (PERSONEL,ISLEM,SAAT) values (@p1,@p2,@p3)", baglanti);
                    command.Parameters.AddWithValue("@p1", kullanici);
                    command.Parameters.AddWithValue("@p2", "Bilet satis islemi gerceklestirildi");
                    command.Parameters.AddWithValue("@p3", an);
                    command.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Ödeme başarıyla gerçekleşti.\n\nPNR'ler:\n"+pnrler, "Ödeme Tamamlandı!");
                    AnaMenu form3 = new AnaMenu();
                    form3.kullanici = kullanici;
                    form3.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Lütfen ödeme bilgileri kısmını eksiksiz doldurunuz!");
            }
          
        }
        private void OdemeEkrani_Load(object sender, EventArgs e)
        {
            label7.Text = "";
            maskedTextBox3.Hide(); maskedTextBox1.Hide(); textBox10.Hide(); textBox2.Hide(); textBox4.Hide();
            textBox5.Hide(); comboBox5.Hide(); comboBox1.Hide(); checkBox1.Hide(); label1.Hide(); label12.Hide();
            label2.Hide(); label11.Hide(); label3.Hide(); label4.Hide(); label5.Hide(); label6.Hide();label16.Hide();
            label6.Hide(); label7.Hide(); label6.Hide();label10.Hide(); label9.Hide(); label13.Hide(); label14.Hide(); 
            label15.Hide(); button1.Hide(); button3.Hide();
            pictureBox1.Hide(); pictureBox2.Hide(); radioButton3.Hide(); radioButton4.Hide(); maskedTextBox2.Hide();
            textBox7.Hide(); comboBox3.Hide(); comboBox2.Hide(); textBox8.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text != "")
            {
                label1.Show(); label12.Show(); label2.Show(); label11.Show(); label3.Show(); label4.Show(); label5.Show(); label16.Show(); textBox4.Show(); button3.Show();
                maskedTextBox3.Show(); maskedTextBox1.Show(); checkBox1.Show(); textBox2.Show(); comboBox5.Show(); comboBox1.Show(); textBox5.Show();
                
                comboBox4.Hide();button2.Hide();label8.Hide();
                sayac += Int32.Parse(comboBox4.Text);
                pnr = randomDortKarakter();
            }
        }

        string randomDortKarakter()
        {
            Random Rnd = new Random();
            char[] karakterler = "ABCDEFGHIJKLMNOPQRSTUVWXVZ0123456789".ToCharArray();
            string donut = "";
            string[] random = new string[4];           
            for (int i = 0; i < 4; i++)
            {
                random[i] = (karakterler[Rnd.Next(0, karakterler.Length)]).ToString();
                donut += random[i];
            }
            return donut;          
        }
        public int sayac2 = 1;
        private void button3_Click(object sender, EventArgs e)
        {
            int yetiskin = 0;
            int cocuk = 0;          //Yaş bilgisine göre müşterilerin sayıları alınır. (Fiyat hesaplamada kullanılacaktır)
            int ogrenci = 0;

            if (button3.Text=="İlerle")
            {
                if ((maskedTextBox3.Text != "" || textBox10.Text != "") && textBox2.Text != "" && textBox5.Text != ""
                    && maskedTextBox1.Text != "" && comboBox1.SelectedItem != null && textBox4.Text != ""&&comboBox5.SelectedItem!=null)
                {
                    if (sayiKontrol(maskedTextBox3) == 11||textBox10.Text!="")
                    {
                        if (sayiKontrol(maskedTextBox1) == 10)
                        {
                            if (epostaKontrol(textBox4.Text) == 0) //tüm bilgilerin eksiksiz ve kusursuz girilmesinin ardından...
                            {
                                string tckno = maskedTextBox3.Text+textBox10.Text;
                                string ozelpnr = pnr + randomDortKarakter();
                                pnrler+=sayac2.ToString()+". PNR:"+ozelpnr+"\n";
                                baglanti.Open();
                                SqlCommand command = new SqlCommand("insert into YolcuBilgileri (AD,TCNO,TELEFON,CINSIYET,MAIL,HES,PNR,YAS,SEFERID,NEREDEN,NEREYE) " +
                                    "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p11,@p12)", baglanti);    //Insert ve Table adini iceren, parametre ağırlıklı bir komut satırı ile
                                //bilgilerin veritabanına girilmesi saglanir.
                                command.Parameters.AddWithValue("@p1", textBox2.Text);
                                command.Parameters.AddWithValue("@p2", tckno);
                                command.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
                                command.Parameters.AddWithValue("@p4", comboBox1.Text);
                                command.Parameters.AddWithValue("@p5", textBox4.Text);
                                command.Parameters.AddWithValue("@p6", textBox5.Text);
                                command.Parameters.AddWithValue("@p7", ozelpnr);                //Bilgiler tek tek girilir.
                                command.Parameters.AddWithValue("@p8", comboBox5.Text);
                                command.Parameters.AddWithValue("@p9", tseferler);
                                command.Parameters.AddWithValue("@p11", nereden);
                                command.Parameters.AddWithValue("@p12", nereye);
                                command.ExecuteNonQuery();     // Değerleri döndürmeden ekleme çıkarım yapabilmek için kullanılır.
                                baglanti.Close();   
                                sayac--;
                                MessageBox.Show(sayac2 + ". Kisi bilgileri kaydedildi.");
                                sayac2++;
                                maskedTextBox1.Clear(); maskedTextBox3.Clear(); textBox10.Clear();textBox2.Clear();comboBox5.Text = "";textBox4.Clear();textBox5.Clear();

                                for(int i = 0; i < seferler.Length; i++)
                                {
                                    baglanti.Open();
                                    SqlCommand liste = new SqlCommand();
                                    liste.Connection = baglanti;
                                    liste.CommandText = "Select *From Seferbilgi";     //Seferbilgi Table'ı baz alınarak bir komut satırı olusturulur.
                                    SqlDataReader dr;
                                    dr = liste.ExecuteReader();
                                    int koltuk = 0;
                                    while (dr.Read())   //Veri okuması baslatilir.
                                    {   
                                        if (dr["ID"].ToString() == seferler[i]) //Idnin eslesmesi durumunda..
                                        {
                                            koltuk = Int32.Parse(dr["KOLTUK"].ToString());  //Aynı ID'ye sahip koltuk bilgisi koltuk degiskeninde int olarak tutulur.
                                                                                        
                                        }                                    
                                    }
                                    koltuk += 1;    //Satisin gerceklesmesinden dolayi koltukta yer ayirtilir.
                                    baglanti.Close();
                                    baglanti.Open();
                                    string sql = "update Seferbilgi set KOLTUK = '" + koltuk + "' where ID = '" + seferler[i] + "' ";
                                    //Yer ayirtmanin tamamlanması icin ID bilgisinin oldugu yerde update kullanarak yeni koltuk sayisi atanir.
                                    SqlCommand cmd2 = new SqlCommand(sql, baglanti);
                                    cmd2.ExecuteNonQuery(); //Değerleri döndürmeden ekleme çıkarım yapabilmek için kullanılır.
                                    baglanti.Close();
                                    
                                }
                                
                                ;
                            }
                            else
                            {
                                MessageBox.Show("Lütfen gecerli bir E-Posta adresi giriniz!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz!");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir TC kimlik numarası giriniz!");
                    }
                }
                else
                {
                    MessageBox.Show("Lutfen müşteri bilgileri kısmını eksiksiz doldurunuz!");
                }
            }
            else if(button3.Text == "Ödemeye Geç")
            {
                label6.Show(); label7.Show(); radioButton3.Show();
                radioButton4.Show(); pictureBox1.Show(); pictureBox2.Show(); textBox7.Show();textBox8.Show();comboBox2.Show();
                comboBox3.Show();button1.Show(); label9.Show(); button3.Hide(); label13.Show(); label10.Show(); maskedTextBox2.Show();
                label15.Show();label14.Show();

                int drSayac = 0;
                string[] sID = new string[100];
                string[] sFiyat = new string[100];
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "Select *From YolcuBilgileri";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (pnrAyikla(dr["PNR"].ToString()) == pnr)
                    {
                        if(dr["YAS"].ToString()== "0-7 Yaş")
                        {
                            cocuk += 1;
                        }
                        else if(dr["YAS"].ToString() == "Öğrenci")
                        {
                            ogrenci += 1;
                        }
                        else if (dr["YAS"].ToString() == "Yetişkin")
                        {
                            yetiskin += 1;
                        }
                    }                   
                }
                baglanti.Close();
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                cmd2.CommandText = "Select *From Seferbilgi";
                SqlDataReader dr2;
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    sID[drSayac] = dr2["ID"].ToString();
                    sFiyat[drSayac] = dr2["FIYAT"].ToString();
                    drSayac += 1;
                }
                baglanti.Close();

                for(int i = 0; i < seferler.Length; i++)
                {
                    for(int j = 0; j < drSayac; j++)
                    {
                        if (seferler[i] == sID[j])
                        {
                            fiyat += Int32.Parse(sFiyat[j]);
                        }
                    }
                }
            }

            if (sayac == 0)
            {
                maskedTextBox3.Hide(); maskedTextBox1.Hide(); textBox10.Hide(); textBox2.Hide(); textBox4.Hide();
                textBox5.Hide(); comboBox5.Hide(); comboBox1.Hide(); checkBox1.Hide(); label1.Hide(); label12.Hide();
                label2.Hide(); label11.Hide(); label3.Hide(); label4.Hide(); label5.Hide(); label16.Hide();

                button3.Text = "Ödemeye Geç";
            }
            odenecek = (fiyat - fiyat * 1 / 10) * ogrenci + fiyat * yetiskin + fiyat - (fiyat * 2 / 10) * cocuk;
            label7.Text = odenecek.ToString();
        }

        string pnrAyikla(string x)
        {
            string ayrilmis = "";
            char[] y = x.ToCharArray();
            for(int i = 0; i < 4; i++)
            {
                ayrilmis += y[i];
            }
            return ayrilmis;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
