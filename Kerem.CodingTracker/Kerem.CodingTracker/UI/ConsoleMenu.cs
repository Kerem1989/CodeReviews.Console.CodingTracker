using Kerem.CodingTracker.Features;
using Kerem.CodingTracker.Features.CreateCodingSession;
using Kerem.CodingTracker.Features.DeleteCodingSession;
using Kerem.CodingTracker.Features.EditCodingSession;
using Kerem.CodingTracker.Features.FindAllCodingSession;
using Spectre.Console;

namespace Kerem.CodingTracker.UI ;

    public class ConsoleMenu
    {
        private readonly CreateCodingSession  _createCodingSession;
        private readonly CountCodingSession _countCodingSession;
        private readonly FindAllCodingSession _findAllCodingSession;
        private readonly EditCodingSession _editCodingSession;
        private readonly DeleteCodingSession _deleteCodingSession;

        public ConsoleMenu(CreateCodingSession createCodingSession, CountCodingSession countCodingSession, FindAllCodingSession findAllCodingSession, EditCodingSession editCodingSession, DeleteCodingSession deleteCodingSession)
        {
            _createCodingSession = createCodingSession;
            _countCodingSession = countCodingSession;
            _findAllCodingSession = findAllCodingSession;
            _editCodingSession = editCodingSession;
            _deleteCodingSession = deleteCodingSession;
        }

        public void Menu()
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.SteelBlue));
            while (true)
            {
                string choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select an [bold steelblue]option[/]:")
                        .AddChoices(
                            "1. View all coding sessions",
                            "2. Create a coding session",
                            "3. Edit a coding session",
                            "4. Delete a coding session",
                            "5. Exit"));

                switch (choice[0])
                {
                    case '1':
                        _countCodingSession.CountCodingSessions();
                        _findAllCodingSession.FindAll();
                        break;
                    case '2':
                        _createCodingSession.Create();
                        break;
                    case '3':
                        _findAllCodingSession.FindAll();
                        _editCodingSession.CodingSessionEdit();
                        break;
                    case '4':
                        _findAllCodingSession.FindAll();
                        _deleteCodingSession.DeleteCodingSessionById();
                        break;
                    case '5':
                        AnsiConsole.MarkupLine("[bold green]Goodbye![/]");
                        return;
                }
            }
        }
    }
    
