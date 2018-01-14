using System;
using System.Linq;

namespace IIA_TP2.Properties
{
    public abstract class IAAlgoritm
    {
        protected Data data;
        protected Hipotese bestSol;
        protected readonly Random rand;
        protected int iteracao;
        public bool log { get; set; }
        
        protected IAAlgoritm(Data data)
        {
            log = true;
            iteracao = 0;
            this.data = data;
            this.data.MaxIteracoes = (int) (this.data.objetivo / this.data.moedas.First());
            rand = new Random(GetHashCode());
        }

        public abstract void run();

        public abstract string descricao();

        public abstract string getName();

        public void imprimeHipotese(Hipotese hip)
        {
            Console.Out.WriteLine("Resultado da avaliação: " + hip.eval + " penalidade: " +
                                  hip.pelidade + " valido: " + hip.valido + " Valor: " + hip.sum);
            for (var i = 0; i < hip.NCMoedas.Count; i++)
            {
                Console.Out.Write(hip.NCMoedas[i] + " de " + data.moedas[i] + "e ");
            }
            Console.Out.WriteLine();
        }

        public Hipotese getBest()
        {
            return bestSol;
        }

        public Data getData()
        {
            return data;
        }
    }
}