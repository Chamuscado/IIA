using System;
using System.Collections.Generic;
using System.IO;

namespace IIA_TP2
{
    internal class Program
    {
        private const string InvalidFomat = "Formato Inváido";
        public const bool debug = false;

        public static void MainT(string[] args)
        {
            var name = "instancia_teste.txt";
            var data = getData(name);
            if (debug)
            {
                if (data == null)
                {
                    Console.Out.WriteLine("data a null");
                    return;
                }
                Console.Out.WriteLine("Numero de Moedas: " + data.numeroMoedas);
                Console.Out.WriteLine("Objetivo: " + data.objetivo);
                Console.Out.WriteLine("Moedas:");
                                
                foreach (var i in data.moedas)
                {
                    Console.Out.Write(i + "  ");
                }
                Console.Out.WriteLine();
            }
            var alg = new TrepaColinas(data);
            alg.run();
        }

        public static void Main(string[] args)
        {
            var name = "instancia_teste.txt";
            var data = getData(name);
            if (debug)
            {
                if (data == null)
                {
                    Console.Out.WriteLine("data a null");
                    return;
                }
                Console.Out.WriteLine("Numero de Moedas: " + data.numeroMoedas);
                Console.Out.WriteLine("Objetivo: " + data.objetivo);
                Console.Out.WriteLine("Moedas:");
                foreach (var i in data.moedas)
                {
                    Console.Out.Write(i + "  ");
                }
                Console.Out.WriteLine();
            }
            var alg = new Evolutivo(data);
            alg.run();
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