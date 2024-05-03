using ASPCoreAssignment;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILoggingMessageWriter, LoggingMessageWriter>();

var app = builder.Build();

app.UseReadData();

app.Run();
