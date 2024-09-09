using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml.Linq;

namespace BasicLibrary
{
    internal class Program
    {
        //global structure 
        static int userId = -1;
        static List<(string BName, string BAuthor, int ID, int Qunatity, int borrow)> Books = new List<(string BName, string BAuthor, int ID, int Qunatity, int borrow)>();
        static List<(string email, int password)> adminRegistration = new List<(string email, int password)>();
        static List<(int Aid, string email, int password)> userReistrtion = new List<(int Aid, string email, int password)>();
        static List<(int userid, int bookid, int returnbook)> borrows = new List<(int userid, int bookid, int returnbook)>();
        static List<(string username, string password)> master = new List<(string username, string password)>();

        //files
        //******************************************************************************************************************************************
        static string filePath = "C:\\projects\\files\\book.txt";
        static string fileAdminRegistration = "C:\\projects\\files\\AdminRegistarion.txt";
        static string fileUserRegistration = "C:\\projects\\files\\UserRegastratin.txt";
        static string fileBorrowBook = "C:\\projects\\files\\BorrowBooks.txt";
        static string filemaster = "C:\\projects\\files\\master.txt";
        //******************************************************************************************************************************************

        //Test Check Out

        //******************************************************************************************************************************************
        static void Main(string[] args)
        {
            // loadeded file  
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
                        AdminMenu();
                        break;

                    case 2:
                        UserMenu();
                        break;
                    case 3:
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

                Console.WriteLine("\n A- Admin registration ");
                Console.WriteLine("\n B- User registrsation ");
                Console.WriteLine("\n C- Save and Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "A":
                        AdminRegistration();
                        SaveAdminRegToFile();
                        break;

                    case "B":
                        UserRegistration();
                        SaveUserRegToFile();
                        break;

                    case "C":
                        ExitFlag = true;

                        break;
                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);

        }

        static void AdminRegistration()
        {
            Console.WriteLine("enter the email :");
            string email = Console.ReadLine();

            Console.WriteLine(" enter the passowrd : ");
            int password = int.Parse(Console.ReadLine());

            adminRegistration.Add((email, password));

            Console.WriteLine("successfully added ");
        }

        static void UserRegistration()
        {
            int userid = userReistrtion.Count + 0;

            Console.WriteLine("enter the email :");
            string email = Console.ReadLine();

            Console.WriteLine(" enter the passowrd : ");
            int password = int.Parse(Console.ReadLine());

            userReistrtion.Add((userid, email, password));

            Console.WriteLine("successfully added ");
        }



        




        //admin menu with the service admin
        //*******************************************************************************************************************************************


        static void AdminMenu()
        {
            bool ExitFlag = false;
            Console.WriteLine("***************** Login ********************");
            Console.WriteLine(" ");

            Console.WriteLine(" enter your email : ");
            string email = Console.ReadLine();

            Console.WriteLine(" enter the password : ");
            int password = int.Parse(Console.ReadLine());

            for (int i = 0; i < adminRegistration.Count; i++)
            {
                if (adminRegistration[i].email == email && adminRegistration[i].password == password)
                {
                    do
                    {


                        Console.WriteLine("\n Enter the char of operation you need :");
                        Console.WriteLine("\n A- Add New Book");
                        Console.WriteLine("\n B- Display All Books");
                        Console.WriteLine("\n C- Search for Book by Name");
                        Console.WriteLine("\n D- Edit the book ");
                        Console.WriteLine("\n E- Remove the book ");
                        Console.WriteLine("\n F- Reporting the data ");
                        Console.WriteLine("\n G- Save and Exit");

                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "A":
                                AddnNewBook();
                                break;

                            case "B":
                                ViewAllBooks();
                                break;

                            case "C":
                                SearchForBook();
                                break;

                            case "D":
                                EditBookMenu();
                                break;

                            case "E":
                                RemoveBook();
                                break;

                            case "F":
                                Reporting();
                                break;

                            case "G":
                                SaveBooksToFile();
                                ExitFlag = true;
                                break;

                            default:
                                Console.WriteLine("Sorry your choice was wrong");
                                break;

                        }

                        Console.WriteLine("press any key to continue");

                        string cont = Console.ReadLine();

                        Console.Clear();

                    } while (ExitFlag != true);
                }
                Console.WriteLine(" Invalid login *** ");

            }
 
                ExitFlag = false;
            }

            static void AddnNewBook()
            {


                int id = Books.Count + 0;

                Console.WriteLine("Enter Book Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Book Author");
                string author = Console.ReadLine();

                //Console.WriteLine("Enter Book ID");
                //int ID = int.Parse(Console.ReadLine());


                Console.WriteLine("Enter quantity");
                int qun = int.Parse(Console.ReadLine());





                Books.Add((name, author, id, qun, 0));
                Console.WriteLine("Book Added Succefully");

            }

            static void ViewAllBooks()
            {
                StringBuilder sb = new StringBuilder();

                int BookNumber = 0;

                for (int i = 0; i < Books.Count; i++)
                {
                    BookNumber = i + 1;
                    sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Books[i].Qunatity);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" borrow : ").Append(Books[i].borrow);
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

                        Books.Remove((Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Qunatity, Books[i].borrow));

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
            List<int> quantity  = new List<int>();

            for (int i = 0; i < Books.Count; i++)
            {
                var( BName,  BAuthor,  ID,  Qunatity, borrow) = Books[i];
                nameBook.Add(BName);
                author.Add(BAuthor);
                borrowBook.Add(borrow);
                quantity.Add(Qunatity);
                
            }

            //total book is available 
            int totalBook = nameBook.Count();
            Console.WriteLine("the total of book is available = " + totalBook);
            Console.WriteLine(" ");

            //total book is borrowe it 
            int borrowtotal = borrowBook.Sum();
            Console.WriteLine("the total of book is borrow it = " + borrowtotal );

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
                bool ExitFlag = false;
                Console.WriteLine("***************** Login ********************");
                Console.WriteLine(" ");

                Console.WriteLine(" enter your email : ");
                string email = Console.ReadLine();

                Console.WriteLine(" enter the password : ");
                int password = int.Parse(Console.ReadLine());



                for (int i = 0; i < userReistrtion.Count; i++)
                {

                    if (userReistrtion[i].email == email && userReistrtion[i].password == password)
                    {

                        userId = userReistrtion[i].Aid;
                        Console.WriteLine("user id is :" + userId);

                        do
                        {

                            Console.WriteLine("\n Enter the char of operation you need :");
                            //Console.WriteLine("\n A-Search for Book by Name");
                            Console.WriteLine("\n A- Borrow the book ");
                            Console.WriteLine("\n B- return the book ");
                            Console.WriteLine("\n C- logout ");

                            string choice = Console.ReadLine();

                            switch (choice)
                            {

                                case "A":


                                Console.WriteLine("user id is :" + userId);
                                ViewAllBooksUser();
                                BarrowBooks();
                                    break;

                                case "B":
                                Console.WriteLine("user id is :" + userId);
                                ReturnBook();
                                    break;

                                case "C":
                                    SaveBooksToFile();
                                    SaveborrowToFile();
                                    userId = -1;
                                    ExitFlag = true;

                                    break;

                               default:
                                    Console.WriteLine("Sorry your choice was wrong");
                                    break;



                            }

                            Console.WriteLine("press any key to continue");
                            string cont = Console.ReadLine();

                            Console.Clear();

                        } while (ExitFlag != true);
                    }
                }

                
                Console.WriteLine("Invalid login ");

                ExitFlag = false;

            }
        static void ViewAllBooksUser()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Books[i].Qunatity);
                sb.AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }
           static void BarrowBooks()
            {

                Console.WriteLine("Enter the book name you want");
                string name = Console.ReadLine();

            
                bool flag = false;

                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName == name)
                    {
                        Console.WriteLine("Book Quantity is : " + Books[i].Qunatity);

                        if (Books[i].Qunatity != 0)
                        {
                            Console.WriteLine("How many quantity you want : ");
                            int quantity = int.Parse(Console.ReadLine());
                            int NewQunatityAfterTakeIt = Books[i].Qunatity - quantity;
                            int borrow = Books[i].borrow + 1;
                            Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, NewQunatityAfterTakeIt, borrow);
                        // recomand(Books[i].BAuthor);
                          


                        }
                        else
                        {

                            Console.WriteLine("IS NOT AVAILABIL ");
                        }
                        flag = true;
                        break;
                    }
                }
                //seggestion ... 

            for (int i = 0; i < Books.Count; i++)
            {
    
            }

            if (flag != true)
                { Console.WriteLine("book not found"); }

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
                        if (Books[i].Qunatity >= 0)  //Console.WriteLine("Book Quantity  : " + Books[i].Qunatity);

                        {
                            Console.WriteLine("How many quantity you want to return: ");
                            int quantity = int.Parse(Console.ReadLine());

                            int NewQunatityAfterTakeIt = Books[i].Qunatity + quantity;

                            int borrow = Books[i].borrow - 1;

                            Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, NewQunatityAfterTakeIt, borrow);

                            Console.WriteLine("successfuly added ");
                        int bookid = Books[i].ID;
                        int returnss = quantity;
                        borrows.Add((userId, bookid, returnss));
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

                        Books[i] = (name, Books[i].BAuthor, Books[i].ID, Books[i].Qunatity, Books[i].borrow);
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

                        Books[i] = (Books[i].BName, author, Books[i].ID, Books[i].Qunatity, Books[i].borrow);
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
                        int quantity = int.Parse(Console.ReadLine());

                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, quantity, Books[i].borrow);
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
                                var parts = line.Split('|');
                                if (parts.Length == 5)
                                {
                                    Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4])));
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
                                var parts = line.Split('|');
                                if (parts.Length == 2)
                                {
                                    adminRegistration.Add((parts[0], int.Parse(parts[1])));
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
                                var parts = line.Split('|');
                                if (parts.Length == 3)
                                {
                                    userReistrtion.Add((int.Parse(parts[0]), parts[1], int.Parse(parts[2])));
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
                            writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.Qunatity}|{book.borrow}");
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
                    HashSet<(string email, int password)> uniqe =
                        new HashSet<(string, int)>(adminRegistration);

                    using (StreamWriter writer = new StreamWriter(fileAdminRegistration))
                    {
                        foreach (var admin in uniqe)
                        {
                            writer.WriteLine($"{admin.email}|{admin.password}");
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
                    HashSet<(int Aid, string email, int password)> uniqe =
                         new HashSet<(int, string, int)>(userReistrtion);

                    using (StreamWriter writer = new StreamWriter(fileUserRegistration))
                    {
                        foreach (var user in uniqe)
                        {
                            writer.WriteLine($"{user.Aid}|{user.email}|{user.password}");

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
                HashSet<(int userid, int bookid,int returnbook)> uniqe =
                    new HashSet<(int, int,int)>(borrows);

                using (StreamWriter writer = new StreamWriter(fileBorrowBook))
                {
                    foreach (var borr in uniqe)
                    {
                        writer.WriteLine($"{borr.userid}|{borr.bookid}|{borr.returnbook}");
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
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                borrows.Add(( int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
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
        // **********************************************************************************************************************************************








    }
    }
