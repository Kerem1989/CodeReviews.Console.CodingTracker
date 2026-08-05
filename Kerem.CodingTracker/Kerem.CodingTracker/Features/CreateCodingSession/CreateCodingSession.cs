using Kerem.CodingTracker.Domain.Entities;
using Kerem.CodingTracker.Domain.Interfaces;
using Spectre.Console;

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
            AnsiConsole.MarkupLine("[bold steelblue]Please enter the start date in the format of yyyy-mm-dd hh:mm[/]");


            var startDate = Console.ReadLine() ?? " ";

            var emptyStartDate = string.IsNullOrEmpty(startDate);
            if (emptyStartDate)
            {
                AnsiConsole.MarkupLine("[red]Date cannot be empty[/]");

                return;
            }
                
   
            var correctFormat = CreateCodingSessionValidator.ValidateDateFormat(startDate);
            
            if (!correctFormat)
            {
                Console.WriteLine("Format is invalid");
                return;
            }

            DateTime startTime = DateTime.Parse(startDate);
            codingSession.StartTime = startTime;
            
            Console.WriteLine("Please enter the end date");
            var endDate = Console.ReadLine() ?? " ";
            var emptyEndDate = string.IsNullOrEmpty(endDate);

            if (emptyEndDate)
            {
                Console.WriteLine("Date cannot be empty");
                return;
            }
            
            correctFormat = CreateCodingSessionValidator.ValidateDateFormat(endDate);

            if (!correctFormat)
            {
                Console.WriteLine("Format is invalid");
                return;
            }
            
            DateTime endTime = DateTime.Parse(endDate);
            codingSession.EndTime = endTime;
            
            
            var difference = endTime - startTime;
            var minutes = (int)difference.TotalMinutes;
            codingSession.Duration = minutes;
            
            _codingSessionRepository.Create(codingSession);
        }
    }