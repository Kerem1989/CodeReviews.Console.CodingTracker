using Kerem.CodingTracker.Domain.Entities;

namespace Kerem.CodingTracker.Domain.Interfaces ;

    public interface ICodingSessionRepository
    {
        List <CodingSession>?  FindAll();
        void Create(CodingSession codingSession);
        int CountCodingSessions();
        CodingSession? FindById(int id);
        void Save(CodingSession codingSession);
    }