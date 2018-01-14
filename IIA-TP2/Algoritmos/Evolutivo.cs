using System;
using System.Collections.Generic;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class Evolutivo : IAAlgoritm
    {
        public int popSize { get; set; }
        public double probabilidadeCrossover { get; set; }
        public double probabilidadeMutation { get; set; }


        private List<Hipotese> pop;
        public int maxGeracoes { get; set; }

        public Evolutivo(Data data, double probabilidadeCrossover = 0.05, double probabilidadeMutation = 0.05,
            int maxGeracoes = 100, int popSize = 100) : base(data)
        {
            this.probabilidadeCrossover = probabilidadeCrossover;
            this.probabilidadeMutation = probabilidadeMutation;
            this.maxGeracoes = maxGeracoes;
            this.popSize = popSize;
            init_pop();
            evaluate();
        }

        public override void run()
        {
            //Console.WriteLine("Evolutivo:");

            iteracao = 0;
            bestSol = pop[0];

            while (iteracao < maxGeracoes)
            {
                var list = tournament();

                list = genetic_operators(list);

                evaluate(list);

                refreshBest(list);
                if ((iteracao % 25) == 0 && log)
                {
                    Console.Out.Write($"Iteração: {iteracao} " + bestSol.ToString());
                    Console.Out.WriteLine(bestSol.ToString2());
                }
                if (bestSol.valido == 0 && log)
                {
                    Console.Out.WriteLine("Solução encontrada, Iteração: " + iteracao);
                    imprimeHipotese(bestSol);
                    break;
                }
                ++iteracao;
            }
            //Console.Out.WriteLine("Fim! ");
        }

        public override string descricao()
        {
            return
                "Evolutivo : mutação aleatória, crossover original, inicia com valores aleatorios entre 0 e e 1/50 do numero maximo de moedas possivel";
        }

        public override string getName()
        {
            return "Evolutivo";
        }


        protected void refreshBest(List<Hipotese> atual)
        {
            foreach (Hipotese i in atual)
            {
                if (bestSol.compareTo(i) < 0)
                    bestSol = i;
            }
        }


        protected void evaluate()
        {
            foreach (var i in pop)
            {
                i.evaluate();
            }
        }

        protected void evaluate(List<Hipotese> list)
        {
            foreach (var i in list)
            {
                i.evaluate();
            }
        }

        protected List<Hipotese> tournament()
        {
            var selecteds = new List<Hipotese>();

            for (var i = 0; i < pop.Count; i++)
            {
                var x1 = rand.Next(0, pop.Count);
                int x2;
                do
                    x2 = rand.Next(0, pop.Count); while (x1 == x2);
                if (pop[x1].compareTo(pop[x2]) > 0) // Aproximação
                    selecteds.Add(pop[x1]);
                else
                    selecteds.Add(pop[x2]);
            }
            return selecteds;
        }

        protected List<Hipotese> genetic_operators(List<Hipotese> list)
        {
            if (Program.debug)
                imprimeHipotese(list, "antes crossover");

            list = crossover(list);

            if (Program.debug)
                imprimeHipotese(list, "antes mutation");

            list = mutation(list);

            if (Program.debug)
                imprimeHipotese(list, "depois do genetic_operators");

            return list;

            //return mutation(crossover(list));
        }

        protected void imprimeHipotese(List<Hipotese> list, string msg = "")
        {
            Console.Out.WriteLine(msg);
            evaluate(list);
            foreach (var hipotese in list)
            {
                Console.Out.Write(hipotese.ToString());
                //Console.Out.WriteLine("   Valido: " + hipotese.valido);
                Console.Out.WriteLine(hipotese.ToString2());
            }
        }

        protected List<Hipotese> crossover(List<Hipotese> list)
        {
            var secondList = new List<Hipotese>();
            foreach (Hipotese t in list)
            {
                secondList.Add(new Hipotese(t, getName(), iteracao));
            }
            int j, point;


            for (var i = 0; i < list.Count; i += 2)
            {
                if (rand.NextDouble() < probabilidadeCrossover)
                {
                    point = rand.Next(0, secondList[i].NCMoedas.Count);
                    for (j = 0; j < point; j++)
                    {
                        secondList[i].NCMoedas[j] = list[i].NCMoedas[j];
                        secondList[i + 1].NCMoedas[j] = list[i + 1].NCMoedas[j];
                    }
                    for (j = point; j < secondList[i].NCMoedas.Count; j++)
                    {
                        secondList[i].NCMoedas[j] = list[i + 1].NCMoedas[j];
                        secondList[i + 1].NCMoedas[j] = list[i].NCMoedas[j];
                    }
                }
                else
                {
                    secondList[i] = list[i];
                    secondList[i + 1] = list[i + 1];
                }
            }
            return secondList;
        }

        protected virtual List<Hipotese> mutation(List<Hipotese> list)
        {
            int i, j;
            var secondList = new List<Hipotese>();
            foreach (Hipotese t in list)
            {
                secondList.Add(new Hipotese(t, getName(), iteracao));
            }
            var rand = this.rand.Next(data.moedas.Count);

            for (i = 0; i < list.Count; i++)
            {
                for (j = 0; j < data.moedas.Count; j++)
                {
                    if (this.rand.NextDouble() < probabilidadeMutation)
                    {
                        if (this.rand.NextDouble() < 50)
                        {
                            if (secondList[i].NCMoedas[rand] > 0)
                                --secondList[i].NCMoedas[rand];
                        }
                        else
                            ++secondList[i].NCMoedas[rand];
                    }
                }
            }
            return secondList;
        }

        protected void init_pop()
        {
            var max = (int) (data.objetivo / data.moedas[0] / 50);
            pop = new List<Hipotese>();

            for (var i = 0; i < popSize; i++)
            {
                var hip = new Hipotese(data, getName(), iteracao);
                for (var j = 0; j < data.moedas.Count; j++)
                {
                    hip.NCMoedas[j] = getRandN(max);
                }
                pop.Add(hip);
            }
        }

        protected int getRandN(int max)
        {
            return rand.Next(0, max);
        }
    }
}