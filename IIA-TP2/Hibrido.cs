using System;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class Hibrido: IAAlgoritm
    {
        public Hipotese solTrepaColinas { get; private set; }
        public Hipotese solEHibrido { get; private set; }


        public Hibrido(Data data) : base(data)
        {
        }

        public override void run()
        {
            Console.WriteLine("Hibrido:");
            Evolutivo evol = new Evolutivo(data);
            evol.run();
            solEHibrido = evol.getBest();
            TrepaColinas tre = new TrepaColinas(data, solEHibrido);
            tre.run();
            solTrepaColinas = tre.getBest();
            bestSol = solTrepaColinas;
        }

    }
}