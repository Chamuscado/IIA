using System;
using System.Diagnostics;
using IIA_TP2.Algoritmos;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TestEvolutivo
    {
        private Data data;
        private string outputfile;
        private int tests;
        private int v;

        public TestEvolutivo(Data data, string outputfile, int tests, int v, double pc, double pm, int gers,
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
            csvFile.addCabecalhoE(data);
            Hipotese best = null;
            for (int i = 0; i < tests; i++)
            {
                evo = getNewIntace();
                evo.run();
                Hipotese hip = evo.getBest();
                if (best == null || best.eval > hip.eval) // guardar a melhor
                {
                    best = new Hipotese(hip, "Melhor", hip.interacao);
                    best.evaluate();
                }
               
                csvFile.addBestEvolutivo(evo);
            }

            best.evaluate();
            csvFile.addHipoteseE(best);
            csvFile.toFile(outputfile);
        }

        private Evolutivo getNewIntace()
        {
            Evolutivo instace;
            switch (v)
            {
                case 1:
                    instace = new EvolutivoV2(data, pc, pm, gers, popSize);
                    break;

                default:
                    instace = new Evolutivo(data, pc, pm, gers, popSize);
                    break;
            }
            instace.log = false;
            return instace;
        }

        public int popSize { get; set; }

        public int gers { get; set; }

        public double pm { get; set; }

        public double pc { get; set; }
    }
}