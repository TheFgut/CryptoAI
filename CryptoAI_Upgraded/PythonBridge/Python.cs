using Binance.Net.Objects.Models.Spot.Staking;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAI_Upgraded.PythonBridge
{
    public class Python
    {
        public static Python instance;

        public void Init()
        {
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                np.numpy = Py.Import("numpy");
            }
        }

        public void ShutDown()
        {

            PythonEngine.Shutdown();
        }
    }

    public static class np
    {
        private static dynamic? numpyInstance;
        internal static dynamic numpy 
        {
            get
            {
                if (numpyInstance == null)
                {
                    throw new Exception("Numpy is not initialized");
                }
                return numpyInstance;
            }
            set 
            {
                numpyInstance = value;
            } 
        }

        public static PyObject array<T>(Array array)
        {
            var flatArray = array.Cast<T>().ToArray();
            var shape = Enumerable.Range(0, array.Rank)
                                  .Select(array.GetLength)
                                  .ToArray();
            return numpy.array(flatArray).reshape(shape);
        }
    }
}
