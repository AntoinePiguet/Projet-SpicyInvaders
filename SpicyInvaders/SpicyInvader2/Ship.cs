//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Vaisseau
using System;

namespace SpicyInvader2
{
    /// <summary>
    /// Vaisseau
    /// </summary>
    class Ship
    {
        //position du vaisseau
        private int x;
        private int y;
        public ConsoleColor color;


        //Dessin du vaisseau
        private string[] sprite;

        //missile du vaisseau
        private Missile missile;

        private int speed = 1; // exemple de valeur
        private const int MAX_SPEED = 10; // exemple de valeur
        private bool destroyed = false; // Déclare la propriété destroyed
        

        /// <summary>
        /// Construit un vaisseau
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="initialX"></param>
        /// <param name="initialY"></param>
        public Ship(string[] sprite, int x, int y, ConsoleColor color)
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
            if (speed == MAX_SPEED || tic % (MAX_SPEED - speed) == 0)
            {
                // Vérifier les collisions avec les ennemis
                /*foreach (var enemy in enemies)
                {
                    if (!enemy.IsDestroyed && x >= enemy.X && x < enemy.X + enemy.SpriteWidth && y == enemy.Y)
                    {
                        // Collision détectée, détruire l'ennemi et le missile
                        enemy.Destroy();
                        this.destroyed = true;
                        return;
                    }
                }
                */
                // Si pas de collision, déplacer le missile
                if (y > 0)
                {
                    Console.MoveBufferArea(x, y, 1, 1, x, --y);
                }
                else
                {
                    // Efface le missile lorsqu'il atteint le haut de l'écran
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                    destroyed = true;
                }
            }


            //Mouvement autonome du missile s'il y en a
            if (IsMissileFired())
            {
            }
            //Le missile est-il arrivé en haut ?
            if (missile.Destroyed)
            {
                missile = null;
            }
            else
            {
                missile.NextMove(tic);//déplacement du missile
            }
        }
            /// <summary>
            /// Indique si un missile est en cours
            /// </summary>
            /// <returns></returns>
            private bool IsMissileFired()
            {
                return missile != null;
            }
        
    }
}
