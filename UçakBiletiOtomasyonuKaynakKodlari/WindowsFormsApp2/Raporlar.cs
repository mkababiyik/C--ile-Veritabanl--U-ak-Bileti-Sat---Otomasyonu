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
    public partial class Raporlar : Form
    {
        public Raporlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-FBH5KQK\\SQLEXPRESS;Initial Catalog=UcakBiletiSatisOtomasyonuVeritabani;Integrated Security=True");

        public string kullanici;
        private void button8_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            anaMenu.kullanici = kullanici;
            anaMenu.Show();
            this.Hide();
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();    //Veritabanı baglantisi olusturulur

            SqlCommand liste = new SqlCommand(); 
            liste.Connection = baglanti;
            liste.CommandText = "Select *From Raporlar";    //Raporlar Table'ının baz alındığı Select kullanarak bir çağırma komut satırı oluşturulur.
            SqlDataReader dr; 
            dr = liste.ExecuteReader(); //Bir veya birden fazla Sql satırına ulaşmak için ExecuteReader kullanılır.
            while (dr.Read()) //Veri okuması sağlandıkça...
            {
                    ListViewItem item = new ListViewItem(dr["PERSONEL"].ToString()); 
                    item.SubItems.Add(dr["ISLEM"].ToString()); //Listview ogelerine rapor bilgileri yerlestirilir.
                    item.SubItems.Add(dr["SAAT"].ToString());
                    listView1.Items.Add(item);
                
            }
            baglanti.Close(); //Baglanti sonlandirilir.
        }
    }
}
