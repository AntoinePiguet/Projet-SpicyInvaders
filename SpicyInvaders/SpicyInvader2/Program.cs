//Auteur : JMY
//Date   : 21.1.2020 
//Lieu   : ETML
//Descr. : Spicy invader2
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
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
            int tac = 0;


            int copy = 0;
            int v = 0;
            int b = 0;
            int score = 0;
            int enemyCount = 1000;
            double realFps;
            int AMMO_X = 1;
            int AMMO_Y = 0;
            string[] ALIEN = { "  ▄ ▄  ",
                               "▄▀███▀▄" };
            string[] UFO = { " ▄▄▄▄▄ ",
                             "▀█▀█▀█▀" };
            string[] PlAYER = { "  ▄  ",
                                "▄███▄",
                                " ▀ ▀ " };
            string KAYOU = "████";
            char bonusAmmo = '▄';


            List<Enemy> ennemiesList = new List<Enemy>();
            List<Rock> rockList = new List<Rock>();
            List<Bonus> bonusList = new List<Bonus>();

            //Création des objets
            Ship ship = new Ship(PlAYER, Console.WindowWidth / 2, 25, ConsoleColor.Yellow);
            //Bonus bonus = new Bonus(bonusAmmo, ConsoleColor.Cyan);

            Enemy[] enemies = new Enemy[enemyCount];
            Bonus[] bonusTab = new Bonus[1000];
            Rock[] rock = new Rock[1000];


            //Affichage initial des objets
            ship.Init();

            //Pour régler les FPS
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //affichage de l'ammo
            Console.SetCursorPosition(AMMO_X, AMMO_Y);
            Console.Write("Ammo : "+ ship.nmbAmmo);

            //boucle de jeu
            while (true)
            {
                tic++; //FRAME
                tic2++;

                //Gère le déplacement + tir du vaisseau
                ship.NextMove(tic, enemies, ennemiesList);


                if (tic2 % 100 == 0)
                {
                    
                    int randomNumber = random.Next(10, Console.WindowWidth-14);
                    
                    while (true)
                    {
                        if (randomNumber + 7 > copy + tac && randomNumber < copy + tac)
                        {
                            randomNumber = random.Next(4, Console.WindowWidth);
                        }
                        else
                        {
                            break;
                        }
                        tac = 0;
                    }
                    copy = randomNumber;
                    tac++;
                    
                    
                    var candidate = new Enemy(null, randomNumber, 4, ConsoleColor.Black, 9, 2);
                    foreach (var enemi in ennemiesList)
                    {
                        while (enemi.CollidesWith(candidate))
                        {
                            candidate = new Enemy(null, randomNumber, 4, ConsoleColor.Black, 9, 2);
                        }
                    }
                    
                    rock[v] = new Rock(KAYOU, randomNumber, 1, ConsoleColor.Green, 4, 1);
                    rockList.Add(rock[v]);
                    rockList[v].Init(randomNumber, 1);

                    if(v % 2 == 0)
                    {
                        enemies[v] = new Enemy(UFO, randomNumber, 7, ConsoleColor.Red, 9, 3);
                    }
                    else
                    {
                        enemies[v] = new Enemy(ALIEN, randomNumber, 7, ConsoleColor.Blue, 9, 3);
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
                    ennemiesList[i].NextMove(tic, ennemiesList);
                    if (ennemiesList[i].destroyed == true)
                    {
                        
                        bonusTab[b] = new Bonus(bonusAmmo, ConsoleColor.Cyan, bonusList);
                        bonusList.Add(bonusTab[b]);
                        bonusList[b].Init(ennemiesList[i].x, ennemiesList[i].y);
                        b++;
                    }
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
