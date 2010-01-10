using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    /// <summary>
    /// Represents a wave
    /// </summary>
    class Wave : IWave
    {
        #region Fields
        /// <summary>
        /// Amplitude (from 0 to 1)
        /// </summary>
        private double amplitude;

        /// <summary>
        /// Amount of wave cycle per common time span
        /// </summary>
        private double frequency;

        /// <summary>
        /// Phase, from -1 to 1
        /// </summary>
        private double phase;

        /// <summary>
        /// Wave's function
        /// </summary>
        private WaveFunction waveFunction;

        private WaveCache waveCache = new WaveCache();
        #endregion

        #region Constructors
        /// <summary>
        /// Create a wave
        /// </summary>
        /// <param name="amplitude">Amplitude (from 0 to 1)</param>
        /// <param name="frequency">Amount of wave cycle per common time span</param>
        /// <param name="phase">Phase, from -1 to 1</param>
        public Wave(double amplitude, double frequency, double phase)
            : this(amplitude, frequency, phase, null)
        {
        }

        /// <summary>
        /// Create a wave
        /// </summary>
        /// <param name="amplitude">Amplitude (from 0 to 1)</param>
        /// <param name="frequency">Amount of wave cycle per common time span</param>
        /// <param name="phase">Phase, from -1 to 1</param>
        /// <param name="waveFunction">wave function (default: Math.sin)</param>
        public Wave(double amplitude, double frequency, double phase, WaveFunction waveFunction)
        {
            if (waveFunction == null)
                waveFunction = Math.Sin;

            this.amplitude = amplitude;
            this.frequency = frequency;
            this.phase = phase;
            this.waveFunction = waveFunction;
        }
        #endregion

        #region Operator Overloads
        public static IWave operator +(Wave wave1, IWave wave2)
        {
            WavePack wavePack = new WavePack();
            wavePack.Add(wave1);
            wavePack.Add(wave2);
            return wavePack;
        }
        #endregion

        #region IEquatable<IWave> Members
        public bool Equals(IWave other)
        {
            if (other is Wave)
            {
                Wave otherWave = (Wave)other;
                return phase == otherWave.phase && frequency == otherWave.frequency && amplitude == otherWave.amplitude && waveFunction == otherWave.waveFunction;
            }
            else if (other is WavePack)
            {
                return ((WavePack)other)[0].Equals(this);
            }
            return false;
        }
        #endregion

        #region IWave Members
        public double GetYValueAt(double x)
        {
            double value = 0.0;

            if (waveCache.ContainsKey(x))
            {
                value = waveCache.Get(x);
            }
            else
            {
                x += (phase / frequency);
                value = waveFunction(Math.PI * x * frequency) * amplitude * -1.0;
                waveCache.Add(x, value);
            }

            return value;
        }

        public void Normalize()
        {
            amplitude = 1.0;
        }
        #endregion
    }

}
