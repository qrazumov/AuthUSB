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
using System.Data.SQLite;

namespace AuthUSB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
        | дейстивия при инициализации form1
        |
        |
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Visible = false; // закыто, пока не прошли аутентификацю
            label1.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            this.toolStripMenuItem2.Visible = false;
        }
        


        // находим md4 hash
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // проверяем hash
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
        | дейстивия при инициализации form1
        |
        |
         */
        public string hash = "";
        private void button1_Click(object sender, EventArgs e)
        {
            // вытягиваем из db hash
            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "select * from auth";
            command.Connection = connection;
            connection.Open();
            SQLiteDataReader r = command.ExecuteReader();
            
                string _md5 = "";

                while (r.Read())
                {
                    _md5 = r["md5"].ToString();
         
                }
                r.Close();
            


            // начинаем аутентификацю
            
            bool isTrue = false;
            while (!isTrue)
            {
                ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");

                int i = 0;
                foreach (ManagementObject currentObject in theSearcher.Get())
                {
                    try
                    {
                        ManagementObject theSerialNumberObjectQuery =
                            new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                        //Console.WriteLine(theSerialNumberObjectQuery["SerialNumber"].ToString());
                        // if (theSerialNumberObjectQuery["SerialNumber"].ToString() == "0B7007773020")
                        // создаем строку, которая будет переведена в hash

                        string _SerialNumber = theSerialNumberObjectQuery["SerialNumber"].ToString();
                        string _Model = currentObject["Model"].ToString();
                        string _Size = currentObject["Size"].ToString();


                        string source = _SerialNumber + _Model + _Size;

                        MD5 md5Hash = MD5.Create();

                        // получаем hesh
                        hash = GetMd5Hash(md5Hash, source);

                        /*
                        VerifyMd5Hash(md5Hash, source, hash)
                         */

                        if (hash == _md5)
                        {
                            isTrue = true;
                            label5.Text = theSerialNumberObjectQuery["SerialNumber"].ToString();
                            label6.Text = currentObject["Model"].ToString();
                            label7.Text = currentObject["Size"].ToString();
                            this.toolStripMenuItem2.Visible = true;
                            break;
                        }

                        if (i + 1 == theSearcher.Get().Count)
                        {
                            //Console.WriteLine("Ошибка!!!");
                            label1.ForeColor = Color.Red;
                            label1.Text = "Ошибка аутентификации";
                            break;
                        }
                        i++;
                    }
                    catch (Exception er)
                    {
                        //MessageBox.Show(er.Message);
                        MessageBox.Show("Ошибка аутентификации!\nОтказано в доступе");
                        // добавил коммент
                        return;
                    }
                }
            }
            if (isTrue) 
            {
                //Console.WriteLine("Аутентификация пройдена!!!");
                label1.ForeColor = Color.Green;
                label1.Text = "Аутентификация пройдена!";
                button2.Visible = true; // 
                
            }


        }
         

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            //frm.textBox1.Text = this.textBox1.Text;
            frm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
        }

        public bool isVisibleHash = true;
        private void label9_Click(object sender, EventArgs e)
        {
            if (isVisibleHash)
            {
                label8.Text = hash;
                this.label9.BackColor = System.Drawing.Color.MediumAquamarine;
            }
            else
            {
                label8.Text = "";
                this.label9.BackColor = System.Drawing.Color.GreenYellow;
            }
            isVisibleHash = !isVisibleHash;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // выводим окно о программе
            Form5 frm5 = new Form5();
            frm5.Show();
        }

        
    }
}
