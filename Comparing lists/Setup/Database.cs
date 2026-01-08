using System.Text.Json;
using Comparing_lists.Models;

namespace Comparing_lists.Setup;

public static class Database
{
        public static void SaveAllBooks(List<Book> books)
        {
            var jsonString = JsonSerializer.Serialize(books);
            File.AppendAllLines(Constants.BookPath, [jsonString]);
        }
        
        public static void SaveCollections(List<Collection> collections)
        {
            var jsonString = JsonSerializer.Serialize(collections);
            File.AppendAllLines(Constants.CollectionPath, [jsonString]);
        }
        
        public static List<Book> GetBooksFromDb()
        {
            var booksAsJson = File.ReadAllLines(Constants.BookPath);
            var books = JsonSerializer.Deserialize<List<Book>>(booksAsJson[0]);
            return books ?? [];
        }
        
        public static List<Collection> GetCollectionsFromDb()
        {
            var collectionsAsJson = File.ReadAllLines(Constants.CollectionPath);
            var collections = JsonSerializer.Deserialize<List<Collection>>(collectionsAsJson[0]);
            return collections ?? [];
        }
}