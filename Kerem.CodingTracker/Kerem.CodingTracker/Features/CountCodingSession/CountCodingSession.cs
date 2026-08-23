using Kerem.CodingTracker.Domain.Interfaces;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.CreateCodingSession ;

    public class CountCodingSession
    {
        private readonly  ICodingSessionRepository _codingSessionRepository;

        public CountCodingSession(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }

        public void CountCodingSessions()
        {
            var amount = _codingSessionRepository.CountCodingSessions();
            AnsiConsole.MarkupLine($"[red]There are {amount} registered coding sessions in the database.[/]");
        }
    }