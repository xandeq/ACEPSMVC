using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class Helpers
    {
        public static string ObterCaminhoArquivo(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string folderPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                int number = 1;

                Match regex = Regex.Match(fileName, @"^(.+) \((\d+)\)$");

                if (regex.Success)
                {
                    fileName = regex.Groups[1].Value;
                    number = int.Parse(regex.Groups[2].Value);
                }

                do
                {
                    number++;
                    string newFileName = $"{fileName}_({number}){fileExtension}";
                    filePath = Path.Combine(folderPath, newFileName);
                }
                while (System.IO.File.Exists(filePath));
            }

            return filePath;
        }

        public static string NomeAleatorio(int size)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLowerInvariant();
        }
    }
}
