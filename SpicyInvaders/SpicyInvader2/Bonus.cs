using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader2
{
    public class Bonus
    {
        private char sprite;
        private int x;
        private int y;
        private int WIDTH;
        private int HEIGHT;
        private ConsoleColor color;
        private int speed = 95;//Vitesse entre 0 et 100
        private bool destroyed = false;
        private List<Bonus> bonusList = new List<Bonus>();


        public Bonus(char sprite, ConsoleColor color, List<Bonus> bonusList)
        {
            this.sprite = sprite;
            this.color = color;
            this.bonusList = bonusList;

        }
        public void Init(int x, int y)
        {
            Draw(x, y);
        }

        public void Draw(int x, int y)
        {
            char line = sprite;
            Console.SetCursorPosition(x, y);
            Console.Write(line);
            Bonus bonus = new Bonus(sprite, color, bonusList);
            bonusList.Add(bonus);
        }
        public void Nextmove(int tic, List<Bonus> bonusList, List<Enemy> ennemiesList, Ship ship)
        {
            //Éxécute l'action selon la vitesse
            if (tic % (100 - speed) == 0)
            {
                int newX = x;
                int newY = y;

                if (destroyed == false)
                {
                    newY++;
                }
                if (newY > Console.WindowHeight || newY < 0)
                {
                    Destroy();
                    destroyed = true;
                }
                if (y > 0 && !destroyed)
                {
                    Console.MoveBufferArea(x, y, 1, 1, newX, newY);
                }
                else
                {
                    //efface le bonus
                    Destroy();
                }
                x = newX;
                y = newY;
            }
            int nombreDElements = ennemiesList.Count;
            for (int i = 0; i < nombreDElements; i++)
            {
                if (x < ennemiesList[i].x + ennemiesList[i].WIDTH && x + WIDTH > ennemiesList[i].x && y < ennemiesList[i].y + ennemiesList[i].HEIGHT && y + 1 + HEIGHT > ennemiesList[i].y)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');

                    int drawY = ennemiesList[i].y + 2;
                    y = drawY;
                    Draw(x, drawY);
                }
            }
            if (x < ship.x + ship.WIDTH && x + WIDTH > ship.x && y + 1 < ship.y + ship.HEIGHT && y + 1 + HEIGHT > ship.y)
            {
                Destroy();
                ship.nmbAmmo++;
            }


        }

        public void Destroy()
        {

        }
    }
}
