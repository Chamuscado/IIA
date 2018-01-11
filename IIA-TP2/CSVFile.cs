using System.Collections.Generic;
using System.IO;

namespace IIA_TP2
{
    public class CSVFile
    {
        private List<List<string>> matriz;

        public CSVFile()
        {
            matriz = new List<List<string>>();
        }

        public void Write(int x, int y, string cont)
        {
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
    }
}