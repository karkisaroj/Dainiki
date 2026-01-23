using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Application.Services
{
    internal static class DatabaseConfig
    {

        public const string DatabaseFilename = "Dainiki.db";

    
        public static string DatabasePath =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         DatabaseFilename);
    }

}
