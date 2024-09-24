//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Spicy invader2
using System;
using System.Diagnostics;
using System.Threading;

namespace SpicyInvader2
{
    /// <summary>
    /// Spicy Invader
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Prépare la console
            Console.WindowWidth = 50;
            Console.WindowHeight = 30;

            //pour régler les FPS
            int FPS_X = Console.WindowWidth - 10;
            int FPS_Y = 0;
            const float FPS = 25.0f;
            const float MS_PER_FRAME = 1000 / FPS;
            float delay;
            int tic = 0;
            int enemyCount = 3;
            double realFps;
            bool destroyed = false; // Déclare la propriété destroyed
            const int MAX_SPEED = 10;
            string[] ALIEN = { "  ▄ ▄  ", "▄▀███▀▄" };
            string[] UFO = { " ▄▄▄▄▄ ", "▀█▀█▀█▀" };
            string[] PlAYER = { "  ▄  ", "▄███▄", " ▀ ▀ " };

            //Création des objets
            Ship ship = new Ship(PlAYER, Console.WindowWidth / 2, 25, ConsoleColor.Yellow);
            Enemy enemy2 = new Enemy(UFO, Console.WindowWidth / 3, 10, ConsoleColor.Red);
            Enemy[] enemies = new Enemy[enemyCount];

            for (int i = 0; i < enemyCount; i++)
            {
                enemies[i] = new Enemy(ALIEN, (10 * (i + 1)), 5, ConsoleColor.Blue);
                enemies[i].Init();
            }

            //Affichage initial des objets
            ship.Init();
            //enemy1.Init();
            enemy2.Init();

            //Pour régler les FPS
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //boucle de jeu
            while (true)
            {
                tic++; //FRAME


                //Gère le déplacement + tir du vaisseau
                ship.NextMove(tic, enemies);

                //Gère le déplacement + tir de l'enemi
                for (int i = 0; i < enemies.Length; i++)
                {

                    enemies[i].NextMove(tic);
                }
                //enemy1.NextMove(tic);
                enemy2.NextMove(tic);

                //Calcl et affichage FPS
                realFps = Convert.ToDouble(tic) / sw.Elapsed.TotalSeconds;
                Console.SetCursorPosition(FPS_X, FPS_Y);
                Console.WriteLine("FPS: {0,-10:#.00}", realFps);


                //Délai éventuel pour ajuster les FPS
                delay = tic * MS_PER_FRAME - sw.ElapsedMilliseconds;
                if (delay > 0)
                {
                    //Reset pour éviter un overflow
                    if (tic > int.MaxValue - 1)
                    {
                        tic = 0;
                        sw.Restart();
                    }
                    Thread.Sleep(Convert.ToInt32(delay));//Ajustement au FPS

                }
            }
        }
    }
}
