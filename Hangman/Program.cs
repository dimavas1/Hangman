using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            List<string> wordList = new() { "abruptly", "syndrome", "vaporize", "transcript", "numbskull" , "zigzagging", "yachtsman", "espionage", "jawbreaker", "microwave" };
            List<char> newList;
            List<char> previousList;
            int numberOfTry = 6;
            int countWords = wordList.Count;
            int wordIndex;
            string selectedWord;
            ConsoleKeyInfo key;
            bool isEqual;
            
            Random rnd = new();


            char continueToPlay;
            do
            {
                List<char> selectedLetters = new();

                wordIndex = rnd.Next(0, countWords);
                selectedWord = wordList[wordIndex];
                newList = GenerateEmptyList(selectedWord.Length);
                previousList = GenerateEmptyList(selectedWord.Length);

                PrintList(newList, $"You have {numberOfTry} tries to guess the word");

                for (i = 0; i < numberOfTry; i++)
                {
                    isEqual = true;
                    Console.WriteLine("\nSelect letter");
                    key = Console.ReadKey();

                    if (!selectedLetters.Contains(key.KeyChar))
                        selectedLetters.Add(key.KeyChar);
                    else
                    {
                        do
                        {
                            Console.WriteLine("\nThis letter already selected try new one");
                            key = Console.ReadKey();
                        } while (selectedLetters.Contains(key.KeyChar));

                        selectedLetters.Add(key.KeyChar);
                    }

                    newList = UpdateList(key, selectedWord, newList);

                    foreach (var item in newList)
                    {
                        if (!previousList.Contains(item))
                        {
                            previousList = UpdateList(key, selectedWord, previousList);
                            isEqual = false;
                            i--;
                            break;
                        }

                    }

                    if (!isEqual)
                    {
                        PrintList(previousList, "\nThis letter exist");

                        if (!previousList.Contains('_'))
                        {
                            Console.WriteLine("You Win!");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThis letter not exist");
                    }

                }

                if (i == numberOfTry)
                {
                    Console.WriteLine("You Lose!");
                }

                Console.WriteLine("Do you Want to play another round? y/n");
                continueToPlay = Console.ReadKey().KeyChar;
            } while (continueToPlay == 'y');

        }

        static List<char> GenerateEmptyList(int numberOfLetters)
        {
            List<char> list = new();

            for (int i = 0; i < numberOfLetters; i++)
            {
                list.Add('_');
            }

            return list;
        }
        static List<char> UpdateList(ConsoleKeyInfo letter, string selectedWord, List<char> currentList)
        {

            var list = currentList;
            
            for (int i = 0; i < selectedWord.Length; i++)
            {
                if (selectedWord[i].ToString().ToLower() ==letter.KeyChar.ToString().ToLower())
                {
                    list[i] = letter.KeyChar;
                }
            }

            return list;
        }
        static void PrintList(List<char> list, string previousComments)
        {
            Console.Clear();
            Console.WriteLine(previousComments +"\n");
            Console.WriteLine(string.Join(" ", list));
        }
    }
}
