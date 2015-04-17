using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuthUSB
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "AuthOnUSB and DB v1.0.2\nРазработчик: Разумов Александр\n" +
                                "Группа: БА-11\n" +
                                "Учебное заведение: ЮЗГУ\n" +
                                "Год: 2015\n" +
                                "\tОписание программы\n" +
                                "Данная программа была разработана на основании задания курсового проекта: \"Разработка программы аутентификации пользователя по вставленному специальному USB накопителю\".  ";
        }
    }
}
