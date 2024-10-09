using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;

namespace HangfireDemoApi.UnitTests;

public static class DbSetMockFactory
{
    public static DbSet<T> CreateDbSetMock<T>() where T : class
    {
        return new List<T>().AsQueryable().BuildMockDbSet();
    }

    public static DbSet<T> CreateDbSetMock<T>(T entity) where T : class
    {
        var list = new List<T> { entity };
        return list.AsQueryable().BuildMockDbSet();
    }

    public static DbSet<T> CreateDbSetMock<T>(IEnumerable<T> entities)
        where T : class
    {
        return entities.AsQueryable().BuildMockDbSet();
    }
}
