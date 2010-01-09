using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _3dWaves
{
    class Program
    {
        #region Parts
        private int frameWidth = 1024;

        private int frameHeight = 768;

        private WaveViewer waveViewer;

        private WaveBuilder waveBuilder = new WaveBuilder();

        private BraneBuilder braneBuilder = new BraneBuilder();

        private double precision = 0.01;
        #endregion

        #region Constructors
        public Program()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            waveViewer = new WaveViewer(frameWidth, frameHeight, precision);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Start the application
        /// </summary>
        private void Start()
        {
            Random random = new Random();

            waveViewer.BraneMatrixHeight = BuildMatrix(random);
            waveViewer.BraneMatrixHue = BuildMatrix(random);
            waveViewer.BraneMatrixSaturation = BuildMatrix(random);
            waveViewer.BraneMatrixStraff = BuildMatrix(random);

            Application.Run(waveViewer);
        }

        private BraneMatrix BuildMatrix(Random random)
        {
            IWave wave1 = waveBuilder.Build(random);
            wave1.Normalize();

            IWave wave2 = waveBuilder.Build(random);
            wave2.Normalize();

            return braneBuilder.Build(wave1, wave2, precision, frameWidth, frameHeight);
        }
        #endregion

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program program = new Program();
            program.Start();
        }
        #endregion
    }
}