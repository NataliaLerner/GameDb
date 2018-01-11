using GameDb.DAL.Abstract;

namespace GameDb.DAL.Base
{
    public class RepositoryBase : IRepository
    {
        public RepositoryBase()
        {
            _connectionString = "Data Source=DESKTOP-VQ1QSGE;Initial Catalog=GameDb;Integrated Security=True";
        }

        protected string _connectionString;
    }
}