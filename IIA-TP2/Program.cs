using System;
using System.Collections.Generic;
using System.IO;


namespace IIA_TP2
{
    internal class Program
    {
        private const string InvalidFomat = "Formato Inváido";
        public const bool debug = false;

        public static void Mainw(string[] args)
        {
            var name = "instancia_teste.txt";
            var data = getData(name);
            if (data == null)
            {
                Console.Out.WriteLine("data a null");
                return;
            }


            var alg = new TrepaColinas(data);
            alg.run();
        }

        public static void MainE(string[] args)
        {
            var name = "instancia_teste.txt";
            var data = getData(name);
            var alg = new Evolutivo(data);
            alg.run();
        }

        public static void Main(string[] args)
        {
            var name = "instancia_teste.txt";
            var data = getData(name);
            data.MaxIteracoes = 10000;
            var alg = new Hibrido(data);
            alg.run();
            var hip = alg.solEHibrido;
            CSVFile file = new CSVFile();
            int x;
            int yy = 0;
            file.Write(0, yy, "Moedas");
            for ( x = 0; x < data.moedas.Count; ++x)
            {
                file.Write(x+1, yy, data.moedas[x].ToString()+"e");
            }
            ++yy;
            file.Write(0, yy, "Evolutivo");
            for ( x = 0; x < hip.NCMoedas.Count; ++x)
            {
                file.Write(x+1, yy, $"{hip.NCMoedas[x]}");
            }
            file.Write(x+1, yy, hip.sum.ToString());
            hip = alg.solTrepaColinas;
            ++yy;
            file.Write(0, yy, "Trepa-Colinas");
            for ( x = 0; x < hip.NCMoedas.Count; ++x)
            {
                file.Write(x+1, yy, hip.NCMoedas[x].ToString());
            }
            file.Write(x+1, yy, hip.sum.ToString());
            hip = alg.getBest();
            ++yy;
            file.Write(0, yy, "Hibrido");
            for ( x = 0; x < hip.NCMoedas.Count; ++x)
            {
                file.Write(x + 1, yy, hip.NCMoedas[x].ToString());
            }
            file.Write(x+1, yy, hip.sum.ToString());
            ++yy;
            file.Write(0, yy, "Valor");file.Write(1, yy,data.objetivo.ToString());
            
            file.toFile("firstTest");
        }

        private static Data getData(string nameFile)
        {
            StreamReader file;
            try
            {
                file = new StreamReader(nameFile);
            }
            catch (FileNotFoundException ex)
            {
                Console.Out.WriteLine("File not found, try again");
                return null;
            }
            var line = file.ReadLine();
            if (line == null)
            {
                Console.Out.Write(InvalidFomat);
                return null;
            }
            line = line.Replace('.', ',');
            var elements = line.Split();
            Data data = new Data();
            try
            {
                data.numeroMoedas = int.Parse(elements[0]);
                data.objetivo = double.Parse(elements[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(InvalidFomat + e);
                return null;
            }
            line = file.ReadLine();
            if (line == null)
            {
                Console.Out.Write(InvalidFomat);
                return null;
            }
            line = line.Replace('.', ',');
            elements = line.Split();
            try
            {
                foreach (var i in elements)
                {
                    data.moedas.Add(double.Parse(i));
                }
            }
            catch (Exception e)
            {
                Console.Out.Write(InvalidFomat);
                return null;
            }


            return data;
        }
    }
}