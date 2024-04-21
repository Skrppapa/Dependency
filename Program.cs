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