using System.Text.Json;
using Comparing_lists.Models;

namespace Comparing_lists.Setup;

public static class Database
{
        public static void SaveAllBooks(List<Book> books)
        {
            var jsonString = JsonSerializer.Serialize(books);
            File.WriteAllText(Constants.BookPath, jsonString);
        }
        
        public static void SaveCollections(List<Collection> collections)
        {
            var jsonString = JsonSerializer.Serialize(collections);
            File.WriteAllText(Constants.CollectionPath, jsonString);
        }
        
        public static void SaveBookshop(string path, Bookshop bookshop)
        {
            var jsonString = JsonSerializer.Serialize(bookshop);
            File.WriteAllText(path, jsonString);
        }
        
        public static List<Book> GetBooks()
        {
            var booksAsJson = File.ReadAllText(Constants.BookPath);
            var books = JsonSerializer.Deserialize<List<Book>>(booksAsJson);
            return books ?? [];
        }
        
        public static List<Collection> GetCollections()
        {
            var collectionsAsJson = File.ReadAllText(Constants.CollectionPath);
            var collections = JsonSerializer.Deserialize<List<Collection>>(collectionsAsJson);
            return collections ?? [];
        }

        public static Bookshop GetBookshop(string path)
        {
            var bookshopAsJson = File.ReadAllText(path);
            var bookshop = JsonSerializer.Deserialize<Bookshop>(bookshopAsJson);
            return bookshop ?? new Bookshop();
        }
}