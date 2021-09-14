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
            bool letterExist;
            int trialCounter;
            
            Random rnd = new();


            char continueToPlay;
            do
            {
                List<char> selectedLetters = new();
                trialCounter = 0;
                wordIndex = rnd.Next(0, countWords);
                selectedWord = wordList[wordIndex];
                newList = GenerateEmptyList(selectedWord.Length);
                previousList = GenerateEmptyList(selectedWord.Length);

                PrintList(newList, $"You have {numberOfTry} tries to guess the word");

                for (i = 0; i < numberOfTry; i++)
                {
                   
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

                    trialCounter++;
                    letterExist = true;
                    newList = UpdateList(key, selectedWord, newList);

                    if (!ListsAreEqual(newList,previousList))
                    {
                        previousList = UpdateList(key, selectedWord, previousList);
                        letterExist = false;
                        i--;
                        trialCounter--;
                    }

                    if (!letterExist)
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

                if (trialCounter == numberOfTry)
                {
                    Console.WriteLine("You Lose!");
                }

                Console.WriteLine("Do you want to play another round? y/n");
                continueToPlay = Console.ReadKey().KeyChar;
            } while (continueToPlay == 'y');

        }

        /// <summary>
        /// Generate new list where each item is '_'
        /// </summary>
        /// <param name="numberOfLetters"> represen number of items to create </param>
        /// <returns>List</returns>
        static List<char> GenerateEmptyList(int numberOfLetters)
        {
            List<char> list = new();

            for (int i = 0; i < numberOfLetters; i++)
            {
                list.Add('_');
            }

            return list;
        }
        
        /// <summary>
        /// Scanning the list for selected char.
        /// If this char not exist updating the list with new char item.
        /// </summary>
        /// <param name="letter">Key info sent from user</param>
        /// <param name="selectedWord">selected word to guess on</param>
        /// <param name="currentList">list with guessed items</param>
        /// <returns>updated list if the letter not exist 
        /// other way return the list without changes</returns>
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
        
        /// <summary>
        /// Adding atached comments, combain list items into one string  and printing output
        /// </summary>
        /// <param name="list">selected list to combain</param>
        /// <param name="previousComments">additional coments to print before list</param>
        static void PrintList(List<char> list, string previousComments)
        {
            Console.Clear();
            Console.WriteLine(previousComments +"\n");
            Console.WriteLine(string.Join(" ", list));
        }

        /// <summary>
        /// Compare 2 lists
        /// </summary>
        /// <param name="newList">first list</param>
        /// <param name="previousList">second list</param>
        /// <returns>True if equal False if not</returns>
        static bool ListsAreEqual(List<char> newList, List<char> previousList)
        {
            bool letterExist = true;

            foreach (var letter in newList)
            {
                if (!previousList.Contains(letter))
                {
                    letterExist = false;
                    break;
                }

            }

            return letterExist;
        }
    }
}
