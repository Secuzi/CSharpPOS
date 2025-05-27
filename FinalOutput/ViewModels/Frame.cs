using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    public delegate void DrawFrame(int width, int x, int y, ConsoleColor color);
    /// <summary>
    /// Class that handles displaying of container boxes in console.
    /// </summary>
    public class Frame
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int MinPosX { get; set; }
        public int MinPosY { get; set; }
        public int CurrentPosX { get; set; }
        public int CurrentPosY { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        /// <summary>
        /// the character for a vertical line in the frame
        /// </summary>
        private const char FRAME_VERTICAL = '│';


        /// <summary>
        /// the character for an horizontal line in the frame
        /// </summary>
        private const char FRAME_HORIZONTAL = '─';

        /// <summary>
        /// the character for a top left corner in the frame
        /// </summary>
        private const char FRAME_TOP_LEFT = '╭';

        /// <summary>
        /// the character for a top right corner in the frame
        /// </summary>
        private const char FRAME_TOP_RIGHT = '╮';

        /// <summary>
        /// the character for a bottom left corner in the frame
        /// </summary>
        private const char FRAME_BOTTOM_LEFT = '╰';

        /// <summary>
        /// the character for bottom right corner in the frame
        /// </summary>
        private const char FRAME_BOTTOM_RIGHT = '╯';

        public Frame(int width, int height, int x, int y)
        {
            Width = width;
            Height = height;
            MinPosX = x;
            MinPosY = y;
            PosX = MinPosX;
            PosY = MinPosY;
            CurrentPosX = MinPosX + 2; //CurrentPosX & PosY kay gi offset na
            CurrentPosY = MinPosY + 1;
        }

        public void WriteLine<T>(T item)
        {
            
            Console.SetCursorPosition(CurrentPosX, CurrentPosY);
            
            Console.Write(item);
            CurrentPosY++;
        }
        public void Write<T>(T item, int posX, int posY)
        {

            Console.SetCursorPosition(posX, posY);
            Console.Write(item);
        }
        public void DrawVerticalLine(int height, int x, int y, ConsoleColor color)
        {
           DrawLine(height, x, y, color, FRAME_VERTICAL, (widthDel, xDel, yDel, colorDel, FRAME_VERTICAL) =>
           {
               for (int i = 0; i < height; i++)
               {
                   Console.ForegroundColor = color;
                   Console.SetCursorPosition(x, y);
                   Console.Write(FRAME_VERTICAL);
                   y++;
               }
               Console.ResetColor();
           });
        }
        
        void DrawLine(int width, int x, int y, ConsoleColor color, char symbol, Action<int, int, int, ConsoleColor, char> draw)
        {
            draw.Invoke(width, x, y, color, symbol);
        }

        //Use a delegate
        public void DrawHorizontalLine(int width, int x, int y, ConsoleColor color)
        {
            DrawLine(width, x, y, color, FRAME_HORIZONTAL, (widthDel, xDel, yDel, colorDel, FRAME_HORIZONTAL) =>
            {
                for (int i = 0; i < width; i++)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(x, y);
                    Console.Write(FRAME_HORIZONTAL);
                    x++;
                }
                Console.ResetColor();
            });

        }

        public void PrintFrame(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            
            for (int row = 0; row < Height; row++)
            {
                for(int col = 0; col < Width; col++)
                {
                    Console.SetCursorPosition(PosX, PosY);
                    if (row == 0 && col == 0)
                    {
                        Console.Write(FRAME_TOP_LEFT);
                    }
                    else if (row == 0 && col == Width - 1)
                    {
                        Console.Write(FRAME_TOP_RIGHT);
                    }
                    else if (row == Height - 1 && col == 0)
                    {
                        Console.Write(FRAME_BOTTOM_LEFT);
                    }
                    else if (row == Height - 1 && col == Width - 1)
                    {
                        Console.Write(FRAME_BOTTOM_RIGHT);
                    }
                    else if (row == 0 || row == Height - 1)
                    {
                        Console.Write(FRAME_HORIZONTAL);

                    }
                    else if (col == 0 || col == Width - 1)
                    {
                        Console.Write(FRAME_VERTICAL);

                    }
                    else
                    {
                        Console.Write(' ');
                    }

                    if (PosX < Console.BufferWidth)
                    {
                        PosX++;
                    }

                }
                PosX = MinPosX;
                PosY++;

            }

            Console.ResetColor();

        }


    }
}
