namespace Kerem.CodingTracker.Domain.Interfaces ;

    public interface ICodingSessionRepository
    {
        List <CodingSession>  FindAll();
        void Create(CodingSession codingSession);
    }