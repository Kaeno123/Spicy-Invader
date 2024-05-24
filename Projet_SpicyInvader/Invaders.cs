﻿///ETML
///Auteur : Kaeno Eyer
///Date : 19.04.2024
///Description : Classe contenant les propriétés de l'ennemi
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Projet_SpicyInvaderTests")]

namespace Projet_SpicyInvader
{
    public class Invaders
    {        
        private int _indicator = 0;
        private int _y = 3;
        private int _x = 5;
        private int _borderLimitLeft = 5;
        private int _borderLimitRight = 110;
        internal bool goLeftElseRight = false;
        private int _numberInvaders = 15;
        List<Invaders> _invaders = new List<Invaders>();

        public int Y { get { return _y; } set { _y = value; } }
        public int X { get { return _x; } set { _x = value; } }
        public int NumberInvaders { get { return _numberInvaders; } set { _numberInvaders = value;} }
        public List<Invaders> Invaderss { get { return _invaders; } set { _invaders = value; } }

        /// <summary>
        /// Déplacement de l'ennemi
        /// </summary>
        public void Update()
        {
            _indicator += 15;
            if (_indicator % 10 == 0)
            {
                foreach (Invaders enemies in _invaders)
                {
                    if (goLeftElseRight is false)
                    {
                        enemies._x++;
                    }
                    else if (goLeftElseRight is true)
                    {
                        enemies._x--;
                    }

                    if (enemies._x == _borderLimitRight)
                    {
                        Console.SetCursorPosition(_borderLimitRight, enemies._y);
                        UndrawActualPosition();
                        foreach (Invaders enemiess in _invaders)
                        {
                            enemiess._y++;
                            Console.SetCursorPosition(enemiess._x -1, enemiess._y - 1);
                            Console.WriteLine("      ");
                        }
                        goLeftElseRight = true;
                    }
                    else if (enemies._x == _borderLimitLeft)
                    {
                        Console.SetCursorPosition(_borderLimitLeft, enemies._y);
                        UndrawActualPosition();
                        foreach (Invaders enemiess in _invaders)
                        {
                            enemiess._y++;
                            Console.SetCursorPosition(enemiess._x, enemiess._y - 1);
                            Console.WriteLine("      ");
                        }
                        goLeftElseRight = false;
                    }
                }
            }
        }

        /// <summary>
        /// Créer un ennemi et ses propriétés, et l'ajoute à la liste d'ennemis 
        /// </summary>
        public void CreateInvaders()
        {
            _y = 5;
            _x = 5;

            for (int j = 0; j < _numberInvaders; j++)
            {
                if (j == 5 || j == 10)
                {
                    this._x = 5;
                    this._y++;
                }

                _invaders.Add(new Invaders());
                _invaders[j]._x = this._x;
                _invaders[j]._y = this._y;

                this._x += 6;
            }            
        }

        /// <summary>
        /// Dessine l'ennemi dans sa position actuelle
        /// </summary>
        public void Draw()
        {
            foreach (Invaders enemies in _invaders)
            {
                Console.SetCursorPosition(enemies._x, enemies._y);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("´-_-`");
                Console.ResetColor();
                Undraw();
            }
        }
        /// <summary>
        /// Efface l'ennemi dans son ancienne position
        /// </summary>
        public void Undraw()
        {
            foreach (Invaders enemies in _invaders)
            {
                if (goLeftElseRight is false)
                {
                    Console.SetCursorPosition(enemies._x -1, enemies._y);
                    Console.Write(" ");
                }
                else if (goLeftElseRight is true)
                {
                    Console.SetCursorPosition(enemies._x +5, enemies._y);
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Efface l'ennemi dans sa position actuelle
        /// </summary>
        public void UndrawActualPosition()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write("     ");
        }

        /// <summary>
        /// Si un ennemie est touché, il meurt
        /// </summary>
        public void InvaderDie(List<Invaders> Enemies, int index)
        {
            Enemies.RemoveAt(index);
        }

        /// <summary>
        /// Gère les collisions de l'ennnemi
        /// </summary>
        /// <param name="m">Missile du vaisseau</param>
        /// <param name="score">Score de la partie</param>
        /// <param name="playerShip">Vaisseau de l'user</param>
        public void Collision(Missile m, Score score, PlayerShip playerShip)
        {
            for (int j = 0; j < _numberInvaders; j++)
            {
                //Supprime l'ennemi et le missile si les 2 se touchent
                if (m.hitbox().IntersectsWith(_invaders[j].hitbox()))
                {
                    m.UnDrawMissileActualPosition();
                    _invaders[j].UndrawActualPosition();
                    _invaders.RemoveAt(j);
                    _numberInvaders--;

                    //Si le groupe d'ennemi est tué, un autre réapparait
                    if (_invaders.Count() == 0)
                    {
                        _numberInvaders = 15;
                        CreateInvaders();
                    }
                    score.Scoree += 20;
                    break;
                }
            }
            //Si l'ennemi est sur la même hauteur que le vaisseau -> PERDU
            if (_invaders[_numberInvaders - 1].Y == 35)
            {
                Game.GameOver(playerShip);
            }
        }

        /// <summary>
        /// Hitbox de l'ennemi
        /// </summary>
        /// <returns>retourne la hitbox</returns>
        public Rectangle hitbox()
        {
            return new Rectangle(_x, _y, 5, 1);
        }
    }
}