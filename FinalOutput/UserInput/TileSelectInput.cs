using FinalOutput.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace FinalOutput
{
    public class TileSelectInput : IUserInputService
    {
        public TileSelectInput(int selectedIndexJ, int selectedIndexI)
        {
           
            SelectedIndexJ = selectedIndexJ;
            SelectedIndexI = selectedIndexI;
        }

        public List<List<Frame>> Frames{ get; set; }
        public int CountMax { get; set; }
        public int Row { get; set; }

        public int SelectedIndexJ { get; set; }

        public int SelectedIndexI { get; set; }

        int[,] arrayMap;
        public ConsoleKey GetInput()
        {

            ConsoleKeyInfo userInput;
            do
            {
                userInput = Console.ReadKey(true);

            } while (Console.KeyAvailable);


            arrayMap = new int[3, 3]
                    {
                        {0, 0, 0},
                        {0, 0, 0},
                        {0, 0, 0}
                    };

            switch (userInput.Key)
            {
                case ConsoleKey.LeftArrow:

                    if (SelectedIndexJ > 0)
                    {
                        SelectedIndexJ--;
                    }
                    return ConsoleKey.LeftArrow;


                case ConsoleKey.RightArrow:

                    

                    for (int i = 0; i < Frames.Count; i++)
                    {
                        if (SelectedIndexI == i)
                        {
                            if (SelectedIndexJ < Frames[i].Count - 1)
                            {
                                SelectedIndexJ++;
                                break;
                            }
                        }
                    }

                    return ConsoleKey.RightArrow;

                case ConsoleKey.UpArrow:
                    if (SelectedIndexI > 0)
                    {
                        SelectedIndexI--;
                    }

                    return ConsoleKey.UpArrow;

                case ConsoleKey.DownArrow:
                    //If last row is less than prev row then prev row cannot increment the row 
                    //The last column of the least number of columns in a row


                    //Make a seperate 2d array and make the gaps be the string block or something na para ma indicate na walay val didto
               

                    for (int i = 0; i < Frames.Count; i++)
                    {
                        for (int j = 0; j < Frames[i].Count; j++)
                        {
                            arrayMap[i, j] = 1;
                        }
                    }
                    


                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if(SelectedIndexI == i && SelectedIndexJ == j && SelectedIndexI < Row - 1)
                            {
                                if (arrayMap[++i, j].Equals(1))
                                {
                                    SelectedIndexI++;
                                }
                                break;
                            }
                        }
                    }

                    
                  
                    return ConsoleKey.DownArrow;

                default:
                    return userInput.Key;
            }
        }
    }
}

namespace FinalOutput.Redundant
{
    public class TileSelectInput : IUserInputService // Previous Version
    {
        public TileSelectInput(int selectedIndexJ, int selectedIndexI)
        {

            SelectedIndexJ = selectedIndexJ;
            SelectedIndexI = selectedIndexI;
        }

        public List<List<Frame>> Frames { get; set; }
        public int CountMax { get; set; }
        public int Row { get; set; }

        public int SelectedIndexJ { get; set; }

        public int SelectedIndexI { get; set; }
        int[,] arrayMap = new int[3, 3]
                    {
                        {0, 0, 0},
                        {0, 0, 0},
                        {0, 0, 0}
                    };
        public ConsoleKey GetInput()
        {

            ConsoleKeyInfo userInput;
            do
            {
                userInput = Console.ReadKey(true);

            } while (Console.KeyAvailable);


            switch (userInput.Key)
            {
                case ConsoleKey.LeftArrow:

                    if (SelectedIndexJ > 0)
                    {
                        SelectedIndexJ--;
                    }
                    return ConsoleKey.LeftArrow;


                case ConsoleKey.RightArrow:

                    //Clues: Count

                    for (int i = 0; i < Frames.Count; i++)
                    {
                        if (SelectedIndexI == i)
                        {
                            if (SelectedIndexJ < Frames[i].Count - 1)
                            {
                                SelectedIndexJ++;
                                break;
                            }
                        }
                    }

                    return ConsoleKey.RightArrow;

                case ConsoleKey.UpArrow:
                    if (SelectedIndexI > 0)
                    {
                        SelectedIndexI--;
                    }

                    return ConsoleKey.UpArrow;

                case ConsoleKey.DownArrow:
                    //If last row is less than prev row then prev row cannot increment the row 
                    //The last column of the least number of columns in a row


                    //Make a seperate 2d array and make the gaps be the string block or something na para ma indicate na walay val didto


                    for (int i = 0; i < Frames.Count; i++)
                    {
                        for (int j = 0; j < Frames[i].Count; j++)
                        {
                            arrayMap[i, j] = 1;
                        }
                    }



                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (SelectedIndexI == i && SelectedIndexJ == j && SelectedIndexI < Row - 1)
                            {
                                if (arrayMap[++i, j].Equals(1))
                                {
                                    SelectedIndexI++;
                                }
                                goto end;
                            }
                        }
                    }

                end:

                    return ConsoleKey.DownArrow;

                case ConsoleKey.Enter:
                    return ConsoleKey.Enter;

                case ConsoleKey.A:

                    return ConsoleKey.A;

                case ConsoleKey.D:
                    return ConsoleKey.D;

                case ConsoleKey.D4:
                    return ConsoleKey.D4;

                case ConsoleKey.D2:
                    return ConsoleKey.D2;

                case ConsoleKey.D5:
                    return ConsoleKey.D5;

                case ConsoleKey.Spacebar:
                    return ConsoleKey.Spacebar;

                case ConsoleKey.NumPad4:

                    return ConsoleKey.NumPad4;

                case ConsoleKey.NumPad6:

                    return ConsoleKey.NumPad6;

                case ConsoleKey.D1:
                    return ConsoleKey.D1;
                default:
                    return ConsoleKey.N;
            }
        }
    }
}
