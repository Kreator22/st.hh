using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{

    [TestClass]
    public sealed class Middle_04
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
            Пример 1
            Входные данные: 
            SELL::11111::Smith::2000000::01-01-2024
            SELL::11112::Smithson::3000000::01-01-2024
            BUY::11111::Johnson::2500000::02-01-2024
            BUY::11112::Peters::1800000::03-01-2024
            Выходные данные:
            {"objectID":"11111","lastName":"Johnson","Price":2500000}
            Peters (11112;1800000)
            */

            string Raw = "SELL::11111::Smith::2000000::01-01-2024\r\n            SELL::11112::Smithson::3000000::01-01-2024\r\n            BUY::11111::Johnson::2500000::02-01-2024\r\n            BUY::11112::Peters::1800000::03-01-2024";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["{\"objectID\":\"11111\",\"lastName\":\"Johnson\",\"Price\":2500000}",
                "Peters (11112;1800000)"];

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*
            Пример 2
            Входные данные:
            BUY::12345::Smith::2000000::01-01-2023
            SELL::12345:: Johnson::2500000::02-01-2023
            SELL::12345::Peters::1800000::03-01-2023
            Выходные данные: 
            none
            */

            string Raw = "BUY::12345::Smith::2000000::01-01-2023\r\n            SELL::12345:: Johnson::2500000::02-01-2023\r\n            SELL::12345::Peters::1800000::03-01-2023";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["none"];

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            /*
            • Пример 1
            Входные данные
            SELL::130001::Bondarenko::17000000::01-01-2024
            BUY::130001::Marchenko::17100000::02-01-2024
            SELL::130002:: Rudenko::17200000::03-01-2024
            BUY::130002::Lukyanenko::17300000::04-01-2024
            SELL::130003::Kuzmenko::17400000::05-01-2024
            BUY::130003::Kovalev::17500000::06-01-2024
            SELL::130004::Kozlovsky::17600000::07-01-2024
            BUY::130004::Zaytsev::17700000::08-01-2024
            SELL::130005::Kravtsov::17800000::09-01-2024
            BUY::130005::Potapenko::17900000::10-01-2024
            SELL::130006::Kornienko::18000000::11-01-2024
            BUY::130006::Omelchenko::18100000::12-01-2024
            SELL::130007::Gomenyuk::18200000::13-01-2024
            BUY::130007::Kucher::18300000::14-01-2024
            Ожидаемый результат
            {"objectID":"130001","lastName":"Marchenko","Price":17100000}
            {"objectID":"130002","lastName":"Lukyanenko","Price":17300000}
            {"objectID":"130003","lastName":"Kovalev","Price":17500000}
            {"objectID":"130004","lastName":"Zaytsev","Price":17700000}
            {"objectID":"130005","lastName":"Potapenko","Price":17900000}
            {"objectID":"130006","lastName":"Omelchenko","Price":18100000}
            {"objectID":"130007","lastName":"Kucher","Price":18300000}
            */

            string RawInput = "SELL::130001::Bondarenko::17000000::01-01-2024\r\n            BUY::130001::Marchenko::17100000::02-01-2024\r\n            SELL::130002:: Rudenko::17200000::03-01-2024\r\n            BUY::130002::Lukyanenko::17300000::04-01-2024\r\n            SELL::130003::Kuzmenko::17400000::05-01-2024\r\n            BUY::130003::Kovalev::17500000::06-01-2024\r\n            SELL::130004::Kozlovsky::17600000::07-01-2024\r\n            BUY::130004::Zaytsev::17700000::08-01-2024\r\n            SELL::130005::Kravtsov::17800000::09-01-2024\r\n            BUY::130005::Potapenko::17900000::10-01-2024\r\n            SELL::130006::Kornienko::18000000::11-01-2024\r\n            BUY::130006::Omelchenko::18100000::12-01-2024\r\n            SELL::130007::Gomenyuk::18200000::13-01-2024\r\n            BUY::130007::Kucher::18300000::14-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "{\"objectID\":\"130001\",\"lastName\":\"Marchenko\",\"Price\":17100000}\r\n            {\"objectID\":\"130002\",\"lastName\":\"Lukyanenko\",\"Price\":17300000}\r\n            {\"objectID\":\"130003\",\"lastName\":\"Kovalev\",\"Price\":17500000}\r\n            {\"objectID\":\"130004\",\"lastName\":\"Zaytsev\",\"Price\":17700000}\r\n            {\"objectID\":\"130005\",\"lastName\":\"Potapenko\",\"Price\":17900000}\r\n            {\"objectID\":\"130006\",\"lastName\":\"Omelchenko\",\"Price\":18100000}\r\n            {\"objectID\":\"130007\",\"lastName\":\"Kucher\",\"Price\":18300000}";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            /*
            • Пример 2
            Входные данные
            SELL::80001::Uriev::8000000::01-01-2024
            SELL::80002:: Fadeev::8100000::02-01-2024
            SELL::80003::Haritonov::8200000::03-01-2024
            SELL::80004::Tsarev::8300000::04-01-2024
            SELL::80005::Eduardov::8400000::05-01-2024
            SELL::80006::Yuriev::8500000::06-01-2024
            SELL::80007::Yakovlev::8600000::07-01-2024
            SELL::80008::Yaroslavov::8700000::08-01-2024
            Ожидаемый результат
            none
            */

            string RawInput = "SELL::80001::Uriev::8000000::01-01-2024\r\n            SELL::80002:: Fadeev::8100000::02-01-2024\r\n            SELL::80003::Haritonov::8200000::03-01-2024\r\n            SELL::80004::Tsarev::8300000::04-01-2024\r\n            SELL::80005::Eduardov::8400000::05-01-2024\r\n            SELL::80006::Yuriev::8500000::06-01-2024\r\n            SELL::80007::Yakovlev::8600000::07-01-2024\r\n            SELL::80008::Yaroslavov::8700000::08-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "none";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {
            /*
            • Пример 3
            Входные данные
            SELL::70001::Leonidov::7000000::01-01-2024
            BUY::70001::Matveev::7100000::02-01-2024
            SELL::70002::Nikitin::7200000::03-01-2024
            BUY::70002::Olegov::7300000::04-01-2024
            SELL::70003::Petrovich::7400000::05-01-2024
            BUY::70003::Rostislavov::7500000::06-01-2024
            SELL::70004::Stanislavov::7600000::07-01-2024
            BUY::70004::Tarasov::7700000::08-01-2024
            Ожидаемый результат
            {"objectID":"70001","lastName":"Matveev","Price":7100000}
            {"objectID":"70002","lastName":"Olegov","Price":7300000}
            {"objectID":"70003","lastName":"Rostislavov","Price":7500000}
            {"objectID":"70004","lastName":"Tarasov","Price":7700000}
            */

            string RawInput = "SELL::70001::Leonidov::7000000::01-01-2024\r\n            BUY::70001::Matveev::7100000::02-01-2024\r\n            SELL::70002::Nikitin::7200000::03-01-2024\r\n            BUY::70002::Olegov::7300000::04-01-2024\r\n            SELL::70003::Petrovich::7400000::05-01-2024\r\n            BUY::70003::Rostislavov::7500000::06-01-2024\r\n            SELL::70004::Stanislavov::7600000::07-01-2024\r\n            BUY::70004::Tarasov::7700000::08-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "{\"objectID\":\"70001\",\"lastName\":\"Matveev\",\"Price\":7100000}\r\n            {\"objectID\":\"70002\",\"lastName\":\"Olegov\",\"Price\":7300000}\r\n            {\"objectID\":\"70003\",\"lastName\":\"Rostislavov\",\"Price\":7500000}\r\n            {\"objectID\":\"70004\",\"lastName\":\"Tarasov\",\"Price\":7700000}";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            /*
            • Пример 4
            Входные данные
            SELL::90001::Zaitsev::9000000::01-01-2024
            BUY::90001::Kotov::9100000::02-01-2024
            SELL::90002::Volkov::9200000::03-01-2024
            BUY::90002::Medvedev::9300000::04-01-2024
            SELL::90003::Sokolov::9400000::05-01-2024
            BUY::90003::Vorobiev::9500000::06-01-2024
            SELL::90004::Sorokin::9600000::07-01-2024
            BUY::90004::Kuzmin::9700000::08-01-2024
            SELL::90005::llyin::9800000::09-01-2024
            BUY::90005::Gusev::9900000::10-01-2024
            SELL::90006::Vinogradov::10000000::11-01-2024
            BUY::90006::Bogdanov::10100000::12-01-2024
            Ожидаемый результат
            {"objectID":"90001","lastName":"Kotov","Price":9100000}
            {"objectID":"90002","lastName":"Medvedev","Price":9300000}
            {"objectID":"90003","lastName":"Vorobiev","Price":9500000}
            {"objectID":"90004","lastName":"Kuzmin","Price":9700000}
            {"objectID":"90005","lastName":"Gusev","Price":9900000}
            {"objectID":"90006","lastName":"Bogdanov","Price":10100000}
            */

            string RawInput = "SELL::90001::Zaitsev::9000000::01-01-2024\r\n            BUY::90001::Kotov::9100000::02-01-2024\r\n            SELL::90002::Volkov::9200000::03-01-2024\r\n            BUY::90002::Medvedev::9300000::04-01-2024\r\n            SELL::90003::Sokolov::9400000::05-01-2024\r\n            BUY::90003::Vorobiev::9500000::06-01-2024\r\n            SELL::90004::Sorokin::9600000::07-01-2024\r\n            BUY::90004::Kuzmin::9700000::08-01-2024\r\n            SELL::90005::llyin::9800000::09-01-2024\r\n            BUY::90005::Gusev::9900000::10-01-2024\r\n            SELL::90006::Vinogradov::10000000::11-01-2024\r\n            BUY::90006::Bogdanov::10100000::12-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "{\"objectID\":\"90001\",\"lastName\":\"Kotov\",\"Price\":9100000}\r\n            {\"objectID\":\"90002\",\"lastName\":\"Medvedev\",\"Price\":9300000}\r\n            {\"objectID\":\"90003\",\"lastName\":\"Vorobiev\",\"Price\":9500000}\r\n            {\"objectID\":\"90004\",\"lastName\":\"Kuzmin\",\"Price\":9700000}\r\n            {\"objectID\":\"90005\",\"lastName\":\"Gusev\",\"Price\":9900000}\r\n            {\"objectID\":\"90006\",\"lastName\":\"Bogdanov\",\"Price\":10100000}";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod7()
        {
            /*
            • Пример 5
            Входные данные
            SELL::140001::Lisitsyn::19000000::01-01-2024
            SELL::140002::Maslov::19100000::02-01-2024
            SELL::140003::Nosov::19200000::03-01-2024
            SELL::140004::Orlov::19300000::04-01-2024
            SELL::140005::Panov::19400000::05-01-2024
            SELL::140006::Rogov::19500000::06-01-2024
            SELL::140007::Savin::19600000::07-01-2024
            SELL::140008::Trofimov::19700000::08-01-2024
            SELL::140009::Ushakov::19800000::09-01-2024
            SELL::140010::Frolov::19900000::10-01-2024
            SELL::140011::Khromov::20000000::11-01-2024
            SELL::140012::Chernov::20100000::12-01-2024
            SELL::140013::Shcherbakov::20200000::13-01-2024
            SELL::140014::Yudin::20300000::14-01-2024
            Ожидаемый результат
            none
            */

            string RawInput = "SELL::140001::Lisitsyn::19000000::01-01-2024\r\n            SELL::140002::Maslov::19100000::02-01-2024\r\n            SELL::140003::Nosov::19200000::03-01-2024\r\n            SELL::140004::Orlov::19300000::04-01-2024\r\n            SELL::140005::Panov::19400000::05-01-2024\r\n            SELL::140006::Rogov::19500000::06-01-2024\r\n            SELL::140007::Savin::19600000::07-01-2024\r\n            SELL::140008::Trofimov::19700000::08-01-2024\r\n            SELL::140009::Ushakov::19800000::09-01-2024\r\n            SELL::140010::Frolov::19900000::10-01-2024\r\n            SELL::140011::Khromov::20000000::11-01-2024\r\n            SELL::140012::Chernov::20100000::12-01-2024\r\n            SELL::140013::Shcherbakov::20200000::13-01-2024\r\n            SELL::140014::Yudin::20300000::14-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "none";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod8()
        {
            /*
            • Пример 6
            Входные данные
            SELL::100001::Belov::11000000::01-01-2024
            SELL::100002::Komarov::11100000::02-01-2024
            SELL::100003::Krylov::11200000::03-01-2024
            SELL::100004::Egorov::11300000::04-01-2024
            SELL::100005::Titov::11400000::05-01-2024
            SELL::100006::Simonov::11500000::06-01-2024
            SELL::100007::Fomin::11600000::07-01-2024
            SELL::100008::Davydov::11700000::08-01-2024
            SELL::100009::Zhukov::11800000::09-01-2024
            SELL::100010::Klimov::11900000::10-01-2024
            SELL::100011::Karpov::12000000::11-01-2024
            SELL::100012::Afanasiev::12100000::12-01-2024
            Ожидаемый результат
            none
            */

            string RawInput = "SELL::100001::Belov::11000000::01-01-2024\r\n            SELL::100002::Komarov::11100000::02-01-2024\r\n            SELL::100003::Krylov::11200000::03-01-2024\r\n            SELL::100004::Egorov::11300000::04-01-2024\r\n            SELL::100005::Titov::11400000::05-01-2024\r\n            SELL::100006::Simonov::11500000::06-01-2024\r\n            SELL::100007::Fomin::11600000::07-01-2024\r\n            SELL::100008::Davydov::11700000::08-01-2024\r\n            SELL::100009::Zhukov::11800000::09-01-2024\r\n            SELL::100010::Klimov::11900000::10-01-2024\r\n            SELL::100011::Karpov::12000000::11-01-2024\r\n            SELL::100012::Afanasiev::12100000::12-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "none";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod9()
        {
            /*
            • Пример 7
            Входные данные
            SELL::120001::Lazarenko::15000000::01-01-2024
            SELL::120002::Vasilenko::15100000::02-01-2024
            BUY::120001::Pavlenko::15200000::03-01-2024
            BUY::120002::Kostenko::15300000::04-01-2024
            SELL::120003::Zakharenko::15400000::05-01-2024
            BUY::120003::Goncharuk::15500000::06-01-2024
            SELL::120004::Antonenko::15600000::07-01-2024
            BUY::120004::Vasilyuk::15700000::08-01-2024
            SELL::120005::Tarasenko::15800000::09-01-2024
            BUY::120005::Kovalchuk::15900000::10-01-2024
            SELL::120006::Kravchuk::16000000::11-01-2024
            BUY::120006::Shevchuk::16100000::12-01-2024
            SELL::120007::Ivanchenko::16200000::13-01-2024
            BUY::120007::Semenyuk::16300000::14-01-2024
            Ожидаемый результат
            {"objectID":"120001","lastName":"Pavlenko","Price":15200000}
            {"objectID":"120002","lastName":"Kostenko","Price":15300000}
            {"objectID":"120003","lastName":"Goncharuk","Price":15500000}
            {"objectID":"120004","lastName":"Vasilyuk","Price":15700000}
            {"objectID":"120005","lastName":"Kovalchuk","Price":15900000}
            {"objectID":"120006","lastName":"Shevchuk","Price":16100000}
            {"objectID":"120007","lastName":"Semenyuk","Price":16300000}
            */

            string RawInput = "SELL::120001::Lazarenko::15000000::01-01-2024\r\n            SELL::120002::Vasilenko::15100000::02-01-2024\r\n            BUY::120001::Pavlenko::15200000::03-01-2024\r\n            BUY::120002::Kostenko::15300000::04-01-2024\r\n            SELL::120003::Zakharenko::15400000::05-01-2024\r\n            BUY::120003::Goncharuk::15500000::06-01-2024\r\n            SELL::120004::Antonenko::15600000::07-01-2024\r\n            BUY::120004::Vasilyuk::15700000::08-01-2024\r\n            SELL::120005::Tarasenko::15800000::09-01-2024\r\n            BUY::120005::Kovalchuk::15900000::10-01-2024\r\n            SELL::120006::Kravchuk::16000000::11-01-2024\r\n            BUY::120006::Shevchuk::16100000::12-01-2024\r\n            SELL::120007::Ivanchenko::16200000::13-01-2024\r\n            BUY::120007::Semenyuk::16300000::14-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "{\"objectID\":\"120001\",\"lastName\":\"Pavlenko\",\"Price\":15200000}\r\n            {\"objectID\":\"120002\",\"lastName\":\"Kostenko\",\"Price\":15300000}\r\n            {\"objectID\":\"120003\",\"lastName\":\"Goncharuk\",\"Price\":15500000}\r\n            {\"objectID\":\"120004\",\"lastName\":\"Vasilyuk\",\"Price\":15700000}\r\n            {\"objectID\":\"120005\",\"lastName\":\"Kovalchuk\",\"Price\":15900000}\r\n            {\"objectID\":\"120006\",\"lastName\":\"Shevchuk\",\"Price\":16100000}\r\n            {\"objectID\":\"120007\",\"lastName\":\"Semenyuk\",\"Price\":16300000}";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod10()
        {
            /*
            • Пример 8
            Входные данные
            SELL::110001::Vlasov::13000000::01-01-2024
            BUY::110001::Melnikov::13100000::02-01-2024
            SELL::110002::Romanenko::13200000::03-01-2024
            BUY::110002::Gavrilov::13300000::04-01-2024
            SELL::110003::Yushchenko::13400000::05-01-2024
            BUY::110003::Lysenko::13500000::06-01-2024
            SELL::110004::Shevchenko::13600000::07-01-2024
            BUY::110004::Kovalenko::13700000::08-01-2024
            SELL::110005::Boyko::13800000::09-01-2024
            BUY::110005::Tkachenko::13900000::10-01-2024
            SELL::110006:: Kravchenko::14000000::11-01-2024
            BUY::110006::Savchenko::14100000::12-01-2024
            SELL::110007::Oliynyk::14200000::13-01-2024
            BUY::110007::Polishchuk::14300000::14-01-2024
            Ожидаемый результат
            {"objectID":"110001","lastName":"Melnikov","Price":13100000}
            {"objectID":"110002","lastName":"Gavrilov","Price":13300000}
            {"objectID":"110003","lastName":"Lysenko","Price":13500000}
            {"objectID":"110004","lastName":"Kovalenko","Price":13700000}
            {"objectID":"110005","lastName":"Tkachenko","Price":13900000}
            {"objectID":"110006","lastName":"Savchenko","Price":14100000}
            {"objectID":"110007","lastName":"Polishchuk","Price":14300000}
            */

            string RawInput = "SELL::110001::Vlasov::13000000::01-01-2024\r\n            BUY::110001::Melnikov::13100000::02-01-2024\r\n            SELL::110002::Romanenko::13200000::03-01-2024\r\n            BUY::110002::Gavrilov::13300000::04-01-2024\r\n            SELL::110003::Yushchenko::13400000::05-01-2024\r\n            BUY::110003::Lysenko::13500000::06-01-2024\r\n            SELL::110004::Shevchenko::13600000::07-01-2024\r\n            BUY::110004::Kovalenko::13700000::08-01-2024\r\n            SELL::110005::Boyko::13800000::09-01-2024\r\n            BUY::110005::Tkachenko::13900000::10-01-2024\r\n            SELL::110006:: Kravchenko::14000000::11-01-2024\r\n            BUY::110006::Savchenko::14100000::12-01-2024\r\n            SELL::110007::Oliynyk::14200000::13-01-2024\r\n            BUY::110007::Polishchuk::14300000::14-01-2024";

            List<string> input = FormatRaw(RawInput);

            string RawExpected = "{\"objectID\":\"110001\",\"lastName\":\"Melnikov\",\"Price\":13100000}\r\n            {\"objectID\":\"110002\",\"lastName\":\"Gavrilov\",\"Price\":13300000}\r\n            {\"objectID\":\"110003\",\"lastName\":\"Lysenko\",\"Price\":13500000}\r\n            {\"objectID\":\"110004\",\"lastName\":\"Kovalenko\",\"Price\":13700000}\r\n            {\"objectID\":\"110005\",\"lastName\":\"Tkachenko\",\"Price\":13900000}\r\n            {\"objectID\":\"110006\",\"lastName\":\"Savchenko\",\"Price\":14100000}\r\n            {\"objectID\":\"110007\",\"lastName\":\"Polishchuk\",\"Price\":14300000}";

            List<string> expected = FormatRaw(RawExpected);

            RealEstateSystem realEstateSystem = new();
            var actual = realEstateSystem.ProcessingInputLines(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        List<string> FormatRaw(string raw) =>
            raw
            .Split("\r\n")
            .Select(str => str.Trim())
            .ToList();
    }    
}
