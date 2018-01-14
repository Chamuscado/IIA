using IIA_TP2.Properties;

namespace IIA_TP2.Algoritmos
{
    public class TrepaColinasV2 : TrepaColinas
    {
        public TrepaColinasV2(Data data) : base(data)
        {
        }

        public TrepaColinasV2(Data data, Hipotese init) : base(data, init)
        {
        }
        
        protected override Hipotese gera_vizinho(Hipotese old)
        {
            var newHip = new Hipotese(old,getName(),iteracao);

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
        
        public override string descricao()
        {
            return
                "TrepaColinas : Gera Vizinho que incrementa ou decrementa conforme se esta a cima ou a baixo do valor e inicia tudo a 0";
        }

        public override string getName()
        {
            return "TrepaColinasV2";
        }
    }
}