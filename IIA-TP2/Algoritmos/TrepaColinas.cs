using System;
using System.Collections.Generic;
using System.Linq;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TrepaColinas : IAAlgoritm
    {
        public TrepaColinas(Data data) : base(data)
        {
            bestSol = init();
        }

        public TrepaColinas(Data data, Hipotese init) : base(data)
        {
            bestSol = init;
        }

        protected virtual Hipotese gera_vizinho(Hipotese old)
        {
            var newHip = new Hipotese(old, getName(), iteracao);

            var rand = this.rand.Next(newHip.NCMoedas.Count);
            var rand2 = this.rand.NextDouble();
            if (rand2 < 0.5)
                --newHip.NCMoedas[rand];
            else
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

        public override void run()
        {
            if (log)
                Console.WriteLine("TrepaColinas:");

            Hipotese hip = bestSol;

            hip.evaluate();
            if (Program.debug)
                imprimeHipotese(hip);

            for (iteracao = 0; iteracao < data.MaxIteracoes; ++iteracao)
            {
                hip = gera_vizinho(hip);
                hip.evaluate();

                if (Program.debug)
                    imprimeHipotese(hip);

                if (bestSol.compareTo(hip) < 0)
                    bestSol = hip;
                if (hip.valido == 0)
                {
                    if (log)
                        Console.Out.WriteLine("Solução encontrada, Iteração: " + iteracao);
                    break;
                }
            }
        }

        public override string descricao()
        {
            return
                "TrepaColinas : Gera Vizinho de forma random e inicia tudo a 0";
        }

        public override string getName()
        {
            return "TrepaColinas";
        }

        public virtual Hipotese init()
        {
            var hip = new Hipotese(data, getName(), iteracao);
            for (var i = 0; i < hip.NCMoedas.Count; ++i)
                hip.NCMoedas[i] = 0;
            return hip;
        }

        public virtual void setSolInit(Hipotese initSol)
        {
            bestSol = initSol;
        }
    }
}