using domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using persistence;

namespace persistence
{
    public class RouteRepository : IRouteRepository
    {
        private static readonly ILog log = LogManager.GetLogger("RouteDbRepository");

        public RouteRepository()
        {
            log.Info("Creating RouteDbRepository");
        }

        public void delete(Route entity)
        {
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from route where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No user deleted!");
            }
        }

        public IEnumerable<Route> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<Route> routesR = new List<Route>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from route";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long route_id = dataR.GetInt32(0);
                        String destination = dataR.GetString(1);
                        DateTime departureDateTime = dataR.GetDateTime(2);
                        int availableSeats = dataR.GetInt32(3);
                        Route route = new Route(route_id, destination, departureDateTime,availableSeats);
                        routesR.Add(route);
                    }
                }
            }

            return routesR;
        }

        public IEnumerable<Route> findByDestination(String destination)
        {
            log.InfoFormat("Entering findByUsername with value {0}", destination);
            IDbConnection con = DBUtils.getConnection();
            IList<Route> routesR = new List<Route>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from route where destination=@destination";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@destination";
                paramId.Value = destination;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long route_id = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        DateTime departureDateTime = dataR.GetDateTime(2);
                        int availableSeats = dataR.GetInt32(3);
                        Route route = new Route(route_id, username, departureDateTime, availableSeats);
                        log.InfoFormat("Exiting findByDestination with value {0}", destination);
                        routesR.Add(route);
                    }
                }
            }
            return routesR;
        }

        public Route findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from route where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long route_id = dataR.GetInt32(0);
                        String username = dataR.GetString(1);
                        DateTime departureDateTime = dataR.GetDateTime(2);
                        int availableSeats = dataR.GetInt32(3);
                        Route route = new Route(route_id, username, departureDateTime, availableSeats);
                        log.InfoFormat("Exiting findOne with value {0}", route);
                        return route;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public void save(Route entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into route(destination, departureDateTime, availableSeats)" +
                    " values (@destination, @departureDateTime, @availableSeats)";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@destination";
                paramId.Value = entity.destination;
                comm.Parameters.Add(paramId);

                var paramDesc = comm.CreateParameter();
                paramDesc.ParameterName = "@departureDateTime";
                paramDesc.Value = entity.departureDateTime;
                comm.Parameters.Add(paramDesc);

                var paramElems = comm.CreateParameter();
                paramElems.ParameterName = "@availableSeats";
                paramElems.Value = entity.availableSeats;
                comm.Parameters.Add(paramElems);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No route added !");
            }
        }

        public void update(Route entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update route set destination = @destination, departureDateTime = @departureDateTime," +
                    " availableSeats = @availableSeats where id = @id";

                var paramDest = comm.CreateParameter();
                paramDest.ParameterName = "@destination";
                paramDest.Value = entity.destination;
                comm.Parameters.Add(paramDest);

                var paramDep = comm.CreateParameter();
                paramDep.ParameterName = "@departureDateTime";
                paramDep.Value = entity.departureDateTime;
                comm.Parameters.Add(paramDep);

                var paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@availableSeats";
                paramSeats.Value = entity.availableSeats;
                comm.Parameters.Add(paramSeats);

                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No route added !");
            }
        }
    }
}
