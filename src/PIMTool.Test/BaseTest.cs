using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PIMTool.Database;
using PIMTool.Extensions;

namespace PIMTool.Test
{
    public abstract class BaseTest
    {
        protected PIMDBContext Context { get; private set; } = null!;
        protected IServiceProvider ServiceProvider { get; private set; } = null!;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PIMDBContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.Register();
            ServiceProvider = services.BuildServiceProvider();
            Context = ServiceProvider.GetRequiredService<PIMDBContext>();
        }

        [TearDown]
        public void TearDown()
        {
            Context.Dispose();
        }
    }
}