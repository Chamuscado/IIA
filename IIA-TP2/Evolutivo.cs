using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class Evolutivo : IAAlgoritm
    {
        
        
        public int popSize { get; set; }
        private List<Hipotese> pop;
        private double probabilidadeCrossover;
        private double probabilidadeMutation;
        private int maxGeracoes;

        public Evolutivo(Data data, double probabilidadeCrossover = 0.5, double probabilidadeMutation = 0.5, int maxGeracoes = 100, int popSize = 100) : base(data)
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
            Console.WriteLine("Evolutivo:");
            
            var numb = 0;
            bestSol = pop[0];

            while (numb < maxGeracoes)
            {
                var list = tournament();

                list = genetic_operators(list);

                evaluate(list);

                refreshBest(list);
                if ((numb % 25) == 0)
                {
                    Console.Out.Write($"Iteração: {numb} " + bestSol.ToString());
                    Console.Out.WriteLine(bestSol.ToString2());
                }
                if (bestSol.valido == 0)
                {
                    Console.Out.WriteLine("Solução encontrada, Iteração: " + numb);
                    imprimeHipotese(bestSol);
                    break;
                }
                ++numb;
            }
            Console.Out.WriteLine("Fim! ");
        }


        private void refreshBest(List<Hipotese> atual)
        {
            foreach (Hipotese i in atual)
            {
                if (bestSol.compareTo(i) < 0)
                    bestSol = i;
            }
        }


        private void evaluate()
        {
            foreach (var i in pop)
            {
                i.evaluate();
            }
        }

        private void evaluate(List<Hipotese> list)
        {
            foreach (var i in list)
            {
                i.evaluate();
            }
        }

        private List<Hipotese> tournament()
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

        private List<Hipotese> genetic_operators(List<Hipotese> list)
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

        private void imprimeHipotese(List<Hipotese> list, string msg = "")
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

        private List<Hipotese> crossover(List<Hipotese> list)
        {
            var secondList = new List<Hipotese>();
            foreach (Hipotese t in list)
            {
                secondList.Add(new Hipotese(t));
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

        private List<Hipotese> mutation(List<Hipotese> list)
        {
            int i, j;
            var secondList = new List<Hipotese>();
            foreach (Hipotese t in list)
            {
                secondList.Add(new Hipotese(t));
            }
            var rand = this.rand.Next(data.moedas.Count);

            for (i = 0; i < list.Count; i++)
            {
                for (j = 0; j < data.moedas.Count; j++)
                {
                    if (this.rand.NextDouble() < probabilidadeMutation)
                    {
                        if (secondList[i].valido < 0)
                            --secondList[i].NCMoedas[rand];
                        if (secondList[i].valido > 0)
                            ++secondList[i].NCMoedas[rand];
                    }
                }
            }
            return secondList;
        }

        private void init_pop()
        {
            pop = new List<Hipotese>();

            for (var i = 0; i < popSize; i++)
            {
                var hip = new Hipotese(data);
                for (var j = 0; j < data.moedas.Count; j++)
                {
                    hip.NCMoedas[j] = getRandN();
                }
                pop.Add(hip);
            }
        }

        private int getRandN()
        {
            return rand.Next(0, pop.Count); // TODO -> temos de verificar o valor maximo
        }
    }
}