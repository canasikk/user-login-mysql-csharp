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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private static Settings pub = new Settings();
        static MySqlConnection db = new MySqlConnection(pub.__DBString);
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adtr;
        MySqlDataReader dr;
        DataSet ds;
        Random r = new Random();
        int x, y;
        bool suruklenmedurumu = false;
        Point ilkkonum;
       


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register reg = new Register();
            this.Hide();
            reg.Show();
        }

    

        private void Login_Load(object sender, EventArgs e)
        {
            hatirla.Checked = Properties.Settings.Default.kontrolcheck;
            if (hatirla.Checked == true)
            {
                username.Text = Properties.Settings.Default.kullanici;
                password.Text = Properties.Settings.Default.sifre;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.kullanici = null;
                Properties.Settings.Default.sifre = null;
                Properties.Settings.Default.kontrolcheck = false;
                Properties.Settings.Default.Save();
            }
         //if (Properties.Settings.Default.oturum == 1)
        // { MessageBox.Show(pub.hataKodlari(17),"Dikkat!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation); Application.Exit(); }
        }


        void UserLogin(String UserName, String Password)
        {

            try {
                Ping ping = new Ping();
                PingReply pingStatus = ping.Send(IPAddress.Parse("216.58.209.14"));
                if (pingStatus.Status == IPStatus.Success)
                {
                    db.Close();
                    db.Open();
                    cmd = new MySqlCommand("Select *From kullanicilar where kullaniciadi  ='" + UserName + "'", db);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (UserName.ToString() == dr["kullaniciadi"].ToString())
                        {
                            if (Password.ToString() == dr["sifre"].ToString())
                            {

                                Properties.Settings.Default.isim = dr["email"].ToString();
                                Properties.Settings.Default.id = Convert.ToInt16(dr["id"]);
                                Properties.Settings.Default.Save();
                                db.Close();
                                db.Open();
                                cmd = new MySqlCommand("Update kullanicilar SET oturum = 1 where id  ='" + Properties.Settings.Default.id + "'", db);
                                cmd.ExecuteReader();
                                Properties.Settings.Default.oturum = 1;
                                Properties.Settings.Default.Save();
                                db.Close();

                                if (hatirla.Checked == true)
                                {
                                    Properties.Settings.Default.kullanici = UserName.ToString();
                                    Properties.Settings.Default.sifre = Password.ToString();
                                    Properties.Settings.Default.kontrolcheck = true;
                                    Properties.Settings.Default.Save();
                                }
                                else
                                {
                                    Properties.Settings.Default.kullanici = null;
                                    Properties.Settings.Default.sifre = null;
                                    Properties.Settings.Default.kontrolcheck = false;
                                    Properties.Settings.Default.Save();
                                }

                                Loading yukle = new Loading();
                                yukle.Show();
                               // Dashboard dash = new Dashboard();
                                //dash.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show(pub.hataKodlari(0), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(pub.hataKodlari(1), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(pub.hataKodlari(2), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(pub.hataKodlari(3), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Ping ping = new Ping();
                PingReply pingStatus = ping.Send(IPAddress.Parse("216.58.209.14"));
                if (pingStatus.Status == IPStatus.Success)
                {
                    MessageBox.Show(pub.hataKodlari(3), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                }
                else
                {
                    MessageBox.Show(pub.hataKodlari(-1), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                 }
        
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            this.Hide();
            reg.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.oturum = 0;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (username.Text.Length < 255 || password.Text.Length < 255)
            {
               if(username.Text.Trim() != "" )
               {
                   if(password.Text.Trim() != ""){
                        UserLogin(username.Text.ToString(), password.Text.ToString());
                   }
                   else
                   {
                       MessageBox.Show(pub.hataKodlari(12), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   }
               }else{MessageBox.Show(pub.hataKodlari(9), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              }
            }
            else { MessageBox.Show(pub.hataKodlari(16), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = true; //işlemi burada true diyerek başlatıyoruz.
            this.Cursor = Cursors.SizeAll; //SizeAll yapmamımızın amacı taşırken hoş görüntü vermek için
            ilkkonum = e.Location; //İlk konuma gördüğünüz gibi değerimizi atıyoruz.
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (suruklenmedurumu) // suruklenmedurumu==true ile aynı.
            {
                this.Left = e.X + this.Left - (ilkkonum.X);
                // button.left ile soldan uzaklığını ayarlıyoruz. Yani e.X dediğimizde buton üzerinde mouseun hareket ettiği pixeli alacağız + butonun soldan uzaklığını ekliyoruz son olarakta ilk mouseın tıklandığı alanı çıkarıyoruz yoksa butonun en solunda olur mouse imleci. Alttakide aynı şekilde Y koordinati için geçerli
                this.Top = e.Y + this.Top - (ilkkonum.Y);
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            suruklenmedurumu = false; //Sol tuştan elimizi çektik artık yani sürükle işlemi bitti.
            this.Cursor = Cursors.Default; //İmlecimiz(Cursor) default değerini alıyor.
        }

         
    }
}