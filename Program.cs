using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleSnake
{
    class Program
    {
        static bool work = true;
        private static int screenWidth = 80;
        private static int screenHeight = 25;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.BufferWidth = screenWidth;
            Console.BufferHeight = screenHeight;

            Thread inputThread = new Thread(Input);
            inputThread.Start();
            Paint.WriteText(new Position(0, 0), "Snake");
            Food.Eat();

            while (work)
            {
                Snake.Move();
                Snake.Render();
                Snake.CollisionCheck();
                Paint.WriteText(new Position(0, 1), "Score=" + Snake.tail.Count);
                Thread.Sleep(500);
            }

        }

        public static void Input()
        {
            while (work)
            {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.A)
                {
                    Snake.dirrection = new Vector(-1, 0);
                }
                if (ki.Key == ConsoleKey.D)
                {
                    Snake.dirrection = new Vector(1, 0);
                }
                if (ki.Key == ConsoleKey.S)
                {
                    Snake.dirrection = new Vector(0, 1);
                }
                if (ki.Key == ConsoleKey.W)
                {
                    Snake.dirrection = new Vector(0, -1);
                }
                if (ki.Key == ConsoleKey.F)
                {
                    Snake.AddCell();
                }

                Thread.Sleep(0100);
            }
        }

        static class Food
        {
            public static Position position = new Position(10,10);

            public static void Eat()
            {
                Paint.SetPixel(position, ConsoleColor.Green);
                position.x = rnd.Next(0, screenWidth);
                position.y = rnd.Next(0, screenHeight);
                Paint.SetPixel(position, ConsoleColor.DarkYellow);
            }
        }

        static class Snake
        {
            public static Position position = new Position(5, 5);
            public static Vector dirrection = new Vector(1, 0);
            public static List<Position> tail = new List<Position>();
            public static Position prevPos = new Position(5, 5);

            public static void Render()
            {
                //Head
                Paint.SetPixel(prevPos, ConsoleColor.Black);
                Paint.SetPixel(position, ConsoleColor.Green);

                DeRenderTail();
                ShiftTail();
                RenderTail();
            }

            static void RenderTail()
            {
                foreach (var cell in tail)
                {
                    Paint.SetPixel(cell, ConsoleColor.Green);
                }
            }

            static void DeRenderTail()
            {
                foreach (var cell in tail)
                {
                    Paint.SetPixel(cell, ConsoleColor.Black);
                }
            }

            static void ShiftTail()
            {
                if (tail.Count != 0)
                    tail.RemoveAt(0);
                    tail.Add(prevPos.Clone());
            }

            public static void AddCell()
            {
                tail.Add(position.Clone());
            }

            public static void Move()
            {
                prevPos.x = position.x;
                prevPos.y = position.y;
                position.x += dirrection.x;
                position.y += dirrection.y;
            }

            public static void CollisionCheck()
            {
                foreach(var cell in tail)
                {
                    if (cell.Equals(position))
                    {
                        DeRenderTail();
                        position = new Position(5, 5);
                        tail = new List<Position>();
                    }                        
                }

                if (Food.position.Equals(position))
                {
                    AddCell();
                    Food.Eat();
                }
            }
        }

        static class Paint
        {
            public static void SetPixel(Position pos, ConsoleColor col)
            {
                SetPixel(pos.x, pos.y, col);
            }

            public static void SetPixel(int x, int y, ConsoleColor col)
            {
                if (x >= 0 && y >= 0 && x < Program.screenWidth && y < Program.screenHeight)
                {
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = col;
                    Console.Write(" ");
                }
            }

            public static void WriteText(Position pos,string text)
            {
                Console.SetCursorPosition(pos.x, pos.y);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(text);
            }
            /*
            public static void Line(int x, int y)
            {
                for (int i = 0; i < Program.playerLength; i++)
                    SetPixel(x, i + y, ConsoleColor.Red);
            }*/
        }

        class Position
        {
            public int x = 0;
            public int y = 0;

            public Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Position Clone()
            {
                return new Position(this.x, this.y);
            }

            public override bool Equals(object obj)
            {
                Position pos = (Position)obj;
                if (x == pos.x && y == pos.y)
                    return true;
                else
                    return false;
            }
        }

        class Vector
        {
            public int x = 0;
            public int y = 0;

            public Vector(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
