using System.Collections.Generic;
using System.IO;
using IIA_TP2.Properties;

namespace IIA_TP2
{
    public class CSVFile
    {
        private List<List<string>> matriz;
        private int lastLine;

        public CSVFile()
        {
            matriz = new List<List<string>>();
        }

        public void Write(int x, int y, string cont)
        {
            if (y == -1)
                y = ++lastLine;
            while (matriz.Count <= y)
            {
                matriz.Add(new List<string>());
            }
            while (matriz[y].Count <= x)
            {
                matriz[y].Add("");
            }
            matriz[y][x] = cont;
        }

        public int addCabecalhoTC(Data data, int x = 0)
        {
            if (x == 0)
                ++lastLine;
            Write(x++, lastLine, "Origem");    
            Write(x++, lastLine, "Iteracao");
            for (var i = 0; i < data.moedas.Count; ++i, ++x)
            {
                Write(x, lastLine, data.moedas[i].ToString() + "e");
            }
            Write(x++, lastLine, "Valor");
            Write(x++, lastLine, "Numero de moedas");
            Write(x++, lastLine, "Maximo de Iteracoes");
            return x;
        }

        public int addHipoteseTC(Hipotese hip, int x = 0)
        {
            if (x == 0)
                ++lastLine;


            Write(x++, lastLine, hip.getSource());
            Write(x++, lastLine, hip.interacao.ToString());

            for (var i = 0; i < hip.NCMoedas.Count; ++i, ++x)
            {
                Write(x, lastLine, $"{hip.NCMoedas[i]}");
            }
            Write(x++, lastLine, hip.sum.ToString());
            Write(x++, lastLine, hip.eval.ToString());
            return x;
        }

        public int addBestTrepaColinas(TrepaColinas tr, int x = 0)
        {
            var hip = tr.getBest();
            if (x == 0)
                ++lastLine;
            Write(x++, lastLine, hip.getSource());
            Write(x++, lastLine, hip.interacao.ToString());

            for (var i = 0; i < hip.NCMoedas.Count; ++i, ++x)
            {
                Write(x, lastLine, $"{hip.NCMoedas[i]}");
            }
            Write(x++, lastLine, hip.sum.ToString());
            Write(x++, lastLine, hip.eval.ToString());
            Write(x++, lastLine, tr.getData().MaxIteracoes.ToString());
            return x;
        }

        public void toFile(string filename)
        {
            using (StreamWriter file = new StreamWriter(filename + ".csv"))
            {
                foreach (var y in matriz)
                {
                    foreach (var x in y)
                    {
                        file.Write(x + ";");
                    }
                    file.WriteLine();
                }
            }
        }


        public int addCabecalhoE(Data data, int x = 0)
        {
            if (x == 0)
                ++lastLine;
            Write(x++, lastLine, "Origem");
            Write(x++, lastLine, "Iteracoes");
            Write(x++, lastLine, "Probabilidade de mutacao");
            Write(x++, lastLine, "Probabilidade de crossover");
            Write(x++, lastLine, "Tamanho da Populacao");
            Write(x++, lastLine, "Maximo de Geracoes");
            for (var i = 0; i < data.moedas.Count; ++i, ++x)
            {
                Write(x, lastLine, data.moedas[i].ToString() + "e");
            }
            Write(x++, lastLine, "Valor");
            Write(x++, lastLine, "Numero de moedas");
            return x;
        }


        public int addBestEvolutivo(Evolutivo evo, int x = 0)
        {
            var hip = evo.getBest();
            if (x == 0)
                ++lastLine;
            Write(x++, lastLine, hip.getSource());
            Write(x++, lastLine, hip.interacao.ToString());
            Write(x++, lastLine, evo.probabilidadeMutation.ToString());
            Write(x++, lastLine, evo.probabilidadeCrossover.ToString());
            Write(x++, lastLine, evo.popSize.ToString());
            Write(x++, lastLine, evo.maxGeracoes.ToString());

            for (var i = 0; i < hip.NCMoedas.Count; ++i, ++x)
            {
                Write(x, lastLine, $"{hip.NCMoedas[i]}");
            }
            Write(x++, lastLine, hip.sum.ToString());
            Write(x++, lastLine, hip.eval.ToString());
            return x;
        }


        public int addHipoteseE(Hipotese hip, int x = 0)
        {
            if (x == 0)
                ++lastLine;

            Write(x++, lastLine, hip.getSource());
            Write(x++, lastLine, hip.interacao.ToString());
            x += 3;

            for (var i = 0; i < hip.NCMoedas.Count; ++i, ++x)
            {
                Write(x, lastLine, $"{hip.NCMoedas[i]}");
            }
            Write(x++, lastLine, hip.sum.ToString());
            Write(x++, lastLine, hip.eval.ToString());
            return x;
        }

        public void addCabecalhoH(Data data)
        {
            addCabecalhoTC(data, addCabecalhoE(data)+1);
        }

        public void addBestHibrido(Hibrido hib)
        {
            addBestTrepaColinas(hib.getTrepaColinas(), addBestEvolutivo(hib.getEvolutivo())+1);
        }

        public void addHipoteseH(Hipotese bestE, Hipotese bestT)
        {
            addHipoteseTC(bestT, addHipoteseE(bestE)+1);
        }
    }
}