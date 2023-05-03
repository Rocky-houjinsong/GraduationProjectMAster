using ToRead.Services;

namespace ToRead.UnitTest.Helpers;

public class FavoriteStorageHelper {
    public static void RemoveDatabaseFile() =>
        File.Delete(FavoriteStorage.PoetryDbPath);
}