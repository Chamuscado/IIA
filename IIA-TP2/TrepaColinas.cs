using System;
using System.Collections.Generic;
using System.Linq;

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


        public class Hipotese
        {
            public List<int> NCMoedas { get; set; }
            public int? valido { get; private set; }
            public double? eval { get; private set; }
            public double? sum { get; private set; }
            public double? pelidade { get; private set; }
            private Data _data;

            public Hipotese(List<int> list, Data data)
            {
                NCMoedas = list;
                valido = null;
                eval = null;
                sum = null;
                pelidade = null;
                _data = data;
            }

            public Hipotese(Data data)
            {
                NCMoedas = new List<int>();
                valido = null;
                eval = null;
                sum = null;
                pelidade = null;
                _data = data;
                for (int i = 0; i < data.moedas.Count; ++i)
                    NCMoedas.Add(0);
            }

            public Hipotese(Hipotese hip)
            {
                NCMoedas = new List<int>(hip.NCMoedas);
                valido = null;
                eval = null;
                sum = null;
                pelidade = null;
                _data = hip._data;
            }

            public double? evaluate()
            {
                eval = 0.0;
                sum = 0.0;
                pelidade = 0.0;
                valido = 0;
                for (int i = 0; i < NCMoedas.Count; ++i)
                {
                    eval += NCMoedas[i];
                    sum += NCMoedas[i] * _data.moedas[i];
                }
                if (sum != _data.objetivo)
                {
                    if (Program.debug)
                        Console.Out.WriteLine("solucao invalida");
                    pelidade = sum - _data.objetivo;
                    if (sum < _data.objetivo)
                        valido = 1;
                    else
                        valido = -1;
                }
                return eval;
            }
        }
    }
}