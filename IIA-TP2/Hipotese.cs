using System;
using System.Collections.Generic;

namespace IIA_TP2.Properties
{
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
            for (var i = 0; i < NCMoedas.Count; ++i)
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

        public double? compareTo(Hipotese hipotese) // melhor > 1 , pior < 0, igual 0
        {
            var i0 = pelidade;
            var i1 = hipotese.pelidade;
                if (i0 < 0)
                    i0 *= -1;
                if (i1 < 0)
                    i1 *= -1;

                return (i1 - i0);
            
        }

        public override string ToString()
        {
            string str = "";
            foreach (var i in NCMoedas)
            {
                str += i.ToString() + " ";
            }
            return str;
        }
        
        public string ToString2()
        {
            return "Resultado da avaliação: " + eval + " penalidade: " +
                   pelidade + " valido: " + valido + " Valor: " + sum;
        }
    }
}