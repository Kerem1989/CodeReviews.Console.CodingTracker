using Dapper;
using Kerem.CodingTracker.Domain.Entities;
using Kerem.CodingTracker.Domain.Interfaces;
using Spectre.Console;

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

        public int CountCodingSessions()
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = "SELECT COUNT(*) FROM CodingSession";
            return connection.ExecuteScalar<int>(sql);
        }

        public CodingSession FindById(int id)
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = "SELECT * FROM  CodingSession WHERE Id = @Id";
            var codingSession = connection.Query<CodingSession>(sql, new { Id = id }).FirstOrDefault();
            return codingSession;
        }

        public void Save(CodingSession codingSession)
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = $"UPDATE CodingSession SET startTime = @StartTime, endTime = @EndTime, duration = @Duration WHERE Id = @Id";
            connection.ExecuteScalar(sql, new  { codingSession.StartTime, codingSession.EndTime, codingSession.Duration, codingSession.Id });
        }
        
        public void  Delete(int id)
        {
            var connection = _dapperDbContext.GetConnection();
            var sql = $"DELETE FROM CodingSession WHERE Id = @Id";
            connection.ExecuteScalar(sql, new { Id = id });
        }
        
        
    }