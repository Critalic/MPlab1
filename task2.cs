using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int pageLength = 45;
            int page = -1;
            int countForLine = 0;
            int countForWord = -1;
            int outputCount = -1;
            int inputLinesLength = 0;
            
            
            int charCount;
            int totalSize=0;
            int initCount = 0;
            int compCount;

            string word = "";
            bool isFinalLine = false;

            string[] inputLines = File.ReadAllLines("F:\\RiderProjects\\Input1.txt");
            

            try
            {
                countInputLines:
                if (inputLines[inputLinesLength] != null)
                {
                    inputLinesLength++;
                    goto countInputLines;
                }
            } catch
            {
                
            }
            int wordsInPage = pageLength*50;
            string[] pageWords = new string[wordsInPage];
            string[] totalWords = new string[wordsInPage*pageLength];
            int[] appearances = new int[wordsInPage*pageLength];
            var pages = new int[wordsInPage*pageLength][];


            initializePages:
            if (initCount < wordsInPage*pageLength)
            {
                pages[initCount] = new int[(inputLinesLength / pageLength) + 1];
                initCount++;
                goto initializePages;
            }

            getLines:
            charCount = -1;
            countForLine++;
            if ((countForLine) % (pageLength+1) == 0 || (countForLine) == inputLinesLength)
            {
                isFinalLine = true;
                goto getWord;
            }
            if(inputLines[countForLine-1] == "") goto getLines;
            word = "";
            
                getWord:
                charCount++;
                if (charCount != inputLines[countForLine - 1].Length)
                {
                   char ch = inputLines[countForLine-1][charCount];
                                   
                                       if (((ch>64 && ch<91) || (ch>96 && ch<123)) )
                                       {
                                           if (ch <= 91 && ch > 64) ch = (char) (ch + 32);
                                           word += ch;
                                           goto getWord;
                                       } 
                }


                if (word == " " || word =="" || word == "\n" || word.Length<=3)
                {
                    word = "";
                    goto evaluate;
                }
                countForWord++;
                pageWords[countForWord] = word;
                
                word = "";

                evaluate:
                if (charCount!= inputLines[countForLine-1].Length)
                {
                    goto getWord;
                }

                if(!isFinalLine)
                {
                    goto getLines;

                }

                int numberOfWordsToInsert = countForWord;
                countForWord = -1;


                page++;

                checkWord:
                int counter = -1;
                bool isPresent = false;
                countForWord++;
                if (countForWord > numberOfWordsToInsert)
                {
                    if (countForLine < inputLinesLength)
                    {
                       pageWords = new string[wordsInPage];
                       countForWord = -1;
                       isFinalLine = false;
                       goto getLines;  
                    }

                    goto estimateTotal;
                }

                verifyInsert:
                    counter++;
                    
                    if (totalWords[counter] == pageWords[countForWord] && pageWords[countForWord]!=null)
                    {
                        isPresent = true;
                        if (appearances[counter] <= 99)
                        {
                            appearances[counter]++;
                            pages[counter][page] = page+1;
                        }
                    }

                    if (totalWords[counter] == null && pageWords[countForWord]!=null)
                    {
                        if (!isPresent)
                        {
                            totalWords[counter] = pageWords[countForWord];
                            pages[counter][page] = page+1;
                            appearances[counter] = 1;
                        }
                        
                        goto checkWord;
                    }
                    goto verifyInsert;
            
                    
                    
                    estimateTotal:
                        if (totalWords[totalSize] != null)
                        {
                            totalSize++;
                            goto estimateTotal;
                        }
                        totalSize--;


                    sort:
                    int outerLoopCount = 0;
                    int innerLoopCount = 0;
                    outerLoop: 
                        if (outerLoopCount < totalSize)
                        {
                            outerLoopCount++;
                            innerLoopCount = 0;
                            goto innerLoop;
                        }
                        outerLoopCount = -1;
                        goto print;
                        
                        
                    innerLoop:
                    if (totalSize > innerLoopCount)
                    {
                        innerLoopCount++;
                        compCount = 0;
                        goto compareStrings;
                    }
                    goto outerLoop;
                        
                        
                    compareStrings:
                    if (compCount < 4)
                    {
                        if (totalWords[innerLoopCount - 1][compCount] == totalWords[innerLoopCount][compCount])
                        {
                            compCount++;
                            goto compareStrings;  
                        }
                        if (totalWords[innerLoopCount-1][compCount] > totalWords[innerLoopCount][compCount])
                        {
                            string tempS = totalWords[innerLoopCount - 1];
                            totalWords[innerLoopCount - 1] = totalWords[innerLoopCount];
                            totalWords[innerLoopCount] = tempS;
                            
                            int[] tempArr = pages[innerLoopCount - 1];
                            pages[innerLoopCount - 1] = pages[innerLoopCount];
                            pages[innerLoopCount] = tempArr;
                            
                            int tempInt = appearances[innerLoopCount - 1];
                            appearances[innerLoopCount - 1] = appearances[innerLoopCount];
                            appearances[innerLoopCount] = tempInt;
                        }
                    }
                    goto innerLoop;   
                    
                    outputPages:
                    outputCount++;
                    if (outputCount < ((inputLinesLength / pageLength) + 1))
                    {
                        if (pages[outerLoopCount][outputCount] == 0) goto outputPages;
                        if (!isPresent)
                        {
                            isPresent = true;
                            Console.Write(pages[outerLoopCount][outputCount]);
                        } else Console.Write(", " + pages[outerLoopCount][outputCount]);
                        
                        goto outputPages;
                    }
                    Console.Write('\n');
                    goto outputWord;
                    
                    print:
                    outputWord:
                    outerLoopCount++;
                    if (outerLoopCount < totalSize)
                    {
                        if (appearances[outerLoopCount] >= 100)
                        {
                            goto outputWord;
                        }
                        isPresent = false;
                        Console.Write(totalWords[outerLoopCount] + " - ");
                        outputCount = -1;
                        goto outputPages;
                    }
                    
        }

    }
}