using System;
using System.Collections.Generic;
using System.Linq;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TrepaColinas
    {
        private readonly Random rand;
        private Data data;
        private Hipotese bestSol;
        private int maxInteracoes;

        public TrepaColinas(Data data)
        {
            this.data = data.Clone();
            this.data.moedas.Sort();
            this.data.MaxIteracoes = (int) (this.data.objetivo / this.data.moedas.First());
            maxInteracoes = -1;
            rand = new Random();
        }

        private Hipotese gera_vizinho(Hipotese old)
        {
            var newHip = new Hipotese(old);

            var rand = this.rand.Next(newHip.NCMoedas.Count);
            
            if (old.valido < 0)
                --newHip.NCMoedas[rand];
            if (old.valido > 0)
                ++newHip.NCMoedas[rand];
            
            for (int i = 0; i < newHip.NCMoedas.Count; i++)
            {
                if (newHip.NCMoedas[i] < 0)
                {
                    newHip.NCMoedas[i] = 0;
                }
            }
            return newHip;
        }

        public void run()
        {
            Hipotese hip = init();

            hip.evaluate();
            imprimeHipotese(hip); 


            for (var i = 0; i < data.MaxIteracoes; i++)
            {
                hip = gera_vizinho(hip);
                hip.evaluate();
             imprimeHipotese(hip);   

                if (hip.valido == 0)
                {
                    Console.Out.WriteLine("Solução encontrada, Iteração: " + i);
                    break;
                }
            }
        }

        public void imprimeHipotese(Hipotese hip)
        {
            Console.Out.WriteLine("Resultado da avaliação: " + hip.eval + " penalidade: " +
                                  hip.pelidade + " valido: " + hip.valido + " Valor: " + hip.sum);
            for (var i = 0; i < hip.NCMoedas.Count; i++)
            {
                Console.Out.Write(hip.NCMoedas[i] + " de " + data.moedas[i] + "€ ");
            }
            Console.Out.WriteLine();
        }

        public Hipotese init()
        {
            var hip = new Hipotese(data);
            for (var i = 0; i < hip.NCMoedas.Count; ++i)
                hip.NCMoedas[i] = 0;
            return hip;
        }

        public class Hipotese_TrepaColinas : Hipotese
        {
            public Hipotese_TrepaColinas(List<int> list, Data data) : base(list, data)
            {
            }

            public Hipotese_TrepaColinas(Data data) : base(data)
            {
            }

            public Hipotese_TrepaColinas(Hipotese hip) : base(hip)
            {
            }
        }
    }
}