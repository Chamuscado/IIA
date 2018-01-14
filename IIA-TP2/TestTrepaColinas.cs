using System;
using System.Diagnostics;
using IIA_TP2.Algoritmos;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TestTrepaColinas
    {
        private Data data;
        private string outputfile;
        private int tests;
        private int v;

        public TestTrepaColinas(Data data, string outputfile, int tests, int v)
        {
            this.data = data;
            this.outputfile = outputfile;
            this.tests = tests;
            this.data.moedas.Sort();
        }


        public void run()
        {
            CSVFile csvFile = new CSVFile();
            TrepaColinas tr = getNewIntace();
            csvFile.Write(0, 0, tr.descricao());
            csvFile.addCabecalhoTC(data);
            Hipotese best = null;
            for (int i = 0; i < tests; i++)
            {
                tr = getNewIntace();
                tr.run();
                Hipotese hip = tr.getBest();
                if ((best == null || best.eval > hip.eval) && hip.valido == 0) // guardar a melhor
                {
                    best = new Hipotese(hip, "Melhor", hip.interacao);
                    best.evaluate();
                }

                //Console.Out.WriteLine(hip.ToString2() + " : " + hip.ToString());
                //System.Threading.Thread.Sleep(100);

                csvFile.addBestTrepaColinas(tr);
            }
            if (best != null)
            {
                best.evaluate();
                csvFile.addHipoteseTC(best);
            }
            else csvFile.Write(0, -1, "Nao foi encontrada nenhuma solucao");
            csvFile.toFile(outputfile);
        }

        private TrepaColinas getNewIntace()
        {
            TrepaColinas instace;
            switch (v)
            {
                case 1:
                    instace = new TrepaColinasV2(data.Clone());
                    break;
                case 2:
                    instace = new TrepaColinasV3(data.Clone());
                    break;
                default:
                    instace = new TrepaColinas(data.Clone());
                    break;
            }
            instace.log = false;
            return instace;
        }
    }
}