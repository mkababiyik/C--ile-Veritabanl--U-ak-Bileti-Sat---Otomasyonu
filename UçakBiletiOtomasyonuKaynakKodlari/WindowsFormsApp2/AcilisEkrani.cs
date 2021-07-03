using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp2
{
    public partial class AcilisEkrani : Form
    {
        public AcilisEkrani()
        {
            InitializeComponent();

            timer1.Interval = 50;
            timer1.Enabled=true;         
            
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value += 1;
            }
            else if (progressBar1.Value == 100)
            {
                GirisEkrani form1 = new GirisEkrani();
                form1.Show();
                this.Hide();
                timer1.Enabled = false;
            }
        }
    }
}
