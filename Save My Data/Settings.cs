using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Save_My_Data
{
    class Settings
    {
        public static String __SERVER = "localhost";
        public static String __DATABASE = "veriler";
        public static String __USERNAME = "root";
        public static String __PASSWORD = "";

        public String __DBString = String.Format("SERVER={0};DATABASE={1};UID={2};PWD={3}", __SERVER, __DATABASE, __USERNAME, __PASSWORD);

        public String __APPVERSION = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public String __SAAT = DateTime.Now.ToLongTimeString();
        public String __TARIH = DateTime.Now.ToShortDateString();


        public String hataMesaji;
        public String hataKodlari(int i)
        {
            if (i == 0) { hataMesaji = "Şifreniz eksik veya hatalı"; }
            else if (i == 1) { hataMesaji = "Kullanıcı adınız eksik veya hatalı"; }
            else if (i == 2) { hataMesaji = "Böyle kullanıcı bulunmamaktadır"; }
            else if (i == 3) { hataMesaji = "İnternet bağlantınızı kontrol ediniz."; }
            else if (i == 4) { hataMesaji = "Hesabınız oluşturuldu"; }
            else if (i == 5) { hataMesaji = "Hesabınız oluşturulamadı"; }
            else if (i == 6) { hataMesaji = "Doğrulama kodu hatalı"; }
            else if (i == 7) { hataMesaji = "Lütfen gerekli yerleri doldurunuz"; }
            else if (i == 8) { hataMesaji = "Şifreler uyuşmamaktadır"; }
            else if (i == 9) { hataMesaji = "Kullanıcı adı boş olamaz"; }
            else if (i == 10) { hataMesaji = "Adınız boş olamaz"; }
            else if (i == 11) { hataMesaji = "Gizli Yanıt boş olamaz"; }
            else if (i == 12) { hataMesaji = "Şifre boş olamaz"; }
            else if (i == 13) { hataMesaji = "Doğrulama kodu boş olamaz"; }
            else if (i == 14) { hataMesaji = "Hatalı doğrulama kodu "; }
            else if (i == 15) { hataMesaji = "Bu bilgilere ait hesap kullanılmaktadır"; }
            else if (i == 16) { hataMesaji = "Karakter sınırını aştınız."; }
            else if (i == 17) { hataMesaji = "Oturum açık."; }



            else { hataMesaji = "Bilinmeyen hata meydana geldi"; }
            return hataMesaji;
        }
    }
}
 