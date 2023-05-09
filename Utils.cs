using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TasosLib
{
    public static class Utils
    {
        public static void PrintProgress(int current, int total)
        {
            double progressPercentage = (double)current / total * 100;
            int progressBarWidth = Console.WindowWidth - 10;
            int completedBlocks = (int)Math.Floor(progressBarWidth * (progressPercentage / 100));
            int remainingBlocks = progressBarWidth - completedBlocks;

            Console.CursorLeft = 0;
            Console.Write("[");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(new string('#', completedBlocks));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', remainingBlocks));
            Console.Write("]");

            Console.CursorLeft = Console.WindowWidth - 6;
            if (progressPercentage >= 100)
            {
                Console.Write($"{progressPercentage:0}%");
            }
            else
            {
                Console.Write($"{progressPercentage:F1}%");
            }

            if (current == total)
            {
                Console.WriteLine();
            }
        }

        public static string FixLength(string input, char fillChar, int size, bool leftSide)
        {
            if (input.Length == size)
            {
                return input;
            }

            if (leftSide)
            {
                while (input.Length != size)
                {
                    input = fillChar + input;
                }
            }
            else
            {
                while (input.Length != size)
                {
                    input = input + fillChar;
                }
            }
            return input;
        }

        public static List<T> ReadAndSplitFile<T>(string filePath, char delimiter) where T : new()
        {
            List<T> list = new List<T>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(delimiter);
                    T item = new T();
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    for (int i = 0; i < fields.Length && i < properties.Length; i++)
                    {
                        properties[i].SetValue(item, fields[i]);
                    }
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
