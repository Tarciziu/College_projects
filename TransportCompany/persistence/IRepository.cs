using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public class RepositoryException : ApplicationException
    {
        public RepositoryException() { }
        public RepositoryException(String mess) : base(mess) { }
        public RepositoryException(String mess, Exception e) : base(mess, e) { }
    }
}

namespace persistence
{
    public interface IRepository<Entity>
    {
        void save(Entity entity);
        void delete(Entity entity);
        void update(Entity entity);
        IEnumerable<Entity> findAll();
        Entity findOne(long id);
    }
}
