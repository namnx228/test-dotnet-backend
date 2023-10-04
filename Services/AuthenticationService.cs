using AuthenticationServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthenticationServer.Services;

public class AuthenticationService {
    private readonly IMongoCollection<User> _userCollection;

    public AuthenticationService(
        IOptions<AuthenticationDatabaseSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(
            DatabaseSettings.Value.CollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string username, string password) =>
        await _userCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
}