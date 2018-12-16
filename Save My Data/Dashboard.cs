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

namespace Save_My_Data
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        static Settings pub = new Settings();
        public MySqlConnection db = new MySqlConnection(pub.__DBString);
        public MySqlCommand cmd = new MySqlCommand();
        public MySqlDataAdapter adtr;
        public MySqlDataReader dr;
        public DataSet ds;
        public int kid = Properties.Settings.Default.id;
       
        private void button1_Click(object sender, EventArgs e)
        {
            db.Close();
            db.Open();
            cmd = new MySqlCommand("Insert Into bilgiler(kid,bir,iki) Values ('" + kid + "','" + textBox1.Text.Trim() + "','" + textBox2.Text.Trim() + "')", db);
            object sonuc = null;
            sonuc = cmd.ExecuteNonQuery();
            db.Close();
            if (sonuc != null)
            {
                MessageBox.Show("Sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                db.Close();
                veriListele();
            }
        }
        void veriListele()
        {
            dataGridView1.DataSource = null;
            db.Close();
            MySqlCommand mycom = new MySqlCommand("Select id,bir,iki From bilgiler Where kid=" + kid.ToString(), db);

            MySqlDataAdapter adap = new MySqlDataAdapter("Select id,bir,iki From bilgiler Where kid=" + kid.ToString(), db);

            DataTable dt = new DataTable();

            adap.Fill(dt);

            dataGridView1.DataSource = dt;

            db.Open();

            MySqlDataReader mydr = mycom.ExecuteReader();

            db.Close();
        }


     
        private void Dashboard_Load(object sender, EventArgs e)
        {
          
            lblVer.Text = pub.__APPVERSION;
            lblName.Text = Properties.Settings.Default.isim;
           // dataGridView1.Columns["id"].Visible = false;

            dataGridView1.RowHeadersVisible = false; //Gizlenmesini sağlar 

            dataGridView1.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSteelBlue;

            veriListele();
           
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
                    db.Close();
                    cmd = new MySqlCommand();
                    db.Open();
                    cmd.Connection = db;
                    cmd.CommandText = "DELETE from bilgiler where id=" + dataGridView1.CurrentRow.Cells[0].Value;
                    cmd.ExecuteNonQuery();
                    db.Close();

                    veriListele();
        }

    

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            db.Close();
            db.Open();
            cmd = new MySqlCommand("Update kullanicilar SET oturum = 0 where id  ='" + Properties.Settings.Default.id + "'", db);
            cmd.ExecuteReader();
            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.oturum = 0;
            Properties.Settings.Default.Save();
            db.Close();
            Application.Exit();
        }
    }
}
 