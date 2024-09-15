using System;
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



            bool ExitFlag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("enter the number of the option: ");
                Console.WriteLine("1. login admin ");
                Console.WriteLine("2. login user ");
                Console.WriteLine("3. Registaration ");
                Console.WriteLine("4. logout ");
                int num = int.Parse(Console.ReadLine());

                switch(num)
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
                                int adminId = -1;

                                // Loop through admin registrations to check email and password
                                for (int i = 0; i < adminRegistration.Count; i++)
                                {
                                    if (adminRegistration[i].email == emailAdmin)
                                    {
                                        emailFound1 = true;  // Email exists
                                        if (adminRegistration[i].password == passwordAdmin)
                                        {
                                            passwordCorrect1 = true;  // Email and password both match
                                            adminId = adminRegistration[i].adminid;
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
                        Console.Clear();
                        AddnNewBook();
                        SaveCategory();
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
                        SaveCategory();
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
                        Console.Clear();
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
            Console.Clear();
            bool ExitFlag = false;

            Console.WriteLine("User ID is: " + userId);

            DateTime nowdate = DateTime.Now.Date;

            // Check for overdue books first
            for (int i = 0; i < borrows.Count; i++)
            {
                if (borrows[i].userid == userId && !borrows[i].isreturn && borrows[i].returndate < nowdate)
                {
                    Console.WriteLine("YOU MUST RETURN THE BOOKS THAT YOU ARE LATE IN RETURNING.");
                    Console.WriteLine("\n1. ** Return the book which is late **");
                    int num = int.Parse(Console.ReadLine());

                    switch (num)
                    {
                        case 1:
                            //Console.Clear();
                            ReturnBookLate();
                            SaveborrowToFile();
                            ExitFlag = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    ExitFlag = false;
                    
                }
            }

            // After checking for late books, show the main user menu
            if (!ExitFlag) // Only show the menu if there are no overdue books or after handling overdue books
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(" U ARE USER : "+ userId);
                    Console.WriteLine("\nEnter the number of the operation you need:");
                    Console.WriteLine("\n1. Borrow a book");
                    Console.WriteLine("\n2. Return a book");
                    Console.WriteLine("\n3. Logout");

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
                            SaveBooksToFile();
                            SaveborrowToFile();
                            userId = -1; // Reset the user ID to indicate a logout
                            ExitFlag = true; // Set exit flag to true to break out of the loop
                            break;

                        default:
                            Console.WriteLine("Sorry, your choice was wrong. Please try again.");
                            break;
                    }

                    if (!ExitFlag)
                    {
                        Console.WriteLine("\tPress enter key to continue...");
                        Console.ReadLine(); // Pause before clearing the screen
                    }

                } while (!ExitFlag); // Exit the loop only when ExitFlag is true
            }

            // Reset ExitFlag in case it's needed for future sessions
            ExitFlag = false;
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
    

