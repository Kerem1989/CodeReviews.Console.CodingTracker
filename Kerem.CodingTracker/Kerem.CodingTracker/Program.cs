using Kerem.CodingTracker;
using Kerem.CodingTracker.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();
var connectionString = config.GetConnectionString("DefaultConnection");

var serviceProvider = new ServiceCollection()
        .AddApplication(connectionString)
        .BuildServiceProvider();

serviceProvider.GetRequiredService<ConsoleMenu>().Menu();
