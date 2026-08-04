using Dapper;
using Kerem.CodingTracker.Domain.Interfaces;

namespace Kerem.CodingTracker.Infrastructure.Repositories ;

    public class CodingSessionRepository : ICodingSessionRepository
    {
        private readonly DapperDbContext  _dapperDbContext;

        public CodingSessionRepository(DapperDbContext context)
        {
            _dapperDbContext = context;
        }
        public List<CodingSession> FindAll()
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = "SELECT * FROM CodingSession";
            var codingSessions = connection.Query<CodingSession>(sql).ToList();
            return codingSessions;
        }

        public void Create(CodingSession codingSession)
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = "INSERT INTO CodingSession (startTime, endTime, duration) VALUES (@StartTime, @EndTime, @Duration)";
            connection.Execute(sql, codingSession);
        }
        
        
    }