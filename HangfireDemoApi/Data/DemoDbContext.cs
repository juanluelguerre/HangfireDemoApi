using Microsoft.EntityFrameworkCore;

namespace HangfireDemoApi.Data
{
    public class DemoDbContext(DbContextOptions<DemoDbContext> options) : DbContext(options)
    {
    }
}
