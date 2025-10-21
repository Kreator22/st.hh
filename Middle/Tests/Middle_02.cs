

namespace Tests
{
    [TestClass]
    public sealed class Middle_02
    {
        [TestMethod]
        public void TestMethod1() 
        { 
            List<string> input =
                ["1001->StardewValley->95->50000",
                "1002->Terraria->85->75000",
                "1003->HollowKnight->72->300000",
                "1004->Celeste->20->15000",
                "1007->SuperNova->75->25000"];

            List<string> expected =
                ["1001:StardewValley:top",
                "1002:Terraria:middle",
                "1003:HollowKnight:top",
                "1004:Celeste:low",
                "1007:SuperNova:middle"];

            var actual = ProcessingGames.GameReport(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<string> input =
                ["1001->Stardew Valley->95->500000",
                "1002->->85->75000",
                "1003->Hollowknight->92->300000",
                "->->80->45000"];

            List<string> expected =
                ["1001:Stardew Valley:incorrect data",
                "1002:unknown:incorrect data",
                "1003:Hollowknight:top",
                "unknown:unknown:incorrect data"];

            var actual = ProcessingGames.GameReport(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<string> input =
                ["4012->Tindo->0->1000",
                "4023->Raadi->15->12000",
                "4034->Tita->25->23000",
                "4045->Minzi->35->34000",
                "4056->Jengo->45->45000",
                "4067->Siti2->55->56000",
                "4078->Kali->65->67000",
                "4089->Nala->78->78000"];

            List<string> expected =
                ["4012:Tindo:low",
                "4023:Raadi:low",
                "4034:Tita:incorrect data",
                "4045:Minzi:low",
                "4056:Jengo:low",
                "4067:Siti2:incorrect data",
                "4078:Kali:incorrect data",
                "4089:Nala:incorrect data"];

            var actual = ProcessingGames.GameReport(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<string> input =
                ["4999->Tindo->78->78000",
                "4888->Raadi->-1->1000",
                "4777->TitaUU->-50->50000",
                "4666->Minzi->12->-12000",
                "4555->Дженго->34->34000",
                "4444->Siti->56->56000",
                "4333->Kali->78->78000",
                "4222->Nala->10->10000"];

            List<string> expected =
                ["4999:Tindo:middle",
                "4888:Raadi:incorrect data",
                "4777:TitaUU:incorrect data",
                "4666:Minzi:incorrect data",
                "4555:Дженго:incorrect data",
                "4444:Siti:incorrect data",
                "4333:Kali:incorrect data",
                "4222:Nala:incorrect data"];

            var actual = ProcessingGames.GameReport(input).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
