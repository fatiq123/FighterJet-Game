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
    public partial class gameOver2 : Form
    {
        Image i;
        public gameOver2(Image img)
        {
            InitializeComponent();
            this.BackgroundImage = img;
            i = img;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void GameOver2_Load(object sender, EventArgs e)
        {
            if (i == FighterJet.Properties.Resources.gameOver_png)
            {
            }
            else
            {
                button2.Text = "Next";
            }
        }
    }
}
