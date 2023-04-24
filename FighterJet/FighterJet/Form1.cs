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
using FighterJet;

namespace FighterJet
{
    public partial class txtlvl1 : Form
    {

        string enemyDirection;
        //string playerDirection = "left";


        PictureBox player;
        PictureBox enemy;
        PictureBox bullet;
        ProgressBar enemy_health = new ProgressBar();

        List<PictureBox> playerFire = new List<PictureBox>();
        List<PictureBox> enemyFire = new List<PictureBox>();

        int enemyGenTime ;
        int enemyCurrentTime ;
        int counter = 0;

        ProgressBar playerHealth;

        int enemySpeed;
        Random random;

        public txtlvl1()
        {
            InitializeComponent();
        }

        private void Txtlvl1_Load(object sender, EventArgs e)
        {
            restart();       
        }

        private void restart()
        {
            this.Controls.Clear();
            playerMovement.Enabled = true;

            random = new Random();
            enemySpeed = 4;
            enemy_health.Visible = true;
            enemyGenTime = 20;
            enemyCurrentTime = 0;
            enemy_health.Maximum = 200;
            enemy_health.Value = 200;
            addPlayer();
            addEnemy1();
            enemy_health.Location = new System.Drawing.Point(835,12);
            Controls.Add(enemy_health);
        }

        private void PlayerMovement_Tick(object sender, EventArgs e)
        {
            if(Keyboard.IsKeyPressed(Key.UpArrow) && player.Top > 0)
            {
                player.Top = player.Top - enemySpeed; //-5
            }
            if(Keyboard.IsKeyPressed(Key.DownArrow) && player.Top + player.Height < this.Height - 40)
            {
                player.Top = player.Top + enemySpeed; //+5
            }

            if(Keyboard.IsKeyPressed(Key.LeftArrow) && player.Left > 0)
            {
                player.Left = player.Left - enemySpeed;
            }
            if (Keyboard.IsKeyPressed(Key.RightArrow) && player.Left + player.Width < this.Width - 40)
            {
                player.Left = player.Left + enemySpeed;
            }

           

            if(Keyboard.IsKeyPressed(Key.Space))
            {
                if (counter % 4 == 0)
                {
                    createPlayerBullet();
                }
            }

            moveEnemy1();
            movePlayerBullet();
            removeBullet();             // to remove bullets that cross the forms
            detectPlayerCollision();

            collisionEnemy();
            removeEnemyBullet();

            enemyCurrentTime++;
            if(enemyCurrentTime == enemyGenTime)
            {
                createEnemyBullet1();
                enemyCurrentTime = 0;
            }

            if(enemy.Visible == false)
            {
                playerMovement.Enabled = false;

                Image img = FighterJet.Properties.Resources.you_win;
                gameOver form = new gameOver(img);
                DialogResult result = form.ShowDialog();


                if(result == DialogResult.No)
                {
                    this.Close();
                }
                if(result == DialogResult.Yes)
                {
                    //restart();
                    Form2 f = new Form2();
                    f.Show();
                }
            }
            if(playerHealth.Value == 0)
            {
                playerMovement.Enabled = false;

                Image img = FighterJet.Properties.Resources.gameOver_png;
                gameOver form = new gameOver(img);
                DialogResult result = form.ShowDialog();


                if (result == DialogResult.No)
                {
                    this.Close();
                }
                if (result == DialogResult.Yes)
                {
                    restart();
                }
            }
            if (counter == 4000)
            {
                counter = 0;
            }
            counter++;
        }



        private void addPlayer()
        {

            player = new PictureBox();
            player.Image = FighterJet.Properties.Resources.player;
            player.BackColor = Color.Transparent;
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Location = new System.Drawing.Point(27,178);
            player.Height = player.Image.Height;
            player.Width = player.Image.Width;
            Controls.Add(player);

            playerHealth = new ProgressBar();
            playerHealth.Value = 100;
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

            enemy.Top = random.Next(0 , this.Height + enemy.Height);
            // enemy.Left = random.Next( this.Height - enemy.Height);

            enemyDirection = "top";
           

            Controls.Add(enemy);
           
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
                enemy.Top = enemy.Top - enemySpeed;
            }
            if (enemyDirection == "down")
            {
                enemy.Top = enemy.Top + enemySpeed;
            }

        }

        private void createPlayerBullet()
        {
            bullet = new PictureBox();
            bullet.Image = FighterJet.Properties.Resources.bullet2;
            bullet.BackColor = Color.Transparent;
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
            foreach(PictureBox bullet in playerFire)
            {
                bullet.Left = bullet.Left + 10; 
            }
            foreach(PictureBox bullet in enemyFire)
            {
                bullet.Left = bullet.Left - 10;
            }

        }

        private void removeBullet()             // remove bullet across form in list
        {
            for(int idx=0; idx < playerFire.Count; idx++)
            {
                if(playerFire[idx].Bottom <= 0)
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
            //bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            //bullet.Location = new System.Drawing.Point(877, 188);

            bullet.Top = enemy.Top + 20;
            // enemy.Left = random.Next( this.Height - enemy.Height);
            bullet.Left = enemy.Left - 20;

            // this if statement is precious that if enemy destroys than bullets also remove from form
            if(enemy.Visible == false)
            {
                bullet.Visible = false;
            }
            enemyFire.Add(bullet);
            Controls.Add(bullet);
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
        private void detectPlayerCollision()
        {
            foreach(PictureBox bullet in playerFire)            // for player
            {
                if(bullet.Bounds.IntersectsWith(enemy.Bounds))
                {
                    bullet.Visible = false;
                    if (enemy_health.Value == 0)
                    {
                        enemy.Visible = false;
                    }
                    else
                    {
                        enemy_health.Increment(-1);
                    }
                }
            }           
        }

        private void collisionEnemy()
        {
            // When Enemy Bullets Hit Our Player The progressBar Decreases.
            foreach(PictureBox bullet in enemyFire)
            {
                if(bullet.Bounds.IntersectsWith(player.Bounds))
                {
                    // player.Visible = false;
                    bullet.Visible = false;
                    playerHealth.Value = playerHealth.Value - 10;
                }

                //if (playerHealth.Value == 0)
                //{
                //    DialogResult end = new DialogResult();
                //    if (end == DialogResult.Yes)
                //    {
                //        //start();
                //    }
                //    if (end == DialogResult.No)
                //    {
                //        this.Close();
                //    }
                //}
            }
        }
       
    }
}
