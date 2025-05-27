
using FinalOutput.Interfaces;
using FinalOutput.Models;
using System.Collections.Generic;
using System.IO;

namespace FinalOutput.Services
{
    public class CostumerAccountGetService : ICostumerAccountGetService
    {
        public IEnumerable<Costumer> GetCostumers()
        {
            string path = GetFilePath.FilePath(@"Accounts\Costumer");

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);

            foreach (var property in line)
            {
                yield return new Costumer(property[0], property[1], decimal.Parse(property[2]));
            }
        }
    }
}
