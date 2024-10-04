//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Missile
using System;
using System.Collections.Generic;

namespace SpicyInvader2
{
    /// <summary>
    /// Missile
    /// </summary>
    public class Missile
    {
        const int MAX_SPEED = 100;

        //position
        private int x;
        private int y;

        //image
        private char sprite = '█';
        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        private int speed = 100;//vitesse entre 0 et MAX_SPEED

        //État du missile
        private bool destroyed = false;
        public bool Destroyed { get => destroyed; }

        /// <summary>
        /// Construit et affiche le missile

        public Missile(int x, int y)
        {
            this.x = x;
            this.y = y;

            Console.SetCursorPosition(x, y);
            Console.Write(sprite);
        }
        

        /// <summary>
        /// Déplacement du missile
        /// </summary>
        /// <param name="tic"></param>
        public void NextMove(int tic,Enemy[] ennemies, Ship ship, List<Enemy> ennemiesList)
        {
            if (speed == MAX_SPEED || tic % (MAX_SPEED - speed) == 0)
            {
                //Déplace le missile
                if (y > 0 && !destroyed)
                {
                    Console.MoveBufferArea(x, y, 1, 1, x, --y);
                }
                else
                {
                    //efface le missile
                    Destroy();
                }
            }

            int nombreDElements = ennemiesList.Count;

            for (int i = 0; i < nombreDElements; i++)

            {
                if (x < ennemiesList[i].x + ennemiesList[i].WIDTH && x + WIDTH > ennemiesList[i].x && y < ennemiesList[i].y + ennemiesList[i].HEIGHT && y + HEIGHT > ennemiesList[i].y)
                {
                    ennemiesList[i].Destroy(ennemiesList);
                    Destroy();
                    break;
                }
            }
            
        }

        private void Destroy()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
            destroyed = true;
        }
    }
}
