using Quote.Contracts;
using Quote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaIngreso.Models
{
    public interface IMarginProvider
    {
        Task<dynamic> getResultApiAsync(string url);
    }
}