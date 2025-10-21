// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

Console.ReadLine();

/*
Задача 3 из 4
Подозрительная активность на сервере
Средний
ООП, регулярные выражения, стру..., Регулярные выражения, Обобщения, Интерфейсы

Вы разрабатываете модуль анализа активности системных служб в корпоративной информационной системе на основе исследования логов запросов, поступающих на сервер.

Запись лога имеет следующий вид:
<service="servicelD" data="datalD" action="read|write">.

Нужно определить корректные записи логов сервера.
Признаки корректности данных:
• Запись лога строка содержит ровно три атрибута, разделенных символом пробел в формате имя Атрибута="значениеАтрибута";
• Внутри имен и значений атрибутов пробелы не допускаются;
• Допускаются пробелы в начале и конце строки, содержащей запись лога;
• Атрибут service содержит значение serviced, которое имеет вид целого числа от 10000 до 99999 включительно;
• Атрибут data содержит значение datalD, которое имеет вид строки символов английского алфавита длиной 9 символов, записанных в верхнем регистре;
• Атрибут action содержит одно из значений (read или write).

Корректные записи обрабатываются следующим образом:
• Для операций read и write для каждого сервиса производится подсчет количества сделанных запросов;
• Операция write: для каждого сервиса производится подсчет количества сделанных запросов, а также детектируется наличие подозрительной активности: количество операций write ≥ 75% от общего количества операций readlwrite.
Для каждого сервиса нужно вывести количество операций read и write либо сообщение о подозрительной активности.
Если корректные записи логов отсутствуют, вывести none.

Требуется разработать класс ServerLogAnalyzer с методом ProcessingServerLogs, который выводит информацию об активности сервисов в требуемом формате.
Реализовать классы CorrectService и FishingSevice, реализующие интерфейс IWriteServiceActivity.

Формат ввода
На вход подаются строки, содержащие логи сервера в формате:
<service="servicelD" data="datalD" action="read|write">.
Количество строк от 1 до 1000. Гарантируется, что есть хотя бы одна строка.

Формат вывода
Данные о корректных записях лога для сервиса с нормальной активностью выводятся в формате сериализованного JSON в порядке, как указано в примере:
{"service":"servicelD","read":readCount,"write":writeCount}.
Данные о корректных записях лога для сервиса с подозрительной активностью выводятся в следующем формате:
Alert! servicelD has suspicious activity.
При этом сервисы сортируются по возрастанию servicelD.
Если нет ни одной корректной записи, вывести одну строку: none

Пример 1 - неверный пример, противоречит заданию (произошла обработка некорректных входных данных). 
Входные данные: 
<service="10001" data="ABCDEFGHI" action="read">
<service="10001" data="JKLMNOPQR" action="read">
<service="10001" data="STUVWXYZa" action="write"> 
<service="10002" data="bcdefghij" action="read">
Выходные данные: 
{"service":"10001", "read":2,"write":1}
{"service":"10002","read":1,"write":0}

Пример 2
Входные данные: 
<service="20001" data="AAAAAAAAA" action="write">
<service="20001" data="BBBBBBBBB" action="write"> 
<service="20001" data="CCCCCCCCC" action="write"> 
<service="20001" data="DDDDDDDDD" action="read"> 
<service="20002" data="EEEEEEEEE" action="read"> 
<service="20002" data="FFFFFFFFF" action="read">
<service="20002" data="GGGGGGGGG" action="write">
Выходные данные: 
Alert! 20001 has suspicious activity
{"service":"20002", "read":2,"write":1}


• Пример 1
Входные данные
<service="31007" data="ABCDEFGHI" action="write">
<service="32008" data="JKLMNOPQR" action="read">
<service="31007" data="STUVWXYZA" action="write">
<service="32008" data="BCDEFGHIJ" action="read">
<service="31007" data="KLMNOPQRS" action="write">
<service="32008" data="TUVWXYZAB" action="read">
<service="31007" data="CDEFGHIJK" action="write">
<service="32008" data="LMNOPQRST" action="read">
Ожидаемый результат
Alert! 31007 has suspicious activity 
{"service":"32008","read":4,"write":0}


• Пример 2
Входные данные
<service="41017" data="ABCDEFGHI" action="write">
<service="42018" data="JKLMNOPQR" action="read">
<service="41017" data="STUVWXYZA" action="write">
<service="42018" data="BCDEFGHIJ" action="read">
<service="41017" data="KLMNOPQRS" action="write">
<service="42018" data="TUVWXYZAB" action="read">
<service="41017" data="CDEFGHIJK" action="write">
<service="42018" data="LMNOPQRST" action="read">
<service="41017" data="UVWXYZABC" action="write">
<service="42018" data="XYZABCDEF" action="read">
<service="41017" data="YZABCDEFG" action="write">
<service="42018" data="ZABCDEFGH" action="read">
<service="41017" data="NEWDATAID" action="write">
<service="42018" data="ANOTHERID" action="write">
Ожидаемый результат
Alert! 41017 has suspicious activity 
{"service":"42018","read":6,"write":1}


• Пример 3
Входные данные
<service="29005" data="ABCDEFGHI" action="read">
<service="30006" data="JKLMNOPQR" action="write">
<service="29005" data="STUVWXYZA" action="read">
<service="30006" data="BCDEFGHIJ" action="write">
<service="29005" data="KLMNOPQRS" action="read">
<service="30006" data="TUVWXYZAB" action="write">
<service="29005" data="CDEFGHIJK" action="read">
 <service="30006" data="LMNOPQRST" action="write">
Ожидаемый результат
{"service":"29005","read":4,"write":0} 
Alert! 30006 has suspicious activity

• Пример 5
Входные данные
<service="33009" data="ABCDEFGHI" action="write">
<service="33009" data="JKLMNOPQR" action="write">
<service="33009" data="STUVWXYZA" action="write">
<service="33009" data="BCDEFGHIJ" action="read">
<service="33009" data="KLMNOPQRS" action="write">
 <service="33009" data="TUVWXYZAB" action="write">
<service="33009" data="CDEFGHIJK" action="write">
<service="33009" data="LMNOPQRST" action="write">
<service="33009" data="UVWXYZABC" action="read">
<service="33009" data="XYZABCDEF" action="write">
<service="33009" data="YZABCDEFG" action="write">
<service="33009" data="ZABCDEFGH" action="read">
Ожидаемый результат
Alert! 33009 has suspicious activity

• Пример 6
Входные данные
<service="37013" data="ABCDEFGHI" action="write">
<service="38014" data="JKLMNOPQR" action="read">
<service="37013" data="STUVWXYZA" action="write">
<service="38014" data="BCDEFGHIJ" action="read">
<service="37013" data="KLMNOPQRS" action="write">
<service="38014" data="TUVWXYZAB" action="read">
<service="37013" data="CDEFGHIJK" action="write">
<service="38014" data="LMNOPQRST" action="write">
<service="37013" data="UVWXYZABC" action="write">
<service="38014" data="XYZABCDEF" action="read">
<service="37013" data="YZABCDEFG" action="write">
<service="38014" data="ZABCDEFGH" action="read">
<service="37013" data="NEWDATAID" action="write">
<service="38014" data="ANOTHERID" action="read">
Ожидаемый результат
Alert! 37013 has suspicious activity
{"service":"38014","read":6,"write":1}

• Пример 7
<service="35011" data="ABCDEFGHI" action="write">
<service="36012" data="JKLMNOPQR" action="read">
<service="35011" data="STUVWXYZA" action="write">
<service="36012" data="BCDEFGHIJ" action="read">
<service="35011" data="KLMNOPQRS" action="write">
<service="36012" data="TUVWXYZAB" action="read">
<service="35011" data="CDEFGHIJK" action="write">
<service="36012" data="LMNOPQRST" action="read">
<service="35011" data="UVWXYZABC" action="write">
<service="36012" data="XYZABCDEF" action="write">
<service="35011" data="YZABCDEFG" action="write">
<service="36012" data="ZABCDEFGH" action="read">
<service="35011" data="NEWDATAID" action="write">
<service="36012" data="ANOTHERID" action="read">
Ожидаемый результат
Alert! 35011 has suspicious activity
{"service":"36012","read":6,"write":1}

• Пример 8
Входные данные
<service="39015" data="ABCDEFGHI" action="write">
<service="40016" data="JKLMNOPQR" action="read">
<service="39015" data="STUVWXYZA" action="write">
<service="40016" data="BCDEFGHIJ" action="read">
<service="39015" data="KLMNOPQRS" action="write">
<service="40016" data="TUVWXYZAB" action="read">
<service="39015" data="CDEFGHIJK" action="write">
<service="40016" data="LMNOPQRST" action="read">
<service="39015" data="UVWXYZABC" action="write">
<service="40016" data="XYZABCDEF" action="read">
<service="39015" data="YZABCDEFG" action="write">
<service="40016" data="ZABCDEFGH" action="write">
<service="39015" data="NEWDATAID" action="write">
<service="40016" data="ANOTHERID" action="read">
Ожидаемый результат
Alert! 39015 has suspicious activity
{"service":"40016","read":6,"write":1}

public interface IWriteServiceActivity
{
    string GetActivityReport();
}

public abstract class Service : IWriteServiceActivity
{
    public string ServiceId { get; }
    public int ReadCount { get; set; }
    public int WriteCount { get; set; }
    protected Service(string serviceId)
    {
        ServiceId = serviceId;
    }
    public void AddRead() => ReadCount++;
    public void AddWrite() => WriteCount++;
    public bool IsSuspicious() => WriteCount >= 0.75 * (ReadCount + WriteCount);
    public abstract string GetActivityReport();
}

public class CorrectService : Service
{
    public CorrectService(string serviceId) : base(serviceId) { }
    public override string GetActivityReport() 
    { 
        //Ваш код
    }
}

public class FishingService : Service
{
    public FishingService(string serviceId) : base(serviceId) { }
    public override string GetActivityReport()
    {
        //Ваш код
    }
}

public class ServerLogAnalyzer
{
    private readonly Dictionary<string, Service> _services = new();

    //Ваш код для регулярного выражения LogRegex

    public ServerLogAnalyzer(IList<string> inputLines)
    {
        foreach(var line in inputLines)
        {
            AddLog(line);
        }
    }

    public void AddLog(string log)
    {
        var match = LogRegex.Match(log);
        if(match.Success)
        {
            //Ваш код
        }
    }

    public List<string> ProcessingServerLogs()
    {
        //Ваш код
    }
}
*/

public interface IWriteServiceActivity
{
    string GetActivityReport();
}
//Требуется разработать класс ServerLogAnalyzer с методом ProcessingServerLogs,
//который выводит информацию об активности сервисов в требуемом формате.
//Реализовать классы CorrectService и FishingSevice, реализующие интерфейс IWriteServiceActivity.
public abstract class Service : IWriteServiceActivity
{
    public string ServiceId { get; }
    public int ReadCount { get; set; }
    public int WriteCount { get; set; }
    protected Service(string serviceId)
    {
        ServiceId = serviceId;
    }
    public void AddRead() => ReadCount++;
    public void AddWrite() => WriteCount++;
    public bool IsSuspicious() => WriteCount >= 0.75 * (ReadCount + WriteCount);
    public abstract string GetActivityReport();
}

public class CorrectService : Service
{
    public CorrectService(string serviceId) : base(serviceId) { }
    public override string GetActivityReport() =>
        JsonSerializer.Serialize(new
        {
            service = ServiceId,
            read = ReadCount,
            write = WriteCount
        });
    //$"{{\"service\":\"{ServiceId}\",\"read\":{ReadCount},\"write\":{WriteCount}}}";
    //{"service":"40016","read":6,"write":1}
}

public class FishingService : Service
{
    public FishingService(string serviceId) : base(serviceId) { }
    public override string GetActivityReport() =>
    $"Alert! {ServiceId} has suspicious activity";
}

public class ServerLogAnalyzer
{
    private readonly Dictionary<string, Service> _services = new();

    //Ваш код для регулярного выражения LogRegex
     Regex LogRegex = new Regex(
         @"^\s*<service=""(?'serviceId'[1-9][0-9]{4})"" data=""(?'dataId'[A-Z]{9})"" action=""(?'actionType'read|write)"">\s*$");

    public ServerLogAnalyzer(IList<string> inputLines)
    {
        foreach (var line in inputLines)
        {
            AddLog(line);
        }
    }

    public void AddLog(string log)
    {
        var match = LogRegex.Match(log);
        if (match.Success)
        {
            //Ваш код
            var values = match.Groups;
            string serviceId = values[1].Value.ToString();
            string dataId = values[2].Value.ToString();
            string actionType = values[3].Value.ToString();

            if(_services.TryGetValue(serviceId, out Service service))
            {
                AddActionType(service, actionType);
            }
            else
            {
                Service newService = new CorrectService(serviceId);
                AddActionType(newService, actionType);
                _services.Add(serviceId, newService);
            }
        }

        static void AddActionType(Service service, string actionType)
        {
            switch (actionType)
            {
                case "read":
                    service.AddRead();
                    break;
                case "write":
                    service.AddWrite();
                    break;
            }
        }
    }

    public List<string> ProcessingServerLogs()
    {
        //Ваш код
        foreach(var pair in _services)
        {
            var servise = pair.Value;
            if (servise.IsSuspicious())
                _services[servise.ServiceId] =
                    new FishingService(servise.ServiceId)
                    {
                        ReadCount = servise.ReadCount,
                        WriteCount = servise.WriteCount
                    };
        }

        if (!_services.Any())
            return new List<string>() { "none" };

        return _services
            .OrderBy(pair => pair.Value.ServiceId)
            .Select(pair => pair.Value.GetActivityReport())
            .ToList();
    }
}