using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
//using Devart.Data.SQLite;
using System.Data.SQLite;

namespace AuthUSB
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public static string currentSN = "";
        public static string model = "";
        public static string size = "";

        private void Form4_Load(object sender, EventArgs e)
        {
            label8.Text = "";
            // SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\razum_000\Desktop\курсач по куратору\AuthUSB\AuthUSB\database\database.db;FailIfMissing=True;");
            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "select * from auth";
            command.Connection = connection;
            connection.Open();

            using (SQLiteDataReader r = command.ExecuteReader())
            {
                string _serial = "";
                string _model = "";
                string _memory = "";
                string _md5 = "";

                while (r.Read())
                {

                    textBox1.Text = r["serial"].ToString();
                    textBox2.Text = r["model"].ToString();
                    textBox3.Text = r["memory"].ToString();
                    textBox4.Text = r["md5"].ToString();

                }
                r.Close();
            }






        }

        private void button1_Click(object sender, EventArgs e)
        {



            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");
            SQLiteCommand cmd = new SQLiteCommand();


            MD5 md5Hash = MD5.Create();
            string source = currentSN + model + size;
            // получаем hesh
            var hash = Form1.GetMd5Hash(md5Hash, source);

            cmd.CommandText = string.Format("UPDATE auth SET " +
                                            "serial='" + currentSN + "', " +
                                            "model='" + model + "', " +
                                            "memory='" + size + "', " +
                                            "md5='" + hash + "'" + "WHERE id=1");

            cmd.Connection = connection;
            connection.Open();
            try
            {
                int aff = cmd.ExecuteNonQuery();
                //this.Close();
                label8.Text = "Операция успешно завершена. Данные авторизации обновлены";
                this.label8.BackColor = System.Drawing.Color.MediumAquamarine;
                textBox1.Text = currentSN;
                textBox2.Text = model;
                textBox3.Text = size;
                textBox4.Text = hash;
            }
            catch
            {

                MessageBox.Show("Ошибка");
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher theSearcher =
                new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            int i = 0;
            foreach (ManagementObject currentObject in theSearcher.Get())
            {

                ManagementObject theSerialNumberObjectQuery =
                new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                try
                {
                   
                   
                    var ser = theSerialNumberObjectQuery["SerialNumber"].ToString();

                    if (ser[0] == 48 && ser[1] == 48 && ser[2] == 48)
                    {
                        
                        continue;
                    }
                    else
                    {
                        currentSN = theSerialNumberObjectQuery["SerialNumber"].ToString();
                        model = currentObject["Model"].ToString();
                        size = currentObject["Size"].ToString();
                        textBox7.Text = currentSN;
                        textBox6.Text = model;
                        textBox5.Text = size;
                        break;
                    }
                        
                        
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("USB накопитель не найден");
                    textBox7.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    break;
                }
               

                



            }

        
    










}


    }
}
