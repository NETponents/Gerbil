using System;
using Gerbil.Gerbil_DataService;
using Gerbil.Gerbil_DataService.Models.Devices;

namespace Gerbil
{
    namespace Gerbil_Engine
    {
        class GerbilRunner
        {
            private Device target;

            public GerbilRunner(Device dTarget)
            {
                target = dTarget;
            }
            public OSResult guessOS()
            {
                OSResult result;
                // Run neural network algorithm

                // Fake data for now
                result = new OSResult("Unknown", 0.0f);
                return result;
            }
        }
        class OSResult
        {
            private string osName;
            private float certainty;

            public OSResult(string name, float ct)
            {
                osName = name;
                certainty = ct;
            }
            public string getName()
            {
                return osName;
            }
            public float getCertainty()
            {
                return certainty;
            }
        }
    }
}
