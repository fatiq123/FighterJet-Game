using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FighterJet
{
    public partial class gameOver : Form
    {
        Image i;
        public gameOver(Image img)
        {
            InitializeComponent();
            this.BackgroundImage = img;
            i = img;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.DialogResult = DialogResult.No;
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void GameOver_Load(object sender, EventArgs e)
        {
            if(i==FighterJet.Properties.Resources.gameOver_png)
            {
            }
            else
            {
                btnRestart.Text = "Next";
            }
        }
    }
}
