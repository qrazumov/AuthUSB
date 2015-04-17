using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
//using Devart.Data.SQLite;

namespace AuthUSB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.student". При необходимости она может быть перемещена или удалена.
            this.studentTableAdapter1.Fill(this.dataSet2.student);
            
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.student". При необходимости она может быть перемещена или удалена.
            this.studentTableAdapter.Fill(this.dataSet1.student);
            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "select * from student";
            command.Connection = connection;
            connection.Open();


            
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                //connection.Close();
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.studentTableAdapter.Fill(this.dataSet1.student);
            //SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\razum_000\Desktop\курсач по куратору\AuthUSB\AuthUSB\database\database.db;FailIfMissing=True;");
           // connection.Open();
            this.studentTableAdapter.Update(this.dataSet1.student);
           // dataGridView1.Refresh();
            //int aff = cmd.ExecuteNonQuery();
            dataGridView1.DataSource = this.dataSet1.student;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string val = dataGridView1.CurrentCell.Value.ToString();
            long id = long.Parse(dataGridView1.CurrentCell.Value.ToString());
            //MessageBox.Show(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
           // this.studentTableAdapter.
            dataGridView1.DataSource = this.dataSet1.student;

            /*
             * sqlCommand.CommandText = "DELETE FROM testTABLE WHERE id=@ID";
                sqlCommand.Parameters.AddWithValue("@ID",MyDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                где MyDataGridView.SelectedRows[0].Cells[0].Value.ToString() - это твой ключ
             */




            SQLiteConnection connection = new SQLiteConnection("Data Source=database.db;FailIfMissing=True;");
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = "DELETE FROM student WHERE id=@ID";
            cmd.Parameters.AddWithValue("@ID",dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            cmd.Connection = connection;
            connection.Open();

            try
            {
                int aff = cmd.ExecuteNonQuery();
                MessageBox.Show("OK");
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
