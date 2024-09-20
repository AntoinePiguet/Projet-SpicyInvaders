//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Ennemi
using System;
using System.Security.Policy;

namespace SpicyInvader2
{
    /// <summary>
    /// Ennemi
    /// </summary>
    class Enemy
    {
        const int MARGIN_LEFT = 10;
        const int MARGIN_RIGHT = 10;

        private string[] sprite;
        public ConsoleColor color;
        private int x;
        private int y;
        private bool directionRight = true;

        public int speed = 95;//Vitesse entre 0 et 100

        /// <summary>
        /// Construit un objet ennemi
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Enemy(string[] sprite, int x, int y, ConsoleColor color)
        {
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.color = color;
        }

        /// <summary>
        /// Initialise le vaisseau (le dessine à l'écran)
        /// </summary>
        public void Init()
        {

            Draw();
        }

        public void Draw()
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;

            for (int i = 0; i < sprite.Length; i++)
            {
                string line = sprite[i];
                Console.SetCursorPosition(x, y + i);
                Console.Write(line);
            }


            Console.ForegroundColor = prev;
        }
        

        /// <summary>
        /// Gère le mouvement+tir
        /// </summary>
        public void NextMove(int tic)
        {
            
            //Éxécute l'action selon la vitesse
            if(tic%(100-speed)==0)
            {
                int newX = x;
                int newY = y;

                if (directionRight)
                {
                    newX = newX + 1;// speed/10;
                }
                else
                {
                    newX = newX - 1;// speed/10;
                }

                //Descend d'une ligne s'il touche le bord
                if (newX > Console.WindowWidth - MARGIN_RIGHT || newX <= MARGIN_LEFT)
                {
                    directionRight = !directionRight;//inverse la direction
                    newY=newY+2;//descend verticalement

                }

                //Déplace efficacement le vaisseau
                Console.MoveBufferArea(x, y, sprite[0].Length, sprite.Length, newX, newY);

                //Mise à jour de la nouvelle position
                x = newX;
                y = newY;
            }

        }

    }
}
