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
    public partial class CheckIn : Form
    {
        public CheckIn()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        public string yolcuPNR="";
        public string kullanici;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                cmd2.CommandText = "Select *From YolcuBilgileri"; //Yolcu bilgileri Table'ı baz alınarak Select ile çağırma işlemi yapılır. 
                SqlDataReader dr;
                dr = cmd2.ExecuteReader(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
                int eslesme = 0; 
                while (dr.Read()) //Verilerin okunması devam ettikce...
                {
                    if (dr["PNR"].ToString() == textBox1.Text) //PNR eslesmesinin saglanması amaclanir.
                    {
                        eslesme = 1;
                        if (dr["KOLTUK"].ToString() == "") //Koltuk secimi yapılmamıs olması onemlidir. (Check-In 2. defa yapılmamalıdır).
                        {
                            CheckIn2 checkIn2 = new CheckIn2(); //Koşullar sağlandığında checkIn2 adinda bir checkin2 form degiskeni olusturulur.
                            checkIn2.seferIDleri = dr["SEFERID"].ToString();    
                            checkIn2.yolcuPNR= dr["PNR"].ToString();        //Yeni forma gerekli bilgiler gonderilir.
                            checkIn2.kullanici = kullanici;
                            checkIn2.Show();    
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Girilen PNR ile eşleşen yolcu için daha önce Check-In Yapılmıştır", "Geçersiz İşlem");
                        }                        
                    }                 
                }
                baglanti.Close();
                if (eslesme == 0)
                {
                    MessageBox.Show("Girilen PNR ile eşleşme sağlanamadı!","Hata!");
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            anaMenu.kullanici = kullanici;
            anaMenu.Show();
            this.Hide();
        }

        private void CheckIn_Load(object sender, EventArgs e)
        {

        }
    }
}