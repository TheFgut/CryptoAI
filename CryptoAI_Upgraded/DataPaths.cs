using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded
{
    static class DataPaths
    {
        public static string networksPath { get 
            {
                string path = $"{Application.CommonAppDataPath.Split("+")[0]}\\networks";
                CheckIfExistsIfNoCreate(path);
                return path;
            } 
        }

        public static string datasetsPath
        {
            get
            {
                string path = $"{Application.CommonAppDataPath.Split("+")[0]}\\datasets";
                CheckIfExistsIfNoCreate(path);
                return path;
            }
        }

        public static string appConfigurationPath
        {
            get
            {
                string path = $"{Application.CommonAppDataPath.Split("+")[0]}\\appConfig";
                CheckIfExistsIfNoCreate(path);
                return path;
            }
        }


        private static void CheckIfExistsIfNoCreate(string path)
        {
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
