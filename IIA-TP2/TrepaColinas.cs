using System;
using System.Collections.Generic;
using System.Linq;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class TrepaColinas: IAAlgoritm
    {
        
        public TrepaColinas(Data data) : base(data)
        {   
            bestSol = init();
        }

        public TrepaColinas(Data data, Hipotese init) : base(data)
        {
            bestSol = init;
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

        public override void run()
        {
            Console.WriteLine("TrepaColinas:");
            
            Hipotese hip = bestSol;

            hip.evaluate();
            if (Program.debug)
                imprimeHipotese(hip);


            for (var i = 0; i < data.MaxIteracoes; i++)
            {
                hip = gera_vizinho(hip);
                hip.evaluate();
                if (Program.debug)
                    imprimeHipotese(hip);

                if (hip.valido == 0)
                {
                    Console.Out.WriteLine("Solução encontrada, Iteração: " + i);
                    bestSol = hip;
                    break;
                }
            }
        }

        public Hipotese init()
        {
            var hip = new Hipotese(data);
            for (var i = 0; i < hip.NCMoedas.Count; ++i)
                hip.NCMoedas[i] = 0;
            return hip;
        }



    }
}