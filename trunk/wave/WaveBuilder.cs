using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    class WaveBuilder
    {
        public IWave Build(Random random)
        {
            WavePack wavePack = new WavePack();

            wavePack.Add(new Wave(random.NextDouble() * 0.2, 1 * random.Next(1, 5), random.NextDouble(), WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.2, 3 * random.Next(1, 5), random.NextDouble(), WaveFunctions.GetRandomWaveFunction(random)));

            
            return wavePack;
        }
    }
}
