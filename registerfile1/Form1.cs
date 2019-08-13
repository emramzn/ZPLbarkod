using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Proje1
{
    public partial class Form1 : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Onur\\Desktop\\ProgramSon\\ProjeVsonP1\\Proje1\\Database1.mdf;Integrated Security=True");



        int count = 1 , index=0 ;

        public Form1()
        {
            InitializeComponent();
            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            string fileinfo = @"C:\Users\Onur\Desktop\proje1\";
            DateTime dt;
            dt = DateTime.Now;
            Random rastgele = new Random();
            
            string[] dizindekiDosyalar = Directory.GetFiles(fileinfo);
            //  int count = dizindekiDosyalar.Length;
            
            string countadd = String.Format("{0:D4}", count);
            
            
            string kontrol = String.Format("{0:yyyyMMdd}" + ".txt", dt);
            
            if (File.Exists(fileinfo + kontrol + countadd))
                MessageBox.Show("A Klasöründe Kayıtlı");
            else
            {
                count = count + 1;
               // MessageBox.Show("Hayır yok , Oluşturuldu");
              //  MessageBox.Show(count.ToString());
                
                FileStream fs = File.Create(fileinfo + String.Format("{0:yyyyMMdd}" + countadd + ".txt", dt));
                fs.Close();
                string path = (fileinfo + String.Format("{0:yyyyMMdd}" + countadd + ".txt", dt));


                FileStream fsa = new FileStream( path , FileMode.OpenOrCreate, FileAccess.Write);
                
                StreamWriter sw = new StreamWriter(fsa);

                
                string harfler = "ABCDEFGHIJKLMNOPRSTUVYZabcdefghijklmnoprstuvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }
                
                sw.WriteLine( (String.Format("{0:yyyyMMdd}" + countadd   , dt) + uret ).ToString());
               
              
                sw.Flush();
               
                sw.Close();
                fs.Close();

                conn.Open();
                SqlCommand lastdata = new SqlCommand("insert into lastData (sayac) VALUES ('"+count+"') ", conn);
                lastdata.ExecuteNonQuery();
                
                //MessageBox.Show("index " + count.ToString());
                conn.Close();
                kayitSay();
               



            }
        }


        public int kayitSay()
        {
            conn.Open();
            SqlCommand lastdata = new SqlCommand("select COUNT(*) from lastData ", conn);
            lastdata.ExecuteNonQuery();
            index = Convert.ToInt32(lastdata.ExecuteScalar());
            

            conn.Close();
            
            return index;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            count = kayitSay();
           // MessageBox.Show(index.ToString());
            // TODO: Bu kod satırı 'database1DataSet1.denemetbl' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.denemetblTableAdapter.Fill(this.database1DataSet1.denemetbl);

        }
    }
}
