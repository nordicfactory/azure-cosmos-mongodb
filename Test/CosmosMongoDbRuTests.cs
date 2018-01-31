using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace Test
{
    public class CosmosMongoDbRuTests
    {
        private const string CosmosConnectionString = @"mongodb://...";

        private readonly BsonDocumentCommand<BsonDocument> _statsCommand  = new BsonDocumentCommand<BsonDocument>(new BsonDocument{
            {"getLastRequestStatistics", 1}
        });

        [Fact]
        public async Task Cost_Of_Single_Read_Should_Match_Cost_In_Capacity_Planner()
        {
            try
            {
                var dbContext = new DbContext(CosmosConnectionString);
                var usersRepo = new ApplicationUserRepository(dbContext);

                var sampleDocument = new ApplicationUser
                {
                    Id = "5a58dfae5920c066f862c96c",
                    GivenName = "Foo",
                    FamilyName = "Bar",
                    Email = "foo@bar.com",
                    NormalizedEmail = "FOO@BAR.COM",
                    UserName = "foo@bar.com",
                    NormalizedUserName = "FOO@BAR.COM"
                };

                await usersRepo.CreateUserAsync(sampleDocument);

                await usersRepo.FindByIdAsync("5a58dfae5920c066f862c96c");

                var commandResult = await dbContext.GetDatabase().RunCommandAsync(_statsCommand);

                var capacityPlannerCharge = 1.05;
                var actualCharge = commandResult["RequestCharge"].AsDouble;

                await usersRepo.DeleteAsync("5a58dfae5920c066f862c96c");

                Assert.Equal(capacityPlannerCharge, actualCharge);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
