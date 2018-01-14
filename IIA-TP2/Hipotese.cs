using System;
using System.Collections.Generic;

namespace IIA_TP2.Properties
{
    public class Hipotese
    {
        public List<int> NCMoedas { get; set; }
        public int? valido { get; private set; }
        public int? eval { get; private set; }                // Numero de moedas
        public double? sum { get; private set; }                //valor acumulado
        public double? pelidade { get; private set; }
        public int interacao { get; set; }
        private Data _data;
        private string source;

        public Hipotese(List<int> list, Data data, string source, int interacao)
        {
            NCMoedas = list;
            valido = null;
            eval = null;
            sum = null;
            pelidade = null;
            _data = data;
            this.source = source;
            this.interacao = interacao;
        }

        public Hipotese(Data data, string source, int interacao)
        {
            this.interacao = interacao;
            NCMoedas = new List<int>();
            valido = null;
            eval = null;
            sum = null;
            pelidade = null;
            _data = data;
            this.source = source;
            for (int i = 0; i < data.moedas.Count; ++i)
                NCMoedas.Add(0);
        }

        public Hipotese(Hipotese hip, string source, int interacao)
        {
            this.interacao = interacao;
            NCMoedas = new List<int>(hip.NCMoedas);
            valido = null;
            eval = null;
            sum = null;
            pelidade = null;
            _data = hip._data;
            this.source = source;
        }

        public double? evaluate()
        {
            eval = 0;
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

        public string getSource()
        {
            return source;
        }
    }
}