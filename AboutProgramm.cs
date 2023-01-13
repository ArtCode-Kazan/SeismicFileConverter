using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryToJSONConverterApp
{
    public partial class AboutProgramm : Form
    {
        public AboutProgramm()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMail_Click(object sender, EventArgs e)
        {
            string target = "mailto:ArtCode-Kazan@yandex.ru";

            try
            {
                System.Diagnostics.Process.Start(target);
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
