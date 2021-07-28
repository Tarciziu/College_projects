using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using persistence;

namespace persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly ILog log = LogManager.GetLogger("UserDbRepository");
        public UserRepository()
        {
            log.Info("Creating UserDbRepository");
        }

        public void delete(User entity)
        {
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from user where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No user deleted!");
            }
        }

        public IEnumerable<User> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<User> usersR = new List<User>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from user";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long user_id = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        String password = dataR.GetString(2);
                        User user = new User(user_id, username, password);
                        usersR.Add(user);
                    }
                }
            }

            return usersR;
        }

        public User findByUsername(string username)
        {
            log.InfoFormat("Entering findByUsername with value {0}", username);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from user where username=@username";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@username";
                paramId.Value = username;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long user_id = dataR.GetInt32(0);
                        String usrn = dataR.GetString(1);
                        String password = dataR.GetString(2);
                        User user = new User(user_id, usrn, password);
                        log.InfoFormat("Exiting findByUsername with value {0}", user);
                        return user;
                    }
                }
            }
            log.InfoFormat("Exiting findByUsername with value {0}", null);
            return null;
        }

        public User findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from user where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long user_id = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        String password = dataR.GetString(2);
                        User user = new User(user_id,username, password);
                        log.InfoFormat("Exiting findOne with value {0}", user);
                        return user;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public void save(User entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into user(username,password) values (@username, @password)";

                var paramDesc = comm.CreateParameter();
                paramDesc.ParameterName = "@username";
                paramDesc.Value = entity.username;
                comm.Parameters.Add(paramDesc);

                var paramElems = comm.CreateParameter();
                paramElems.ParameterName = "@password";
                paramElems.Value = entity.password;
                comm.Parameters.Add(paramElems);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No user added !");
            }
        }

        public void update(User entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update user set username = @username, password = @password where id = @id)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramDesc = comm.CreateParameter();
                paramDesc.ParameterName = "@username";
                paramDesc.Value = entity.username;
                comm.Parameters.Add(paramDesc);

                var paramElems = comm.CreateParameter();
                paramElems.ParameterName = "@password";
                paramElems.Value = entity.password;
                comm.Parameters.Add(paramElems);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No user added !");
            }
        }
    }
}
