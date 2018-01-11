using System;
using System.Linq;

namespace IIA_TP2.Properties
{
    public abstract class IAAlgoritm
    {
        protected Data data;
        protected Hipotese bestSol;
        protected readonly Random rand;

        protected IAAlgoritm(Data data)
        {
            this.data = data.Clone();
            this.data.moedas.Sort();
            this.data.MaxIteracoes = (int) (this.data.objetivo / this.data.moedas.First());
            this.data = data;
            rand = new Random();
        }

        public abstract void run();
        
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
    }
}