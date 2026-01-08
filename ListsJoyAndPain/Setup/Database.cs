using System.Text.Json;
using ListsJoyAndPain.Models;

namespace ListsJoyAndPain.Setup;

public static class Database
{
    public static void SaveAuthors(List<Author> authors)
    {
        var jsonString = JsonSerializer.Serialize(authors);
        File.WriteAllText(Constants.AuthorPath, jsonString);
    }
    
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
        
        public static void SaveBookshop(string path, Models.Bookshop bookshop)
        {
            var jsonString = JsonSerializer.Serialize(bookshop);
            File.WriteAllText(path, jsonString);
        }

        public static List<Author> GetAuthors()
        {
            var authorsAsJson = File.ReadAllText(Constants.AuthorPath);
            var authors = JsonSerializer.Deserialize<List<Author>>(authorsAsJson);
            return authors ?? [];
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

        public static Models.Bookshop GetBookshop(string path)
        {
            var bookshopAsJson = File.ReadAllText(path);
            var bookshop = JsonSerializer.Deserialize<Models.Bookshop>(bookshopAsJson);
            return bookshop ?? new Models.Bookshop();
        }
}