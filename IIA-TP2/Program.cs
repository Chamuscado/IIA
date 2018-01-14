using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace IIA_TP2
{
    internal class Program
    {
        private const string InvalidFomat = "Formato Inváido";
        public const bool debug = false;
        private const string filenameInit = "instancia";
        private const string fileExtension = ".txt";

        public static void Main(string[] args)
        {
            //testaTrepaColinas();
            //testaEvolutivo();
            testaHibrido();
        }


        public static void testaTrepaColinas()
        {
            string dest = "C:\\Users\\Chamuscado\\Dropbox\\IIA 2017\\TP2\\TrepaColinas\\";
            var test = new List<TestTrepaColinas>();
            for (var i = 0; i < 8; ++i)
            {
                string name = $"{filenameInit}{i}{fileExtension}";
                var data = getData(name);

                for (var j = 0; j < 3; j++)
                {
                    test.Add(new TestTrepaColinas(data.Clone(), $"{dest}Trepa-colinas V{j + 1} {filenameInit}{i}", 1000,
                        j));
                }
            }

            foreach (TestTrepaColinas t in test)
            {
                var tr = new Thread(t.run);
                tr.Start();
            }
        }


        public static void testaEvolutivo()
        {
            string dest = "C:\\Users\\Chamuscado\\Dropbox\\IIA 2017\\TP2\\Evolutivo\\";
            var test = new List<TestEvolutivo>();
            var soma = 0;
            for (var i = 0; i < 8; i += 2)
            {
                string name = $"{filenameInit}{i}{fileExtension}";
                var data = getData(name);

                for (var j = 0; j < 2; j++)
                {
                    for (var pc = 0.01; pc <= 0.25; pc *= 5)
                    {
                        //for (var pm = 0.01; pm <= 0.25; pm *= 5)
                        // {
                        var pm = pc;
                        for (var ger = 100; ger <= 1000; ger *= 10)
                        {
                            for (var popSize = 100; popSize <= 1000; popSize *= 10)
                            {
                                test.Add(new TestEvolutivo(data.Clone(),
                                    $"{dest}Evolutivo V{j + 1} {filenameInit}-Ins{i}-PC{pc}-Pm{pm}-ger{ger}-popSize{popSize}",
                                    100, j, pc, pm, ger, popSize));
                                Console.WriteLine($"{i}-{j}-{pc}-{pc}-{ger}-{popSize}");
                            }
                        }
                        //}
                    }
                }
            }
            foreach (var t in test)
            {
                var tr = new Thread(t.run);
                tr.Start();
            }
            Console.Out.WriteLine("feito");
        }


        public static void testaHibrido()
        {
            string dest = "C:\\Users\\Chamuscado\\Dropbox\\IIA 2017\\TP2\\Hibrido\\";
            var test = new List<TestHibrido>();
            for (var i = 0; i < 8; ++i)
            {
                string name = $"{filenameInit}{i}{fileExtension}";
                var data = getData(name);

                for (var pc = 0.01; pc <= 0.25; pc *= 5)
                {
                    var pm = pc;
                    for (var ger = 100; ger <= 1000; ger *= 10)
                    {
                        for (var popSize = 100; popSize <= 1000; popSize *= 10)
                        {
                            test.Add(new TestHibrido(data.Clone(), $"{dest}Hibrido {filenameInit}{i}-pc{pc}-pm{pm}-ger{ger}-popSize{popSize}", 100, 0, pc, pm,
                                ger, popSize));
                        }
                    }
                }
            }

            foreach (var t in test)
            {
                var tr = new Thread(t.run);
                tr.Start();
            }
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