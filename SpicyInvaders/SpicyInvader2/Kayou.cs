
//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Vaisseau
using System;
using System.Collections.Generic;

namespace SpicyInvader2
{
    public class Rock
    {
        const int MARGIN_LEFT = 10;
        const int MARGIN_RIGHT = 10;
        private string sprite;
        public ConsoleColor color;
        private int x;
        private int y;
        public int WIDTH = 4;
        public int HEIGHT = 1;
        public int speed = 95;//Vitesse entre 0 et 100
        public bool destroyed = false;

        public Rock(string sprite, int x, int y, ConsoleColor color, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.color = color;
            this.HEIGHT = height;
            this.WIDTH = width;
        }
        public void Init(int x, int y)
        {

            Draw(x, y);
        }

        public void Draw(int x, int y)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;

            string line = sprite;
            Console.SetCursorPosition(x, y);
            Console.Write(line);
            


            Console.ForegroundColor = prev;
        }
        public void NextMove(int tic,List<Rock> rockList, List<Enemy> ennemiesList, Ship ship)
        {

            //Éxécute l'action selon la vitesse
            if (tic % (100 - speed) == 0)
            {
                int newX = x;
                int newY = y;

                if(destroyed == false)
                {
                    newY++;
                }

                //Descend d'une ligne s'il touche le bord
                if (newX > Console.WindowWidth - MARGIN_RIGHT || newX <= MARGIN_LEFT)
                {
                    Destroy(rockList);
                    destroyed = true;
                }

                //Déplace efficacement le vaisseau
                if (y > 0 && !destroyed)
                {
                    Console.MoveBufferArea(x, y, sprite.Length, 1, newX, newY);
                }
                else
                {
                    //efface le missile
                    Destroy(rockList);
                }

                //Mise à jour de la nouvelle position
                x = newX;
                y = newY;
            }

            //Gestion des colisions avec les ennemis pour ne pas les affecter
            int nombreDElements = ennemiesList.Count;
            for (int i = 0; i < nombreDElements; i++)
            {
                if (x < ennemiesList[i].x + ennemiesList[i].WIDTH && x + WIDTH > ennemiesList[i].x && y < ennemiesList[i].y + ennemiesList[i].HEIGHT && y+1 + HEIGHT > ennemiesList[i].y)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(new string(' ', sprite.Length));

                    int drawY = ennemiesList[i].y + 2;
                    y = drawY;
                    Draw(x, drawY);
                }
            }

            if (x < ship.x + ship.WIDTH && x + WIDTH > ship.x && y + 1 < ship.y + ship.HEIGHT && y + 1 + HEIGHT > ship.y)
            {
                ship.canné = true;
            }
        }
        internal void Destroy(List<Rock> rockList)
        {
            string line = sprite;

            
            Console.SetCursorPosition(x, y);
            Console.Write(new string (' ', sprite.Length));
            destroyed = true;
            //rockList.Remove(this);

            /*var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.ForegroundColor = prev;*/


        }
    }

}