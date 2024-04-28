using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection().AddTransient<ILogService, SimpleLogService>(); // Добавили ILogService в контейнер через AddTransient

using var serviceProvider = services.BuildServiceProvider(); // Провайдер - будет вытягивать сервисы из контейнера

ILogService? logService = serviceProvider.GetService<ILogService>(); // GetService вызывает сервис из контейнера через провайдер

logService?.Write("Hello METANIT.COM"); // Воспользовались сервисом logService из контейнера

var services2 = new ServiceCollection().AddTransient<IEmailService, SimpleLogServiceEmale>();
// Теперь добавил IEmailService и его конкретную реализацию в контейнер через AddTransient
IEmailService? logemailService = serviceProvider.GetService<IEmailService>();
// GetService вызывает сервис из контейнера через провайдер - теперь это мой сервис Email
logemailService?.SendEmail("My Email"); // Воспользовались сервисом logemailService из контейнера



var logger = new Logger(new SimpleLogService(), new SimpleLogServiceEmale());
logger.LogMessage("Hello METANIT.COM", LogType.Console);

logger = new Logger(new GreenLogService(), new SimpleLogServiceEmale());
logger.LogMessage("Hello METANIT.COM", LogType.Console);

logger = new Logger(new SimpleLogService(), new SimpleLogServiceEmale());
logger.LogMessage("ABCD@mail.ru", LogType.Email);



public enum LogType
{
    Console,
    Email
}


interface ILogService
{
    void Write(string message);
}
// простой вывод на консоль
class SimpleLogService : ILogService
{
    public void Write(string message) => Console.WriteLine(message);
}

// сервис, который выводит сообщение зеленым цветом
class GreenLogService : ILogService
{
    public void Write(string message)
    {
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(message);
        Console.ForegroundColor = defaultColor;
    }
}


// Сервис который выводит emaile
interface IEmailService
{
    void SendEmail(string email);
}

class SimpleLogServiceEmale : IEmailService
{
    public void SendEmail(string email) => Console.WriteLine(email);
}



class Logger
{
    ILogService logService;
    IEmailService emailService;

    public Logger(ILogService logService, IEmailService emailService)
    {
        this.logService = logService;
        this.emailService = emailService;
    }

    public void LogMessage(string message, LogType logType)
    {
        switch (logType)
        {
            case LogType.Console:
                logService?.Write($"{DateTime.Now}  {message}");
                break;
            case LogType.Email:
                emailService?.SendEmail($"{DateTime.Now}  {message}");
                break;
        }
    }

}







