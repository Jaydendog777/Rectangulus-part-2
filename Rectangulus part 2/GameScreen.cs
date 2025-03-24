using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectangulus_part_2
{
    public partial class GameScreen : UserControl
    {
        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown, nKeyDown;
        public static bool reset = false;

        public static int screenWidth;
        public static int screenHeight;

        public static int test1, test2;

        public int timer = 0;
        int attackBarTime = 20;
        int attackBarFull = 310;
        int attackTime = 0;

        Player hero = new Player();
        Player playerHealthBar = new Player();
        Player attackDirection = new Player();

        Balls enemyBall;
        List<Balls> balls = new List<Balls>();

        Random randGen = new Random();

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Pen blackPen = new Pen(Color.Black);

        public int circleCount = 5;

        public GameScreen()
        {
            InitializeComponent();

            screenWidth = this.Width;
            screenHeight = this.Height;

            InitializeGame();
        }

        public void InitializeGame()
        {
            reset = false;
            hero = new Player();
            attackDirection = new Player();
            playerHealthBar = new Player();

            int x = randGen.Next(20, this.Width - 50);
            int y = randGen.Next(20, this.Height - 50);

            enemyBall = new Balls(x, y, 5, 5);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.W:
                    upArrowDown = true;
                    break;

                case Keys.S:
                    downArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;

                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.A:
                    leftArrowDown = true;
                    break;

                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.D:
                    rightArrowDown = true;
                    break;

                //Testing keys
                case Keys.N:
                    nKeyDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.W:
                    upArrowDown = false;
                    break;

                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.S:
                    downArrowDown = false;
                    break;

                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.A:
                    leftArrowDown = false;
                    break;

                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.D:
                    rightArrowDown = false;
                    break;

                //Testing keys
                case Keys.N:
                    nKeyDown = false;
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Hero
            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.width, hero.height);
            e.Graphics.FillRectangle(redBrush, playerHealthBar.x, playerHealthBar.y, playerHealthBar.width, playerHealthBar.height);
            //e.Graphics.FillRectangle(greenBrush, attackDirection.x, attackDirection.y, attackDirection.width, attackDirection.height);
            e.Graphics.DrawRectangle(blackPen, hero.x, hero.y, hero.width, hero.height);
            e.Graphics.DrawArc(blackPen, attackDirection.x, attackDirection.y, attackDirection.width, attackDirection.height, test1, test2);

            //Enemy balls
            foreach (Balls b in balls)
            {
                e.Graphics.FillEllipse(whiteBrush, b.x, b.y, b.size, b.size);
                e.Graphics.DrawEllipse(blackPen, b.x, b.y, b.size, b.size);
            }

            //attack bar
            e.Graphics.FillRectangle(whiteBrush, 77, this.Height - 25, attackTime , 20);
            e.Graphics.DrawRectangle(blackPen, 77, this.Height - 25, 310, 20);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Moving the hero
            if (rightArrowDown == true)
            {
                hero.Move("right");
                playerHealthBar.Move("right");

                attackDirection.width = 15;
                attackDirection.height = hero.height;
                attackDirection.x = hero.x + hero.width;
                attackDirection.y = hero.y;
            }
            if (leftArrowDown == true)
            {
                hero.Move("left");
                playerHealthBar.Move("left");

                attackDirection.width = 15;
                attackDirection.height = hero.height;
                attackDirection.x = hero.x - 15;
                attackDirection.y = hero.y;
            }
            if (upArrowDown == true)
            {
                hero.Move("up");
                playerHealthBar.Move("up");

                attackDirection.width = hero.width;
                attackDirection.height = 15;
                attackDirection.x = hero.x;
                attackDirection.y = hero.y - 15;
            }
            if (downArrowDown == true)
            {
                hero.Move("down");
                playerHealthBar.Move("down");

                attackDirection.width = hero.width;
                attackDirection.height = 15;
                attackDirection.x = hero.x;
                attackDirection.y = hero.y + hero.height;
            }

            if (nKeyDown == true)
            {
                attackTime = 0;
            }

            //Moving enemy balls
            foreach (Balls b in balls)
            {
                b.Move(hero.x, hero.y);
            }

            //Spawning enemy balls
            if (circleCount > 0)
            {
                int num = randGen.Next(1, 51);
                if (num == 1)
                {
                    int x = randGen.Next(20, this.Width - 50);

                    Balls b = new Balls(x, 0, 5, 5);
                    balls.Add(b);
                    circleCount--;
                }
                if (num == 2)
                {
                    int x = randGen.Next(20, this.Width - 50);

                    Balls b = new Balls(x, this.Height - 15, 5, 5);
                    balls.Add(b);
                    circleCount--;
                }
            }

            //Balls attacking
            if (timer == 35)
            {
                foreach (Balls b in balls)
                {
                    if (hero.Collision(b, attackDirection, "damaged"))
                    {
                        playerHealthBar.takeDamage(1);
                        playerHealthBar.height = playerHealthBar.healthBarHeight;
                        playerHealthBar.y += playerHealthBar.num;

                        if (playerHealthBar.height <= 0)
                        {
                            reset = true;
                            Form1.ChangeScreen(this, new MainMenu());
                        }
                    }

                }
                timer = 0;
            }

            //Testing Labels
            testingLabel.Text = $"Health: {playerHealthBar.health}";
            testingLabel.Text += $"\nHeight: {playerHealthBar.healthBarHeight}";
            testingLabel.Text += $"\nTimer: {timer}";
            if (attackTime < attackBarFull)
            {
                attackTime += attackBarFull / attackBarTime;
            }
            else
            {
                attackTime = attackBarFull;
            }
            timer++;
            Refresh();
        }
    }
}
