using CsvHelper;
using log4net;
using PowerTradeService.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService
{
    public class FileManager : IFileManager
    {

        private readonly ILog _log;
        public FileManager(ILog log)
        {
            _log = log;
        }

        public async Task WriteToCSVAsync<T>(List<T> positions, string path, string fileName)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                _log.Info($"Intraday begin wrting to {path}{fileName}.csv");
                
                    using (StreamWriter streatWriter = new StreamWriter($"{path}{fileName}.csv"))
                    {
                        using (var csvWriter = new CsvWriter(streatWriter, CultureInfo.InvariantCulture))
                        {

                            csvWriter.WriteRecords(positions);
                        }
                    }
                

                _log.Info($"Intraday CSV file created {path}{fileName}.csv");
            }
            catch
            {
                _log.Error($"Failed to generat CSV file {path}{fileName}.csv");                
            }
        }
    }
}
