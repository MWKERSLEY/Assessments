using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKAssessmentBooks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pre-build library of books
            List<Book> library = new List<Book>() { };
            Book book1 = new Book("To Kill A Mocking Bird", "Harper Lee");
            Book book2 = new Book("Harry Potter", "JK Rowling", 4674960354825, 12.99m);
            Book book3 = new Book("The Hobbit", "JRR Tolkien", 3765495736548);
            library.Add(book1);
            library.Add(book2);
            library.Add(book3);

            //greet
            Console.WriteLine("Welcome to the book management system.");

            //do-while check char
            char action = new char();
            //enter book management loop
            do
            {
                //request input
                Console.WriteLine("Press 'a' to add a book, 'd' to delete a book, 'v' to view all books, 'f' to search for a book, 't' for the total book value, 's' to sort books, 'x' to exit.");
                //check for valid input
                if (char.TryParse(Console.ReadLine(), out action))
                {
                    switch (action) //take appropriate action
                    {
                        case 'a':
                            //input book details
                            Console.Write("Please input a book title: ");
                            string title = Console.ReadLine();
                            Console.Write("Please input a book author: ");
                            string author = Console.ReadLine();
                            Console.Write("Please input the book's ISBN: ");
                            long ISBN;
                            long.TryParse(Console.ReadLine(), out ISBN);
                            Console.Write("Please input a price: ");
                            decimal price;
                            decimal.TryParse(Console.ReadLine(), out price);
                            //create then add book, to use showInfo method more easily
                            Book newBook = new Book(title, author, ISBN, price);
                            library.Add(newBook);
                            newBook.showInfo();
                            break;
                        case 'd':
                            Console.Write("Please input the book title to be deleted: ");
                            title = Console.ReadLine().ToLower();//get book to be deleted by title
                            bool check = false;
                            for (int i = 0; i < library.Count; i++)
                            {
                                if (title==library[i].title.ToLower())//compare to all books in list
                                {
                                    Console.WriteLine(library[i].title + " has been removed from the system.");
                                    library.RemoveAt(i);
                                    Book.totalBooks--;
                                    check = true;
                                    break;//delete found book, exit loop
                                }
                            }
                            if (!check)//if not found
                            {
                                Console.WriteLine(title + " is not in the system.");
                            }
                            break;
                        case 'f':
                            Console.Write("Please input the book title to be found: ");
                            title = Console.ReadLine().ToLower();//get book to be found
                            check = false;
                            for (int i = 0; i < library.Count; i++)
                            {
                                if (title == library[i].title.ToLower())
                                {
                                    library[i].showInfo();
                                    check = true;
                                    break;//if found, show info and break loop
                                }
                            }
                            if (!check) //if not found in list
                            {
                                Console.WriteLine(title + " is not in the system.");
                            }
                            break;
                        case 's'://sort library bubble
                            for (int k = 0; k < library.Count; k++)//passes
                            {
                                for (int i = 0; i < library.Count; i++)//compare each book
                                {
                                    for (int j = i + 1; j < library.Count; j++)//to every other book once
                                    {
                                        if (string.Compare(library[i].title, library[j].title) == 1) //if out of order, swap
                                        {
                                            Book tempBook = new Book(library[i].title, library[i].author, library[i].ISBN, library[i].price);
                                            Book.totalBooks--;
                                            library[i] = library[j];
                                            library[j] = tempBook;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case 'v'://display all books by looping over library
                            Console.WriteLine("Number of books: "+Book.totalBooks);
                            foreach(Book book in library)
                            {
                                book.showInfo();
                            }
                            break;
                        case 't'://display total value of 'library'
                            Book.totalBookValue(library);
                            break;
                        case 'x':
                            Console.WriteLine("Closing book manager."); //parting message
                            break;
                        default://unrecognised character
                            Console.WriteLine("Unrecognised command.");
                            break;
                    }
                } else //if input invalid
                {
                    Console.WriteLine("Unrecognised command.");
                }
                Console.WriteLine();
            } while (action!='x');
            
        }
    }
    class Book //book class, for storing all individual book data
    {
        //attributes
        public string title;
        public string author;
        public long ISBN;
        public decimal price;
        public bool ISBNCheck;

        public static int totalBooks;

        public Book(string title  = "Unknown", string author = "Unknown", long ISBN = 0, decimal price = 4.99m)
        {
            this.title = title;
            this.author = author;
            this.ISBN = ISBN;
            this.price = price;
            if (title == "")
            {
                this.title = "Unknown";
            }
            if (author == "")
            {
                this.author = "Unknown";
            }
            if (price == 0)
            {
                this.price = 4.99m;
            }
            if (this.ISBN!=0)
            {
                this.ISBNCheck = true;
            }
            totalBooks++;
        }

        public void showInfo()
        {
            if (ISBNCheck)//don't display 0 ISBN numbers
            {
                Console.WriteLine("{0} by {1}, ISBN - {2}, Price £{3}", this.title, this.author, this.ISBN, this.price);
            } else
            {
                Console.WriteLine("{0} by {1}, ISBN - {2}, Price £{3}", this.title, this.author, "Unknown", this.price);
            }
        }

        public static void totalBookValue(List<Book> allBooks)//sum up book prices in a list of books
        {
            decimal sum = 0m;
            foreach(Book book in allBooks)//loop over all books in the list
            {
                sum += book.price;//add price
            }
            Console.WriteLine("Total price of all books: £{0}.", sum);//display total
        }
    }
}

/* Plan: Create Book object
 * give it properties of title, author, ISBN and price
 * give it a constructor to create a book
 * give it a method to change the price
 * create a 'library'- List of Book
 * create a do while loop to give the option to manage the list of books in various ways
 * give the option to sort and search for books
 * 
 * Testing: Checked basic loop was functioning
 * then added first attempt for each option.
 * Checked all options for correct functionality
 * Checked all options to respond to bad input */
