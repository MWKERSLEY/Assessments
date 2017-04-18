using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKAssessmentAverages
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialise variables for the do-while loops
            char loopCheck = new char(); //option selector
            string inputString = ""; //for reading in numbers
            int inputNumber = 0;//to put numbers into list
            List<int> numberList = new List<int>() { };//the list of numbers

            do //loop to add numbers to an array
            {
                Console.WriteLine("Would you like to add a number to the list? (y/n):");
                try //catch error if there is no input
                {
                    //grab input
                    loopCheck = Convert.ToChar(Console.ReadLine().ToLower()[0]);
                }
                catch (IndexOutOfRangeException) //can't index 0 of empty string
                {
                    loopCheck = 'e'; //re-enter the loop if no input
                }
                if (loopCheck == 'y') //if user wants to input a number
                {
                    Console.WriteLine("Please enter a number, followed by return:");
                    inputString = Console.ReadLine();
                    if (int.TryParse(inputString, out inputNumber)) //if it is a number
                    {
                        numberList.Add(inputNumber); //add to list
                    }
                }
                else if (loopCheck!='n')
                {
                    Console.WriteLine("Please enter either 'y' or 'n'.");
                }
            } while (loopCheck != 'n'); //continue adding numbers to list

            //once all numbers entered
            if (numberList.Count>0) //if there have been numbers entered
            {
                do
                {
                    Console.WriteLine("\nType ‘A’ to get the average of the list, 'D' to display the list, ‘S’ to sort it, 'T' to totalise numers, 'M' for the median, 'O' for the mode, or ‘X’ to exit:");
                    try //catch error if there is no input
                    {
                        //grab input
                        loopCheck = Convert.ToChar(Console.ReadLine().ToLower()[0]);
                    }
                    catch (IndexOutOfRangeException) //can't index 0 of empty string
                    {
                        loopCheck = 'e'; //re-enter the loop if no input
                        Console.WriteLine("Not a valid input");
                    }
                    switch (loopCheck)//different output options
                    {
                        case 'a'://show average
                            Console.WriteLine("The average is: {0}", numberList.Average());
                            break;
                        case 's':
                            numberList.Sort();//sort list
                            Console.WriteLine("List sorted as follows:");
                            foreach (int i in numberList)
                            {
                                Console.Write(i + ", ");
                            }
                            Console.WriteLine();
                            break;
                        case 'd'://display list
                            Console.WriteLine("Here is the list:");
                            foreach (int i in numberList)
                            {
                                Console.Write(i + ", ");
                            }
                            Console.WriteLine();
                            break;
                        case 't'://show total
                            Console.WriteLine("The total of all these numbers is: {0}", numberList.Sum());
                            break;
                        case 'm'://median
                            List<int> tempList = numberList.ToList();
                            tempList.Sort();
                            if ((numberList.Count%2)==0)
                            {
                                Console.WriteLine("The median of these numbers is: {0}", (double)(tempList[tempList.Count / 2] + tempList[(tempList.Count / 2) - 1]) / 2);
                            } else
                            {
                                Console.WriteLine("The median of these numbers is: {0}", tempList[(int)tempList.Count / 2]);
                            }
                            break;
                        case 'o'://mode
                            int[] countArray = new int[numberList.Count];//an array to match the elements of the main List to the number of their occurances
                            int enumerator = 0; //increment index of array, to use foreach
                            foreach (int i in numberList) //compare every element to every element
                            {
                                int count = 0;
                                foreach (int j in numberList)
                                {
                                    if (i == j) //if the number appears, increment
                                    {
                                        count++;
                                    }
                                }
                                countArray[enumerator] = count;
                                enumerator++;
                            }
                            Console.WriteLine("The mode(s): ");
                            List<int> duplicates = new List<int>();//to make sure modes aren't listed more than once
                            //output mode numbers only once, checking each number in numberList against its count
                            for(int k=0;k< countArray.Length; k++)
                            {
                                if ((countArray.Max() == countArray[k]) && (duplicates.IndexOf(numberList[k]))==-1)
                                {
                                    Console.Write(numberList[k] + ", ");
                                    duplicates.Add(numberList[k]);
                                }
                            }
                            Console.WriteLine();
                            break;
                        case 'x'://exit
                            Console.WriteLine("The program will now close.");
                            break;
                        default://not valid character
                            Console.WriteLine("Please enter a valid option.");
                            break;
                    }
                } while (loopCheck != 'x');
            } else //list empty
            {
                Console.WriteLine("List of numbers is empty. Program terminating.");
            }
        }
    }
}

/* Plan: Create 2 do-while loops which ask for y, n then a, d, s, x
 * read input at the top of the do-while
 * if/switch on the input key
 * ask for a number/display the appropriate list action
 * check all inputs are valid, re-ask if not
 */
/* Test: I tested all requested options and all entered invalid responses at each point
 * All outputs appeared as expected, eventually.
 * Where there were errors they could be quickly found as the logic here is fairly simple
 * Used breakpoints to check that 'countArray' was being populated correctly.*/
