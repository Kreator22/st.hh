using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

Console.ReadLine();

/*
Задача 2 из 4
Популярные компьютерные игры
Средний, Обработка исключений, Обработка исключительных ситуаций, Валидация данных

Администратор маркетплейса компьютерных игр сформировал данные о размещенных играх в формате:
gamelD->Название->Рейтинг->КоличествоСкачиваний.
Службе продвижения сайта требуется информация об играх с высокой, средней и низкой популярностью для стандартизации данных в описании игры на сайте и проведения маркетинговой кампании.
Требуется разработать модуль, который выполняет проверку данных на корректность (проверка останавливается при первой ошибке) и для каждой записи о компьютерной игре выводит информацию в следующем формате:
gamelD:Название:Message,
где Message - это одно из значений (top, middle, low, incorrect data).
Признаки корректности данных:
• строка содержит ровно четыре поля, разделенных ->;
• разделитель без пробелов;
• поле gamelD целое число от 1000 до 9999 включительно;
• поле Название содержит только символы английского алфавита без пробелов и других символов, длина поля — от 5 до 40 символов включительно;
• поле Рейтинг - целое число от 0 до 100 включительно;
• поле КоличествоСкачиваний - целое число от 0 до 10000000 включительно.
Требуется разработать класc ProcesingGames, который содержит метод ValidateGame для
определения корректности данных об игре, метод calculateGameRate и метод gamesReport, который возвращает данные об играх в требуемом формате.

Формат ввода
На вход подаются строки, содержащие информацию об играх, в формате: 
gamelD->Название->Рейтинг->КоличествоСкачиваний.
Количество строк - от 1 до 1000. Гарантируется, что есть хотя бы одна строка.

Формат вывода
Вывести для каждой игры данные в формате:
gamelD:Название:Message, разделитель двоеточие ":" без пробелов.
gamelD - это поле gamelD из исходных данных., Если поле пустое, вывести unknown; 
Название - это поле Название из исходных данных. Если поле пустое, вывести unknown. 
Поле Message принимает следующие значения:
top - если Рейтинг больше либо равен 90 или Количество скачиваний больше 100000; 
middle - если Рейтинг от 65 до 89 включительно или Количество скачиваний от 50000 до 99999 включительно;
low - в остальных случаях;
incorrect data - если в данных обнаружена ошибка. Проверка выполняется до обнаружения первой ошибки.

Пример 1
Входные данные: 
1001->StardewValley->95->50000
1002->Terraria->85->75000 
1003->HollowKnight->72->300000
1004->Celeste->20->15000 
1007->SuperNova->75->25000

Выходные данные: 
1001:StardewValley:top
1002:Terraria:middle
1003:HollowKnight:top
1004:Celeste:low
1007:SuperNova:middle

Пример 2
Входные данные: 
1001->Stardew Valley->95->500000
1002->->85->75000
1003->Hollowknight->92->300000
->->80->45000

Выходные данные: 
1001:Stardew Valley:incorrect data
1002:unknown:incorrect data
1003:Hollowknight:top
unknown:unknown:incorrect data

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Solution
{
    //Ваш код
    public class ProcessingGames
    {
        public IList<string> GameReport(List<string> inputLines)
        {
            //Ваш код
        }

        //Ваш код ValidateGame

        //Ваш код CalculateGameRate
    }
}
*/



public static class ProcessingGames
{
    public static IList<string> GameReport(List<string> inputLines)
    {
        //gamelD->Название->Рейтинг->КоличествоСкачиваний
        List<string> gameReport = new();

        foreach (var inputLine in inputLines)
        {
            var separated = inputLine
                .Split("->", StringSplitOptions.None);

            string
                gameID = separated[0],
                name = separated[1],
                rate = separated[2],
                downloads = separated[3];

            bool isCorrect = ValidateGame(gameID, name, rate, downloads);
            string result = $"{CheckString(gameID)}:{CheckString(name)}:";

            if (isCorrect)
            {
                result += CalculateGameRate(rate, downloads);
            }
            else
            {
                result += "incorrect data";
            }
            gameReport.Add(result);
        }
        return gameReport;

        static string CheckString(string str) =>
                string.IsNullOrEmpty(str) ? "unknown" : str;
    }

    //Ваш код ValidateGame
    static bool ValidateGame(string gameID, string name, string rate, string downloads)
    {
        if (!int.TryParse(gameID, out int _gameID) || _gameID < 1000 || _gameID > 9999)
            return false;

        if (!Regex.IsMatch(name, @"^[a-zA-Z]{5,40}$"))
            return false;

        if (!int.TryParse(rate, out int _rate) || _rate < 0 || _rate > 100)
            return false;

        if (!int.TryParse(downloads, out int _downloads) || _downloads < 0 || _downloads > 10000000)
            return false;

        return true;
    }


    //Ваш код CalculateGameRate
    static string CalculateGameRate(string rate, string downloads)
    {
        int _rate = int.Parse(rate);
        int _downloads = int.Parse(downloads);

        rate = (_rate, _downloads) switch
        {
            ( >= 90, _) => "top",
            (_, >= 100000) => "top",
            ( >= 65 and < 90, _) => "middle",
            (_, >= 50000 and < 100000) => "middle",
            (_, _) => "low"
        };
        return rate;
    }
}
