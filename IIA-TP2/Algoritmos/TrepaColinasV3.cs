using IIA_TP2.Properties;

namespace IIA_TP2.Algoritmos
{
    public class TrepaColinasV3 : TrepaColinasV2
    {
        public TrepaColinasV3(Data data) : base(data)
        {
        }

        public TrepaColinasV3(Data data, Hipotese init) : base(data, init)
        {
        }


        public override string descricao()
        {
            return
                "TrepaColinas : Gera Vizinho que incrementa ou decrementa conforme se esta a cima ou a baixo do valor e inicia tudo com valores random entre 0 e 1/10 do numero máximo de moedas possível";
        }

        public override string getName()
        {
            return "TrepaColinasV3";
        }

        public override Hipotese init()
        {
            var max = (int) (data.objetivo / data.moedas[0] / 10);
            var hip = new Hipotese(data, getName(), iteracao);
            for (var i = 0; i < hip.NCMoedas.Count; ++i)
                hip.NCMoedas[i] = rand.Next(0, max);
            return hip;
        }
    }
}