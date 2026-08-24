using Kerem.CodingTracker.Domain.Entities;
using Kerem.CodingTracker.Domain.Interfaces;
using Kerem.CodingTracker.Features.CreateCodingSession;
using Spectre.Console;

namespace Kerem.CodingTracker.Features.EditCodingSession ;

    public class EditCodingSession
    {
        private readonly ICodingSessionRepository _codingSessionRepository;

        public EditCodingSession(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;
        }

        public void CodingSessionEdit()
        {
            AnsiConsole.MarkupLine("[blue]Please enter the id of the coding session you want to edit[/]");
            int id = int.TryParse(Console.ReadLine(), out var selectedId) ? selectedId : 0;
            if (id == 0)
            {
                AnsiConsole.MarkupLine("[red]The input its not a numerical value[/]");
                return;
            }
            var codingSession = _codingSessionRepository.FindById(id);
            if (codingSession == null)
            {
                AnsiConsole.MarkupLine("[red]The coding session you want to edit does not exist[/]");
                return;
            }

            bool runProgram = true;
            while (runProgram)
            {
                AnsiConsole.MarkupLine("[blue]Please select 1 to edit start date, 2 to edit end date or 3 to exit[/]");
                int selectedProperty = Convert.ToInt32(Console.ReadLine());
                switch (selectedProperty)
                {
                    case 1:
                        AnsiConsole.MarkupLine("[bold steelblue]Please enter the start date in the format of yyyy-mm-dd hh:mm[/]");
                        var startDate = Console.ReadLine() ?? " ";
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
                        break;
                    case 2:
                        AnsiConsole.MarkupLine("[blue]Please enter the end date in the format of yyyy-mm-dd hh:mm[/]");
                        var endDate = Console.ReadLine() ?? " ";
                        var emptyEndDate = string.IsNullOrEmpty(endDate);
                        if (emptyEndDate)
                        {
                            AnsiConsole.MarkupLine("[red]Date cannot be empty[/]");

                            return;
                        }
                        var correctFormatEndDate = Validator.ValidateDateFormat(endDate);
            
                        if (!correctFormatEndDate)
                        {
                            AnsiConsole.MarkupLine("[red]Format is invalid[/]");
                            return;
                        }
                        DateTime endTime = DateTime.Parse(endDate);
                        codingSession.EndTime = endTime;
                        break;
                    case 3:
                        runProgram = false;
                        break;
                }
            }

            var difference = codingSession.EndTime - codingSession.StartTime;
            var minutes = (int)difference.TotalMinutes;
            codingSession.Duration = minutes;
            _codingSessionRepository.Save(codingSession);
            AnsiConsole.MarkupLine("[green]Coding session has been updated[/]");

        }
    }