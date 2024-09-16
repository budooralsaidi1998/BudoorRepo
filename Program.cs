﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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

        static List<(int Cid, string NameCat , int NumofBook )> Category = new List<(int Cid, string NameCat, int NumofBook)>();

        //files
        //******************************************************************************************************************************************
        static string filePath = "C:\\Users\\budoo\\Desktop\\files\\BooksFile.txt";
        static string fileAdminRegistration = "C:\\Users\\budoo\\Desktop\\files\\AdminsFile.txt";
        static string fileUserRegistration = "C:\\Users\\budoo\\Desktop\\files\\Usersfile.txt";
        static string fileBorrowBook = "C:\\Users\\budoo\\Desktop\\files\\BorrowingFile.txt";
        static string filemaster = "C:\\Users\\budoo\\Desktop\\files\\master.txt";
        static string fileCategory = "C:\\Users\\budoo\\Desktop\\files\\CategoriesFile.txt";
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
            LoadCategory();


            Console.WriteLine();
            Drowing();
            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t\t             ... Click enter key ...");
            Console.ForegroundColor = originalColor;
            Console.ReadLine();

            bool ExitFlag = false;
            do 
            {
                Console.Clear();

                Console.WriteLine();
               
                drwingeyeclose();
               
                Console.WriteLine();
                ConsoleColor orgnalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("\t\t\t\t\t\t\t\t\t1. login admin ");
                Console.WriteLine();

                Console.WriteLine("\t\t\t\t\t\t\t\t\t-----------------");
                Console.WriteLine("\t\t\t\t\t\t\t\t\t2. login user ");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t\t------------------");
                Console.WriteLine("\t\t\t\t\t\t\t\t\t3. Registaration ");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t\t-------------------");
                Console.WriteLine("\t\t\t\t\t\t\t\t\t 4. logout  ");
                Console.ForegroundColor = orgnalColor;
                Console.WriteLine();
                Console.WriteLine();
                ConsoleColor orginalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\t\t\t\t\t\t\t\tenter the number of the option: ");
                int num = int.Parse(Console.ReadLine());
                Console.ForegroundColor = orginalColor;





                switch (num)
                {

                    case 1:
                       bool IsLogin = false;

                       while (!IsLogin)
                       {
                                Console.Clear();
                                Console.WriteLine(" ");
                                Console.WriteLine("***************** Login admin  ********************");
                                Console.WriteLine(" ");

                                Console.WriteLine("Enter your email: ");
                                string emailAdmin = Console.ReadLine();

                                Console.WriteLine("Enter the password: ");
                                string passwordAdmin = Console.ReadLine();

                                Console.WriteLine("Re-enter the password: ");
                                string reEnterPasswordAdmin = Console.ReadLine();

                                // Check if the passwords match
                                if (passwordAdmin != reEnterPasswordAdmin)
                                {
                                    Console.WriteLine("Passwords do not match. Please try again.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                    break;  // Restart the loop
                                }

                                bool emailFound1 = false;
                                bool passwordCorrect1 = false;
                               // int adminId = -1;

                                // Loop through admin registrations to check email and password
                                for (int i = 0; i < adminRegistration.Count; i++)
                                {
                                    if (adminRegistration[i].email == emailAdmin)
                                    {
                                        emailFound1 = true;  // Email exists
                                        if (adminRegistration[i].password == passwordAdmin)
                                        {
                                        adminId = adminRegistration[i].adminid;
                                        passwordCorrect1 = true;  // Email and password both match
                                           
                                            break;
                                        }
                                    }
                                }
                                 
                                if (!emailFound1)
                                {
                                    Console.WriteLine("Admin email not found. Do you want to try logging in again? (yes or no)");
                                    string ask = Console.ReadLine();

                                    if (ask.ToLower() != "yes" && ask.ToLower() != "y")
                                    {
                                        Console.Clear();
                                        break;  // Exit the login loop
                                    }
                                }
                                else if (!passwordCorrect1)
                                {
                                    Console.WriteLine("Password is incorrect. Please try again.");
                                    Console.WriteLine("Press any key to continue.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                Console.Clear();
                                IsLogin = true;  // Successful login
                                    AdminMenu();  // Redirect to the admin menu



                                }

                }
            
                break;



                    case 2:
                        
                        bool Isloginuser = false;
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


                        Console.WriteLine(" re - enter the password : ");
                        string reenterPasswordadmin = Console.ReadLine();

                        if (password != reenterPasswordadmin)
                        {

                            Console.WriteLine("Passwords do not match. Please try again.");
                            Console.WriteLine("enter enter key");
                            Console.ReadKey();
                            IsLogin = false;
                            break;

                        }
                            bool emailFound = false;
                            bool passwordCorrect = false;
                            for (int i = 0; i < userReistrtion.Count; i++)
                            {
                                if (userReistrtion[i].email == email)
                                {
                                    emailFound = true;  // Email exists
                                    if (userReistrtion[i].password == password)
                                    {
                                        userId = userReistrtion[i].Aid;
                                        passwordCorrect = true;  // Email and password both match
                                      
                                        break;
                                    }
                                }
                            }

                            if (!emailFound)
                            {
                                Console.WriteLine("User email not found. Do you want to try logging in again? (yes or no)");
                                string ask = Console.ReadLine();

                                if (ask.ToLower() != "yes" && ask.ToLower() != "y")
                                {
                                    Console.Clear();
                                    break;  // Exit the login loop
                                }
                            }
                            else if (!passwordCorrect)
                            {
                                Console.WriteLine("Password is incorrect. Please try again.");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                IsLogin = true;  // Successful login
                                UserMenu();

                                break;

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
                    Console.Clear();
                    Console.WriteLine("Incorrect choice. Please enter a number between 1 and 4.");

                    Console.WriteLine("Press enter key to try again...");
                    Console.ReadKey();
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







        //Regastration for admin and user with validate 
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
             List<string> existingNames = new List<string>();
             List<string> existingEmails = new List<string>();

            for (int i = 0; i < adminRegistration.Count; i++)
            {
                var (id, name, email, pass) = adminRegistration[i];
             
                existingNames.Add((name));
                existingEmails.Add((email));

            }
           
            // Regex patterns
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            while (true)
            {
                int id = adminRegistration.Count + 1;

                
                Console.WriteLine("Enter the name:");
                string name = Console.ReadLine();

                // Check for duplicate name
                if (existingNames.Contains(name))
                {
                    Console.WriteLine("Name already exists. Try again.");
                    break;
                }

               
                Console.WriteLine("Enter the email:");
                string email = Console.ReadLine();

                // Validate email
                if (!IsValidEmail(email, emailPattern))
                {
                    Console.WriteLine("Invalid email format. Try again.");
                    break; 
                }

                // Check for duplicate email
                if (existingEmails.Contains(email))
                {
                    Console.WriteLine("Email already exists. Try again.");
                    break; 
                }

               
                Console.WriteLine("Enter the password:");
                string password = Console.ReadLine();

                // Validate password
                if (!IsValidPassword(password, passwordPattern))
                {
                    Console.WriteLine("Invalid password format. Try again.");
                    break;
                }

                // Add registration
                adminRegistration.Add((id, name, email, password));
                

                Console.WriteLine("Successfully added");
                break; 
            }
        }
        static bool IsValidEmail(string email, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        static bool IsValidPassword(string password, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
        static void UserRegistration()
        {
            List<string> existingNames = new List<string>();
            List<string> existingEmails = new List<string>();

            for (int i = 0; i < userReistrtion.Count; i++)
            {
                var (id, name, email, pass) = userReistrtion[i];

                existingNames.Add((name));
                existingEmails.Add((email));

            }

            // Regex patterns
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            while (true)
            {
                int userid = adminRegistration.Count + 1;


                Console.WriteLine("Enter the name:");
                string name = Console.ReadLine();

                // Check for duplicate name
                if (existingNames.Contains(name))
                {
                    Console.WriteLine("Name already exists. Try again.");
                    break;
                }


                Console.WriteLine("Enter the email:");
                string email = Console.ReadLine();

                // Validate email
                if (!IsValidEmail(email, emailPattern))
                {
                    Console.WriteLine("Invalid email format. Try again.");
                    break;
                }

                // Check for duplicate email
                if (existingEmails.Contains(email))
                {
                    Console.WriteLine("Email already exists. Try again.");
                    break;
                }


                Console.WriteLine("Enter the password:");
                string password = Console.ReadLine();

                // Validate password
                if (!IsValidPassword(password, passwordPattern))
                {
                    Console.WriteLine("Invalid password format. Try again.");
                    break;
                }

                // Add registration
                userReistrtion.Add((userid, name, email, password));


                Console.WriteLine("Successfully added");
                break;
            }

        }






        //admin menu with the service admin
        //*******************************************************************************************************************************************


        static void AdminMenu()
        {
           // bool Auth = false;
            bool ExitFlag = false;

            do
            {
                Console.WriteLine();
                drwingeyeopen();
                Console.WriteLine();

                Console.WriteLine(" Iam Admin : " + adminId);
                Console.WriteLine("\n Enter the option of operation you need :");
                Console.WriteLine("\n 1- Add New Book");
                Console.WriteLine("\n 2- Display All Books");
                Console.WriteLine("\n 3- Search for Book by Name");
                Console.WriteLine("\n 4- Edit the book ");
                Console.WriteLine("\n 5- Remove the book ");
                Console.WriteLine("\n 6- Reporting the data ");
                Console.WriteLine("\n 7- Save and Exit");
                Console.WriteLine();
                drwingeyeopen();

                int choice = int.Parse(Console.ReadLine());
                 
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddnNewBook();
                        SaveCategory();
                        break;

                    case 2:
                        Console.Clear();
                        ViewAllBooks();
                        break;

                    case 3:
                        Console.Clear();
                        SearchForBook();
                        break;

                    case 4:
                        Console.Clear();
                        EditBookMenu();
                        break;

                    case 5:
                        Console.Clear();
                        RemoveBook();
                        break;

                    case 6:
                        Console.Clear();
                        Reporting();
                        break;

                    case 7:
                        SaveBooksToFile();
                        SaveCategory();
                        adminId = -1;
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

            List<string> existingNames = new List<string>();

            
          

            for (int i = 0; i < Books.Count; i++)
            {
                var (ID, BName, BAuthor, copiesd, Borrowedcopies, priced, category, borrowperiod) = Books[i];

                existingNames.Add((BName));
            }


            int id = Books.Count + 1;



            string name;
            while (true) 
            {
                Console.WriteLine("Enter Book Name:");
                name = Console.ReadLine();

                if (existingNames.Contains(name))
                {
                    Console.WriteLine("Name already exists. Try again.");
                }
                else
                {
                    // If the name is valid, add it to the existing names list and break the loop
                    existingNames.Add(name);
                    break;
                }
            }

            Console.WriteLine("Enter Book Author :");
            string author = Console.ReadLine();


            Console.WriteLine("Enter copies : ");
            int copies = int.Parse(Console.ReadLine());


            Console.WriteLine("Enter the price : ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book category:");
         
            string Categoryselect = CategorBookMenu();

           //to get the category name and check it after that count the book
            for (int i = 0; i < Category.Count; i++)
            {
                if (Category[i].NameCat.Trim() == Categoryselect.Trim()) // Trim() to handle extra spaces
                {
                    
                    int updatedNumOfBooks = Category[i].NumofBook + 1;

                    
                    Category[i] = (Category[i].Cid, Category[i].NameCat, updatedNumOfBooks);

                    
                    break;
                }
            }
            Console.WriteLine("Enter the borrow period  : ");
            int borrowporied = int.Parse(Console.ReadLine());


            Books.Add((id, name, author, copies, 0, price, Categoryselect, borrowporied));

            
            

          
            Console.WriteLine("Book Added Succefully");


        }

        static void ViewAllBooks()
        {
            Console.WriteLine("\n\n\n\n\t\t\t\t\t\t******   DETAILS OF BOOKS    ******");
            Console.WriteLine("   ");
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            // Define padding width for each column
            int idPadding = 10;
            int namePadding = 30;
            int authorPadding = 20;
            int copiesPadding = 10;
            int borrowedPadding = 10;
            int pricePadding = 10;
            int categoryPadding = 15;
            int periodPadding = 15;

            // Add headers
            sb.Append("\n\n\t|");
            sb.Append(CenterText("ID", idPadding)).Append("|");
            sb.Append(CenterText("Name", namePadding)).Append("|");
            sb.Append(CenterText("Author", authorPadding)).Append("|");
            sb.Append(CenterText("Copies", copiesPadding)).Append("|");
            sb.Append(CenterText("Borrowed", borrowedPadding)).Append("|");
            sb.Append(CenterText("Price", pricePadding)).Append("|");
            sb.Append(CenterText("Category", categoryPadding)).Append("|");
            sb.Append(CenterText("Borrow Period", periodPadding)).Append("|")
              .AppendLine();

            // Add a separator line
            sb.Append("\t").Append(new string('-', idPadding + namePadding + authorPadding + copiesPadding + borrowedPadding + pricePadding + categoryPadding + periodPadding + 9 ))
              .AppendLine();

            // Loop through each book and display its details
            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("\t|");
                sb.Append(CenterText(Books[i].ID.ToString(), idPadding)).Append("|");
                sb.Append(CenterText(Books[i].BName, namePadding)).Append("|");
                sb.Append(CenterText(Books[i].BAuthor, authorPadding)).Append("|");
                sb.Append(CenterText(Books[i].copies.ToString(), copiesPadding)).Append("|");
                sb.Append(CenterText(Books[i].Borrowedcopies.ToString(), borrowedPadding)).Append("|");
                sb.Append(CenterText(Books[i].price.ToString(), pricePadding)).Append("|");
                sb.Append(CenterText(Books[i].category, categoryPadding)).Append("|");
                sb.Append(CenterText(Books[i].borrowperiod.ToString(), periodPadding)).Append("|")
                  .AppendLine();
            }

            // Add another separator line at the bottom
            sb.Append("\t").Append(new string('-', idPadding + namePadding + authorPadding + copiesPadding + borrowedPadding + pricePadding + categoryPadding + periodPadding + 9))
              .AppendLine();

            // Display the final result
            Console.WriteLine(sb.ToString());
        }

        // Method to center text within a given width
        static string CenterText(string text, int width)
        {
            if (text.Length >= width)
            {
                return text;
            }

            int padding = (width - text.Length) / 2;
            int paddingRight = width - text.Length - padding;
            return new string(' ', padding) + text + new string(' ', paddingRight);
        }


        static void SearchForBook()
        {
            Console.WriteLine("Enter the book name or part of the name you want to search for:");
            string keyword = Console.ReadLine().ToLower(); // Convert input to lowercase for case-insensitive search
            bool found = false;

            Console.WriteLine("\n\n\n\t\t\t\t\tSearch Results");
            Console.WriteLine("   ");

            // Iterate through the list of books to find any book with a name that contains the keyword
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName.ToLower().Contains(keyword)) 
                {
                    // If a match is found, display all book details
                    Console.WriteLine("\nBook ID: " + Books[i].ID);
                    Console.WriteLine("Book Name: " + Books[i].BName);
                    Console.WriteLine("Book Author: " + Books[i].BAuthor);
                    Console.WriteLine("Copies Available: " + Books[i].copies);
                    Console.WriteLine("Borrowed Copies: " + Books[i].Borrowedcopies);
                    Console.WriteLine("Price: " + Books[i].price);
                    Console.WriteLine("Category: " + Books[i].category);
                    Console.WriteLine("Borrow Period: " + Books[i].borrowperiod);
                    Console.WriteLine("-------------------------------------------");

                    found = true;
                }
            }

            // If no book was found
            if (!found)
            {
                Console.WriteLine("No books found matching the keyword '" + keyword + "'.");
            }
        }

        static void RemoveBook()
        {
            // Display all books before attempting to remove one
            ViewAllBooks();

            Console.WriteLine(" ");
            Console.WriteLine("Choose the ID of the book you want to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
                return; // Exit if the input is not a valid number
            }

            bool bookFound = false;

            // Loop through the books to find the one with the given ID
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {
                    bookFound = true; // Mark the book as found

                    // Check if the book has borrowed copies
                    if (Books[i].Borrowedcopies != 0)
                    {
                        Console.WriteLine("Can't remove the book because someone has borrowed it.");
                        return; // Exit if the book can't be deleted
                    }
                    else
                    {
                      
                        Books.RemoveAt(i);

                        Console.WriteLine("Book successfully deleted.");

                        
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadKey();
                        Console.Clear();

                        return; 
                    }
                }
            }

            // If the book with the given ID was not found, display a message
            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }
        }


        static void Reporting()
        {
            List<string> nameBook = new List<string>();
            List<string> author = new List<string>();
            List<int> borrowBook = new List<int>();
            List<int> quantity = new List<int>();
            List<(string category, int count)> categoryCounts = new List<(string category, int count)>(); // List of tuples for categories

            int totalCopies = 0;   
            int totalBorrowed = 0; 
            int totalReturned = 0; 

            // Loop through all books
            for (int i = 0; i < Books.Count; i++)
            {
                var (ID, BName, BAuthor, copies, Borrowedcopies, price, category, borrowperiod) = Books[i];

                nameBook.Add(BName);
                author.Add(BAuthor);
                borrowBook.Add(Borrowedcopies);
                quantity.Add(copies);

                // Update total number of copies and borrowed books
                totalCopies += copies;
                totalBorrowed += Borrowedcopies;
                totalReturned += (copies - Borrowedcopies); // to get return book 

                // Update category count using a list of tuples
                bool categoryExists = false;
                for (int j = 0; j < categoryCounts.Count; j++)
                {
                    if (categoryCounts[j].category == category)
                    {
                        categoryCounts[j] = (categoryCounts[j].category, categoryCounts[j].count + 1); // Increment the book count for the existing category
                        categoryExists = true;
                        break;
                    }
                }
                if (!categoryExists)
                {
                    categoryCounts.Add((category, 1)); // Add a new category with an initial count of 1
                }
            }

            int consoleWidth = Console.WindowWidth;

            // Helper method to center text in the console
            void CenterText(string text)
            {
                int padding = (consoleWidth - text.Length) / 2;
                Console.WriteLine(new string(' ', padding) + text);
            }
            Console.WriteLine();
            CenterText("-^-^-^-^-^-^-^-^-^-^-^-^ THE REPORT OF THE BOOK -^-^-^-^-^-^-^-^-^-^-^-^");
            Console.WriteLine();
            CenterText("*******************************************************");

            // Display the total number of books in the library
            int totalBooks = nameBook.Count;
            CenterText($"Total number of books in the library: {totalBooks}");
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the number of categories and count of books in each category
            int totalCategories = categoryCounts.Count;
            CenterText($"Total number of categories: {totalCategories}");
            CenterText("Categories and the number of books in each:");
            foreach (var category in categoryCounts)
            {
                CenterText($"- {category.category}: {category.count} books");
            }
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the total number of copies of all books
            CenterText($"Total number of copies of all books: {totalCopies}");
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the total number of borrowed books
            CenterText($"Total number of borrowed books: {totalBorrowed}");
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the total number of returned books
            CenterText($"Total number of returned books: {totalReturned}");
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the most borrowed book
            if (borrowBook.Count > 0)
            {
                // Find the maximum borrowed count
                int maxBorrowed = borrowBook.Max();

                List<int> mostBorrowedIndices = new List<int>();

                // collect the book is max 
                for (int i = 0; i < borrowBook.Count; i++)
                {
                    if (borrowBook[i] == maxBorrowed)
                    {
                        mostBorrowedIndices.Add(i);
                    }
                }

                // maximum borrowed count
                CenterText($"The books most borrowed (Borrowed {maxBorrowed} times):");
                foreach (int index in mostBorrowedIndices)
                {
                    CenterText($"- {nameBook[index]}");
                }
            }
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the least borrowed book
            if (borrowBook.Count > 0)
            {
                // Find the minimum borrowed count
                int minBorrowed = borrowBook.Min();
                List<int> leastBorrowedIndices = new List<int>();

                // Collect all books that have the minimum borrowed count
                for (int i = 0; i < borrowBook.Count; i++)
                {
                    if (borrowBook[i] == minBorrowed)
                    {
                        leastBorrowedIndices.Add(i);
                    }
                }

                // Display all books with the minimum borrowed count
                CenterText($"The books least borrowed (Borrowed {minBorrowed} times):");
                foreach (int index in leastBorrowedIndices)
                {
                    CenterText($"- {nameBook[index]}");
                }
            }
        }


        static string CategorBookMenu()
        {
            bool validCategory = false;
            string ch1 = "";  // Initialize the variable

            while (!validCategory)
            {
                Console.WriteLine("Choose the category of book:");
                Console.WriteLine(" 1. ** History **");
                Console.WriteLine(" 2. ** IT **");
                Console.WriteLine(" 3. ** Software **");
                Console.WriteLine(" 4. ** Science **");
                Console.WriteLine(" 5. ** Stories **");

                string chooseCate = Console.ReadLine();

                switch (chooseCate)
                {
                    case "1":
                        ch1 = "History";
                        validCategory = true; // Mark as valid and exit the loop
                        break;
                    case "2":
                        ch1 = "IT";
                        validCategory = true;
                        break;
                    case "3":
                        ch1 = "Software";
                        validCategory = true;
                        break;
                    case "4":
                        ch1 = "Science";
                        validCategory = true;
                        break;
                    case "5":
                        ch1 = "Stories";
                        validCategory = true;
                        break;
                    default:
                       
                        Console.WriteLine("Incorrect choice, please try again...");
                        break;
                }
            }

            return ch1; // Return the selected category
        }


        //********************************************************************************************************************************************





        //user menu with service's 
        //*********************************************************************************************************************************************
        static void UserMenu()
        {

            bool shouldLogout = false; // Flag to indicate when to log out

            while (!shouldLogout)
            {
                Console.Clear();
                Console.WriteLine("User ID is: " + userId);

                DateTime nowdate = DateTime.Now.Date;

                // Check for overdue books
                bool hasOverdueBooks = false;
                foreach (var borrow in borrows)
                {
                    if (borrow.userid == userId && !borrow.isreturn && borrow.returndate < nowdate)
                    {
                        Console.WriteLine("YOU MUST RETURN THE BOOKS THAT YOU ARE LATE IN RETURNING.");
                        Console.WriteLine("\n1. ** Return the book which is late **");

                        int num;
                        if (int.TryParse(Console.ReadLine(), out num) && num == 1)
                        {
                            ReturnBookLate();
                            SaveborrowToFile();
                            hasOverdueBooks = true;
                            break; // Exit the loop after handling late return
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                }

                // If there are no overdue books or after handling overdue books, show the main user menu
                if (!hasOverdueBooks)
                {
                    Console.Clear();
                    Drowinguser();
                    Console.WriteLine("U ARE USER : " + userId);
                    Console.WriteLine("\nEnter the number of the operation you need:");
                    Console.WriteLine("\n1. Borrow a book");
                    Console.WriteLine("\n2. Return a book");
                    Console.WriteLine("\n3. Search for a book");
                    Console.WriteLine("\n4. View all books");
                    Console.WriteLine("\n5. View profile user");
                    Console.WriteLine("\n6. Logout");
                    Console.WriteLine();
                    drwingeyeopen();
                    int choice;
                    bool validInput = int.TryParse(Console.ReadLine(), out choice);

                    if (!validInput)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            BarrowBooks();
                            SaveborrowToFile();
                            break;

                        case 2:
                            ReturnBook();
                            SaveborrowToFile();
                            break;

                        case 3:
                            Console.Clear();
                            SearchForBook();
                            break;

                        case 4:
                            Console.Clear();
                            ViewAllBooks();
                            break;

                        case 5:
                            Console.Clear();
                            ViewProfile(userId);
                            break;

                        case 6:
                            Console.Clear();
                            SaveBooksToFile();
                            SaveborrowToFile();

                            userId = -1; // Reset the user ID to indicate a logout
                            shouldLogout = true; // Set flag to true to exit the loop
                            break;

                        default:
                            Console.WriteLine("Sorry, your choice was wrong. Please try again.");
                            break;
                    }

                    Console.WriteLine("\tPress enter key to continue...");
                    Console.ReadLine(); // Pause before clearing the screen
                }
            }

            // Reset the flag (though it will not be used as the loop has ended)
            shouldLogout = false;
        }

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
            Console.WriteLine("U are user : " + userId);
            Console.WriteLine("\n\n              ***** THE BOOK IS AVAILABLE TO BORROW IT ***** ");
            Console.WriteLine(" ");
            Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Name".PadRight(nameWidth)}{"Author".PadRight(authorWidth)}{"Available Copies".PadRight(copiesWidth)}{"Borrowed Copies".PadRight(borrowedWidth)}{"Price".PadRight(priceWidth)}{"Category".PadRight(categoryWidth)}{"Borrow Period".PadRight(periodWidth)}");
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
                        DateTime dateBorrow = DateTime.Now.Date;
                        DateTime returnDate = dateBorrow.AddDays(Books[i].borrowperiod).Date;

                        borrows.Add((userId, enterId, dateBorrow, returnDate, "N/A", "N/A", false));

                        Console.WriteLine("Book borrowed successfully!");

                        //suggstion for the author similriaty 

                        Console.WriteLine("\n **** Other books Can borrow by the same author if you want : ****");
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
        static void ReturnBook()
        {

            Console.Clear();
            bool userflag = false;
            bool hasBooksToReturn = false; // Tracks if the user has any books to return

            // Loop through borrows to check if the user has any books to return
            for (int i = 0; i < borrows.Count; i++)
            {
                if (borrows[i].userid == userId && !borrows[i].isreturn)
                {
                    hasBooksToReturn = true;
                    break;
                }
            }

            // If no books to return, notify the user
            if (!hasBooksToReturn)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n\n                                 ************           NO BOOKS TO RETURN ^-^      *********     ");
                userflag = true;
            }
            else
            {
                int idWidth = 10;
                int DateBorrowWidth = 25;
                int DateReturnWidth = 25;
                int IsReturnWidth = 25;

                Console.WriteLine("U ARE USER ID : " + userId);
                // Header
                Console.WriteLine("\n\n                         ***** BOOKS AVAILABLE TO RETURN ***** ");
                Console.WriteLine(" ");
                Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Date Borrow".PadRight(DateBorrowWidth)}{"Date Return".PadRight(DateReturnWidth)}{"IsReturned".PadRight(IsReturnWidth)}");
                Console.WriteLine(new string('-', idWidth + DateBorrowWidth + DateReturnWidth + IsReturnWidth));

                // Loop through the borrows to display books the user hasn't returned
                foreach (var borrow in borrows)
                {
                    if (borrow.userid == userId && !borrow.isreturn)
                    {
                        Console.WriteLine($"{borrow.bookid.ToString().PadRight(idWidth)}" +
                                          $"{borrow.borrowdate.ToString("yyyy-MM-dd").PadRight(DateBorrowWidth)}" +
                                          $"{borrow.returndate.ToString("yyyy-MM-dd").PadRight(DateReturnWidth)}" +
                                          $"{borrow.isreturn.ToString().PadRight(IsReturnWidth)}");
                    }
                }

                // Prompt user to enter book ID to return
                Console.WriteLine("Enter the book ID you want to return:");
                int id = int.Parse(Console.ReadLine());
                bool flag = false;

                // Loop through the list of books to find the one being returned
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].ID == id)
                    {
                        // Decrement the borrowed copies since the book is being returned
                        int updatedBorrowedCopies = Books[i].Borrowedcopies - 1;

                        // Update the book's record in the list
                        Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, updatedBorrowedCopies, Books[i].price, Books[i].category, Books[i].borrowperiod);

                        flag = true; // Mark that the book was found
                        break;
                    }
                }

                // Handle actual return date and rating process
                if (flag)
                {

                    bool ratingValid = false;

                    // Loop through the borrows list to update the return information
                    for (int i = 0; i < borrows.Count; i++)
                    {
                        if (borrows[i].bookid == id && !borrows[i].isreturn)
                        {
                            DateTime actualReturnDate = DateTime.Now.Date;
                            //string actualReturnDateStr = actualReturnDate.ToString("yyyy-MM-dd");

                            // Ensure the rating is within the valid range
                            string rateuser = "N/A"; // Default value if no valid rating is provided

                            // Prompt the user for a valid rating between 1 and 5
                            while (!ratingValid)
                            {
                                Console.WriteLine("Enter the rating of the book (1-5):");
                                int rate = int.Parse(Console.ReadLine());

                                if (rate >= 1 && rate <= 5)
                                {
                                    rateuser = rate.ToString();
                                    ratingValid = true; // Exit loop if rating is valid
                                }
                                else
                                {
                                    Console.WriteLine("Invalid rating. Please enter a value between 1 and 5.");
                                }
                            }

                            // Update the borrow entry with the return date, rating, and isreturn flag
                            borrows[i] = (borrows[i].userid, id, borrows[i].borrowdate, borrows[i].returndate, actualReturnDate.ToString("yyyy-MM-dd"), rateuser, true);

                            Console.WriteLine("Successfully returned the book.");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Book ID not found.");
                }
            }



        }

        static void ReturnBookLate()
        {
            Console.Clear();
            bool flaglate = false;
            bool hasBooksToReturnlate = false;
            DateTime datelate = DateTime.Now.Date;

            // First, check if the user has any late books
            for (int i = 0; i < borrows.Count; i++)
            {
                if (borrows[i].userid == userId && !borrows[i].isreturn && borrows[i].returndate < datelate)
                {
                    hasBooksToReturnlate = true;
                    break;
                }
            }

            // If user has late books, proceed
            if (hasBooksToReturnlate)
            {
                flaglate = true;
                int idWidth = 10;
                int DateBorrowWidth = 25;
                int DateReturnWidth = 25;
                int IsReturnWidth = 25;
                int numberofdatelate = 10;

                Console.WriteLine("U ARE USER ID : " + userId);
                // Header for the table of late books
                Console.WriteLine("\n\n                         ***** BOOKS AVAILABLE THAT YOU ARE LATE IN RETURNING ***** ");
                Console.WriteLine(" ");
                Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Date Borrow".PadRight(DateBorrowWidth)}{"Date Return".PadRight(DateReturnWidth)}{"IsReturned".PadRight(IsReturnWidth)}{"DayLate".PadRight(numberofdatelate)}");
                Console.WriteLine(new string('-', idWidth + DateBorrowWidth + DateReturnWidth + IsReturnWidth + numberofdatelate));

                // Display all late books for this user
                foreach (var borrow in borrows)
                {
                    if (borrow.userid == userId && !borrow.isreturn && borrow.returndate < datelate)
                    {
                        var numlate = (datelate - borrow.returndate).Days;
                        Console.WriteLine($"{borrow.bookid.ToString().PadRight(idWidth)}" +
                                          $"{borrow.borrowdate.ToString("yyyy-MM-dd").PadRight(DateBorrowWidth)}" +
                                          $"{borrow.returndate.ToString("yyyy-MM-dd").PadRight(DateReturnWidth)}" +
                                          $"{borrow.isreturn.ToString().PadRight(IsReturnWidth)}" +
                                          $"{numlate.ToString().PadRight(numberofdatelate)}days");
                    }
                }

                // Ask user for the book ID to return
                Console.WriteLine("Enter the book ID you want to return:");
                int id = int.Parse(Console.ReadLine());
                bool flag = false;

                // Find the book by ID in the Books list
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].ID == id)
                    {
                        int updatedBorrowedCopies = Books[i].Borrowedcopies - 1;
                        Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, updatedBorrowedCopies, Books[i].price, Books[i].category, Books[i].borrowperiod);
                        flag = true; // Book found
                        break;
                    }
                }

                if (!flag)
                {
                    // If book ID wasn't found in the Books list
                    Console.WriteLine("Book ID not found.");
                    return;
                }

                // If the book was found, proceed to return
                bool ratingValid = false;

                // Loop through borrows to update the return info
                for (int i = 0; i < borrows.Count; i++)
                {
                    if (borrows[i].bookid == id && !borrows[i].isreturn)
                    {
                        DateTime actualReturnDate = DateTime.Now.Date;
                        string rateuser = "N/A"; // Default value if no rating is given

                        // Prompt user for rating, ensure it's valid
                        while (!ratingValid)
                        {
                            Console.WriteLine("Enter the rating of the book (1-5):");
                            int rate = int.Parse(Console.ReadLine());

                            if (rate >= 1 && rate <= 5)
                            {
                                rateuser = rate.ToString();
                                ratingValid = true; // Exit loop if rating is valid
                            }
                            else
                            {
                                Console.WriteLine("Invalid rating. Please enter a value between 1 and 5.");
                            }
                        }

                        // Update the borrow record
                        borrows[i] = (borrows[i].userid, id, borrows[i].borrowdate, borrows[i].returndate, actualReturnDate.ToString("yyyy-MM-dd"), rateuser, true);

                        Console.WriteLine("Successfully returned the book.");
                        return; // Exit after successful return
                    }
                }

                // If the borrow record wasn't found
                Console.WriteLine("Error: No matching borrow record found for this book.");
            }
            else
            {
                Console.WriteLine("No late books found to return.");
            }
        }
        static void ViewProfile(int userId)
        {
            Console.Clear();
            // Find the user by ID in the userReistrtion list
            //new thing i learn it by "FirstOrDefault"..s used to find the first book in the Books list that matches a given condition
            var user = userReistrtion.FirstOrDefault(u => u.Aid == userId);
            //by defualt mean that is null if is null the list no value inside it 
            if (user == default)
            {
                Console.WriteLine("User not found.");
                return;
            }

            // Display user profile details
            Console.WriteLine("\n\n\t\t******   USER PROFILE    ******\n");
            Console.WriteLine($"User ID: {user.Aid}");
            Console.WriteLine($"Name: {user.username}");
            Console.WriteLine($"Email: {user.email}");


            List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)> currentBorrows = new List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)>();


            // Books that the user has borrowed but not yet returned
            for (int i = 0; i < borrows.Count; i++)
            {
                // Check if the borrow entry belongs to the specific user and hasn't been returned
                if (borrows[i].userid == userId && !borrows[i].isreturn)
                {
                    // Add the current borrow record to the result list
                    currentBorrows.Add(borrows[i]);
                }
            }


            List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)> returnedBooks = new List<(int userid, int bookid, DateTime borrowdate, DateTime returndate, string acaualreturndate, string rating, bool isreturn)>();
            // Books that the user has borrowed and returned
            for (int i = 0; i < borrows.Count; i++)
            {
                // Check if the borrow entry belongs to the specific user and has been returned
                if (borrows[i].userid == userId && borrows[i].isreturn)
                {
                    // Add the returned borrow record to the result list
                    returnedBooks.Add(borrows[i]);
                }
            }
            //padRight mean the width size of container the value 
            Console.WriteLine("\nBooks Currently Borrowed (Not Returned):");
            if (currentBorrows.Count > 0)
            {
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine($"| {"Book Name".PadRight(25)} | {"Borrow Date".PadRight(20)} | {"Return Date".PadRight(20)} |");
                Console.WriteLine("-------------------------------------------------------------------------------");

                foreach (var borrow in currentBorrows)
                {
                    // Find the book details using bookid
                    var book = Books.FirstOrDefault(b => b.ID == borrow.bookid);
                    string bookName = book.BName;

                    Console.WriteLine($"| {bookName.PadRight(25)} | {borrow.borrowdate.ToString("yyyy-MM-dd").PadRight(20)} | {borrow.returndate.ToString("yyyy-MM-dd").PadRight(20)} |");
                }
                Console.WriteLine("-------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No books currently borrowed.");
            }

            Console.WriteLine("\nBooks Previously Borrowed and Returned:");
            if (returnedBooks.Count > 0)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| {"Book Name".PadRight(25)} | {"Borrow Date".PadRight(20)} | {"Return Date".PadRight(20)} | {"Actual Return Date".PadRight(20)} | {"On Time?".PadRight(10)} |");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                foreach (var borrow in returnedBooks)
                {
                    // Find the book details using bookid
                    var book = Books.FirstOrDefault(b => b.ID == borrow.bookid);
                    string bookName = book.BName;

                    // Parse the actual return date from string to DateTime
                    DateTime actualReturnDate;
                    if (!DateTime.TryParse(borrow.acaualreturndate, out actualReturnDate))
                    {
                      
                        Console.WriteLine("Error: Invalid return date format.");
                        continue;
                    }

                    // Check if the book was returned on time
                    string onTime;
                    if (actualReturnDate <= borrow.returndate)
                    {
                        onTime = "Yes";
                    }
                    else
                    {
                        onTime = "No";
                    }

                    // Display the book details
                    Console.WriteLine($"| {bookName.PadRight(25)} | {borrow.borrowdate.ToString("yyyy-MM-dd").PadRight(20)} | {borrow.returndate.ToString("yyyy-MM-dd").PadRight(20)} | {actualReturnDate.ToString("yyyy-MM-dd").PadRight(20)} | {onTime.PadRight(10)} |");
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No books previously borrowed and returned.");
            }
        }



        //***********************************************************************************************************************************************


        

        //edits books
        //will added all in editbookmenu .....
        //**********************************************************************************************************************************************
        static void EditBookMenu()
        {
            Console.Clear();
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
            Console.Clear();
            // List to store existing book names
            List<string> existingNames = new List<string>();

            // Display all books (assuming ViewAllBooks is a method to show all books)
            ViewAllBooks();

            Console.WriteLine(" ");

            Console.WriteLine("Enter the ID of the book you want to edit:");
            int id = int.Parse(Console.ReadLine());

            bool bookFound = false;

            // Loop through all books to find the book by its ID
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {
                    // Populate the existingNames list with all current book names
                    foreach (var book in Books)
                    {
                        existingNames.Add(book.BName);
                    }

                    string name;
                    while (true) // Loop to get a valid book name
                    {
                        Console.WriteLine("Enter a new book name:");
                        name = Console.ReadLine();

                        if (existingNames.Contains(name))
                        {
                            Console.WriteLine("Name already exists. Try again.");
                        }
                        else
                        {
                            // If the name is valid, break the loop and proceed
                            existingNames.Add(name);
                            break;
                        }
                    }

                    // Update the book's name in the list
                    Books[i] = (Books[i].ID, name, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies, Books[i].price, Books[i].category, Books[i].borrowperiod);
                    Console.WriteLine("Successfully updated the book name.");

                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadKey(); 
                    Console.Clear();
                    bookFound = true;
                    break; // Exit the loop since the book was found and updated
                }
            }

            // If the book ID was not found, inform the user
            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }
        }

        static void EditAuthor()
        {
            Console.Clear();
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

                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadKey(); // Wait for the user to press any key
                    Console.Clear();

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
            Console.Clear();
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

                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadKey(); // Wait for the user to press any key
                    Console.Clear();
                    flag = true;
                    break;
                }
            }

            if (flag != true)

            {
                Console.WriteLine("book not found");
            }

        }

        static void Drowing()
        {
            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;

            string dr = @"











                                                             ____        ____              _          
                                                            | __ )      | __ )  ___   ___ | | __  ___ 
                                                            |  _ \      |  _ \ / _ \ / _ \| |/ / / __|
                                                            | |_) |  _  | |_) | (_) | (_) |   <  \__ \
                                                            |____/  (_) |____/ \___/ \___/|_|\_\ |___/

";
           Console.WriteLine( "\n\n\n\n\n\t\t\t\t\t\t " + dr + "\n\n\n\n");
            // Console.WriteLine("\n\n\t\t\t\t\t\t "+ dr +"\n\n");
            Console.ForegroundColor = originalColor;
        }
        
        static void Drowinguser()
        {
            string dr = @"


                               _                                 _   _                   
            __      __   ___  | |   ___    ___    _ __ ___      | | | |  ___    ___  _ __ 
            \ \ /\ / /  / _ \ | |  / __|  / _ \  | '_ ` _ \     | | | | / __|  / _ \  '__|
             \ V  V /  |  __/ | | | (__  | (_) | | | | | | |    | |_| | \__ \ |  __/  |   
              \_/\_/    \___| |_|  \___|  \___/  |_| |_| |_|     \___/  |___/  \___| _|   

";
            Console.WriteLine(dr);
        }
        static void Drowingadmin()
        {
            string dr = @" 


                              _                             _       _           _       
                __      _____| | ___ ___  _ __ ___         / \   __| |_ __ ___ (_)_ __  
                \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \       / _ \ / _` | '_ ` _ \| | '_ \ 
                 \ V  V /  __/ | (_| (_) | | | | | |     / ___ \ (_| | | | | | | | | | |
                  \_/\_/ \___|_|\___\___/|_| |_| |_|    /_/   \_\__,_|_| |_| |_|_|_| |_|


";
            Console.WriteLine(dr);
        }
        static void drwingeyeopen() 
        {

            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;
            string eye = @"
                          
 








                         -----        -----
                     /\ |     |      |     | /\
                       \|  /\ |------|  /\ |/
                        |     |      |     |
                         -----        -----
                             _         _
                              \       /
                               ------- 
                             WokeUp robot
                        ";
            Console.WriteLine("\t\t\t" + eye);
            Console.ForegroundColor = originalColor;
        }

        static void drwingeyeclose()
        {
            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;

            // Define the ASCII art
            string eye = @"
                                                                '\./' 
                                                                          _.-'''''-._
                                                     '\./'              .'  _     _  '.       '\./' 
                                 '\./'                                 /   ---   ---   \                   '\./'
                                                                      |                 |
                                          '\./'                       |  \           /  |
                                                               '\./'   \  '.       .'  /      '\./'                     '\./'
                                                      '\./'             '.  `'---'`  .'
                                                                          '-._____.-'                    '\./' 
                                                                                      Zzz   
                                                                          Sleep Robot              

                 ";

            // Print the text in the new color
            Console.WriteLine("\t" + eye);

            // Reset the color back to the original
            Console.ForegroundColor = originalColor;
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
                        writer.WriteLine($"{borr.userid} | {borr.bookid} | {borr.borrowdate.ToString("yyyy-MM-dd")} | {borr.returndate.ToString("yyyy-MM-dd")} | {borr.acaualreturndate} | {borr.rating} | {borr.isreturn}");
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
                                
                                //DateTime borrowDate = DateTime.Parse(parts[2], "yyyy-MM-dd");
                               // DateTime returnDate = DateTime.ParseExact(parts[3], "yyyy-MM-dd", null);
                                // Convert actualReturnDate string to DateTime
                              //  DateTime actualReturnDate = DateTime.ParseExact(parts[4], "yyyy-MM-dd", null);
                                borrows.Add((int.Parse(parts[0]), int.Parse(parts[1]), DateTime.Parse(parts[2]).Date, DateTime.Parse(parts[3]).Date, parts[4], parts[5], bool.Parse(parts[6])));
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

        static void LoadCategory()
        {
            try
            {
                if (File.Exists(fileCategory))
                {
                    using (StreamReader reader = new StreamReader(fileCategory))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(" | ");
                            if (parts.Length == 3)
                            {
                                Category.Add((int.Parse(parts[0]), parts[1], int.Parse(parts[2])));
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

        static void SaveCategory()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileCategory))
                {
                    foreach (var cat in Category)
                    {
                        writer.WriteLine($"{cat.Cid} | {cat.NameCat} | {cat.NumofBook}");
                    }
                }
                // Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
    }
            // **********************************************************************************************************************************************








        }
    

