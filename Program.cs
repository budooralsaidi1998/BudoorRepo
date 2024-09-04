using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int Qunatity)> Books = new List<(string BName, string BAuthor, int ID, int Qunatity)>();
        static string filePath = "C:\\projects\\book.txt";

        //Test Check Out
        static void Main(string[] args)
        {// downloaded form ahmed device 
            LoadBooksFromFile();
            Console.WriteLine("enter the number of the option: ");
            Console.WriteLine("1. i am admin ");
            Console.WriteLine("2. i am user ");
            Console.WriteLine("3. Save and Exit");
            int num = int.Parse(Console.ReadLine());
            bool ExitFlag = false;
            do
            {
                switch (num)
                {

                    case 1:
                        AdminMenu();
                        break;

                    case 2:
                        UserMenu();
                        break;
                    case 3:
                        SaveBooksToFile();
                        ExitFlag = true;
                        break;
                    default:

                        break;

                }
            }

            while (ExitFlag != true);
        }
        static void AdminMenu()
        {
            bool ExitFlag = false;
            Console.WriteLine("what your name : ");
            string name = Console.ReadLine();
            Console.WriteLine("\n hello " + name + " " + "Admin  ^_^ ");
            do
            {
                Console.WriteLine("\n " + name + "Admin  ^_^ ");

                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Add New Book");
                Console.WriteLine("\n B- Display All Books");
                Console.WriteLine("\n C- Search for Book by Name");
                Console.WriteLine("\n D- Save and Exit");

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
                     
                        ExitFlag = false;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != false);
        }
        static void UserMenu()
        {
            bool ExitFlag = false;

            do
            {
                Console.WriteLine("I am user ....");
                Console.WriteLine("\n Enter the char of operation you need :");
                //Console.WriteLine("\n A-Search for Book by Name");
                Console.WriteLine("\n A- Borrow the book ");
                Console.WriteLine("\n B- return the book ");
                Console.WriteLine("\n C- Save and Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "A":
                        BarrowBooks();
                        break;

                    case "B":
                        //ReturnBook();
                        break;

                    case "C":
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

            } while (ExitFlag != false);
        }
        static void AddnNewBook()
        {
            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Book ID");
            int ID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter quantity");
            int qun = int.Parse(Console.ReadLine());

            Books.Add((name, author, ID, qun));
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
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].Qunatity);
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
                            if (parts.Length == 4)
                            {
                                Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3])));
                            }
                        }
                    }
                    Console.WriteLine("Books loaded from file successfully.");
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
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.Qunatity}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
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
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, NewQunatityAfterTakeIt); 

                    }
                    else 
                    {
                        Console.WriteLine("IS NOT AVAILABIL ");
                    }
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); }

        }
        //static void ReturnBook()
        //{
        //    Console.WriteLine("Enter the book name you want to return : ");
        //    string name = Console.ReadLine();
        //    bool flag = false;

        //    for (int i = 0; i < Books.Count; i++)
        //    {
        //        if (Books[i].BName == name)
        //        {
        //            Console.WriteLine("Book Quantity is : " + Books[i].Qunatity);

        //            if (Books[i].Qunatity != 0)
        //            {
        //                Console.WriteLine("How many quantity you want : ");
        //                int quantity = int.Parse(Console.ReadLine());
        //                int NewQunatityAfterTakeIt = Books[i].Qunatity - quantity;
        //                Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, NewQunatityAfterTakeIt);

        //            }
        //            else
        //            {
        //                Console.WriteLine("IS NOT AVAILABIL ");
        //            }
        //            flag = true;
        //            break;
        //        }
        //    }
        //}


    }
}