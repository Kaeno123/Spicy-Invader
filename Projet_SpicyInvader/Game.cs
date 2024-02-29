﻿///ETML
///Auteur : Kaeno Eyer
///Date : 18.01.2024
///Description : Contient les mécanismes fondamentaux du jeu.
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Timers;


namespace Projet_SpicyInvader
{
    internal class Game
    {      
        
        public Game()
        {
            PlayerShip player;
            Missile missile;
            Invaders invaders;
        }
        /// <summary>
        /// Lancement du programme
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {     
            //initialisation des dimensions de la fenêtre de la console
            Console.WindowWidth = 120;
            Console.WindowHeight = 40;
            //Lancement du menu du jeu
            Menu();           
        }
        
        /// <summary>
        /// Menu de démarrage
        /// </summary>
        public static void Menu()
        {
            string choiceOfMenu;
            do
            {
                Console.WriteLine("                 **********************************************************************************" );
                Console.WriteLine("                                            Bienvenue sur Space Invaders");
                Console.WriteLine("                 **********************************************************************************\n\n");
                Console.WriteLine("1. Jouer\n2. Options\n3. A propos\n4. Quitter\n\n");
                Console.Write("Mettez le chiffre de l'action que vous souhaitez réaliser : ");
                choiceOfMenu = Console.ReadLine();

                Console.Clear();
                switch (choiceOfMenu)
                {
                    case "1":
                          GameSP();
                        break;

                    case "2":
                        Options();
                        break ;

                    case "3":
                        Console.WriteLine("");
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                        default:
                        Error();
                        Menu();
                        break;
                }               

            } while (true);
        }
        
        /// <summary>
        /// Lance le jeu
        /// </summary>
        public static void GameSP()
        {
            int _score = 0;
            int _highscore = 0;            
           
            PlayerShip playerShip = new PlayerShip();
            Invaders badInvaders = new Invaders();
            Missile missile = new Missile(playerShip.PositionX, 33);

             while (playerShip.Alive() != false)
             {  
                _score++;
                if (_score > _highscore)
                {
                    _highscore = _score;
                }
                Console.SetCursorPosition(0,0);
                Console.WriteLine($"Score : {_score}    High-Score : {_highscore} ");

                KeyPressChosen(playerShip, missile);
                Update(playerShip, missile,badInvaders);
                Draw(playerShip, missile, badInvaders);
                
                Thread.Sleep(20);
             }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerShip"></param>
        /// <param name="m"></param>
        public static void KeyPressChosen(PlayerShip playerShip, Missile m)
        {
            if (Keyboard.IsKeyDown(Key.Left))
            {
                playerShip.Move(false);

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (m.IsMissile is false)
                    {
                        m._x = playerShip.PositionX;
                        m._y = 33;
                        m.IsMissile = true;
                    }
                }
            }
            else if(Keyboard.IsKeyDown(Key.Right))
            {
                playerShip.Move(true);

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (m.IsMissile is false)
                    {
                        m._x = playerShip.PositionX;
                        m._y = 33;
                        m.IsMissile = true;
                    }
                }
            }
            else if (Keyboard.IsKeyDown(Key.Space))
            {
                if (m.IsMissile is false)
                {
                    m._x = playerShip.PositionX;
                    m._y = 33;
                    m.IsMissile = true;
                }
            }                                      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerShip"></param>
        /// <param name="m"></param>
        public static void Update(PlayerShip playerShip, Missile m, Invaders enemies)
        {
            if (m._y == 2)
            {
                m.IsMissile = false;   
            }
            else if (m.IsMissile is true)
            {
                m.Shoot();
            }
            if (enemies.Invadersdie is false)
            {
                enemies.Update();
            }
            else if(enemies.Invadersdie is true)
            {
                
            }
                    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerShip"></param>
        /// <param name="m"></param>
        public static void Draw(PlayerShip playerShip, Missile m, Invaders enemies)
        {
            playerShip.Draw();
            if (m.IsMissile == true)
            {
                m.DrawMissile();
            }
            enemies.Draw();

            //Hitbox
            if (m.Y == enemies.Y && m.X == enemies.Y)
            {
                enemies.Undraw();
                enemies.Invadersdie = true;
            }

            if (enemies.Y == 35)
            {
                Console.SetCursorPosition(15, 15);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over, appuyer sur un bouton pour revenir au menu.");
            }
        }

        /// <summary>
        /// Options du jeu (Son et difficultés)
        /// </summary>
        public static void Options()
        {
            string choiceOfOptions;
            Console.WriteLine("1. Activer / Désactiver le son\n2. Niveau de difficulté\n3. Revenir au menu\n");
            Console.Write("Mettez le chiffre de l'action que vous souhaitez réaliser : ");
            choiceOfOptions = Console.ReadLine();
           
            if (choiceOfOptions == "1")
            {
                Console.Clear();
                Console.WriteLine("1. Activer le son\n2. Désactiver le son\n");
                Console.Write("Mettez le chiffre de l'action que vous souhaitez réaliser : ");
                choiceOfOptions = Console.ReadLine();

                if (choiceOfOptions == "1")
                {
                    Console.Clear();
                    ShowAndErase("Son activé", TimeSpan.FromSeconds(1));
                    Menu();
                }
                else if (choiceOfOptions == "2")
                {
                    Console.Clear();
                    ShowAndErase("Son désactivé", TimeSpan.FromSeconds(1));
                    Menu();
                }
                else
                {
                    Error();
                    Options();
                }
            }
            else if (choiceOfOptions == "2")
            {
                Console.Clear();
                Console.WriteLine("1. Niveau facile\n2. Niveau difficile\n");
                Console.Write("Mettez le chiffre de l'action que vous souhaitez réaliser : ");
                choiceOfOptions= Console.ReadLine();

                if (choiceOfOptions == "1")
                {
                    Console.Clear();
                    ShowAndErase("Niveau facile activé", TimeSpan.FromSeconds(1));
                    Menu();
                }
                else if (choiceOfOptions == "2")
                {
                    Console.Clear();
                    ShowAndErase("Niveau difficile activé", TimeSpan.FromSeconds(1));
                    Menu();
                }                
                else
                {
                    Error();
                    Options();
                }
            }
            else if (choiceOfOptions == "3")
            {
                Console.Clear();
                Menu();
            }
            else
            {
                Error();
                Options();
            }
        }

        /// <summary>
        /// S'éxécute lorsque l'user ne met pas les entrées attendues du clavier
        /// </summary>
        public static void Error()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vous n'avez pas inséré les chiffres attendus. Veuillez recommencer.\n\n");
            Console.ResetColor();
        }

        public static void ShowAndErase(string text, TimeSpan TimeToShow)
        {
            Console.WriteLine(text);
            Thread.Sleep(TimeToShow);
            Console.Clear();
        }
    }
}