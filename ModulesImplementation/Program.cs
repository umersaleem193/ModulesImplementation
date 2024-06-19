using Microsoft.EntityFrameworkCore;
using ModulesImplementation.Data;
using Hangfire;
using Hangfire.SqlServer;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ModulesImplementation")));

//Hangfire is used here so email job can be monitored or can be triggered manually to test or debug
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("ModulesImplementation"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromSeconds(15),
        UseRecommendedIsolationLevel = true,
        UsePageLocksOnDequeue = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireDashboard();
app.UseHangfireServer();

var emailService = app.Services.GetRequiredService<EmailService>();
RecurringJob.AddOrUpdate(
    "SendReminderEmails",
    () => emailService.SendReminderEmails(),
    Cron.Minutely);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



public class EmailService
{
    private readonly IServiceProvider _services;

    public EmailService(IServiceProvider services)
    {
        _services = services;
    }

    public void SendReminderEmails()
    {
        using (var scope = _services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var reminders = context.Reminders
                .Where(r => r.ReminderDateTime <= DateTime.Now && !r.IsSent)
                .ToList();

            foreach (var reminder in reminders)
            {
                SendEmailAsync(reminder.Email, reminder.Title, reminder.ReminderDateTime.ToString());

                reminder.IsSent = true;
                context.Update(reminder);
            }
            context.SaveChanges();
        }
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("RingoMedia", "emailaddress@gmail.com"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<p>{message}</p>",
            TextBody = message
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            //I created an app in gmail ccount to authenticate using app password
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("emailaddress@gmail.com", "app name here", CancellationToken.None); 

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
