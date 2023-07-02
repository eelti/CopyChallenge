using CopyChallenge.Models;

namespace CopyChallenge
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            // Look for any students.
            if (context.SampleOrders.Any())
            {
                return;   // DB has been seeded
            }

            context.SampleOrders.AddRange(SampleData.Orders);
            context.SaveChanges();

            var players = new List<Player>(){
                new Player() {
                    FirstName = "Eric",
                    LastName = "Leduc",
                    Week1 = 1,
                    Week2 = 1,
                    Week3 = 3
                },
                new Player()
                {
                    FirstName = "Fidelis",
                    LastName = " Ramanantsalana"
                },
                new Player() {
                    FirstName = "Mahay",
                    LastName = "Finaritra Rakotonoely"
                },
                new Player()
                {
                    FirstName = "Antsatia",
                    LastName = "Rakotoarijaona"
                },
                new Player()
                {
                    FirstName = "Marc",
                    LastName = "Andrianmahaleo"
                }
            };
            context.Players.AddRange(players);
            context.SaveChanges();
        }
    }
}