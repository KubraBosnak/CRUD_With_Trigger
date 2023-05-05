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

namespace CRUD_With_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-I9TTNVF;Initial Catalog=DbTest;Integrated Security=True");


        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLKITAPLAR", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void Sayac()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLSAYAC", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKitapSayisi.Text = dr[0].ToString();

            }
            baglanti.Close();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            Sayac();
        }



        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) VALUES (@P1, @P2, @P3, @P4, @P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtYazar.Text);
            komut.Parameters.AddWithValue("@P3", txtSayfa.Text);
            komut.Parameters.AddWithValue("@P4", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@P5", txtTur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Eklendi.");
            Listele();
            Sayac();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtYayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLKITAPLAR WHERE ID= @P1",baglanti);
            komut.Parameters.AddWithValue("@P1",txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silindi");
            Listele();
            Sayac();
        }
    }
}



//CREATE TRIGGER ARTTIR
//ON TBLKITAPLAR
//AFTER INSERT
//AS
//UPDATE TBLSAYAC SET ADET=ADET+1

//-----------------------------------
//CREATE TRIGGER AZALT
//ON TBLKITAPLAR
//AFTER DELETE
//AS 
//UPDATE TBLSAYAC SET ADET=ADET-1

//-----------------------------------
//CREATE TRIGGER YEDEK
//ON TBLKITAPLAR
//AFTER DELETE
//AS
//DECLARE @KitapAd VARCHAR(50)
//DECLARE @KitapYazar VARCHAR(50)

//SELECT @KitapAd = Ad, @KitapYazar = Yazar from deleted
//INSERT INTO TBLKITAPYEDEK(AD, YAZAR) VALUES(@KitapAd, @KitapYazar)