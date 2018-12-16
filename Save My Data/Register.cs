using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace Save_My_Data
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
       
        static Settings pub = new Settings();
        static MySqlConnection db = new MySqlConnection(pub.__DBString);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adtr;
        MySqlDataReader dr;
        DataSet ds;
        Random r = new Random();
        int x, y;
        bool suruklenmedurumu = false;
        Point ilkkonum;

        private void Register_Load(object sender, EventArgs e)
        {
            CaptchaUret();
       

        }
    
        private void registerbtn_Click(object sender, EventArgs e)
        {
            if (username.Text.Trim() != "")
            {
                if (email.Text.Trim() != "")
                {
                    if (security.Text.Trim() != "")
                    {
                        if (password.Text.Trim() != "")
                        {
                            if (repassword.Text.Trim() != "")
                            {
                                if (captcha.Text.Trim() != "")
                                {
                                    if (password.Text == repassword.Text)
                                    {
                                        if (captcha.Text == captchalbl.Text)
                                        {
                                            UserRegister(username.Text, email.Text, security.Text, password.Text, repassword.Text);
                                        }
                                        else
                                        {
                                            MessageBox.Show(pub.hataKodlari(14), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            CaptchaUret();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(pub.hataKodlari(8), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        CaptchaUret();
                                    }

                                }
                                else
                                {
                                    MessageBox.Show(pub.hataKodlari(14), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    CaptchaUret();
                                }
                            }
                            else
                            {
                                MessageBox.Show(pub.hataKodlari(12), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                CaptchaUret();
                            }
                        }
                        else
                        {
                            MessageBox.Show(pub.hataKodlari(12), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CaptchaUret();
                        }
                    }
                    else
                    {
                        MessageBox.Show(pub.hataKodlari(11), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CaptchaUret();
                    }
                }
                else
                {

                    MessageBox.Show(pub.hataKodlari(10), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CaptchaUret();
                }
            }
            else
            {
                MessageBox.Show(pub.hataKodlari(9), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CaptchaUret();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.oturum = 0;
            Properties.Settings.Default.Save();
            Application.Exit();
        }
        private void captchalbl_Click(object sender, EventArgs e)
        {
            CaptchaUret();
        }

        private void UserRegister(String UserName, String EMmail, String Security, String Password, String REepassword)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingStatus = ping.Send(IPAddress.Parse("216.58.209.14"));
                if (pingStatus.Status == IPStatus.Success)
                {
                    db.Close();
                    db.Open();
                    if (Password == REepassword)
                    {
                        cmd = new MySqlCommand("Insert Into kullanicilar(kullaniciadi,email,gizliyanit,sifre) Values ('"
                           + UserName + "','"
                            + EMmail + "','"
                             + Security + "','"
                           + Password + "')", db);
                        object sonuc = null;
                        sonuc = cmd.ExecuteNonQuery();
                        db.Close();
                        if (sonuc != null)
                        {
                            MessageBox.Show(pub.hataKodlari(4), "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            Login logn = new Login();
                            logn.Show();
                            db.Close();
                        }
                        else
                        {
                            MessageBox.Show(pub.hataKodlari(5), "Hata!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            db.Close();
                        }
                    }
                    else { MessageBox.Show(pub.hataKodlari(8), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                }
                else
                {
                    MessageBox.Show(pub.hataKodlari(3), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                Ping ping = new Ping();
                PingReply pingStatus = ping.Send(IPAddress.Parse("216.58.209.14"));
                if (pingStatus.Status == IPStatus.Success)
                {
                    if (UserName.Length > 255 || EMmail.Length > 255 || Security.Length > 255 || Password.Length > 255 || REepassword.Length > 255)
                    {
                        MessageBox.Show(pub.hataKodlari(16), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                    }
                    else
                    {
                        MessageBox.Show(pub.hataKodlari(15), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    }
                else

                { MessageBox.Show(pub.hataKodlari(3), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        void CaptchaUret()
        {
            Random rastgele = new Random();
            string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ0123456789";
            string uret = "";
            captchalbl.Text = "";
            for (int i = 0; i < 6; i++)
            {
                uret += harfler[rastgele.Next(harfler.Length)];
                captchalbl.Text = uret;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int c = 0;
            Point l = this.Location;

            while (c < 50)
            {
                int x = r.Next(1, 10);
                int y = r.Next(1, 10);

                this.Location = new Point(l.X + x, l.Y + y);
                Thread.Sleep(20); // bu çalışan kod parçacığının belirtilen bir süre duraklatılmasını sağlar.
                c++;
            }

            this.Location = l;
            timer1.Stop();
        }

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true; //işlemi burada true diyerek başlatıyoruz.
            this.Cursor = Cursors.SizeAll; //SizeAll yapmamımızın amacı taşırken hoş görüntü vermek için
            ilkkonum = e.Location; //İlk konuma gördüğünüz gibi değerimizi atıyoruz.
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu) // suruklenmedurumu==true ile aynı.
            {
                this.Left = e.X + this.Left - (ilkkonum.X);
                // button.left ile soldan uzaklığını ayarlıyoruz. Yani e.X dediğimizde buton üzerinde mouseun hareket ettiği pixeli alacağız + butonun soldan uzaklığını ekliyoruz son olarakta ilk mouseın tıklandığı alanı çıkarıyoruz yoksa butonun en solunda olur mouse imleci. Alttakide aynı şekilde Y koordinati için geçerli
                this.Top = e.Y + this.Top - (ilkkonum.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false; //Sol tuştan elimizi çektik artık yani sürükle işlemi bitti.
            this.Cursor = Cursors.Default; //İmlecimiz(Cursor) default değerini alıyor.
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Login frm1 = new Login();
            frm1.Show();
            this.Close();
            
        }

        private void username_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}