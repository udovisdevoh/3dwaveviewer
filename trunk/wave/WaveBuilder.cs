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

            int waveCount = random.Next(1, random.Next(1, 4) * random.Next(1, 4));

            double amplitude, phase, frequency;
            WaveFunction waveFunction;

            for (int i = 0; i < waveCount; i++)
            {
                amplitude = random.NextDouble();
                phase = random.NextDouble();
                if (random.Next(0, 2) == 1)
                    phase *= -1.0;

                waveFunction = WaveFunctions.GetRandomWaveFunction(random);
                frequency = random.NextDouble() * (double)(random.Next(1, 4)) * (double)(random.Next(1, 4));
                wavePack.Add(new Wave(amplitude, frequency, phase, waveFunction));
            }
            
            return wavePack;
        }

        public IWave BuildSinePlusSquare()
        {
            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(1.0,2,0,WaveFunctions.Sine));
            wavePack.Add(new Wave(1.0, 4, 0.5, WaveFunctions.Square));
            return wavePack;
        }

        public IWave BuildSawPlusSine()
        {
            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(1.0, 2, 0, WaveFunctions.Saw));
            wavePack.Add(new Wave(1.0, 4, 0.5, WaveFunctions.Sine));
            return wavePack;
        }
    }
}
