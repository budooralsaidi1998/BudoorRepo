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

        static List<(int Cid, string NameCat, int NumofBook)> Category = new List<(int Cid, string NameCat, int NumofBook)>();

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

            try
            {
                do
                {
                    Console.Clear();

                    Console.WriteLine();
                    drwingeyeclose();
                    Console.WriteLine();

                    ConsoleColor lColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine("\t\t\t\t\t\t\t\t\t1. login admin ");
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t-----------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t2. login user ");
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t3. Registration ");
                    Console.WriteLine();
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t-------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t\t4. logout ");
                    Console.ForegroundColor = lColor;
                    Console.WriteLine();
                    Console.WriteLine();
                    ConsoleColor iiii = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\t\t\t\t\t\t\t\tEnter the number of the option: ");
                    Console.ForegroundColor = iiii;

                    int num;
                    try
                    {
                        num = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Skip to the next iteration of the loop
                    }

                    Console.Clear();

                    switch (num)
                    {
                        case 1:
                            bool IsLogin = false;
                            Drowingadmin();
                            ConsoleColor originlColor = Console.ForegroundColor;

                            // Set the desired color
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t\t\t\t\t\t\t             ... Click enter key to Login ...");
                            Console.ForegroundColor = originlColor;
                            Console.ReadLine();

                            while (!IsLogin)
                            {
                                Console.Clear();
                                ConsoleColor oiginlColor = Console.ForegroundColor;

                                // Set the desired color
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine(" ");
                                Console.WriteLine("\n\n\n\t\t\t\t\t\t ***************** Login admin  ********************");
                                Console.WriteLine(" ");
                                Console.ForegroundColor = oiginlColor;

                                ConsoleColor iginlColor = Console.ForegroundColor;
                                // Set the desired color
                                Console.ForegroundColor = ConsoleColor.Cyan;

                                Console.Write(" \n\n\t\t\t\t\t\tEnter your email: ");
                                string emailAdmin = Console.ReadLine();

                                Console.WriteLine(" ");
                                Console.Write("\n\n\t\t\t\t\t\tEnter the password: ");
                                string passwordAdmin = Console.ReadLine();

                                Console.WriteLine(" ");
                                Console.Write("\n\n\t\t\t\t\t\tRe-enter the password: ");
                                string reEnterPasswordAdmin = Console.ReadLine();
                                Console.ForegroundColor = oiginlColor;

                                // Check if the passwords match
                                if (passwordAdmin != reEnterPasswordAdmin)
                                {
                                    Console.WriteLine("\n\n\t\t\t\t\t\tPasswords do not match. Please try again.");
                                    Console.WriteLine("\n\n\t\t\t\t\t\tPress any key to continue.");
                                    Console.ReadKey();
                                    break;  // Restart the loop
                                }

                                bool emailFound1 = false;
                                bool passwordCorrect1 = false;

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
                                    Console.Write("\n\n\t\t\t\t\t\tAdmin email not found. Do you want to try logging in again? (yes or no) ");
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
                            Drowinguser();
                            ConsoleColor r = Console.ForegroundColor;

                            // Set the desired color
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\t\t\t\t\t\t\t             ... Click enter key to Login ...");
                            Console.ForegroundColor = r;
                            Console.ReadLine();

                            bool Isloginuser = false;
                            while (!Isloginuser)
                            {
                                Console.Clear();
                                bool flaguser = false;
                                ConsoleColor n = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine(" ");
                                Console.WriteLine("\n\n\n\t\t\t\t\t\t***************** Login user  ********************");
                                Console.WriteLine(" ");
                                Console.ForegroundColor = n;

                                ConsoleColor a = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(" ");
                                Console.Write("\n\n\t\t\t\t\t\t enter your email: ");
                                string email = Console.ReadLine();

                                Console.WriteLine(" ");
                                Console.Write("\n\n\t\t\t\t\t\tEnter the password: ");
                                string password = Console.ReadLine();

                                Console.WriteLine(" ");
                                Console.Write("\n\n\t\t\t\t\t\tRe-enter the password: ");
                                string reenterPasswordadmin = Console.ReadLine();
                                Console.ForegroundColor = a;

                                if (password != reenterPasswordadmin)
                                {
                                    Console.WriteLine("Passwords do not match. Please try again.");
                                    Console.WriteLine("Press enter key");
                                    Console.ReadKey();
                                    Isloginuser = false;
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
                                    Console.WriteLine("User email not found. Do you want to try logging in again? (yes or no) ");
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
                                    Isloginuser = true;  // Successful login
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
                } while (!ExitFlag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
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

                try
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t\t\t\t\t\t\t\t1- Admin registration ");
                        Console.WriteLine("\n\t\t\t\t\t\t\t\t2- User registration ");
                        Console.WriteLine("\n\t\t\t\t\t\t\t\t3- Save and Exit");
                        Console.Write("\t\t\t\t\t\t\t\t");

                        int choice;
                        try
                        {
                            choice = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            continue; // Skip to the next iteration of the loop
                        }

                        switch (choice)
                        {
                            case 1:
                                try
                                {
                                    AdminRegistration();
                                    SaveAdminRegToFile();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("An error occurred during admin registration or saving: " + ex.Message);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case 2:
                                try
                                {
                                    UserRegistration();
                                    SaveUserRegToFile();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("An error occurred during user registration or saving: " + ex.Message);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case 3:
                                ExitFlag = true;
                                break;

                            default:
                                Console.WriteLine("Sorry, your choice was incorrect. Please choose a valid option.");
                                break;
                        }

                        Console.WriteLine("Press enter key to continue");
                        Console.ReadLine();

                    } while (!ExitFlag);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }


            static void AdminRegistration()
            {
                Console.Clear();
                List<string> existingNames = new List<string>();
                List<string> existingEmails = new List<string>();

                // Populate existing names and emails
                for (int i = 0; i < adminRegistration.Count; i++)
                {
                    var (id, name, email, pass) = adminRegistration[i];
                    existingNames.Add(name);
                    existingEmails.Add(email);
                }

                // Regex patterns
                string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

                try
                { 
                    while (true)
                    {
                      
                        int id = adminRegistration.Count + 1;
                        Console.WriteLine();
                        Console.Write("\t\t\t\t\t\t\t\tEnter the name:");
                        string  name = Console.ReadLine().Trim().ToLower(); // Convert input to lowercase and trim whitespace
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("  cannot be empty.");
                            Console.WriteLine("enter key..");
                            Console.ReadKey();
                            break;
                        }
                        //try
                        //{
                        //     name = Console.ReadLine();
                        //}
                        //catch (FormatException)
                        //{
                        //    Console.WriteLine(" input is empty . Please try again...");
                        //    Console.WriteLine("Press any key to continue...");
                        //    Console.ReadKey();
                        //    continue; // Skip to the next iteration of the loop
                        //}
                        
                        Console.WriteLine();
                        // Check for duplicate name
                        //if (existingNames.Contains(name))
                        //{
                        //    Console.Write("\t\t\t\t\t\t\t\tName already exists. Try again.");
                        //    Console.WriteLine();
                        //    Console.Write("Press any key to continue...");
                        //    Console.ReadKey();
                        //    continue; // Prompt user again for input
                        //}
                        Console.WriteLine();
                        Console.Write("\t\t\t\t\t\t\t\tEnter the email:");
                        string email = Console.ReadLine();
                        Console.WriteLine();
                        // Validate email
                        if (!IsValidEmail(email, emailPattern))
                        {
                            Console.Write("\t\t\t\t\t\t\t\tInvalid email format. Try again.");
                            Console.WriteLine();
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            return; // Prompt user again for input
                        }

                        // Check for duplicate email
                        if (existingEmails.Contains(email))
                        {
                            Console.WriteLine();
                            Console.Write("\t\t\t\t\t\t\t\tEmail already exists. Try again.");
                            Console.WriteLine();
                            Console.Write("\t\t\t\t\t\t\t\tPress any key to continue...");
                            Console.ReadKey();
                            continue; // Prompt user again for input
                        }
                        Console.WriteLine();
                        Console.Write("\t\t\t\t\t\t\t\tEnter the password:");
                        string password = Console.ReadLine();
                        Console.WriteLine();
                        // Validate password
                        if (!IsValidPassword(password, passwordPattern))
                        {
                            Console.Write("\t\t\t\t\t\t\t\tInvalid password format. Try again.");
                            Console.WriteLine();
                            Console.Write("\t\t\t\t\t\t\t\tPress any key to continue...");
                            Console.ReadKey();
                            continue; // Prompt user again for input
                        }

                        // Add registration
                        adminRegistration.Add((id, name, email, password));
                        Console.WriteLine();
                        Console.Write("\t\t\t\t\t\t\t\tSuccessfully added");
                        Console.WriteLine();
                        Console.Write("\t\t\t\t\t\t\t\tPress any key to continue...");
                        Console.ReadKey();
                        break; // Exit loop after successful registration
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.Write("\t\t\t\t\t\t\t\tAn unexpected error occurred: " + ex.Message);
                    Console.WriteLine();
                    Console.Write("\t\t\t\t\t\t\t\tPress any key to exit...");
                    Console.ReadKey();
                }
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
            Console.Clear();
            List<string> existingNames = new List<string>();
            List<string> existingEmails = new List<string>();

            // Populate existing names and emails
            for (int i = 0; i < userReistrtion.Count; i++)
            {
                var (id, name, email, pass) = userReistrtion[i];
                existingNames.Add(name);
                existingEmails.Add(email);
            }

            // Regex patterns
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            try
            {
                while (true)
                {
                    int userId = userReistrtion.Count + 1;

                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter the name:");
                    string name = Console.ReadLine();

                    // Check for duplicate name
                    if (existingNames.Contains(name))
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tName already exists. Try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Prompt user again
                    }

                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter the email:");
                    string email = Console.ReadLine();

                    // Validate email
                    if (!IsValidEmail(email, emailPattern))
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tInvalid email format. Try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Prompt user again
                    }

                    // Check for duplicate email
                    if (existingEmails.Contains(email))
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tEmail already exists. Try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Prompt user again
                    }

                    Console.WriteLine("\t\t\t\t\t\t\t\tEnter the password:");
                    string password = Console.ReadLine();

                    // Validate password
                    if (!IsValidPassword(password, passwordPattern))
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tInvalid password format. Try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Prompt user again
                    }

                    // Add registration
                    userReistrtion.Add((userId, name, email, password));

                    Console.WriteLine("\t\t\t\t\t\t\t\tSuccessfully added");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break; // Exit loop after successful registration
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }






        //admin menu with the service admin 
        //*******************************************************************************************************************************************


        static void AdminMenu()
        {
            bool ExitFlag = false;

            do
            {
                try
                {
                    Console.Clear();
                    drwingeyeopen();

                    ConsoleColor originalColor = Console.ForegroundColor;

                    // Display admin info
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t I am Admin: " + adminId);
                    Console.ForegroundColor = originalColor;

                    // Display menu options
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 1- Add New Book             |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 2- Display All Books       |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 3- Search for Book by Name |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 4- Edit the Book           |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 5- Remove the Book         |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 6- Reporting the Data      |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 7- Save and Exit           |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.Write("     \t\t\t\t\t\t\t\t");
                    Console.ForegroundColor = originalColor;

                    int choice;
                    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

                    if (!validChoice)
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue; // Prompt user again
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            AddNewBook();
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
                            Console.WriteLine("Invalid choice. Please select a number between 1 and 7.");
                            break;
                    }

                    if (!ExitFlag)
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tPress Enter to continue...");
                        Console.ReadLine();
                    }

                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    ExitFlag = true; // Exit the loop on error
                }
            } while (!ExitFlag);
        }


        static void AddNewBook()
        {
            List<string> existingNames = new List<string>();

            // Populate existing names
            for (int i = 0; i < Books.Count; i++)
            {
                var (ID, BName, BAuthor, copiesd, Borrowedcopies, priced, category, borrowperiod) = Books[i];
                existingNames.Add(BName);
            }

            int id = Books.Count + 1;
            string name;
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\t\t\t\t\t\t\t------ Input the details of the book ------ ");
                    Console.ForegroundColor = originalColor;
                    Console.WriteLine();

                    Console.Write("\t\t\t\t\t\t\t\tEnter Book Name: ");
                    name = Console.ReadLine();

                    if (existingNames.Contains(name))
                    {
                        Console.WriteLine("\t\t\t\t\t\t\t\tName already exists. Try again.");
                    }
                    else
                    {
                        // If the name is valid, add it to the existing names list and break the loop
                        existingNames.Add(name);
                        break;
                    }
                }

                // Get and validate the author
                Console.Write("\t\t\t\t\t\t\t\tEnter Book Author: ");
                string author = Console.ReadLine();

                // Get and validate the number of copies
                int copies;
                while (true)
                {
                    Console.Write("\t\t\t\t\t\t\t\tEnter copies: ");
                    if (int.TryParse(Console.ReadLine(), out copies) && copies > 0)
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter a positive integer for the number of copies.");
                }

                // Get and validate the price
                decimal price;
                while (true)
                {
                    Console.Write("\t\t\t\t\t\t\t\tEnter the price: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter a non-negative decimal value for the price.");
                }

                // Get and validate the book category
                Console.WriteLine("\t\t\t\t\t\t\t\tEnter Book Category:");
                string categorySelect = CategorBookMenu();

                bool categoryFound = false;
                for (int i = 0; i < Category.Count; i++)
                {
                    if (Category[i].NameCat.Trim() == categorySelect.Trim()) // Trim() to handle extra spaces
                    {
                        int updatedNumOfBooks = Category[i].NumofBook + 1;
                        Category[i] = (Category[i].Cid, Category[i].NameCat, updatedNumOfBooks);
                        categoryFound = true;
                        break;
                    }
                }

                if (!categoryFound)
                {
                    Console.WriteLine("Category not found. Please add the category before adding books.");
                    return;
                }

                // Get and validate the borrow period
                int borrowPeriod;
                while (true)
                {
                    Console.Write("\t\t\t\t\t\t\t\tEnter the borrow period: ");
                    if (int.TryParse(Console.ReadLine(), out borrowPeriod) && borrowPeriod > 0)
                        break;
                    else
                        Console.WriteLine("Invalid input. Please enter a positive integer for the borrow period.");
                }

                // Add the new book to the list
                Books.Add((id, name, author, copies, 0, price, categorySelect, borrowPeriod));

                Console.ForegroundColor = originalColor;
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t------ Book Added Successfully -----");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                Console.ForegroundColor = originalColor;
            }
        }


        static void ViewAllBooks()
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\t\t\t\t\t\t******   DETAILS OF BOOKS    ******");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("   ");

                ConsoleColor headerColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;

                StringBuilder sb = new StringBuilder();

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
                sb.Append("\t").Append(new string('-', idPadding + namePadding + authorPadding + copiesPadding + borrowedPadding + pricePadding + categoryPadding + periodPadding + 9))
                  .AppendLine();

                // Check if the book list is empty
                if (Books.Count == 0)
                {
                    sb.Append("\tNo books available to display.")
                      .AppendLine();
                }
                else
                {
                    // Loop through each book and display its details
                    for (int i = 0; i < Books.Count; i++)
                    {
                        var (ID, BName, BAuthor, copies, Borrowedcopies, price, category, borrowperiod) = Books[i];

                        sb.Append("\t|");
                        sb.Append(CenterText(ID.ToString(), idPadding)).Append("|");
                        sb.Append(CenterText(BName, namePadding)).Append("|");
                        sb.Append(CenterText(BAuthor, authorPadding)).Append("|");
                        sb.Append(CenterText(copies.ToString(), copiesPadding)).Append("|");
                        sb.Append(CenterText(Borrowedcopies.ToString(), borrowedPadding)).Append("|");
                        sb.Append(CenterText(price.ToString("C"), pricePadding)).Append("|"); // Format price as currency
                        sb.Append(CenterText(category, categoryPadding)).Append("|");
                        sb.Append(CenterText(borrowperiod.ToString(), periodPadding)).Append("|")
                          .AppendLine();
                    }
                }

                // Add another separator line at the bottom
                sb.Append("\t").Append(new string('-', idPadding + namePadding + authorPadding + copiesPadding + borrowedPadding + pricePadding + categoryPadding + periodPadding + 9))
                  .AppendLine();

                // Display the final result
                Console.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
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
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Enter the book name or part of the name you want to search for:");
                string keyword = Console.ReadLine().Trim().ToLower(); // Convert input to lowercase and trim whitespace
                if (string.IsNullOrEmpty(keyword))
                {
                    Console.WriteLine("Search keyword cannot be empty.");
                    return;
                }

                bool found = false;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n\n\t\t\t\t\tSearch Results");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("   ");

                // Iterate through the list of books to find any book with a name that contains the keyword
                foreach (var book in Books)
                {
                    if (book.BName.ToLower().Contains(keyword))
                    {
                        // If a match is found, display all book details
                        Console.WriteLine("\nBook ID: " + book.ID);
                        Console.WriteLine("Book Name: " + book.BName);
                        Console.WriteLine("Book Author: " + book.BAuthor);
                        Console.WriteLine("Copies Available: " + book.copies);
                        Console.WriteLine("Borrowed Copies: " + book.Borrowedcopies);
                        Console.WriteLine("Price: " + book.price.ToString("C")); // Format price as currency
                        Console.WriteLine("Category: " + book.category);
                        Console.WriteLine("Borrow Period: " + book.borrowperiod);
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
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }


        static void RemoveBook()
        {
            // Display all books before attempting to remove one
            ViewAllBooks();

            Console.WriteLine();
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Choose the ID of the book you want to delete:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = originalColor;
                Console.WriteLine("Invalid ID. Please enter a valid number.");
                return; // Exit if the input is not a valid number
            }

            Console.ForegroundColor = originalColor;
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Can't remove the book because someone has borrowed it.");
                        Console.ForegroundColor = originalColor;
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        return; // Exit if the book can't be deleted
                    }
                    else
                    {
                        // Display confirmation prompt
                        Console.WriteLine($"\nAre you sure you want to delete '{Books[i].BName}' by {Books[i].BAuthor}? (yes/no)");
                        string confirmation = Console.ReadLine().Trim().ToLower();

                        if (confirmation == "yes")
                        {
                            // Remove the book from the list
                            Books.RemoveAt(i);
                            Console.WriteLine("---Book successfully deleted---");
                        }
                        else
                        {
                            Console.WriteLine("Book deletion canceled.");
                        }

                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        return; // Exit after processing the request
                    }
                }
            }

            // If the book with the given ID was not found, display a message
            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadKey();
            Console.Clear();
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
            ConsoleColor orglCl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            CenterText("-^-^-^-^-^-^-^-^-^-^-^-^ THE REPORT OF THE BOOK -^-^-^-^-^-^-^-^-^-^-^-^");
            Console.ForegroundColor = orglCl;
            Console.WriteLine();
            CenterText("*******************************************************");
            ConsoleColor orglC = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            // Display the total number of books in the library
            int totalBooks = nameBook.Count;
            CenterText($"Total number of books in the library: {totalBooks}");
            Console.WriteLine();
            Console.ForegroundColor = orglCl;
            CenterText("*******************************************************");

            // Display the number of categories and count of books in each category

            ConsoleColor orgl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            int totalCategories = categoryCounts.Count;

            CenterText($"Total number of categories: {totalCategories}");
            CenterText("Categories and the number of books in each:");
            foreach (var category in categoryCounts)
            {
                CenterText($"- {category.category}: {category.count} books");
            }
            Console.ForegroundColor = orgl;
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the total number of copies of all books
            ConsoleColor org = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            CenterText($"Total number of copies of all books: {totalCopies}");
            Console.WriteLine();
            Console.ForegroundColor = org;
            CenterText("*******************************************************");

            // Display the total number of borrowed books
            ConsoleColor or = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            CenterText($"Total number of borrowed books: {totalBorrowed}");
            Console.ForegroundColor = or;
            Console.WriteLine();

            CenterText("*******************************************************");

            // Display the total number of returned books
            ConsoleColor o = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            CenterText($"Total number of returned books: {totalReturned}");
            Console.ForegroundColor = o;
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
                ConsoleColor j = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                CenterText($"The books most borrowed (Borrowed {maxBorrowed} times):");
                foreach (int index in mostBorrowedIndices)
                {
                    CenterText($"- {nameBook[index]}");
                }
                Console.ForegroundColor = j;
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
                ConsoleColor r = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                CenterText($"The books least borrowed (Borrowed {minBorrowed} times):");
                foreach (int index in leastBorrowedIndices)
                {
                    CenterText($"- {nameBook[index]}");
                }
                Console.ForegroundColor = r;
            }
        }


        static string CategorBookMenu()
        {
            bool validCategory = false;
            string selectedCategory = "";  // Initialize the variable

            while (!validCategory)
            {
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\t\t\t\t\t\t\t\tChoose the category of the book:");
                Console.WriteLine("\t\t\t\t\t\t\t\t1. **History**");
                Console.WriteLine("\t\t\t\t\t\t\t\t2. **IT**");
                Console.WriteLine("\t\t\t\t\t\t\t\t3. **Software**");
                Console.WriteLine("\t\t\t\t\t\t\t\t4. **Science**");
                Console.WriteLine("\t\t\t\t\t\t\t\t5. **Stories**");
                Console.Write("\n\t\t\t\t\t\t\t\tEnter your choice (1-5): ");
                string userChoice = Console.ReadLine();
                Console.ForegroundColor = originalColor;

                try
                {
                    switch (userChoice)
                    {
                        case "1":
                            selectedCategory = "History";
                            validCategory = true; // Mark as valid and exit the loop
                            break;
                        case "2":
                            selectedCategory = "IT";
                            validCategory = true;
                            break;
                        case "3":
                            selectedCategory = "Software";
                            validCategory = true;
                            break;
                        case "4":
                            selectedCategory = "Science";
                            validCategory = true;
                            break;
                        case "5":
                            selectedCategory = "Stories";
                            validCategory = true;
                            break;
                        default:
                            throw new ArgumentException("Invalid choice. Please enter a number between 1 and 5.");
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handle specific exceptions
                    Console.WriteLine($"\n\t\t\t\t\t\t\t\t{ex.Message}");
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions
                    Console.WriteLine($"\n\t\t\t\t\t\t\t\tAn unexpected error occurred: {ex.Message}");
                }
            }

            return selectedCategory; // Return the selected category
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

                        try
                        {
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
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                    }
                }

                // If there are no overdue books or after handling overdue books, show the main user menu
                if (!hasOverdueBooks)
                {
                    Console.Clear();
                    Console.WriteLine();
                    drwingeyeopen();
                    ConsoleColor l = Console.ForegroundColor;
                    // Set the desired color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t U ARE USER : " + userId);
                    Console.ForegroundColor = l;

                    ConsoleColor ii = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 1-     Borrow a book        |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 2-     Return a book        |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 3-   Search for a book      |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 4-     View all books       |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 5-    View profile user     |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.WriteLine("\t\t\t\t\t\t\t\t| 6-         Logout           |");
                    Console.WriteLine("\t\t\t\t\t\t\t\t------------------------------");
                    Console.Write("     \t\t\t\t\t\t\t\t");
                    Console.ForegroundColor = ii;

                    try
                    {
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
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
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
            ConsoleColor ll = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t U ARE USER : " + userId);
            Console.ForegroundColor = ll;

            ConsoleColor yl = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n\n              ***** THE BOOK IS AVAILABLE TO BORROW IT ***** ");
            Console.WriteLine(" ");
            Console.ForegroundColor = yl;

            ConsoleColor mm = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
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

            Console.ForegroundColor = mm;
            Console.WriteLine("\n\nEnter the book ID you want to borrow:");

            try
            {
                int enterId;
                if (!int.TryParse(Console.ReadLine(), out enterId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid book ID.");
                    return; // Exit if the input is not a valid number
                }

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

                            // Display confirmation prompt
                            Console.WriteLine($"\nAre you sure you want to borrow '{Books[i].BName}' by {Books[i].BAuthor}? (yes/no)");
                            string confirmation = Console.ReadLine().Trim().ToLower();

                            if (confirmation == "yes")
                            {
                                // Update the book's borrowed copies
                                Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies + 1, Books[i].price, Books[i].category, Books[i].borrowperiod);

                                // Add to borrows list
                                DateTime dateBorrow = DateTime.Now.Date;
                                DateTime returnDate = dateBorrow.AddDays(Books[i].borrowperiod).Date;

                                borrows.Add((userId, enterId, dateBorrow, returnDate, "N/A", "N/A", false));

                                Console.WriteLine("Book borrowed successfully!");

                                // Suggest other books by the same author
                                Console.WriteLine("\n **** Other books you can borrow by the same author if you want: ****");
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
                                Console.WriteLine("Book borrowing canceled.");
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

                ConsoleColor nnm = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t\t U ARE USER : " + userId);
                Console.ForegroundColor = nnm;

                // Header
                ConsoleColor uuu = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n                         ***** BOOKS AVAILABLE TO RETURN ***** ");
                Console.ForegroundColor = uuu;
                Console.WriteLine(" ");

                ConsoleColor ss = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Date Borrow".PadRight(DateBorrowWidth)}{"Date Return".PadRight(DateReturnWidth)}{"Is Returned".PadRight(IsReturnWidth)}");
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
                Console.ForegroundColor = ss;

                try
                {
                    // Prompt user to enter book ID to return
                    Console.WriteLine("Enter the book ID you want to return:");
                    int id;
                    if (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        return; // Exit if the input is not a valid number
                    }

                    bool flag = false;

                    // Loop through the list of books to find the one being returned
                    for (int i = 0; i < Books.Count; i++)
                    {
                        if (Books[i].ID == id)
                        {
                            // Display confirmation prompt
                            Console.WriteLine($"\nAre you sure you want to return '{Books[i].BName}' by {Books[i].BAuthor}? (yes/no)");
                            string confirmation = Console.ReadLine().Trim().ToLower();

                            if (confirmation == "yes")
                            {
                                // Decrement the borrowed copies since the book is being returned
                                int updatedBorrowedCopies = Books[i].Borrowedcopies - 1;

                                // Update the book's record in the list
                                Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, updatedBorrowedCopies, Books[i].price, Books[i].category, Books[i].borrowperiod);

                                flag = true; // Mark that the book was found

                                // Handle actual return date and rating process
                                bool ratingValid = false;

                                // Loop through the borrows list to update the return information
                                for (int j = 0; j < borrows.Count; j++)
                                {
                                    if (borrows[j].bookid == id && !borrows[j].isreturn)
                                    {
                                        DateTime actualReturnDate = DateTime.Now.Date;

                                        string rateuser = "N/A"; // Default value if no valid rating is provided

                                        // Prompt the user for a valid rating between 1 and 5
                                        while (!ratingValid)
                                        {
                                            Console.WriteLine("Enter the rating of the book (1-5):");
                                            int rate;
                                            if (!int.TryParse(Console.ReadLine(), out rate) || rate < 1 || rate > 5)
                                            {
                                                Console.WriteLine("Invalid rating. Please enter a value between 1 and 5.");
                                            }
                                            else
                                            {
                                                rateuser = rate.ToString();
                                                ratingValid = true; // Exit loop if rating is valid
                                            }
                                        }

                                        // Update the borrow entry with the return date, rating, and isreturn flag
                                        borrows[j] = (borrows[j].userid, id, borrows[j].borrowdate, borrows[j].returndate, actualReturnDate.ToString("yyyy-MM-dd"), rateuser, true);

                                        Console.WriteLine("Successfully returned the book.");
                                        break;
                                    }
                                }

                                break; // Exit the loop after processing the return
                            }
                            else
                            {
                                Console.WriteLine("Book return canceled.");
                            }

                            break; // Exit the loop after confirming or canceling
                        }
                    }

                    if (!flag)
                    {
                        Console.WriteLine("Book ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }



        static void ReturnBookLate()
        {
            Console.Clear();
            bool hasBooksToReturnLate = false;
            DateTime todayDate = DateTime.Now.Date;

            // Check if the user has any late books
            foreach (var borrow in borrows)
            {
                if (borrow.userid == userId && !borrow.isreturn && borrow.returndate < todayDate)
                {
                    hasBooksToReturnLate = true;
                    break;
                }
            }

            if (hasBooksToReturnLate)
            {
                int idWidth = 10;
                int dateBorrowWidth = 25;
                int dateReturnWidth = 25;
                int isReturnWidth = 25;
                int daysLateWidth = 10;

                Console.WriteLine("U ARE USER ID : " + userId);
                // Header for the table of late books
                Console.WriteLine("\n\n                         ***** BOOKS AVAILABLE THAT YOU ARE LATE IN RETURNING ***** ");
                Console.WriteLine(" ");
                Console.WriteLine($"{"Book ID".PadRight(idWidth)}{"Date Borrow".PadRight(dateBorrowWidth)}{"Date Return".PadRight(dateReturnWidth)}{"Is Returned".PadRight(isReturnWidth)}{"Days Late".PadRight(daysLateWidth)}");
                Console.WriteLine(new string('-', idWidth + dateBorrowWidth + dateReturnWidth + isReturnWidth + daysLateWidth));

                // Display all late books for this user
                foreach (var borrow in borrows)
                {
                    if (borrow.userid == userId && !borrow.isreturn && borrow.returndate < todayDate)
                    {
                        int daysLate = (todayDate - borrow.returndate).Days;
                        Console.WriteLine($"{borrow.bookid.ToString().PadRight(idWidth)}" +
                                          $"{borrow.borrowdate.ToString("yyyy-MM-dd").PadRight(dateBorrowWidth)}" +
                                          $"{borrow.returndate.ToString("yyyy-MM-dd").PadRight(dateReturnWidth)}" +
                                          $"{borrow.isreturn.ToString().PadRight(isReturnWidth)}" +
                                          $"{daysLate.ToString().PadRight(daysLateWidth)} days");
                    }
                }

                try
                {
                    // Ask user for the book ID to return
                    Console.WriteLine("Enter the book ID you want to return:");
                    int bookId;
                    if (!int.TryParse(Console.ReadLine(), out bookId))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        return; // Exit if the input is not a valid number
                    }

                    bool bookFound = false;

                    // Find the book by ID in the Books list
                    for (int i = 0; i < Books.Count; i++)
                    {
                        if (Books[i].ID == bookId)
                        {
                            // Update the book's borrowed copies
                            Books[i] = (Books[i].ID, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].Borrowedcopies - 1, Books[i].price, Books[i].category, Books[i].borrowperiod);
                            bookFound = true;
                            break;
                        }
                    }

                    if (!bookFound)
                    {
                        Console.WriteLine("Book ID not found.");
                        return; // Exit if the book is not found
                    }

                    bool ratingValid = false;

                    // Loop through borrows to update the return info
                    for (int i = 0; i < borrows.Count; i++)
                    {
                        if (borrows[i].bookid == bookId && !borrows[i].isreturn)
                        {
                            DateTime actualReturnDate = DateTime.Now.Date;
                            string rateUser = "N/A"; // Default value if no rating is given

                            // Prompt user for rating, ensure it's valid
                            while (!ratingValid)
                            {
                                Console.WriteLine("Enter the rating of the book (1-5):");
                                int rate;
                                if (!int.TryParse(Console.ReadLine(), out rate) || rate < 1 || rate > 5)
                                {
                                    Console.WriteLine("Invalid rating. Please enter a value between 1 and 5.");
                                }
                                else
                                {
                                    rateUser = rate.ToString();
                                    ratingValid = true; // Exit loop if rating is valid
                                }
                            }

                            // Update the borrow record
                            borrows[i] = (borrows[i].userid, bookId, borrows[i].borrowdate, borrows[i].returndate, actualReturnDate.ToString("yyyy-MM-dd"), rateUser, true);

                            Console.WriteLine("Successfully returned the book.");
                            return; // Exit after successful return
                        }
                    }

                    // If the borrow record wasn't found
                    Console.WriteLine("Error: No matching borrow record found for this book.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input format error: {ex.Message}. Please enter valid numeric values.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
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

            ConsoleColor bbb = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"User ID: {user.Aid}");
            Console.WriteLine($"Name: {user.username}");
            Console.WriteLine($"Email: {user.email}");
            Console.ForegroundColor = bbb;

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
                ConsoleColor vvv = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
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
            Console.ForegroundColor = bbb;
            Console.WriteLine("\nBooks Previously Borrowed and Returned:");
            if (returnedBooks.Count > 0)
            {
                ConsoleColor tt = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
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
                Console.ForegroundColor = tt;
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
            bool continueEditing = true;

            while (continueEditing)
            {
                Console.WriteLine();
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\t\t\t\t\t\t\t\t 1. Edit name of book ");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t2. Edit author of book ");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t3. Edit quantity of book ");
                Console.WriteLine();
                Console.WriteLine("\t\t\t\t\t\t\t\t4. Save and exit ");
                Console.WriteLine();
                Console.Write("\t\t\t\t\t\t\t\tWhat do you want to edit? ");

                string input = Console.ReadLine();
                Console.ForegroundColor = originalColor;

                try
                {
                    int choice = int.Parse(input);

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
                            continueEditing = false;
                            break;

                        default:
                            Console.WriteLine("Invalid number. Please choose between 1 and 4.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is too large. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
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
            int id;
            Console.WriteLine("Enter the ID of the book you want to edit:");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID.");
                return;  // Exit if the input is not a valid number
            }

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

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Invalid input. Author name cannot be empty.");
                        }
                        else 
                        {
                            Console.WriteLine($"You entered: {name}");
                            break;
                        }

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
            int id;
            Console.WriteLine("Enter id book you want :");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID.");
                return; // Exit if the input is not a valid number
            }

            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {

                    Console.WriteLine(" enter the Author  you want  to update it : ");
                    string author = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(author))
                    {
                        Console.WriteLine("Invalid input. Author name cannot be empty.");
                    }
                    else
                    {
                        Console.WriteLine($"You entered: {author}");
                    }

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
            int id;
            Console.WriteLine(" ");
            Console.WriteLine("Enter id book you want :");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID.");
                return; // Exit if the input is not a valid number
            }

            bool flag = false;
            int copies;
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == id)
                {
                  
                    Console.WriteLine(" enter the Quantity  you want  to update it : ");
                    if (!int.TryParse(Console.ReadLine(), out copies))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid book ID.");
                        return; // Exit if the input is not a valid number
                    }

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
            Console.WriteLine("\n\n\n\n\n\t\t\t\t\t\t " + dr + "\n\n\n\n");
            // Console.WriteLine("\n\n\t\t\t\t\t\t "+ dr +"\n\n");
            Console.ForegroundColor = originalColor;
        }

        static void Drowinguser()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            string dr = @"


           












                                   '\./'                       _                                 _   _                   '\./'
                                            __      __   ___  | |   ___    ___    _ __ ___      | | | |  ___    ___  _ __ 
                         '\./'              \ \ /\ / /  / _ \ | |  / __|  / _ \  | '_ ` _ \     | | | | / __|  / _ \  '__|
                                             \ V  V /  |  __/ | | | (__  | (_) | | | | | | |    | |_| | \__ \ |  __/  |      '\./'
                                  '\./'       \_/\_/    \___| |_|  \___|  \___/  |_| |_| |_|     \___/  |___/  \___| _|                  '\./'

";
            Console.WriteLine("\t" + dr);
            Console.ForegroundColor = originalColor;
        }
        static void Drowingadmin()
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;
            string dr = @" 
















                                                          _                             _       _           _       
                                    '\./'   __      _____| | ___ ___  _ __ ___         / \   __| |_ __ ___ (_)_ __     '\./' 
                                            \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \       / _ \ / _` | '_ ` _ \| | '_ \ 
                       '\./'                 \ V  V /  __/ | (_| (_) | | | | | |     / ___ \ (_| | | | | | | | | | |                '\./' 
                                  '\./'       \_/\_/ \___|_|\___\___/|_| |_| |_|    /_/   \_\__,_|_| |_| |_|_|_| |_|     '\./' 


";
            Console.WriteLine("\t" + dr);
            Console.ForegroundColor = originalColor;
        }
        static void drwingeyeclose()
        {

            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.WriteLine("\t\t\t" + eye);
            Console.ForegroundColor = originalColor;
        }

        static void drwingeyeopen()
        {
            // Save the current console color
            ConsoleColor originalColor = Console.ForegroundColor;

            // Set the desired color
            Console.ForegroundColor = ConsoleColor.Green;

            // Define the ASCII art
            string eye = @"
                                                                --.-- 
                                                                          _.-'''''-._
                                                     --.--              .'  _     _  '.       --.-- 
                                 --.--                                 /   (o)   (o)   \                   --.--
                                                                      |                 |
                                          --.--                       |  \           /  |
                                                               --.--   \  '.       .'  /      --.--                     --.--
                                                      --.--             '.  `'---'`  .'
                                                                          '-._____.-'                    --.-- 
                                                                                         
                                                                          WokeUp Robot              

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
    

