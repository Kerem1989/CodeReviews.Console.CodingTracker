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
            AnsiConsole.MarkupLine("[Orange1]Enter abort to exit back to the main menu[/]");

            var startDate = Console.ReadLine() ?? " ";
            bool shouldAbort = Validator.Abort(startDate);

            if (shouldAbort)
            {
                AnsiConsole.MarkupLine("[Orange1]Aborted[/]");
                return;
            }
            

            var emptyStartDate = string.IsNullOrEmpty(startDate);
            if (emptyStartDate)
            {
                AnsiConsole.MarkupLine("[red]Date cannot be empty[/]");

                return;
            }
                
   
            var correctFormat = Validator.ValidateDateFormat(startDate);
            
            if (!correctFormat)
            {
                AnsiConsole.MarkupLine("[red]Format is invalid[/]");
                return;
            }

            DateTime startTime = DateTime.Parse(startDate);
            codingSession.StartTime = startTime;
            
            AnsiConsole.MarkupLine("[bold steelblue]Please enter the end date in the format of yyyy-mm-dd hh:mm[/]");

            var endDate = Console.ReadLine() ?? " ";
            
            shouldAbort = Validator.Abort(startDate);

            if (shouldAbort)
            {
                AnsiConsole.MarkupLine("[Orange1]Aborted[/]");
                return;
            }
            
            var emptyEndDate = string.IsNullOrEmpty(endDate);

            if (emptyEndDate)
            {
                AnsiConsole.MarkupLine("[red]Date cannot be empty[/]");
                return;
            }
            
            correctFormat = Validator.ValidateDateFormat(endDate);

            if (!correctFormat)
            {
                AnsiConsole.MarkupLine("[red]Format is invalid[/]");
                return;
            }
            
            DateTime endTime = DateTime.Parse(endDate);
            codingSession.EndTime = endTime;
            
            
            var difference = endTime - startTime;
            var minutes = (int)difference.TotalMinutes;
            codingSession.Duration = minutes;
            
            _codingSessionRepository.Create(codingSession);
            AnsiConsole.MarkupLine("[green]Coding session created[/]");
        }
    }