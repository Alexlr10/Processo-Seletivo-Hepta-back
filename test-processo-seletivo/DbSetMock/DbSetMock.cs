using Microsoft.EntityFrameworkCore;
using Moq;

namespace test_processo_seletivo.DbSetMock
{
    public static class DbContextMock
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            dbSetMock.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            dbSetMock.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => sourceList.Remove(entity));

            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(m => m.SaveChanges()).Returns(1).Verifiable();

            return dbSetMock.Object;
        }

    }
}
