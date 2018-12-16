using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_My_Data
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        int sayi;
        Random r = new Random();

        private void Loading_Load(object sender, EventArgs e)
        {
            sayi = r.Next(30, 100);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (islem.Value < sayi)
            {
                islem.Value = islem.Value + 1;
            }

            if (islem.Value == sayi)
            {
                timer1.Stop();
                Dashboard dash = new Dashboard();
                dash.Show();
                this.Close();

            }

        }
    }
}