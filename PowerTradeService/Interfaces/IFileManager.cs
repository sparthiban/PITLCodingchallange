using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerTradeService.Interfaces
{
    public interface IFileManager
    {
        Task WriteToCSVAsync<T>(List<T> list, string path, string fileName);
    }
}
