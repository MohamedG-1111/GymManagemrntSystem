using GymManagementSystemDAL.Data.Repository.Implementation;
using GymManagementSystemDAL.Data.Repository.Interface;
using GymManagementSystemDAL.Model;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;

    public ISessionRepository SessionRepository { get; }

    public Dictionary<Type, Object> Repos = new Dictionary<Type, Object>();

    public UnitOfWork(AppDbContext Context, ISessionRepository sessionRepository)
    {
        context = Context;
        SessionRepository = sessionRepository; 
    }

    public IGenericRepository<T> GetGenericRepository<T>() where T : BaseEntity, new()
    {
        var EntityType = typeof(T);
        if (Repos.TryGetValue(EntityType, out var value))
            return (GenericRepository<T>)value;

        var newRepo = new GenericRepository<T>(context);
        Repos.Add(EntityType, newRepo);
        return newRepo;
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }
}
