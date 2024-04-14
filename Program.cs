var logger = new Logger(new SimpleLogService(), new SimpleLogServiceEmale());
logger.LogMessage("Hello METANIT.COM");

logger = new Logger(new GreenLogService(), new SimpleLogServiceEmale());
logger.LogMessage("Hello METANIT.COM");

logger = new Logger(new SimpleLogService(), new SimpleLogServiceEmale());
logger.LogEmail("ABCD@mail.ru");

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

interface IEmailService
{
    void Write(string email);
}

class SimpleLogServiceEmale : IEmailService
{
    public void Write(string email) => Console.WriteLine(email);
}

class Logger
{
    ILogService logService;
    IEmailService logServiceEmail;
    public Logger(ILogService logService, IEmailService logServiceEmail)
    {
        this.logService = logService;
        this.logServiceEmail = logServiceEmail;
    }
    public void LogMessage(string message) => logService?.Write($"{DateTime.Now}  {message}");
    public void LogEmail(string emale) => logServiceEmail?.Write($"{DateTime.Now}  {emale}");
}

