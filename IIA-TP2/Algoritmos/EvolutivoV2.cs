using System.Collections.Generic;
using IIA_TP2.Properties;

namespace IIA_TP2.Algoritmos
{
    public class EvolutivoV2 : Evolutivo
    {
        
        public EvolutivoV2(Data data, double probabilidadeCrossover = 0.05, double probabilidadeMutation = 0.05,
            int maxGeracoes = 100, int popSize = 100) : base(data, probabilidadeCrossover, probabilidadeMutation,
            maxGeracoes, popSize)
        {
        }
        protected override List<Hipotese> mutation(List<Hipotese> list)
        {
            int i, j;
            var secondList = new List<Hipotese>();
            foreach (Hipotese t in list)
            {
                secondList.Add(new Hipotese(t,getName(),iteracao));
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

        public override string descricao()
        {
            return
                "Evolutivo : mutação inteligente, crossover original, inicia com valores aleatorios entre 0 e e 1/50 do numero maximo de moedas possivel";
        }

        public override string getName()
        {
            return "Evolutivo";
        }

    }
}