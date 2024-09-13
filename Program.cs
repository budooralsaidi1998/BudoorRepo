using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicLibrary
{
    internal class Program
    {
        //global structure 
        static int userId = -1;
        static int adminId = -1;
        static List<(int ID, string BName, string BAuthor, int copies, int Borrowedcopies, decimal price, string category, int borrowperiod)> Books = new List<(int ID, string BName, string BAuthor, int copies, int Borrowedcopies, decimal price, string category, int borrowperiod)>();

        static List<(int adminid, string adminname, string email, string password)> adminRegistration = new List<(int adminid, string adminname, string email, string password)>();

        static List<(int Aid, string username, string email, string password)> userReistrtion = new List<(int Aid, string username, string email, string password)>();

        static List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)> borrows = new List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)>();

        static List<(string username, string password)> master = new List<(string username, string password)>();

        //files
        //******************************************************************************************************************************************
        static string filePath = "C:\\Users\\budoo\\Desktop\\files\\BooksFile.txt";
        static string fileAdminRegistration = "C:\\Users\\budoo\\Desktop\\files\\AdminsFile.txt";
        static string fileUserRegistration = "C:\\Users\\budoo\\Desktop\\files\\Usersfile.txt";
        static string fileBorrowBook = "C:\\Users\\budoo\\Desktop\\files\\BorrowingFile.txt";
        static string filemaster = "C:\\Users\\budoo\\Desktop\\files\\master.txt";
        //******************************************************************************************************************************************

        //Test Check Out

        //******************************************************************************************************************************************
        static void Main(string[] args)
        {
            // loadeded file  
            Console.Clear();
            Master();
            LoadBooksFromFile();
            LoadAdminFromFile();
            LoadUserFromFile();
            LoadborrowFromFile();




            bool ExitFlag = false;
            do
            {
                Console.WriteLine("enter the number of the option: ");
                Console.WriteLine("1. login admin ");
                Console.WriteLine("2. login user ");
                Console.WriteLine("3. Registaration ");
                Console.WriteLine("4. logout ");
                int num = int.Parse(Console.ReadLine());

                switch (num)
                {

                    case 1:
                        bool IsLogin = false;

                        while (!IsLogin)
                        {
                            Console.Clear();
                            bool flag = false;
                            Console.WriteLine(" ");
                            Console.WriteLine("***************** Login admin  ********************");
                            Console.WriteLine(" ");

                            Console.WriteLine(" enter your email : ");
                            string emailadmin = Console.ReadLine();

                            Console.WriteLine(" enter the password : ");
                            string passwordadmin = Console.ReadLine();


                            for (int i = 0; i < adminRegistration.Count; i++)
                            {
                                if (adminRegistration[i].email == emailadmin && adminRegistration[i].password == passwordadmin)
                                {
                                    flag = true;

                                    adminId = adminRegistration[i].adminid;

                                    AdminMenu();
                                    IsLogin = true;
                                    break;
                                }


                            }

                            if (flag != true)
                            {
                                string ask;
                                Console.WriteLine("*********** Invalid login ***********");

                                Console.WriteLine("Do you want login again ? yes or no  ");
                                ask = Console.ReadLine();

                                if (ask[0] != 'y')
                                {
                                    Console.Clear ();
                                    break;
                                }

                               
                                //Console.ReadKey();


                            }
                        }
                        break;

                    case 2:
                        bool Isloginuser=false;
                        while (!Isloginuser)
                        {
                            Console.Clear();
                            bool flaguser = false;
                            Console.WriteLine(" ");
                            Console.WriteLine("***************** Login user  ********************");
                            Console.WriteLine(" ");

                            Console.WriteLine(" enter your email : ");
                            string email = Console.ReadLine();

                            Console.WriteLine(" enter the password : ");
                            string password = Console.ReadLine();

                            for (int i = 0; i < userReistrtion.Count; i++)
                            {
                                if (userReistrtion[i].email == email && userReistrtion[i].password == password)
                                {
                                    flaguser = true;

                                    userId = userReistrtion[i].Aid;

                                    UserMenu();
                                    Isloginuser=true;
                                }
                            }
                            if (flaguser != true)
                            {
                                string ask;
                                Console.WriteLine("*********** Invalid login ***********");

                                Console.WriteLine("Do you want login again ? yes or no  ");
                                ask = Console.ReadLine();

                                if (ask[0] != 'y')
                                {
                                    Console.Clear();
                                    break;
                                }

                            }
                        }
                        break;


                    case 3:
                         Console.Clear();
                        RegistrationMenu();
                        break;
                    case 4:

                        ExitFlag = true;
                        break;
                    default:

                        break;

                }
            }

            while (ExitFlag != true);
        }

        //*******************************************************************************************************************************************

        //setup master************************************************************************************************************* 
        //to write master one time not open again 
        static void Master()
        {

            if (!File.Exists(filemaster))
            {
                File.Create(filemaster).Close();
                AddMaster();

            }
        }

        static void AddMaster()
        {
            Console.WriteLine("************ Master ************");

            Console.WriteLine("enter username : ");
            string username = Console.ReadLine();

            Console.WriteLine("enter password");
            string password = Console.ReadLine();

            Console.WriteLine("added master admin ");

            master.Add((username, password));
            try
            {
                using (StreamWriter writer = new StreamWriter(filemaster))
                {
                    foreach (var m in master)
                    {
                        writer.WriteLine($"{m.username}|{m.password}");
                    }
                }
                Console.WriteLine("master saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }

            try
            {
                if (File.Exists(filemaster))
                {
                    using (StreamReader reader = new StreamReader(filemaster))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                master.Add((parts[0], parts[1]));
                            }
                        }
                    }
                    Console.WriteLine("master loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        //***********************************************************************************************************************







        //Regastration for admin and user 
        //*******************************************************************************************************************************************

        static void RegistrationMenu()
        {
            bool ExitFlag = false;

            do
            {

                Console.WriteLine("\n 1- Admin registration ");
                Console.WriteLine("\n 2- User registrsation ");
                Console.WriteLine("\n 3- Save and Exit");

                int choice = int.Parse(Console.ReadLine());


                switch (choice)
                {

                    case 1:
                        AdminRegistration();
                        SaveAdminRegToFile();
                        break;

                    case 2:
                        UserRegistration();
                        SaveUserRegToFile();
                        break;

                    case 3:
                        ExitFlag = true;

                        break;
                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);

        }

        static void AdminRegistration()
        {
            int id = adminRegistration.Count + 1;

            Console.WriteLine("enter the name  :");
            string name = Console.ReadLine();

            Console.WriteLine("enter the email :");
            string email = Console.ReadLine();

            Console.WriteLine(" enter the passowrd : ");
            string password = Console.ReadLine();

            adminRegistration.Add((id, name, email, password));

            Console.WriteLine("successfully added ");
        }

        static void UserRegistration()
        {
            int userid = userReistrtion.Count + 1;

            Console.WriteLine("enter the name :");
            string name = Console.ReadLine();

            Console.WriteLine("enter the email :");
            string email = Console.ReadLine();

            Console.WriteLine(" enter the passowrd : ");
            string password = Console.ReadLine();

            userReistrtion.Add((userid, name, email, password));

            Console.WriteLine("successfully added ");
        }








        //admin menu with the service admin
        //*******************************************************************************************************************************************


        static void AdminMenu()
        {
            bool Auth = false;
            bool ExitFlag = false;
                   
                    do
                    {
                        Console.WriteLine("\n Enter the char of operation you need :");
                        Console.WriteLine("\n 1- Add New Book");
                        Console.WriteLine("\n 2- Display All Books");
                        Console.WriteLine("\n 3- Search for Book by Name");
                        Console.WriteLine("\n 4- Edit the book ");
                        Console.WriteLine("\n 5- Remove the book ");
                        Console.WriteLine("\n 6- Reporting the data ");
                        Console.WriteLine("\n 7- Save and Exit");

                        int choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                AddnNewBook();
                                break;

                            case 2:
                                ViewAllBooks();
                                break;

                            case 3:
                                SearchForBook();
                                break;

                            case 4:
                                EditBookMenu();
                                break;

                            case 5:
                                RemoveBook();
                                break;

                            case 6:
                                Reporting();
                                break;

                            case 7:
                                SaveBooksToFile();
                                ExitFlag = true;
                                break;

                            default:
                                Console.WriteLine("Sorry your choice was wrong");
                                break;

                        }

                        Console.WriteLine("press enter key to continue");

                        string cont = Console.ReadLine();

                        Console.Clear();

                    } while (ExitFlag != true);

           
            ExitFlag = false;
        }

        static void AddnNewBook()
        {


            int id = Books.Count + 1;
            Console.WriteLine("Enter Book Name :");

            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Author :");
            string author = Console.ReadLine();


            Console.WriteLine("Enter copies : ");
            int copies = int.Parse(Console.ReadLine());


            Console.WriteLine("Enter the price : ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book category :");
            string category = Console.ReadLine();

            Console.WriteLine("Enter the borrow period  : ");
            int borrowporied = int.Parse(Console.ReadLine());


            Books.Add((id, name, author, copies, 0, price, category, borrowporied));


            Console.WriteLine("Book Added Succefully");

        }

        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" Copies available : ").Append(Books[i].copies);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" borrwed copies : ").Append(Books[i].Borrowedcopies);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" price : ").Append(Books[i].price);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" category : ").Append(Books[i].category);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" borrow period  : ").Append(Books[i].borrowperiod);
                sb.AppendLine().AppendLine();
                sb.AppendLine().AppendLine();

                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }

        static void SearchForBook()
        {
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == name)
                {
                    Console.WriteLine("Book Author is : " + Books[i].BAuthor);
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); }
        }

        static void RemoveBook()
        {
            ViewAllBooks();

            Console.WriteLine("Choose the id of book you want to delete the book ");
            int id = int.Parse(Console.ReadLine());

            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {

                    Books.Remove((Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies, Books[i].price, Books[i].category, Books[i].borrowperiod));

                    Console.WriteLine("SUCCESSFULLY DELETE");
                    flag = true;
                    break;
                }
            }

            if (flag != true)

            {
                Console.WriteLine("book not found");
            }

        }

        static void Reporting()
        {
            List<string> nameBook = new List<string>();
            List<string> author = new List<string>();
            List<int> borrowBook = new List<int>();

            List<int> quantity = new List<int>();

            for (int i = 0; i < Books.Count; i++)
            {
                var (ID, BName, BAuthor, copies, Borrowedcopies, price, category, borrowperiod) = Books[i];
                nameBook.Add(BName);
                author.Add(BAuthor);
                borrowBook.Add(Borrowedcopies);
                quantity.Add(copies);

            }

            //total book is available 
            int totalBook = nameBook.Count();
            Console.WriteLine("the total of book is available = " + totalBook);
            Console.WriteLine(" ");

            //total book is borrowe it 
            int borrowtotal = borrowBook.Sum();
            Console.WriteLine("the total of book is borrow it = " + borrowtotal);

            Console.WriteLine(" ");

            //most borrowed 
            int IndexOfMostBorrowed = borrowBook.IndexOf(borrowBook.Max());
            Console.WriteLine($" the book is most borrow it : {nameBook[IndexOfMostBorrowed]}");

            Console.WriteLine(" ");

            //less borrow 
            int IndexOflessBorrowed = borrowBook.IndexOf(borrowBook.Min());
            if (IndexOflessBorrowed > 0)

                Console.WriteLine($" the book less  borrow is  : {nameBook[IndexOflessBorrowed]}, number borrow is {borrowBook.Min()} ");






            //





        }
        //********************************************************************************************************************************************





        //user menu with service's 
        //*********************************************************************************************************************************************
        static void UserMenu()
        {
            Console.Clear();
            bool ExitFlag = false;
            bool Auth = false;

            Console.WriteLine("user id is :" + userId);

            do
            {

                Console.WriteLine("\n Enter the char of operation you need :");
                //Console.WriteLine("\n A-Search for Book by Name");
                Console.WriteLine("\n 1- Borrow the book ");
                Console.WriteLine("\n 2- return the book ");
                Console.WriteLine("\n 3- logout ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {

                    case 1:


                       // Console.WriteLine("user id is :" + userId);
                        BarrowBooks();
                        break;

                    case 2:
                       // Console.WriteLine("user id is :" + userId);
                        ReturnBook();
                        break;

                    case 3:
                        SaveBooksToFile();
                        SaveborrowToFile();
                        userId = -1;
                        ExitFlag = true;

                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press enter key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);


   


            if (Auth != true)
            {
                Console.WriteLine("Invalid login ");
            }

            ExitFlag = false;

        }
        static void ViewAllBooksUser()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" Copies available : ").Append(Books[i].copies);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" price : ").Append(Books[i].price);
                sb.AppendLine();

                sb.Append("Book ").Append(BookNumber).Append(" category : ").Append(Books[i].category);
                sb.AppendLine();
                sb.AppendLine().AppendLine();
                sb.Clear();

            }
        }
        //static void BarrowBooks()
        //{
        //    Console.Clear();
        //    StringBuilder sb = new StringBuilder();

        //    // Display books that the user has not borrowed before
        //    Console.WriteLine("Books available for borrowing:");

        //    foreach (var book in Books)
        //    {
        //        // Check if the user has borrowed this book before and it has not yet been returned
        //        bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == book.ID && !b.isreturn);

        //        if (!borrowedBefore)
        //        {
        //            // Display book details
        //            sb.Append("Book ID : ").Append(book.ID).AppendLine();
        //            sb.Append("Name : ").Append(book.BName).AppendLine();
        //            sb.Append("Author : ").Append(book.BAuthor).AppendLine();
        //            sb.Append("Copies available : ").Append(book.copies).AppendLine();
        //            sb.Append("Borrowed copies : ").Append(book.Borrowedcopies).AppendLine();
        //            sb.Append("Price : ").Append(book.price).AppendLine();
        //            sb.Append("Category : ").Append(book.category).AppendLine();
        //            sb.Append("Borrow period : ").Append(book.borrowperiod).AppendLine().AppendLine();

        //            Console.WriteLine(sb.ToString());
        //            sb.Clear();
        //        }
        //    }

        //    Console.WriteLine("\n\nEnter the book ID you want to borrow:");
        //    int enterId = int.Parse(Console.ReadLine());

        //    bool bookFound = false;
        //    bool canBorrow = false;

        //    for (int i = 0; i < Books.Count; i++)
        //    {
        //        if (Books[i].ID == enterId)
        //        {
        //            bookFound = true;
        //            bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == enterId && !b.isreturn);

        //            if (borrowedBefore)
        //            {
        //                Console.WriteLine("You have already borrowed this book and it has not yet been returned.");
        //            }
        //            else if (Books[i].copies > Books[i].Borrowedcopies)
        //            {
        //                canBorrow = true;

        //                // Update the book's borrowed copies
        //                Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies + 1, Books[i].price, Books[i].category, Books[i].borrowperiod);

        //                // Add to borrows list
        //                DateTime dateBorrow = DateTime.Now;
        //                DateTime returnDate = dateBorrow.AddDays(Books[i].borrowperiod);

        //                borrows.Add((userId, enterId, dateBorrow, returnDate, "N/A", "N/A", false));

        //                Console.WriteLine("Book borrowed successfully!");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Cannot borrow this book as there are no available copies.");
        //            }
        //            break;
        //        }
        //    }

        //    if (!bookFound)
        //    {
        //        Console.WriteLine("Book not found.");
        //    }
        //}

        static void BarrowBooks()
        {
            Console.Clear();

            // Define column widths for neat alignment
            int idWidth = 10;
            int nameWidth = 30;
            int authorWidth = 20;
            int copiesWidth = 20;
            int borrowedWidth = 20;
            int priceWidth = 10;
            int categoryWidth = 15;
            int periodWidth = 15;

            // Header
            //padright = add colums with width 
            Console.WriteLine($"{"BookID".PadRight(idWidth)}{"Name".PadRight(nameWidth)}{"Author".PadRight(authorWidth)}{"Available Copies".PadRight(copiesWidth)}{"Borrowed Copies".PadRight(borrowedWidth)}{"Price".PadRight(priceWidth)}{"Category".PadRight(categoryWidth)}{"Borrow Period".PadRight(periodWidth)}");
            Console.WriteLine(new string('-', idWidth + nameWidth + authorWidth + copiesWidth + borrowedWidth + priceWidth + categoryWidth + periodWidth));

            foreach (var book in Books)
            {
                // Check if the user has borrowed this book before and it has not yet been returned
                bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == book.ID && !b.isreturn);

                if (!borrowedBefore)
                {
                    // Display book details in tabular format
                    Console.WriteLine($"{book.ID.ToString().PadRight(idWidth)}{book.BName.PadRight(nameWidth)}{book.BAuthor.PadRight(authorWidth)}{book.copies.ToString().PadRight(copiesWidth)}{book.Borrowedcopies.ToString().PadRight(borrowedWidth)}{book.price.ToString("C").PadRight(priceWidth)}{book.category.PadRight(categoryWidth)}{book.borrowperiod.ToString().PadRight(periodWidth)}");
                }
            }
            //suggestion for the most book is borrowed 

            Console.WriteLine("\n\nEnter the book ID you want to borrow:");
            int enterId = int.Parse(Console.ReadLine());

            bool bookFound = false;
            bool canBorrow = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == enterId)
                {
                    bookFound = true;
                    bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == enterId && !b.isreturn);

                    if (borrowedBefore)
                    {
                        Console.WriteLine("You have already borrowed this book and it has not yet been returned.");
                    }
                    else if (Books[i].copies > Books[i].Borrowedcopies)
                    {
                        canBorrow = true;

                        // Update the book's borrowed copies
                        Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies + 1, Books[i].price, Books[i].category, Books[i].borrowperiod);

                        // Add to borrows list
                        DateTime dateBorrow = DateTime.Now;
                        DateTime returnDate = dateBorrow.AddDays(Books[i].borrowperiod);

                        borrows.Add((userId, enterId, dateBorrow, returnDate, "N/A", "N/A", false));

                        Console.WriteLine("Book borrowed successfully!");
                        static void BarrowBooks()
                    {
                        Console.Clear();
    
                        // Define column widths for neat alignment
                        int idWidth = 10;
                        int nameWidth = 25;
                        int authorWidth = 20;
                        int copiesWidth = 15;
                        int borrowedWidth = 20;
                        int priceWidth = 10;
                        int categoryWidth = 15;
                        int periodWidth = 15;
    
                        // Header
                        Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Name".PadRight(nameWidth)}{"Author".PadRight(authorWidth)}{"Available Copies".PadRight(copiesWidth)}{"Borrowed Copies".PadRight(borrowedWidth)}{"Price".PadRight(priceWidth)}{"Category".PadRight(categoryWidth)}{"Borrow Period".PadRight(periodWidth)}");
                        Console.WriteLine(new string('-', idWidth + nameWidth + authorWidth + copiesWidth + borrowedWidth + priceWidth + categoryWidth + periodWidth));

                        foreach (var book in Books)
                        {
                            // Check if the user has borrowed this book before and it has not yet been returned
                            bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == book.ID && !b.isreturn);
        
                            if (!borrowedBefore)
                            {
                                // Display book details in tabular format
                                Console.WriteLine($"{book.ID.ToString().PadRight(idWidth)}{book.BName.PadRight(nameWidth)}{book.BAuthor.PadRight(authorWidth)}{(book.copies - book.Borrowedcopies).ToString().PadRight(copiesWidth)}{book.Borrowedcopies.ToString().PadRight(borrowedWidth)}{book.price.ToString("C").PadRight(priceWidth)}{book.category.PadRight(categoryWidth)}{book.borrowperiod.ToString().PadRight(periodWidth)}");
                            }
                        }

                        Console.WriteLine("\n\nEnter the book ID you want to borrow:");
                        int enterId = int.Parse(Console.ReadLine());

                        bool bookFound = false;
                        bool canBorrow = false;

                        for (int i = 0; i < Books.Count; i++)
                        {
                            if (Books[i].ID == enterId)
                            {
                                bookFound = true;
                                bool borrowedBefore = borrows.Any(b => b.userid == userId && b.bookid == enterId && !b.isreturn);

                                if (borrowedBefore)
                                {
                                    Console.WriteLine("You have already borrowed this book and it has not yet been returned.");
                                }
                                else if (Books[i].copies > Books[i].Borrowedcopies)
                                {
                                                canBorrow = true;
                
                                                // Update the book's borrowed copies
                                                Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies + 1, Books[i].price, Books[i].category, Books[i].borrowperiod);
                
                                                // Add to borrows list
                                                DateTime dateBorrow = DateTime.Now;
                                                DateTime returnDate = dateBorrow.AddDays(Books[i].borrowperiod);
                
                                                borrows.Add((userId, enterId, dateBorrow, returnDate, "N/A", "N/A", false));
                
                                                Console.WriteLine("Book borrowed successfully!");
                                        
                                                //suggstion for the author similriaty 

                                                Console.WriteLine("\nOther books by the same author:");
                                        // to filter elements from the Books collection
                                        //only returns the elements that satisfy this condition.
                                        //his part checks if the current book (b) has the same author as the book that was just borrowed
                                        //b.ID != Books[i].ID: This condition ensures that the current book (b) is not the same book as the one that was just borrowed
                                        //(since you don't want to suggest the same book again)
                                        var otherBooks = Books.Where(b => b.BAuthor == Books[i].BAuthor && b.ID != Books[i].ID);

                                                  if (otherBooks.Any())
                                                    {
                                                        foreach (var book in otherBooks)
                                                          {
                                                            Console.WriteLine($"{book.BName} by {book.BAuthor}");
                                                              }
                                                         }
                                                  else
                                                      {
                                                         Console.WriteLine("No other books by this author.");
                                                       }
                                                     }
                                                else
                                                {
                                                    Console.WriteLine("Cannot borrow this book as there are no available copies.");
                                                }
                                                break;
                                            }
                        }

                        if (!bookFound)
                        {
                            Console.WriteLine("Book not found.");
                        }
                    }


                                        }
                                        else
                                        {
                                            Console.WriteLine("Cannot borrow this book as there are no available copies.");
                                        }
                                        break;
                                    }
                                }

            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }
        }


        static void ReturnBook()
        {

            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == name)
                {
                    if (Books[i].copies >= 0)  //Console.WriteLine("Book Quantity  : " + Books[i].Qunatity);

                    {
                        Console.WriteLine("How many quantity you want to return: ");
                        int quantity = int.Parse(Console.ReadLine());

                        int NewQunatityAfterTakeIt = Books[i].copies + quantity;

                        int borrow = Books[i].Borrowedcopies - 1;

                        Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, NewQunatityAfterTakeIt, borrow, Books[i].price, Books[i].category, Books[i].borrowperiod);

                        Console.WriteLine("successfuly added ");
                        int bookid = Books[i].ID;
                        // int returnss = quantity;

                        // borrows.Add((userId, bookid, returnss));
                        flag = true;
                        break;
                    }
                }
            }

        }

        //recomand for the author similarity 
        //    static void recomand(string author)
        //{
        //    for (int i = 0; i < Books.Count; i++)
        //    {
        //        if (Books[i].BAuthor == author)
        //        {
        //            Console.WriteLine("you can choose this book : ");
        //            Console.WriteLine($"the book : {Books[i].BName}");
        //        }

        //    }


        //}

        //***********************************************************************************************************************************************




        //edits books
        //will added all in editbookmenu .....
        //**********************************************************************************************************************************************
        static void EditBookMenu()
        {

            bool flage = true;

            while (flage)
            {
                Console.WriteLine("1. edit name of book ");
                Console.WriteLine("2. edit author of book ");
                Console.WriteLine("3. edit quantity of book ");
                Console.WriteLine("4. Save and exit ");
                Console.WriteLine(" what you want to edit ?");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:

                        EditName();

                        break;

                    case 2:

                        EditAuthor();

                        break;

                    case 3:

                        EditQuantity();

                        break;

                    case 4:

                        SaveBooksToFile();
                        flage = false;

                        break;


                    default:

                        Console.WriteLine("invalid number you choose it ");

                        break;

                }



            }
        }

        static void EditName()
        {
            ViewAllBooks();

            Console.WriteLine(" ");
            Console.WriteLine("Enter id book you want :");
            int id = int.Parse(Console.ReadLine());

            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {

                    Console.WriteLine(" enter the name you to update : ");
                    string name = Console.ReadLine();

                    Books[i] = (Books[i].ID, name, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies, Books[i].price, Books[i].category, Books[i].borrowperiod);
                    Console.WriteLine(" successfully Update name ");


                    flag = true;
                    break;
                }
            }

            if (flag != true)

            {
                Console.WriteLine("book not found");
            }
        }

        static void EditAuthor()
        {

            ViewAllBooks();

            Console.WriteLine(" ");
            Console.WriteLine("Enter id book you want :");
            int id = int.Parse(Console.ReadLine());

            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {

                    Console.WriteLine(" enter the Author  you want  to update it : ");
                    string author = Console.ReadLine();

                    Books[i] = (Books[i].ID, Books[i].BName, author, Books[i].copies, Books[i].Borrowedcopies, Books[i].price, Books[i].category, Books[i].borrowperiod);
                    Console.WriteLine(" successfully Update Author ");


                    flag = true;
                    break;
                }
            }

            if (flag != true)

            {
                Console.WriteLine("book not found");
            }

        }

        static void EditQuantity()
        {
            ViewAllBooks();

            Console.WriteLine(" ");
            Console.WriteLine("Enter id book you want :");
            int id = int.Parse(Console.ReadLine());

            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {

                    Console.WriteLine(" enter the Quantity  you want  to update it : ");
                    int copies = int.Parse(Console.ReadLine());

                    Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, copies, Books[i].Borrowedcopies, Books[i].price, Books[i].category, Books[i].borrowperiod);
                    Console.WriteLine(" successfully Update quantity ");


                    flag = true;
                    break;
                }
            }

            if (flag != true)

            {
                Console.WriteLine("book not found");
            }

        }

        //***********************************************************************************************************************************************





        //file the read and write
        //***********************************************************************************************************************************************
        static void LoadBooksFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(" | ");
                            if (parts.Length == 8)
                            {
                                Books.Add((int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), decimal.Parse(parts[5]), parts[6], int.Parse(parts[7])));
                            }
                        }
                    }
                    //  Console.WriteLine("Books loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void LoadAdminFromFile()
        {
            try
            {
                if (File.Exists(fileAdminRegistration))
                {
                    using (StreamReader reader = new StreamReader(fileAdminRegistration))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(" | ");
                            if (parts.Length == 4)
                            {
                                adminRegistration.Add((int.Parse(parts[0]), parts[1], parts[2], parts[3]));
                            }
                        }
                    }
                    // Console.WriteLine("admin loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void LoadUserFromFile()
        {
            try
            {
                if (File.Exists(fileUserRegistration))
                {
                    using (StreamReader reader = new StreamReader(fileUserRegistration))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(" | ");

                            if (parts.Length == 4)
                            {
                                userReistrtion.Add((int.Parse(parts[0]), parts[1], parts[2], parts[3]));
                            }
                        }
                    }
                    // Console.WriteLine("user loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void SaveBooksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var book in Books)
                    {
                        writer.WriteLine($"{book.ID} | {book.BName} | {book.BAuthor} | {book.copies} | {book.Borrowedcopies} | {book.price} | {book.category} | {book.borrowperiod}");
                    }
                }
                // Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void SaveAdminRegToFile()
        {
            try
            {
                HashSet<(int id, string username, string email, string password)> uniqe =
                    new HashSet<(int, string, string, string)>(adminRegistration);

                using (StreamWriter writer = new StreamWriter(fileAdminRegistration))
                {
                    foreach (var admin in uniqe)
                    {
                        writer.WriteLine($"{admin.id} | {admin.username} | {admin.email} | {admin.password}");
                    }
                }
                //Console.WriteLine("the data admin saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void SaveUserRegToFile()
        {
            try
            {
                HashSet<(int Aid, string username, string email, string password)> uniqe =
                     new HashSet<(int, string, string, string)>(userReistrtion);

                using (StreamWriter writer = new StreamWriter(fileUserRegistration))
                {
                    foreach (var user in uniqe)
                    {
                        writer.WriteLine($"{user.Aid} | {user.username} | {user.email} | {user.password}");

                    }
                }
                // Console.WriteLine("the data user saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void SaveborrowToFile()
        {
            try
            {
                HashSet<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)> uniqe =
                    new HashSet<(int, int, DateTime, DateTime, string, string, bool)>(borrows);

                using (StreamWriter writer = new StreamWriter(fileBorrowBook))
                {
                    foreach (var borr in uniqe)
                    {
                        writer.WriteLine($"{borr.userid} | {borr.bookid} | {borr.borrowdate} | {borr.returndate} | {borr.acaualreturndate} | {borr.rating} | {borr.isreturn}");
                    }
                }
                //Console.WriteLine("the data admin saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void LoadborrowFromFile()
        {
            try
            {
                if (File.Exists(fileBorrowBook))
                {
                    using (StreamReader reader = new StreamReader(fileBorrowBook))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(" | ");
                            if (parts.Length == 7)
                            {
                                borrows.Add((int.Parse(parts[0]), int.Parse(parts[1]), DateTime.Parse(parts[2]), DateTime.Parse(parts[3]), parts[4], parts[5], bool.Parse(parts[6])));
                            }
                        }
                    }
                    //  Console.WriteLine("Books loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
      
   
    }
            // **********************************************************************************************************************************************








        }
    

