using System.Collections.Generic;

namespace IIA_TP2
{
    public class Data
    {
        public int numeroMoedas { get; set; }
        public double objetivo { get; set; }
        public List<double> moedas { get; set; }
        public int MaxIteracoes { get; set; }

        public Data()
        {
            moedas = new List<double>();
            numeroMoedas = -1;
            objetivo = -1.0;
            MaxIteracoes = 0;
        }

        public Data Clone()
        {
            var newData = new Data();
            newData.numeroMoedas = numeroMoedas;
            newData.objetivo = objetivo;
            newData.moedas = new List<double>(moedas);
            newData.MaxIteracoes = MaxIteracoes;
            return newData;
        }
    }
}