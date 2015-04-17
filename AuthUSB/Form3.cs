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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");
            SQLiteCommand cmd = new SQLiteCommand();
            
            string id = textBox1.Text;
            string name = textBox2.Text;
            string lastname = textBox3.Text;
            string _group = textBox4.Text;
            
            cmd.CommandText = "INSERT INTO student (id, name, lastname, _group) VALUES (" +
                       "'" + id + "', " +
                       "'" + name + "', " +
                       "'" + lastname + "', " +
                       "'" + _group + "')";
            
            cmd.Connection = connection;
            connection.Open();
            /*
            cmd.CommandText = "INSERT INTO student (id, name, lastname, _group) VALUES ('9', 'Олег', 'Соклаков', 'БТ-21')";
             */
            try
             
            {
                int aff = cmd.ExecuteNonQuery();
                this.Close();
            }
            catch
            {
               
               MessageBox.Show("Error encountered during INSERT operation.");
            }
            finally
            {
                connection.Close();
            }
             
        }
    }


}
