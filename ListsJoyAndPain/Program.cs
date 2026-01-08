using ListsJoyAndPain.Setup;
using ListsJoyAndPain.Use_cases;

DatabaseInitializer.ResetAuthors();
DatabaseInitializer.ResetBookDatabase();
DatabaseInitializer.ResetBookshops();

UseCase01.WhoHasMoreBooks();