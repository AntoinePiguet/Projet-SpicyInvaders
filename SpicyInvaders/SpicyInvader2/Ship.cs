
//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Vaisseau
using System;
using System.Collections.Generic;

namespace SpicyInvader2
{
    /// <summary>
    /// Vaisseau
    /// </summary>
    
    public class Ship
    {
        //position du vaisseau
        public int x;
        public int y;
        public int WIDTH = 5;
        public int HEIGHT = 3;
        public bool destroyed = false;
        public ConsoleColor color;
        public bool canné = false;
        public int nmbAmmo = 3;


        //Dessin du vaisseau
        public string[] sprite;

        //missile du vaisseau
        public Missile missile;

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
        public void NextMove(int tic, Enemy[] ennemies, List<Enemy> ennemiesList)
        {
            int newX = x; //nouvelle coordonnées

            //Mouvement du vaisseau géré par l'utilisateur avec gestion de l'appui long sur une touche (nativekeyboard)
            if (Console.KeyAvailable)
            {
                //GAUCHE
                if (NativeKeyboard.IsKeyDown(KeyCode.Left))
                {
                    if (x > 0)
                    {
                        newX--;
                    }
                }
                //DROITE
                else if (NativeKeyboard.IsKeyDown(KeyCode.Right))
                {
                    if (x < Console.WindowWidth)
                    {
                        newX++;
                    }
                }
                //BARRE ESPACE
                else if (NativeKeyboard.IsKeyDown(KeyCode.Spacebar))
                {
                    if(nmbAmmo > 0) 
                    {
                        if (!IsMissileFired())
                        {
                            missile = new Missile(x, y - 1);
                            nmbAmmo--;
                            Console.SetCursorPosition(1, 0);
                            Console.Write("Ammo : " + nmbAmmo);
                        }
                    }
                }

                //Le vaisseau s'est-il déplacé ?
                if (newX != x)
                {
                    //Déplace efficacement le vaisseau
                    Console.MoveBufferArea(x, y, sprite[0].Length, sprite.Length, newX, y);

                    //Mise à jour de la nouvelle position
                    x = newX;
                }
            }

            //Mouvement autonome du missile s'il y en a
            if (IsMissileFired())
            {
                //Le missile est-il arrivé en haut ?
                if (missile.Destroyed)
                {
                    missile = null;
                }
                else
                {
                    missile.NextMove(tic, ennemies, this, ennemiesList);//déplacement du missile
                }
            }

            int nombreDElements = ennemiesList.Count;
            for (int i = 0; i < nombreDElements; i++)
            {
                if (x < ennemiesList[i].x + ennemiesList[i].WIDTH && x + WIDTH > ennemiesList[i].x && y < ennemiesList[i].y + ennemiesList[i].HEIGHT && y + HEIGHT > ennemiesList[i].y)
                {
                    ennemiesList[i].Destroy(ennemiesList);
                    destroyed = Destroy(destroyed, x, y, sprite);
                    break;
                }
            }

            if (canné == true)
            {
                destroyed = Destroy(destroyed, x, y, sprite);
            }
        }

        public bool Destroy(bool destroyed, int x, int y, string[] sprite)
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                string line = sprite[i];
                Console.SetCursorPosition(x, y + i);
                Console.Write(new string(' ', line.Length));
            }
            destroyed = true;
            return destroyed;

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