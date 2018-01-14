using IIA_TP2.Algoritmos;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TestHibrido
    {
        private Data data;
        private string outputfile;
        private int tests;
        private int v;

        public TestHibrido(Data data, string outputfile, int tests, int v, double pc, double pm, int gers,
            int popSize)
        {
            this.data = data;
            this.data.moedas.Sort();
            this.outputfile = outputfile;
            this.tests = tests;
            this.v = v;
            this.popSize = popSize;
            this.gers = gers;
            this.pm = pm;
            this.pc = pc;
        }

        public void run()
        {
            CSVFile csvFile = new CSVFile();
            var evo = getNewIntace();
            csvFile.Write(0, 0, evo.descricao());
            csvFile.addCabecalhoH(data);
            Hipotese bestE = null;
            Hipotese bestT = null;
            for (int i = 0; i < tests; i++)
            {
                evo = getNewIntace();
                evo.run();
                Hipotese hip = evo.getBest();
                if (bestT == null || bestT.eval > hip.eval) // guardar a melhor
                {
                    bestT = new Hipotese(evo.solTrepaColinas, "Melhor",i);
                    bestE = new Hipotese(evo.solEHibrido, "Melhor", i);
                    bestT.evaluate();
                    bestE.evaluate();
                }

                csvFile.addBestHibrido(evo);
            }

            bestE.evaluate();bestT.evaluate();
            csvFile.addHipoteseH(bestE, bestT);
            csvFile.toFile(outputfile);
        }

        private Hibrido getNewIntace()
        {
            var evo = new EvolutivoV2(data.Clone(), pc, pm, gers, popSize);
            var tr = new TrepaColinasV3(data);
            evo.log = false;
            tr.log = false;
            var hib = new Hibrido(data.Clone(),evo,tr);
            hib.log = false;
            return hib;
        }

        public int popSize { get; set; }

        public int gers { get; set; }

        public double pm { get; set; }

        public double pc { get; set; }
    }
}