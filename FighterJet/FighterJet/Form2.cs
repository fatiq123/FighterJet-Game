using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZInput;

namespace FighterJet
{
    public partial class Form2 : Form
    {

        string enemyDirection;
        string enemyDirection2;
        int counter = 0;
        //string playerDirection = "left";


        PictureBox player = new PictureBox();
        PictureBox enemy = new PictureBox();
        PictureBox enemy2=new PictureBox();
        PictureBox bullet=new PictureBox();
        PictureBox bullet2=new PictureBox();

        List<PictureBox> playerFire = new List<PictureBox>();
        List<PictureBox> enemyFire = new List<PictureBox>();
        List<PictureBox> enemyFire2 = new List<PictureBox>();

        int enemyGenTime;
        int enemyCurrentTime;

        int enemyGenTime2;
        int enemyCurrentTime2;


        ProgressBar playerHealth = new ProgressBar();
        int enemySpeed; //used
        int enemySpeed2;
        Random random;

        ProgressBar enemyHealth1 = new ProgressBar();

        //==============================================================================================

        public Form2()
        {
            InitializeComponent();
        }

        private void PlayerMovement2_Tick(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyPressed(Key.UpArrow) && player.Top > 0)
            {
                player.Top = player.Top - 5;//(enemySpeed+enemySpeed2); //-5
            }
            if (Keyboard.IsKeyPressed(Key.DownArrow) && player.Top + player.Height < this.Height - 40)
            {
                player.Top = player.Top + 5;//(enemySpeed + enemySpeed2); //+5
            }

            if (Keyboard.IsKeyPressed(Key.LeftArrow) && player.Left > 0)
            {
                player.Left = player.Left - 5;// (enemySpeed + enemySpeed2);
            }
            if (Keyboard.IsKeyPressed(Key.RightArrow) && player.Left + player.Width < this.Width - 40)
            {
                player.Left = player.Left + 5;//(enemySpeed + enemySpeed2);
            }
            if (Keyboard.IsKeyPressed(Key.Space))
            {
                createPlayerBullet();
            }

            moveEnemy1(); // used
            moveEnemy2();

            movePlayerBullet();
            removeBullet();             // to remove bullets that cross the forms
            

            detectPlayerCollision();// used
            detectPlayerCollision2();

            collisionEnemy();//used
            collisionEnemy2();

            removeEnemyBullet();// used
            removeEnemyBullet2();

            enemyCurrentTime++;
            enemyCurrentTime2++;
            if (player.Top > enemy.Top && player.Top < enemy.Height+enemy.Top && counter % 2 == 0)
            {
                createEnemyBullet1();

                //enemyCurrentTime = 0;
                //enemyCurrentTime2 = 0;
            }
            if(player.Top < enemy2.Top && player.Top < enemy2.Height + enemy2.Top && counter%2==0)
            {
                createEnemyBullet2();
            }


           
            if (enemy.Visible == false && enemy2.Visible==false)
            {

                playerMovement2.Enabled = false;

                Image img = FighterJet.Properties.Resources.you_win;
                gameOver2 form = new gameOver2(img);
                DialogResult result = form.ShowDialog();


                if (result == DialogResult.No)
                {
                    this.Close();
                }
                if (result == DialogResult.Yes)
                {
                    restart2();
                }
            }
            if (playerHealth.Value == 0)
            {
                playerMovement2.Enabled = false;

                Image img = FighterJet.Properties.Resources.gameOver_png;
                gameOver2 form = new gameOver2(img);
                DialogResult result = form.ShowDialog();


                if (result == DialogResult.No)
                {
                    this.Close();
                }
                if (result == DialogResult.Yes)
                {
                    restart2();
                }
            }
            counter++;
            if(counter==8000)
            {
                counter = 0;
            }
        }


        private void Txtlvl1_Load(object sender, EventArgs e)
        {
            restart2();
        }

        private void restart2()
        {
            this.Controls.Clear();
            playerMovement2.Enabled = true;

            random = new Random();
            enemySpeed = 4;
            enemySpeed2 = 3;

            enemyHealth1.Visible = true;

            enemyGenTime = 20;
            enemyCurrentTime = 0;

            enemyGenTime2 = 15;     // enemy 2
            enemyCurrentTime2 = 0;

           
            enemyHealth1.Maximum = 500;
            enemyHealth1.Value = 500;
          

            addPlayer();
            addEnemy1();//used
            addEnemy2();

            enemyHealth1.Location = new System.Drawing.Point(815, 21);
            this.Controls.Add(enemyHealth1);
        }

    


        private void addPlayer()
        {

            player = new PictureBox();
            player.Image = FighterJet.Properties.Resources.Fighter_type_A1;
            player.BackColor = Color.Transparent;
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Height = player.Image.Height;
            player.Width = player.Image.Width;
            player.Location = new System.Drawing.Point(27, 178);
            player.Name = "player";
            Controls.Add(player);

            playerHealth = new ProgressBar();
            playerHealth.Maximum = 200;
            playerHealth.Value = 200;
            playerHealth.Location = new System.Drawing.Point(24, 12);
            this.Controls.Add(playerHealth);


        }

        private void addEnemy1()
        {
            enemy = new PictureBox();
            enemy.Image = FighterJet.Properties.Resources.enemy;
            enemy.BackColor = Color.Transparent;
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.Height = enemy.Image.Height;
            enemy.Width = enemy.Image.Width;
            enemy.Location = new System.Drawing.Point(877, 188);
            enemy.Top = 0;
            //enemy.Top = random.Next(0, this.Height + enemy.Height);
            enemy.Name = "enemy1";
            // enemy.Left = random.Next( this.Height - enemy.Height);

            enemyDirection = "top";
            Controls.Add(enemy);
        }

        private void addEnemy2()
        {
            enemy2 = new PictureBox();
            enemy2.Image = FighterJet.Properties.Resources.Fighter_type_A2;
            enemy2.BackColor = Color.Transparent;
            enemy2.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy2.Height = enemy2.Image.Height;
            enemy2.Width = enemy2.Image.Width;
            enemy2.Location = new System.Drawing.Point(877, 188);

            //enemy2.Top = random.Next(0, this.Height + enemy.Height);
            enemy2.Top = this.Height - 100;
            // enemy.Left = random.Next( this.Height - enemy.Height);
            enemy2.Name = "enemy2";
            enemyDirection2 = "top2";


            Controls.Add(enemy2);
        }

        private void moveEnemy1()
        {
            //________________________________enemy direction________________________________
            if (enemy.Top <= 0)
            {
                enemyDirection = "down";
            }
            if ((enemy.Top + enemy.Height) + enemySpeed >= this.Height - 30)
            {
                enemyDirection = "top";
            }
            if (enemyDirection == "top")
            {
                enemy.Top = enemy.Top - 5;
            }
            if (enemyDirection == "down")
            {
                enemy.Top = enemy.Top + 5;
            }

        }

        private void moveEnemy2()
        {
            if (enemy2.Top <= 0)
            {
                enemyDirection2 = "down2";
            }
            if ((enemy2.Top + enemy2.Height) + enemySpeed2 >= (this.Height/2) - 30)
            {
                enemyDirection2 = "top2";
            }
            if (enemyDirection2 == "top2")
            {
                enemy2.Top = enemy2.Top - 5;
            }
            if (enemyDirection2 == "down2")
            {
                enemy2.Top = enemy2.Top + 5;
            }
        }


        private void createPlayerBullet()
        {
            bullet = new PictureBox();
            bullet.Image = FighterJet.Properties.Resources.bullet2;
            bullet.BackColor = Color.Transparent;
            bullet.Height = bullet.Image.Height;
            bullet.Width = bullet.Image.Width;
            //bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            //bullet.Location = new System.Drawing.Point(877, 188);

            bullet.Top = player.Top + 20;
            // enemy.Left = random.Next( this.Height - enemy.Height);
            bullet.Left = player.Left + 100;

            playerFire.Add(bullet);
            Controls.Add(bullet);
        }

        private void movePlayerBullet()
        {
            // ONLY FUNCTION IS TO MOVE BULLETS OF PLAYER AND ENEMY
            foreach (PictureBox bullet in playerFire)
            {
                bullet.Left = bullet.Left + 10;
            }
            foreach (PictureBox bullet in enemyFire)
            {
                bullet.Left = bullet.Left - 10;
            }
            foreach (PictureBox bullet in enemyFire2) 
            {
                //move enemy 2 bullet
                bullet.Left = bullet.Left - 10;
            }

        }

        private void removeBullet()             // remove bullet across form in list
        {
            for (int idx = 0; idx < playerFire.Count; idx++)
            {
                if (playerFire[idx].Bottom <= 0)
                {
                    playerFire.Remove(playerFire[idx]);
                }
            }
        }

        private void createEnemyBullet1()
        {
            bullet = new PictureBox();
            bullet.Image = FighterJet.Properties.Resources.Bullet1;
            bullet.BackColor = Color.Transparent;
            bullet.Height = bullet.Image.Height;
            bullet.Width = bullet.Image.Width;
            //bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            //bullet.Location = new System.Drawing.Point(877, 188);

            bullet.Top = enemy.Top + 20;
            // enemy.Left = random.Next( this.Height - enemy.Height);
            bullet.Left = enemy.Left - 20;

            // this if statement is precious that if enemy destroys than bullets also remove from form
            if (enemy.Visible == false)
            {
                bullet.Visible = false;
            }
            enemyFire.Add(bullet);
            Controls.Add(bullet);
        }

        private void createEnemyBullet2()
        {
            bullet2 = new PictureBox();
            bullet2.Image = FighterJet.Properties.Resources.Bullet1;
            bullet2.BackColor = Color.Transparent;
            bullet2.Height = bullet2.Image.Height;
            bullet2.Width = bullet2.Image.Width;
            bullet2.Top = enemy.Top + 20;
            bullet2.Left = enemy.Left - 20;

            if (enemy2.Visible == false)
            {
                bullet2.Visible = false;
            }
            enemyFire2.Add(bullet2);
            Controls.Add(bullet2);

        }

        private void removeEnemyBullet()
        {
            for (int idx = 0; idx < enemyFire.Count; idx++)         // for enemy
            {
                if (enemyFire[idx].Left >= this.Width || enemyFire[idx].Visible == false)
                {
                    enemyFire.Remove(enemyFire[idx]);
                    //enemy.Visible = false;
                    //bullet.Visible = false;
                }

            }
        }

        private void removeEnemyBullet2()
        {
            for (int idx = 0; idx < enemyFire2.Count; idx++)         // for enemy 2
            {
                if (enemyFire2[idx].Left >= this.Width || enemyFire2[idx].Visible == false)
                {
                    enemyFire2.Remove(enemyFire2[idx]);
                    //enemy.Visible = false;
                    //bullet.Visible = false;
                }

            }
        }


        private void detectPlayerCollision()
        {
            foreach (PictureBox bullet in playerFire)            // for player 1
            {
                if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                {
                    bullet.Visible = false;
                    if (enemyHealth1.Value == 0)
                    {
                        enemy.Visible = false;
                    }
                    else
                    {
                        enemyHealth1.Increment(-1);
                    }
                }
            }
        }

        private void detectPlayerCollision2()
        {
            foreach (PictureBox bullet2 in playerFire)            // for player 2
            {
                if (bullet2.Bounds.IntersectsWith(enemy2.Bounds))
                {
                    bullet2.Visible = false;
                    enemy2.Visible = false;
                }
            }
        }

        private void collisionEnemy()
        {
            // When Enemy Bullets Hit Our Player The progressBar Decreases.
            foreach (PictureBox bullet in enemyFire)
            {
                if (bullet.Bounds.IntersectsWith(player.Bounds))                    // enemy 1
                {
                    // player.Visible = false;
                    bullet.Visible = false;
                    if (playerHealth.Value == 0)
                    {
                        gameOver2 g = new gameOver2(FighterJet.Properties.Resources.gameOver_png);
                        g.ShowDialog();
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 5;
                    }
                }

            }
        }

        private void collisionEnemy2()
        {
            // When Enemy Bullets Hit Our Player The progressBar Decreases.
            foreach (PictureBox bullet2 in enemyFire)
            {
                if (bullet2.Bounds.IntersectsWith(player.Bounds))                    // enemy 2
                {
                    // player.Visible = false;
                    bullet2.Visible = false;
                    if (playerHealth.Value == 0)
                    {
                        gameOver2 g = new gameOver2(FighterJet.Properties.Resources.gameOver_png);
                        g.ShowDialog();
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                    }
                }

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            addPlayer();
            addEnemy1();
            addEnemy2();
        }
    }
}
