using System.Data;
using Kerem.CodingTracker.Domain.Interfaces;
using Kerem.CodingTracker.Infrastructure.Repositories;

namespace Kerem.CodingTracker.Features.CreateCodingSession ;

    public class CreateCodingSession
    {
        private readonly  ICodingSessionRepository _codingSessionRepository;

        public CreateCodingSession(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }

        public void Create()
        {
            CodingSession codingSession = new CodingSession();
            Console.WriteLine("Please enter the start date");

            var startDate = Console.ReadLine();
            var correctFomat = CreateCodingSessionValidator.ValidateDateFormat(startDate);
            
            if (!correctFomat)
            {
                Console.WriteLine("Format is invalid");
                return;
            }

            DateTime startTime = DateTime.Parse(startDate);
            codingSession.startTime = startTime;
            
            Console.WriteLine("Please enter the end date");
            var endDate = Console.ReadLine();
            DateTime endTime = DateTime.Parse(endDate);
            codingSession.endTime = endTime;
            decimal durationInSeconds = Convert.ToDecimal(DateTime.Now.Subtract(startTime).TotalSeconds);
            codingSession.duration = 0;
            _codingSessionRepository.Create(codingSession);
        }
    }