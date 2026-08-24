using Kerem.CodingTracker.Domain.Interfaces;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.DeleteCodingSession ;

    public class DeleteCodingSession
    {
        private readonly ICodingSessionRepository _codingSessionRepository;

        public DeleteCodingSession(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }
        public void DeleteCodingSessionById()
        {
            AnsiConsole.MarkupLine("[blue]Please enter the id of the coding session you want to delete[/]");
            int selectedSesssion = int.TryParse(Console.ReadLine(), out var selectedId) ? selectedId : 0;
            if (selectedSesssion == 0)
            {
                AnsiConsole.MarkupLine("[red]The input its not a numerical value[/]");
                return;
            }
            var codingSession = _codingSessionRepository.FindById(selectedSesssion);
            if (codingSession == null)
            {
                AnsiConsole.MarkupLine("[red]The coding session you want to delete does not exist[/]");
                return;
            }
            _codingSessionRepository.Delete(codingSession.Id);
            AnsiConsole.MarkupLine($"[green]Coding session with id {codingSession.Id} has been deleted[/]");

        }
    }