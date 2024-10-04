//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Spicy invader2
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SpicyInvader2
{
    /// <summary>
    /// Spicy Invader
    /// </summary>
    class Program
    {
        static Random random = new Random();
        

        public static void Main(string[] args)
        {
            Console.WindowWidth = 50;
            Console.WindowHeight = 30;


            //Prépare la console

            //pour régler les FPS
            int FPS_X = Console.WindowWidth - 10;
            int FPS_Y = 0;
            const float FPS = 25.0f;
            const float MS_PER_FRAME = 1000 / FPS;
            float delay;
            int tic = 0;
            int tic2 = 0;

            int v = 0;
            int score = 0;
            int enemyCount = 1000;
            double realFps;
            string[] ALIEN = { "  ▄ ▄  ",
                               "▄▀███▀▄" };
            string[] UFO = { " ▄▄▄▄▄ ",
                             "▀█▀█▀█▀" };
            string[] PlAYER = { "  ▄  ",
                                "▄███▄",
                                " ▀ ▀ " };
            string KAYOU = "████";


            List<Enemy> ennemiesList = new List<Enemy>();
            List<Rock> rockList = new List<Rock>();

            //Création des objets
            Ship ship = new Ship(PlAYER, Console.WindowWidth / 2, 25, ConsoleColor.Yellow);

            Enemy[] enemies = new Enemy[enemyCount];
            Enemy enemy = new Enemy(ALIEN, 0, 0, ConsoleColor.Blue, 7, 3);

            Rock[] rock = new Rock[1000];

            /*for (int i = 0; i < enemyCount; i++)
            {
                if (i < enemyCount - 3)
                {
                    enemies[i] = new Enemy(ALIEN, (10 * (i + 1)), 4, ConsoleColor.Blue, 7, 3);
                }
                else
                {
                    enemies[i] = new Enemy(UFO, ((10 * i) - 10), 10, ConsoleColor.Red, 7, 3);
                }
                enemies[i].Init();
                ennemiesList.Add(enemies[i]);
            }*/

            //Affichage initial des objets
            ship.Init();

            //Pour régler les FPS
            Stopwatch sw = new Stopwatch();
            sw.Start();


            //boucle de jeu
            while (true)
            {
                tic++; //FRAME
                tic2++;
                //Gère le déplacement + tir du vaisseau
                ship.NextMove(tic, enemies, ennemiesList);


                if (tic2 % 50 == 0)
                {
                    int randomNumber = random.Next(1, Console.WindowWidth - 10);
                    bool caseLibreTrouvee = false;

                    var candidate = new Enemy(null, randomNumber, 4, ConsoleColor.Black, 9, 2);

                    foreach (var enemi in ennemiesList)
                    {
                        while (enemi.CollidesWith(candidate))
                        {
                            randomNumber = random.Next(1, Console.WindowWidth - 10);
                            candidate = new Enemy(null, randomNumber, 4, ConsoleColor.Black, 9, 2);
                        }
                    }
                    
                    /*
                    do
                    {
                        int j = 0;
                        for (int l = 0; l > ennemiesList.Count; l++)
                        {
                            //collision détectée
                            if ((randomNumber + enemy.WIDTH > ennemiesList[j].x && randomNumber < ennemiesList[j].x + enemy.WIDTH) && ennemiesList[j].y == 4)
                            {
                                randomNumber = random.Next(1, Console.WindowWidth - 10);
                                caseLibreTrouvee = false;
                                break;//optimisation
                            }
                            else
                            {
                                caseLibreTrouvee = true;
                            }
                            j++;

                        }
                    } while (caseLibreTrouvee==false);
                    */

                    rock[v] = new Rock(KAYOU, randomNumber, 2, ConsoleColor.Green, 4, 1);
                    rockList.Add(rock[v]);
                    rockList[v].Init(randomNumber, 2);

                    if(randomNumber < 20)
                    {
                        enemies[v] = new Enemy(UFO, randomNumber, 4, ConsoleColor.Red, 7, 3);
                    }
                    else
                    {
                        enemies[v] = new Enemy(ALIEN, randomNumber, 4, ConsoleColor.Blue, 7, 3);
                    }
                    enemies[v].Init();
                    ennemiesList.Add(enemies[v]);

                    v++;
                    tic2 = 0;
                }


                //Gère le déplacement + tir de l'enemi
                int nombreDElements = ennemiesList.Count;
                for (int i = 0; i < nombreDElements; i++)
                {
                    ennemiesList[i].NextMove(tic);
                }


                int NombreRochers = rockList.Count;
                for (int r = 0; r < NombreRochers; r++)
                {
                    rockList[r].NextMove(tic, rockList, ennemiesList, ship);
                }




                if (nombreDElements >= 100 || ship.destroyed == true)
                {
                    FinDuJeu(score, ship);
                    break;
                }
                 
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

        public static void FinDuJeu(int score, Ship ship)
        {
            Console.Clear();
            if(ship.destroyed)
            {
                Console.WriteLine("\n\n\tYou died, game over...");
                Console.WriteLine("\n\tYour score is : "+ score);
            }
            else
            {
                Console.WriteLine("\n\n\t You killed all the Spaceinvaders ");
                Console.WriteLine("\n\tYour score is : " + score);
            }
            Console.WriteLine("\nPress enter to quit");

            Console.ReadLine();


        }
    }

}
