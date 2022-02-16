using System;
using System.IO;

namespace MPLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = 0;
            int countForWord = 0;
            string word = "";
            string input = File.ReadAllText("F:\\RiderProjects\\Input1.txt");
            int inputLength = 0;
            try
            {
                inputWordsLengthCount:
                if (input[inputLength] != null)
                {
                    inputLength++;
                    goto inputWordsLengthCount;
                }
            }
            catch 
            {
                
            }

            string[] inputWords = new string[inputLength/2];
            int[] wordCount = new int[inputLength/2];
            string[] wordValues = new string[inputLength/2];
            
            //print vars 
            int counter = 0;
            int maxOcc = 0;
            int current = -1;

            getWord :
            char ch = input[inputCount];
                inputCount++;
                if (ch != ' ' && ch != '\n' && ch != '\r' && ch != ',' && ch != '.' && ch != '!' && ch != '?' && inputCount!=inputLength)
                {
                    if (ch <= 91 && ch > 64) ch = (char) (ch + 32);
                    word += ch;
                    goto getWord;
                }


                if (word == " " || word == "\n")
                {
                    inputCount++;
                    word = "";
                    goto getWord;
                }
                inputWords[countForWord] = word;
                countForWord++;
                word = "";
                if (inputCount != inputLength)
                {
                    goto getWord;
                }
                countForWord = -1;



                selectWords:
                countForWord++;
                if (countForWord == inputLength/2)
                {
                    countForWord = -1;
                    goto printSummary;
                    
                }
                int iterator = 0;
                int occurrences = 0;


                checkIfPresent :
                if (iterator == inputLength/2)
                {
                    wordValues[countForWord] = inputWords[countForWord];
                    iterator = -1;
                    goto countWordOccurrences;
                } if (inputWords[countForWord] == wordValues[iterator] || inputWords[countForWord]==null|| inputWords[countForWord].Length<=3)
                {
                    goto selectWords;
                }

                iterator++;
                goto checkIfPresent;


                countWordOccurrences:
                iterator++;
                if (iterator==inputLength/2)
                {
                    wordCount[countForWord] = occurrences;
                    goto selectWords;
                }
                if (wordValues[countForWord] == inputWords[iterator])
                {
                    occurrences++;
                }

                goto countWordOccurrences;



                printSummary:
                counter++;
                printer:
                countForWord++;
                if (counter > 25) goto finish;
                if(countForWord==inputLength/2)
                {
                    if (maxOcc == 0) goto finish;
                    Console.WriteLine(wordValues[current] + " - " + wordCount[current]);
                    wordValues[current] = null;
                    wordCount[current] = 0;
                    maxOcc = 0;
                    countForWord = -1;
                    goto printSummary;
                }
                if (wordCount[countForWord] > maxOcc)
                {
                    maxOcc = wordCount[countForWord];
                    current = countForWord;
                }
                goto printer;

                finish: ;

        } 
    }
}