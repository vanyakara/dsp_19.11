using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsp_19._11
{
    public class Lecturer
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Affiliation { get; set; }
        public string Department { get; set; }
        public Lecturer(string firstName, string secondName, string lastName, string affiliation, string department)
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Affiliation = affiliation;
            Department = department;
        }
        public void Update(string affiliation, string department)
        {
            Affiliation = affiliation;
            Department = department;
        }
        public override string ToString()
        {
            return $"{FirstName} {SecondName} {LastName}, {Affiliation}, {Department}";
        }
    }
    public class Book
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
        public Book(string author, string name, string isbn, string details, string type)
        {
            Author = author;
            Name = name;
            ISBN = isbn;
            Details = details;
            Type = type;
        }
        public void Update(string details, string type)
        {
            Details = details;
            Type = type;
        }
        public override string ToString()
        {
            return $"Книга: {Name}, Автор: {Author}, ISBN: {ISBN}, Тип: {Type}, Детали: {Details}";
        }
    }
    public class Paper
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
        public Paper(string author, string title, string isbn, string details, string type)
        {
            Author = author;
            Title = title;
            ISBN = isbn;
            Details = details;
            Type = type;
        }
        public void Update(string details, string type)
        {
            Details = details;
            Type = type;
        }
        public override string ToString()
        {
            return $"Публикация: {Title}, Автор: {Author}, ISBN: {ISBN}, Тип: {Type}, Детали: {Details}";
        }
    }
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
    public class Facade
    {
        public List<Lecturer> lecturers = new List<Lecturer>();
        public List<Book> books = new List<Book>();
        public List<Paper> papers = new List<Paper>();
        public void AddLecturers(Lecturer lecturer)
        {
            lecturers.Add(lecturer);
        }
        public void UpdateLecturer(string fullName, string affiliation, string department)
        {
            var lecturer = lecturers.FirstOrDefault(I => $"{I.FirstName}{I.SecondName}{I.LastName}" == fullName);
            if (lecturer != null)
            {
                lecturer.Update(affiliation, department);
            }
        }
        public void AddBooks(List<Book> book)
        {
            books.Add(book);
        }
        public void AddPaper(Paper paper)
        {
            papers.Add(paper);
        }
        public bool IsAuthorOfBook(string lecturerFullName, string bookTitle)
        {
            return books.Any(b => b.Author == lecturerFullName && b.Name == bookTitle);
        }
        public bool IsAuthorOfPaper(string lecturerFullName, string paperTitle)
        {
            return papers.Any(p => p.Author.Contains(lecturerFullName) && p.Title == paperTitle);
        }
        public string GetLecturerInfo(string fullName)
        {
            var lecturer = lecturers.FirstOrDefault(I => $"{I.FirstName} {I.SecondName} {I.LastName}" == fullName);
            return lecturer != null ? lecturer.ToString() : "Преподавателят не е намерен.";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var facade = new Facade();
            var lecturer = new Lecturer("Иван", "Иванов", "Иванов", "Доц.", "Компютърни науки");
            facade.AddLecturers(lecturer);
            var book = new List<Book> { ("Иван Иванов Иванов", "Архитектура на процесора", "123-456-789", "Научен учебник", "Учебник") };
            facade.AddBooks(book);
            var paper = new Paper("Иван Иванов Иванов", "Машинно обучение", "987-654-321", "Доклад по AI", "Доклад");
            facade.AddPaper(paper);
            Console.WriteLine(facade.IsAuthorOfBook("Иван Иванов Иванов", "Архитектура на процесора"));
            Console.WriteLine(facade.IsAuthorOfPaper("Иван Иванов Иванов", "Машинно обучение"));
            Console.WriteLine(facade.GetLecturerInfo("Иван Иванов Иванов"));
            Console.ReadLine();
            AuthorsPubAndBook("Иван Иванов Иванов")
        }
        public static void AuthorsPubAndBook(string author, List<Book> book, List<Paper> paper)
        {
            Console.WriteLine($"Публикации и книги на автора: {author}\n");
            var authorPub = paper.Where(p => p.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
            if (authorPub.Any())
            {
                Console.WriteLine("Публикации: ");
                foreach(var Paper in authorPub)
                {
                    Console.WriteLine($"-{Paper.Title}, {Paper.Author}");
                }
            }
            else
            {
                Console.WriteLine("Няма публикации.");
            }
            var authorBook = book.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
            if (authorBook.Any())
            {
                Console.WriteLine("\nКниги:");
                foreach(var books in authorBook)
                {
                    Console.WriteLine($"- {books.Name}, {books.Author}, ISBN: {books.ISBN}");
                }
            }
            else
            {
                Console.WriteLine("\nНяма книги");
            }
        }
    }
}
