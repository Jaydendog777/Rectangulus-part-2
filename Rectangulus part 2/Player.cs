using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangulus_part_2
{
    internal class Player
    {
        public int x, y;
        public int width = 20;
        public int height = 55;
        public int speed = 7;
        public int maxHeight = 55;

        public int maxHealth = 10;
        public int health = 10;
        public int num;
        public int healthBarHeight = 55;

        string dir;
        public int knockback = 100;

        public Player()
        {
            x = GameScreen.screenWidth / 2 - width / 2;
            y = GameScreen.screenHeight / 2 - height / 2;
        }

        public void Move(string direction)
        {
            if (direction == "right" && x < GameScreen.screenWidth - width)
            {
                x += speed;
                dir = "right";
                GameScreen.test1 = 270;
                GameScreen.test2 = 180;
            }
            else if (direction == "left" && x > 0)
            {
                x -= speed;
                dir = "left";
                GameScreen.test1 = 90;
                GameScreen.test2 = 180;
            }
            else if (direction == "up" && y > 0)
            {
                y -= speed;
                dir = "up";
                GameScreen.test1 = 180;
                GameScreen.test2 = 180;
            }
            else if (direction == "down" && y < GameScreen.screenHeight - height)
            {
                y += speed;
                dir = "down";
                GameScreen.test1 = 360;
                GameScreen.test2 = 180;
            }
        }

        public void takeDamage(int damage)
        {
            health = maxHealth / damage;
            num = maxHeight / health;
            healthBarHeight -= num;
        }

        public bool Collision(Balls b, Player a, string type)
        {
            Rectangle playerRec = new Rectangle(x, y, width, height);
            Rectangle enemyRec = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle directionRec = new Rectangle(a.x, a.y, a.width, a.height);

            string collisionType = type;

            if (type == "damaged")
            {
                if (playerRec.IntersectsWith(enemyRec))
                {
                    if (directionRec.IntersectsWith(enemyRec))
                    {
                        if (dir == "right")
                        {
                            b.x += knockback;
                        }

                        else if (dir == "left")
                        {
                            b.x -= knockback;
                        }

                        else if (dir == "up")
                        {
                            b.y -= knockback;
                        }

                        else if (dir == "down")
                        {
                            b.y += knockback;
                        }
                    }
                    else
                    {
                        if (b.x < a.x)
                        {
                            b.x -= knockback;
                        }
                        else if (b.x > a.x)
                        {
                            b.x += knockback;
                        }

                        if (b.y < a.y)
                        {
                            b.y -= knockback;
                        }
                        else if (b.y > a.y)
                        {
                            b.y += knockback;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
