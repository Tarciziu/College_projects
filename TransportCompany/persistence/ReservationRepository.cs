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
    public class ReservationRepository : IReservationRepository
    {
        private static readonly ILog log = LogManager.GetLogger("ReservationDbRepository");

        public ReservationRepository()
        {
            log.Info("Creating ReservationDbRepository");
        }

        public void delete(Reservation entity)
        {
            log.InfoFormat("Deleting reservation", entity);
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from reservation where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                    throw new RepositoryException("No reservation deleted!");
            }
        }

        public IEnumerable<Reservation> findAll()
        {
            IDbConnection con = DBUtils.getConnection();
            IList<Reservation> reservationsR = new List<Reservation>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from reservation res inner join" +
                    " route rou on rou.id = res.id_route";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long res_id = dataR.GetInt32(0);
                        string clientName = dataR.GetString(1);
                        int reservedSeat = dataR.GetInt32(2);
                        long route_id = dataR.GetInt32(3);
                        String username = dataR.GetString(4);
                        DateTime departureDateTime = dataR.GetDateTime(5);
                        int availableSeats = dataR.GetInt32(6);
                        Route route = new Route(route_id, username, departureDateTime, availableSeats);
                        Reservation reservation = new Reservation(res_id, clientName, reservedSeat, route);
                        reservationsR.Add(reservation);
                    }
                }
            }

            return reservationsR;
        }

        public IEnumerable<Reservation> findByClientName(string clientName)
        {
            log.InfoFormat("Entering findByClientName with value {0}", clientName);
            IDbConnection con = DBUtils.getConnection();
            IList<Reservation> reservationsR = new List<Reservation>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from reservation res inner join" +
                    " route rou on rou.id = res.id_route where res.clientName=@clientName";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@clientName";
                paramId.Value = clientName;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long res_id = dataR.GetInt32(0);
                        string client = dataR.GetString(1);
                        int reservedSeat = dataR.GetInt32(2);
                        long route_id = dataR.GetInt32(3);
                        String username = dataR.GetString(4);
                        DateTime departureDateTime = dataR.GetDateTime(5);
                        int availableSeats = dataR.GetInt32(6);
                        Route route = new Route(route_id, username, departureDateTime, availableSeats);
                        Reservation reservation = new Reservation(res_id, client, reservedSeat, route);
                        reservationsR.Add(reservation);
                    }
                }
            }
            return reservationsR;
        }

        public IEnumerable<Reservation> findByRoute(Route route)
        {
            log.InfoFormat("Entering findByClientName with value {0}", route);
            IDbConnection con = DBUtils.getConnection();
            IList<Reservation> reservationsR = new List<Reservation>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select res.id as resid, clientName," +
                " id_route, rou.id as rouid, reservedSeat" +
                " from reservation res " +
                "inner join route rou on rouid = res.id_route " +
                "where id_route = @id_route";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id_route";
                paramId.Value = route.Id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long res_id = dataR.GetInt32(0);
                        string client = dataR.GetString(1);
                        int reservedSeat = dataR.GetInt32(4);
                        long route_id = dataR.GetInt32(2);
                        Reservation reservation = new Reservation(res_id, client, reservedSeat, route);
                        reservationsR.Add(reservation);
                    }
                }
            }
            return reservationsR;
        }

        public Reservation findOne(long id)
        {
            log.InfoFormat("Entering findOne with value {0}", id);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select * from reservation where id=@id";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long res_id = dataR.GetInt32(0);
                        string client = dataR.GetString(1);
                        int reservedSeat = dataR.GetInt32(2);
                        long route_id = dataR.GetInt32(3);
                        String username = dataR.GetString(4);
                        DateTime departureDateTime = dataR.GetDateTime(5);
                        int availableSeats = dataR.GetInt32(6);
                        Route route = new Route(route_id, username, departureDateTime, availableSeats);
                        Reservation reservation = new Reservation(res_id, client, reservedSeat, route);
                        log.InfoFormat("Exiting findOne with value {0}", route);
                        return reservation;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value {0}", null);
            return null;
        }

        public void save(Reservation entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "insert into reservation (clientName, reservedSeat, id_route)" +
                    " values (@clientName, @reservedSeat, @route_id)";
                var paramCN = comm.CreateParameter();
                paramCN.ParameterName = "@clientName";
                paramCN.Value = entity.clientName;
                comm.Parameters.Add(paramCN);

                var paramRS = comm.CreateParameter();
                paramRS.ParameterName = "@reservedSeat";
                paramRS.Value = entity.reservedSeat;
                comm.Parameters.Add(paramRS);

                var paramElems = comm.CreateParameter();
                paramElems.ParameterName = "@route_id";
                paramElems.Value = entity.route.Id;
                comm.Parameters.Add(paramElems);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No reservation added !");
            }
        }

        public void update(Reservation entity)
        {
            var con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "update reservation set clientName = @clientName, reservedSeat = @reservedSeat," +
                    " id_route = @route_id where id = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = entity.Id;
                comm.Parameters.Add(paramId);

                var paramDest = comm.CreateParameter();
                paramDest.ParameterName = "@clientName";
                paramDest.Value = entity.clientName;
                comm.Parameters.Add(paramDest);

                var paramDep = comm.CreateParameter();
                paramDep.ParameterName = "@reservedSeat";
                paramDep.Value = entity.reservedSeat;
                comm.Parameters.Add(paramDep);

                var paramSeats = comm.CreateParameter();
                paramSeats.ParameterName = "@route_id";
                paramSeats.Value = entity.route.Id;
                comm.Parameters.Add(paramSeats);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new RepositoryException("No user added !");
            }
        }
    }
}
