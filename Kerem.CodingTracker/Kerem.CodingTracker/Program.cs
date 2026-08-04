using Kerem.CodingTracker;
using Kerem.CodingTracker.Domain.Interfaces;
using Kerem.CodingTracker.Features.CreateCodingSession;
using Kerem.CodingTracker.Infrastructure.Repositories;
using Kerem.CodingTracker.UI;

ConsoleMenu startProgram = new ConsoleMenu();
var connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=CodingTracker;Integrated Security=true;TrustServerCertificate=true;";   
DapperDbContext dapperDbContext = new DapperDbContext(connectionString);
ICodingSessionRepository codingSessionRepository = new CodingSessionRepository(dapperDbContext);
CreateCodingSession createCodingSession = new CreateCodingSession(codingSessionRepository);
startProgram.Menu(createCodingSession);