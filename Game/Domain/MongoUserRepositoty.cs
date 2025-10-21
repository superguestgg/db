using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> userCollection;
        public const string CollectionName = "users";

        public MongoUserRepository(IMongoDatabase database)
        {
            userCollection = database.GetCollection<UserEntity>(CollectionName);
        }

        public UserEntity Insert(UserEntity user)
        {
            //TODO: Ищи в документации InsertXXX.
            throw new NotImplementedException();
        }

        public UserEntity FindById(Guid id)
        {
            var filter = new BsonDocument();
            using var cursor = userCollection.Find(filter).ToCursor();
            return cursor.FirstOrDefault();
        }

        public UserEntity GetOrCreateByLogin(string login)
        {
            //TODO: Это Find или Insert
            throw new NotImplementedException();
        }

        public void Update(UserEntity user)
        {
            var filter = new BsonDocument();
            userCollection.ReplaceOne(filter, user);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        // Для вывода списка всех пользователей (упорядоченных по логину)
        // страницы нумеруются с единицы
        public PageList<UserEntity> GetPage(int pageNumber, int pageSize)
        {
            //TODO: Тебе понадобятся SortBy, Skip и Limit
            throw new NotImplementedException();
        }

        // Не нужно реализовывать этот метод
        public void UpdateOrInsert(UserEntity user, out bool isInserted)
        {
            throw new NotImplementedException();
        }
    }
}