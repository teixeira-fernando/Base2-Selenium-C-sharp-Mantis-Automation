using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mantis_Automation.Helpers
{
    public class DataDrivenCSV
    {
        public static IEnumerable retornaDadosCSV(string csvPath)
        {
            using (StreamReader sr = new StreamReader(csvPath, System.Text.Encoding.GetEncoding(1252)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ArrayList result = new ArrayList();
                    result.AddRange(line.Split(';'));
                    yield return result;
                }
            }
        }

        //faz a leitura de uma linha unica passada como referencia
        public static IEnumerable retornaDadosCSV(string csvPath, int linha)
        {
            using (StreamReader sr = new StreamReader(csvPath, System.Text.Encoding.GetEncoding(1252)))
            {
                string line;
                int count = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    if (count == linha)
                    {
                        ArrayList result = new ArrayList();
                        result.AddRange(line.Split(';'));
                        yield return result;
                    }
                    count++;
                }
            }
        }

        //faz a leitura de um conjunto de linhas passadas como referencia
        public static IEnumerable retornaDadosCSV(string csvPath, int[] linha)
        {
            using (StreamReader sr = new StreamReader(csvPath, System.Text.Encoding.GetEncoding(1252)))
            {
                string line;
                int count = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (int element in linha)
                    {
                        if (count == element)
                        {
                            ArrayList result = new ArrayList();
                            result.AddRange(line.Split(';'));
                            yield return result;
                        }
                    }
                    count++;
                }
            }
        }
    }
}
