using System;
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
            userCollection.InsertOne(user);
            
            return FindById(user.Id);
        }

        public UserEntity FindById(Guid id)
        {
            //TODO: Ищи в документации FindXXX
            throw new NotImplementedException();
        }

        public UserEntity GetOrCreateByLogin(string login)
        {
            return userCollection
                       .Find(item => item.Login == login)
                       .FirstOrDefault() 
                   ?? Insert(new UserEntity() { Login = login });
        }

        public void Update(UserEntity user)
        {
            //TODO: Ищи в документации ReplaceXXX
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            userCollection.DeleteOne(item => item.Id == id);
        }

        // Для вывода списка всех пользователей (упорядоченных по логину)
        // страницы нумеруются с единицы
        public PageList<UserEntity> GetPage(int pageNumber, int pageSize)
        {
            var count = userCollection.Find(item => true)
                .CountDocuments();
            var usersCursor = userCollection.FindSync(entity => true,
                options: new FindOptions<UserEntity, UserEntity>()
                {
                    Sort = new SortDefinitionBuilder<UserEntity>()
                        .Ascending(x => x.Login),
                    Skip = (pageNumber - 1) * pageSize ,
                    Limit = pageSize,
                });
            return new PageList<UserEntity>(usersCursor.ToList(),  count, pageNumber,  pageSize);
        }

        // Не нужно реализовывать этот метод
        public void UpdateOrInsert(UserEntity user, out bool isInserted)
        {
            throw new NotImplementedException();
        }
    }
}