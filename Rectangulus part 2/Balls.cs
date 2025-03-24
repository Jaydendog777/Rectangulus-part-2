using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectangulus_part_2
{
    internal class Balls
    {
        public int x, y;
        public int size = 15;
        public int xSpeed, ySpeed;

        public Balls(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        public void Move(int heroX, int heroY)
        {
            if (heroX > x)
            {
                x += xSpeed;
            }
            if (heroX < x)
            {
                x -= xSpeed;
            }

            if (heroY > y)
            {
                y += ySpeed;
            }
            if (heroY < y)
            {
                y -= ySpeed;
            }
        }
    }
}
