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

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-2HOS6DT\\SQLEXPRESS;Initial Catalog=TestBenzin;Integrated Security=True");
        void temizle()
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            txtEuroDizelFiyat.Text = "";
            txtKursunsuz97Fiyat.Text = "";
            txtKursunsuzFiyat.Text = "";
            txtYeniProFiyat.Text = "";
            txtGazFiyat.Text = "";
            txtLitre.Text = "";
        }
        void listele()
        {
            //Kursunsuz 95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLBENZIN where PETROLTUR='KURSUNSUZ95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                lblKursunsuz95L.Text = dr[4].ToString();
            }
            baglanti.Close();
            //Kursunsuz 97
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='KURSUNSUZ97'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblKursunsuz97.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                lblKursunsuz97L.Text = dr2[4].ToString();
            }
            baglanti.Close();
            //Euro Dizel 10
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='EURODISEL10'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblEuroDizel.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lblEuroDizelL.Text = dr3[4].ToString();
            }
            baglanti.Close();
            //Yeni pro dizel
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='YENIPRODISEL'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblYeniProDizel.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lblYeniProL.Text = dr4[4].ToString();
            }
            baglanti.Close();
            //Yeni pro dizel
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from TBLBENZIN where PETROLTUR='GAZ'", baglanti);
            SqlDataReader dr5 = komut4.ExecuteReader();
            while (dr5.Read())
            {
                lblGaz.Text = dr5[3].ToString();
                progressBar5.Value = int.Parse(dr5[4].ToString());
                lblGazL.Text = dr5[4].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("select*from TBLKASA", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblKasa.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(lblKursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz95 * litre;
            txtKursunsuzFiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(lblKursunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = kursunsuz97 * litre;
            txtKursunsuz97Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double euroDizel, litre, tutar;
            euroDizel = Convert.ToDouble(lblEuroDizel.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = euroDizel * litre;
            txtEuroDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double proDizel, litre, tutar;
            proDizel = Convert.ToDouble(lblYeniProDizel.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = proDizel * litre;
            txtYeniProFiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(lblGaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = gaz * litre;
            txtGazFiyat.Text = tutar.ToString();
        }

        private void btnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "KURSUNSUZ95");
                komut.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(txtKursunsuzFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txtKursunsuzFiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK=STOK-@p1 where PETROLTUR='KURSUNSUZ95'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Gerçekleşti");
                listele();
                temizle();
            }
            else if (numericUpDown2.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut4 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut4.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut4.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                komut4.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                komut4.Parameters.AddWithValue("@p4", decimal.Parse(txtKursunsuz97Fiyat.Text));
                komut4.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut5 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut5.Parameters.AddWithValue("@p1", decimal.Parse(txtKursunsuz97Fiyat.Text));
                komut5.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut6 = new SqlCommand("update TBLBENZIN set STOK=STOK-@p1 where PETROLTUR='KURSUNSUZ97'", baglanti);
                komut6.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut6.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Gerçekleşti");
                listele();
                temizle();
            }
            else if (numericUpDown3.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut7 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut7.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut7.Parameters.AddWithValue("@p2", "EURODISEL10");
                komut7.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                komut7.Parameters.AddWithValue("@p4", decimal.Parse(txtEuroDizelFiyat.Text));
                komut7.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut8 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut8.Parameters.AddWithValue("@p1", decimal.Parse(txtEuroDizelFiyat.Text));
                komut8.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut9 = new SqlCommand("update TBLBENZIN set STOK=STOK-@p1 where PETROLTUR='EURODISEL10'", baglanti);
                komut9.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                komut9.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Gerçekleşti");
                listele();
                temizle();
            }
            else if (numericUpDown4.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut10.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut10.Parameters.AddWithValue("@p2", "YENIPRODISEL");
                komut10.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                komut10.Parameters.AddWithValue("@p4", decimal.Parse(txtYeniProFiyat.Text));
                komut10.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut11 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut11.Parameters.AddWithValue("@p1", decimal.Parse(txtYeniProFiyat.Text));
                komut11.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut12 = new SqlCommand("update TBLBENZIN set STOK=STOK-@p1 where PETROLTUR='YENIPRODISEL'", baglanti);
                komut12.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                komut12.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut13.Parameters.AddWithValue("@p2", "GAZ");
                komut13.Parameters.AddWithValue("@p3", numericUpDown5.Value);
                komut13.Parameters.AddWithValue("@p4", decimal.Parse(txtGazFiyat.Text));
                komut13.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut14 = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR+@p1", baglanti);
                komut14.Parameters.AddWithValue("@p1", decimal.Parse(txtGazFiyat.Text));
                komut14.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komut15 = new SqlCommand("update TBLBENZIN set STOK=STOK-@p1 where PETROLTUR='GAZ'", baglanti);
                komut15.Parameters.AddWithValue("@p1", numericUpDown5.Value);
                komut15.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Gerçekleşti");
                listele();
                temizle();
            }
        }
        private void btnEklek95_Click(object sender, EventArgs e)
        {
            if (lblKursunsuz95L.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.94);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@p1 where PETROLTUR='KURSUNSUZ95'", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtLitre.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ95");
                komut13.Parameters.AddWithValue("@p3", int.Parse(txtLitre.Text));
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }



        }

        private void btneklek97_Click(object sender, EventArgs e)
        {
            if (lblKursunsuz97L.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.95);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@p1 where PETROLTUR='KURSUNSUZ97'", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtLitre.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "KURSUNSUZ97");
                komut13.Parameters.AddWithValue("@p3", int.Parse(txtLitre.Text));
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }

        private void btnEkleEd10_Click(object sender, EventArgs e)
        {
            if (lblEuroDizelL.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 4.49);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@p1 where PETROLTUR='EURODISEL10'", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtLitre.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "EURODISEL10");
                komut13.Parameters.AddWithValue("@p3", int.Parse(txtLitre.Text));
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }

        private void btnEkleYpd_Click(object sender, EventArgs e)
        {
            if (lblYeniProL.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 5.51);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@p1 where PETROLTUR='YENIPRODISEL'", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtLitre.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "YENIPRODISEL");
                komut13.Parameters.AddWithValue("@p3", int.Parse(txtLitre.Text));
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }

        private void btnEkleGaz_Click(object sender, EventArgs e)
        {
            if (lblGazL.Text != "10000")
            {
                decimal tutar = (decimal)(double.Parse(txtLitre.Text) * 3.28);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update TBLKASA set MIKTAR=MIKTAR-@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", tutar);
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLBENZIN set STOK=STOK+@p1 where PETROLTUR='GAZ'", baglanti);
                komut2.Parameters.AddWithValue("@p1", int.Parse(txtLitre.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut13 = new SqlCommand("insert into TBLHAREKET (PLAKA,BENZINTUR, LITRE, FIYAT) values (@p1,@p2,@p3,@p4)", baglanti);
                komut13.Parameters.AddWithValue("@p1", "ALIM");
                komut13.Parameters.AddWithValue("@p2", "GAZ");
                komut13.Parameters.AddWithValue("@p3", int.Parse(txtLitre.Text));
                komut13.Parameters.AddWithValue("@p4", tutar);
                komut13.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Alış Gerçekleşti");
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Depo zaten dolu");
            }

        }
    }

}
