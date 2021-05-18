using CosmosDb.Domain;
using CosmosDB.EF.Context;
using CosmosDB.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB.EF
{
    class Program
    {
        private static BookRepository _bookRepository;

        static async Task Main(string[] args)
        {
            Initialize();
            ShowMenu();            
            await Run();
            End();
        }
     
        private static void Initialize()
        {
            _bookRepository = new BookRepository();
        }

        private static void ShowMenu()
        {

            Console.WriteLine("Starting CosmosDb.EF...");
            Console.WriteLine("-----------");
            Console.WriteLine("Type the command: ");
            Console.WriteLine("1 (Add)");
            Console.WriteLine("2 (Get)");
            Console.WriteLine("3 (Update)");
            Console.WriteLine("4 (Remove)");
            Console.WriteLine("-----------");
        }        

        private static async Task Run()
        {            
            var cmd = Console.ReadLine();

            switch (cmd)
            {
                case "1":
                    await Add();
                    break;

                case "2":
                    var book = Get();
                    Console.WriteLine(book.ToString());
                    break;

                case "3":
                    await Update();
                    break;

                case "4":
                    await Remove();
                    break;

                default:
                    break;
            }
        }

        private static async Task Add()
        {
            Console.WriteLine("Adding book...");
            var book = GetBookInfoFromUI();
            await _bookRepository.Add(book);
            Console.WriteLine($"Book {book.Title} added!");
        }

        private static Book GetBookInfoFromUI()
        {            
            Console.WriteLine("ISBN: ");
            var isbn = Console.ReadLine();

            Console.WriteLine("Edition: ");
            var edition = int.Parse(Console.ReadLine());

            Console.WriteLine("Title: ");
            var title = Console.ReadLine();

            Console.WriteLine("Publishing Company: ");
            var publishCompany = Console.ReadLine();

            Console.WriteLine("Authors (split them using ';'): ");
            var authors = Console.ReadLine().Split(';');
            
            var book = new Book
            {
                Isbn = isbn,
                Edition = edition,
                Title = title,
                PublishingCompany = new PublishingCompany
                {
                    Name = publishCompany
                },
                Authors = authors.Select(array => new Author { Name = array }).ToList()
            };

            return book;
        }

        private static Book Get()
        {
            Console.WriteLine("Getting book...");
            var isbn = GetIsbnFromUI();
            var book = _bookRepository.GetByIsbn(isbn);            
            return book;
        }

        private static string GetIsbnFromUI()
        {
            Console.WriteLine("Enter the ISBN: ");
            var isbn = Console.ReadLine();
            return isbn;
        }

        private static async Task Update()
        {
            Console.WriteLine("Updating book...");
            var book = Get();
            var newTitle = GetNewTitleFromUI();
            book.Title = newTitle;

            await _bookRepository.Update(book);

            Console.WriteLine($"Book {book.Title} updated!");
        }

        private static string GetNewTitleFromUI()
        {
            Console.WriteLine("Enter the new Title: ");
            var title = Console.ReadLine();
            return title;
        }

        private static async Task Remove()
        {
            var isbn = GetIsbnFromUI();
            await _bookRepository.Remove(new Book { Isbn = isbn });
            Console.WriteLine($"Book ISBN {isbn} deleted!");
        }

        private static void End()
        {
            Console.WriteLine("Enter any key to exit...");
            Console.ReadLine();
        }
    }
}
