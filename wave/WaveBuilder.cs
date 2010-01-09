﻿using System;
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
                waveFunction = WaveFunctions.GetRandomWaveFunction(random);
                frequency = random.NextDouble() * random.Next(1, 4) * random.Next(1, 4);
                wavePack.Add(new Wave(amplitude, frequency, phase, waveFunction));
            }
            
            return wavePack;
        }
    }
}
