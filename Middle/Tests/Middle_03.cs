using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public sealed class Middle_03
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
            Пример 1 - неверный пример, противоречит заданию (произошла обработка некорректных входных данных). 
            Входные данные: 
            <service="10001" data="ABCDEFGHI" action="read">
            <service="10001" data="JKLMNOPQR" action="read">
            <service="10001" data="STUVWXYZa" action="write"> 
            <service="10002" data="bcdefghij" action="read">
            Выходные данные: 
            {"service":"10001","read":2,"write":1}
            {"service":"10002","read":1,"write":0}
            Правильный вариант выходных данных:
            {"service":"10001","read":2,"write":0}
            */
            List<string> input =
                ["<service=\"10001\" data=\"ABCDEFGHI\" action=\"read\">",
                "<service=\"10001\" data=\"JKLMNOPQR\" action=\"read\">",
                "<service=\"10001\" data=\"STUVWXYZa\" action=\"write\"> ",
                "<service=\"10002\" data=\"bcdefghij\" action=\"read\">"];

            List<string> expected =
                ["{\"service\":\"10001\",\"read\":2,\"write\":0}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            /*
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
            {"service":"20002","read":2,"write":1}
            */

            List<string> input =
                ["<service=\"20001\" data=\"AAAAAAAAA\" action=\"write\">",
                "<service=\"20001\" data=\"BBBBBBBBB\" action=\"write\">",
                "<service=\"20001\" data=\"CCCCCCCCC\" action=\"write\"> ",
                "<service=\"20001\" data=\"DDDDDDDDD\" action=\"read\">",
                "<service=\"20002\" data=\"EEEEEEEEE\" action=\"read\">",
                "<service=\"20002\" data=\"FFFFFFFFF\" action=\"read\">",
                "<service=\"20002\" data=\"GGGGGGGGG\" action=\"write\">"];

            List<string> expected =
                ["Alert! 20001 has suspicious activity",
                "{\"service\":\"20002\",\"read\":2,\"write\":1}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            /*
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
            */

            List<string> input =
                ["<service=\"31007\" data=\"ABCDEFGHI\" action=\"write\">",
                "<service=\"32008\" data=\"JKLMNOPQR\" action=\"read\">",
                "<service=\"31007\" data=\"STUVWXYZA\" action=\"write\">",
                "<service=\"32008\" data=\"BCDEFGHIJ\" action=\"read\">",
                "<service=\"31007\" data=\"KLMNOPQRS\" action=\"write\">",
                "<service=\"32008\" data=\"TUVWXYZAB\" action=\"read\">",
                "<service=\"31007\" data=\"CDEFGHIJK\" action=\"write\">",
                "<service=\"32008\" data=\"LMNOPQRST\" action=\"read\">"];

            List<string> expected =
                ["Alert! 31007 has suspicious activity",
                "{\"service\":\"32008\",\"read\":4,\"write\":0}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {

            /*
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
            */

            string Raw = "<service=\"41017\" data=\"ABCDEFGHI\" action=\"write\">\r\n            <service=\"42018\" data=\"JKLMNOPQR\" action=\"read\">\r\n            <service=\"41017\" data=\"STUVWXYZA\" action=\"write\">\r\n            <service=\"42018\" data=\"BCDEFGHIJ\" action=\"read\">\r\n            <service=\"41017\" data=\"KLMNOPQRS\" action=\"write\">\r\n            <service=\"42018\" data=\"TUVWXYZAB\" action=\"read\">\r\n            <service=\"41017\" data=\"CDEFGHIJK\" action=\"write\">\r\n            <service=\"42018\" data=\"LMNOPQRST\" action=\"read\">\r\n            <service=\"41017\" data=\"UVWXYZABC\" action=\"write\">\r\n            <service=\"42018\" data=\"XYZABCDEF\" action=\"read\">\r\n            <service=\"41017\" data=\"YZABCDEFG\" action=\"write\">\r\n            <service=\"42018\" data=\"ZABCDEFGH\" action=\"read\">\r\n            <service=\"41017\" data=\"NEWDATAID\" action=\"write\">\r\n            <service=\"42018\" data=\"ANOTHERID\" action=\"write\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();
                
            List<string> expected =
                ["Alert! 41017 has suspicious activity",
                "{\"service\":\"42018\",\"read\":6,\"write\":1}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {

            /*
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
            */

            string Raw = "<service=\"29005\" data=\"ABCDEFGHI\" action=\"read\">\r\n            <service=\"30006\" data=\"JKLMNOPQR\" action=\"write\">\r\n            <service=\"29005\" data=\"STUVWXYZA\" action=\"read\">\r\n            <service=\"30006\" data=\"BCDEFGHIJ\" action=\"write\">\r\n            <service=\"29005\" data=\"KLMNOPQRS\" action=\"read\">\r\n            <service=\"30006\" data=\"TUVWXYZAB\" action=\"write\">\r\n            <service=\"29005\" data=\"CDEFGHIJK\" action=\"read\">\r\n             <service=\"30006\" data=\"LMNOPQRST\" action=\"write\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["{\"service\":\"29005\",\"read\":4,\"write\":0}",
                "Alert! 30006 has suspicious activity"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            /*
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
            */

            string Raw = "<service=\"33009\" data=\"ABCDEFGHI\" action=\"write\">\r\n            <service=\"33009\" data=\"JKLMNOPQR\" action=\"write\">\r\n            <service=\"33009\" data=\"STUVWXYZA\" action=\"write\">\r\n            <service=\"33009\" data=\"BCDEFGHIJ\" action=\"read\">\r\n            <service=\"33009\" data=\"KLMNOPQRS\" action=\"write\">\r\n             <service=\"33009\" data=\"TUVWXYZAB\" action=\"write\">\r\n            <service=\"33009\" data=\"CDEFGHIJK\" action=\"write\">\r\n            <service=\"33009\" data=\"LMNOPQRST\" action=\"write\">\r\n            <service=\"33009\" data=\"UVWXYZABC\" action=\"read\">\r\n            <service=\"33009\" data=\"XYZABCDEF\" action=\"write\">\r\n            <service=\"33009\" data=\"YZABCDEFG\" action=\"write\">\r\n            <service=\"33009\" data=\"ZABCDEFGH\" action=\"read\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["Alert! 33009 has suspicious activity"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod7()
        {
            /*
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
            */

            string Raw = "<service=\"37013\" data=\"ABCDEFGHI\" action=\"write\">\r\n            <service=\"38014\" data=\"JKLMNOPQR\" action=\"read\">\r\n            <service=\"37013\" data=\"STUVWXYZA\" action=\"write\">\r\n            <service=\"38014\" data=\"BCDEFGHIJ\" action=\"read\">\r\n            <service=\"37013\" data=\"KLMNOPQRS\" action=\"write\">\r\n            <service=\"38014\" data=\"TUVWXYZAB\" action=\"read\">\r\n            <service=\"37013\" data=\"CDEFGHIJK\" action=\"write\">\r\n            <service=\"38014\" data=\"LMNOPQRST\" action=\"write\">\r\n            <service=\"37013\" data=\"UVWXYZABC\" action=\"write\">\r\n            <service=\"38014\" data=\"XYZABCDEF\" action=\"read\">\r\n            <service=\"37013\" data=\"YZABCDEFG\" action=\"write\">\r\n            <service=\"38014\" data=\"ZABCDEFGH\" action=\"read\">\r\n            <service=\"37013\" data=\"NEWDATAID\" action=\"write\">\r\n            <service=\"38014\" data=\"ANOTHERID\" action=\"read\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["Alert! 37013 has suspicious activity",
                "{\"service\":\"38014\",\"read\":6,\"write\":1}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod8()
        {
            /*
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
            */

            string Raw = "<service=\"35011\" data=\"ABCDEFGHI\" action=\"write\">\r\n            <service=\"36012\" data=\"JKLMNOPQR\" action=\"read\">\r\n            <service=\"35011\" data=\"STUVWXYZA\" action=\"write\">\r\n            <service=\"36012\" data=\"BCDEFGHIJ\" action=\"read\">\r\n            <service=\"35011\" data=\"KLMNOPQRS\" action=\"write\">\r\n            <service=\"36012\" data=\"TUVWXYZAB\" action=\"read\">\r\n            <service=\"35011\" data=\"CDEFGHIJK\" action=\"write\">\r\n            <service=\"36012\" data=\"LMNOPQRST\" action=\"read\">\r\n            <service=\"35011\" data=\"UVWXYZABC\" action=\"write\">\r\n            <service=\"36012\" data=\"XYZABCDEF\" action=\"write\">\r\n            <service=\"35011\" data=\"YZABCDEFG\" action=\"write\">\r\n            <service=\"36012\" data=\"ZABCDEFGH\" action=\"read\">\r\n            <service=\"35011\" data=\"NEWDATAID\" action=\"write\">\r\n            <service=\"36012\" data=\"ANOTHERID\" action=\"read\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["Alert! 35011 has suspicious activity",
                "{\"service\":\"36012\",\"read\":6,\"write\":1}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod9()
        {
            /*
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
            */

            string Raw = "<service=\"39015\" data=\"ABCDEFGHI\" action=\"write\">\r\n            <service=\"40016\" data=\"JKLMNOPQR\" action=\"read\">\r\n            <service=\"39015\" data=\"STUVWXYZA\" action=\"write\">\r\n            <service=\"40016\" data=\"BCDEFGHIJ\" action=\"read\">\r\n            <service=\"39015\" data=\"KLMNOPQRS\" action=\"write\">\r\n            <service=\"40016\" data=\"TUVWXYZAB\" action=\"read\">\r\n            <service=\"39015\" data=\"CDEFGHIJK\" action=\"write\">\r\n            <service=\"40016\" data=\"LMNOPQRST\" action=\"read\">\r\n            <service=\"39015\" data=\"UVWXYZABC\" action=\"write\">\r\n            <service=\"40016\" data=\"XYZABCDEF\" action=\"read\">\r\n            <service=\"39015\" data=\"YZABCDEFG\" action=\"write\">\r\n            <service=\"40016\" data=\"ZABCDEFGH\" action=\"write\">\r\n            <service=\"39015\" data=\"NEWDATAID\" action=\"write\">\r\n            <service=\"40016\" data=\"ANOTHERID\" action=\"read\">";

            List<string> input = Raw
                .Split("\r\n")
                .Select(str => str.Trim())
                .ToList();

            List<string> expected =
                ["Alert! 39015 has suspicious activity",
                "{\"service\":\"40016\",\"read\":6,\"write\":1}"];

            ServerLogAnalyzer serverLogAnalyzer = new(input);
            var actual = serverLogAnalyzer.ProcessingServerLogs();
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
