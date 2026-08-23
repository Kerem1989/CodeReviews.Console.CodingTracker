using Kerem.CodingTracker.Domain.Interfaces;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.FindAllCodingSession ;

    public class FindAllCodingSession
    {
        private readonly ICodingSessionRepository _codingSessionRepository;

        public FindAllCodingSession(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }

        public void FindAll()
        {
            var amount = _codingSessionRepository.FindAll();
            
            if (amount == null)
            {
                AnsiConsole.MarkupLine("[red]There are no registered coding sessions in the database.[/]");
                return;
            }

            var table = new Table()
                .RoundedBorder()
                .BorderColor(Color.Green);
            
            table.AddColumn(("Id"));
            table.AddColumn(("Start Time"));           
            table.AddColumn(("End Time"));
            table.AddColumn(("Duration"));
            
            foreach (var codingSession in amount)
            {
                table.AddRow($"{codingSession.Id}", $"{codingSession.StartTime}", $"{codingSession.EndTime}", $"{codingSession.Duration}");
                
            }
            AnsiConsole.Write(table);
        }
    }