using System;
using IIA_TP2.Algoritmos;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class Hibrido : IAAlgoritm
    {
        public Hipotese solTrepaColinas { get; private set; }
        public Hipotese solEHibrido { get; private set; }
        private Evolutivo _evolutivo;
        private TrepaColinas _trepaColinas;
        private int id;

        public Hibrido(Data data, int id = 0) : base(data)
        {
            _evolutivo = new EvolutivoV2(data);
            _trepaColinas = new TrepaColinasV3(data, solEHibrido);
            this.id = id;
        }

        public Hibrido(Data data, Evolutivo evolutivo, TrepaColinas trepaColinas, int id = 0) : base(data)
        {
            _evolutivo = evolutivo;
            _trepaColinas = trepaColinas;

            this.id = id;
        }

        public override void run()
        {
            if (log)
            {
                Console.WriteLine("Hibrido:");
                Console.Out.WriteLine($"{id} -> Inicio Evolutivo");
            }
            _evolutivo.run();
            solEHibrido = _evolutivo.getBest();
            _trepaColinas.setSolInit(solEHibrido);
            if (log)
                Console.Out.WriteLine($"{id} -> Inicio Trepa-Colinas");
            _trepaColinas.run();
            solTrepaColinas = _trepaColinas.getBest();
            bestSol = solTrepaColinas;
            if (log)
                Console.Out.WriteLine($"{id} -> Terminou");
        }

        public override string descricao()
        {
            return _evolutivo.descricao() + " e " + _trepaColinas.descricao();
        }

        public override string getName()
        {
            return "Hibrido";
        }

        public TrepaColinas getTrepaColinas()
        {
            return _trepaColinas;
        }

        public Evolutivo getEvolutivo()
        {
            return _evolutivo;
        }
    }
}