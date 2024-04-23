using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Test
{
    public class UnitTestBase
    {
        protected ContosoDbContext ContosoDbContext { get; set; } = null!;


        [TestInitialize]
        public void Initialize() => ContosoDbContext = GetOmniDbContext();

        [TestCleanup]
        public void Cleanup() => ContosoDbContext?.Dispose();

        private static ContosoDbContext GetOmniDbContext()
        {
            var dbConnection = new DbContextOptionsBuilder<ContosoDbContext>()
                .UseSqlite("DataSource=file:memdb1?mode=memory&cache=shared")
                .Options;

            var contosoDbContext = new ContosoDbContext(dbConnection);
            contosoDbContext.Database.EnsureCreated();
            return contosoDbContext;
        }


    }
}
