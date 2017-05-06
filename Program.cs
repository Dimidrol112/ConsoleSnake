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
        private static int screenWidth = 30;
        private static int screenHeight = 30;

        static void Main(string[] args)
        {
            Thread inputThread = new Thread(Input);
            inputThread.Start();

            while (work)
            {
                Snake.Move();
                Snake.Render();
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
                    Snake.dirrection = new Vector(-1,0);
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
                Thread.Sleep(0100);
            }
        }

        static class Snake
        {
            public static Position position = new Position(5,5);
            public static Vector dirrection = new Vector(0,0);
            public static Position[] tail;
            public static Position prevPos = new Position(5, 5);

            public static void Render()
            {
                Paint.SetPixel(prevPos, ConsoleColor.Black);
                Paint.SetPixel(position, ConsoleColor.Green);
            }

            public static void Move()
            {
                prevPos.x = position.x;
                prevPos.y = position.y;
                position.x += dirrection.x;
                position.y += dirrection.y;
            }

        }

        static class Paint
        {
            public static void SetPixel(Position pos, ConsoleColor col)
            {
                SetPixel(pos.x, pos.y,col);
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

            public Position(int x,int y)
            {
                this.x = x;
                this.y = y;
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
