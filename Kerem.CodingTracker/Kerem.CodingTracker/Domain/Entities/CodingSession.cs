namespace Kerem.CodingTracker ;

    public class CodingSession
    {
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public Decimal duration { get; set; }
    }