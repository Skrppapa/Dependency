var logger = new Logger(new SimpleLogService());
logger.LogMessage("Hello METANIT.COM");

logger = new Logger(new GreenLogService());
logger.LogMessage("Hello METANIT.COM");

logger = new Logger(new SimpleLogServiceEmale());
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
    void Write(string emale);
}

class SimpleLogServiceEmale : IEmailService
{
    public void Write(string emale) => Console.WriteLine(emale);
}

class Logger
{
    ILogService logService;
    IEmailService logServiceEmale;
    public Logger(ILogService logService) => this.logService = logService;
    public Logger(IEmailService logServiceEmale) => this.logServiceEmale = logServiceEmale;
    public void LogMessage(string message) => logService?.Write($"{DateTime.Now}  {message}");
    public void LogEmail(string emale) => logServiceEmale?.Write($"{DateTime.Now}  {emale}");
}