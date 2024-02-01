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

namespace Projet_SpicyInvader
{
    internal class Game
    {      

        /// <summary>
        /// Lancement du programme
        /// </summary>
        /// <param name="args"></param>
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

            Console.ReadLine();
        }

        /// <summary>
        /// Lance le jeu
        /// </summary>
        public static void GameSP()
        {
            SpaceShip playerShip = new SpaceShip();   
            Missile missile = new Missile(playerShip.PositionX, 33);
            playerShip.Update();

            ConsoleKeyInfo MissileLauching = Console.ReadKey();
            while (playerShip.Alive() != false)
            {
                //Si l'user appuie sur Espace, le missile se lance
                if (MissileLauching.Key == ConsoleKey.Spacebar)
                {
                     missile.Shoot();                                        
                }
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