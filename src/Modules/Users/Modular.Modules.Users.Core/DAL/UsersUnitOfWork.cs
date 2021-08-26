using Modular.Infrastructure.Postgres;

namespace Modular.Modules.Users.Core.DAL
{
    internal class UsersUnitOfWork : PostgresUnitOfWork<UsersDbContext>
    {
        public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
        {
        }
    }
}