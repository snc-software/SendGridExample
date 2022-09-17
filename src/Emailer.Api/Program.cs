using Emailer.Business;
using Emailer.Business.Interfaces;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddSendGrid(options =>
{
    options.ApiKey = configuration.GetValue<string>("SendGrid:ApiKey");
});

builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();

app.MapGet("/send-email", async (IEmailService service) => await service.SendEmail());

app.Run();