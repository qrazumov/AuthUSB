using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Devart.Data.SQLite;

namespace AuthUSB
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
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

        }


    }
}
