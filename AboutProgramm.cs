using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BinaryToJSONConverterApp
{
    public partial class AboutProgram : Form
    {
        public static string mailtoMainAdress = "mailto:ArtCode-Kazan@yandex.ru";

        public AboutProgram()
        {
            TopMost = true;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMail_Click(object sender, EventArgs e)
        {
            string mailtoUrl = mailtoMainAdress;

            try
            {
                System.Diagnostics.Process.Start(mailtoUrl);
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}
