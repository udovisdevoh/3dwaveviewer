using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _3dWaves
{
    class Program
    {
        #region Parts
        private int frameWidth = 800;

        private int frameHeight = 600;

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

            IWave waveHeight1 = waveBuilder.Build(random);
            waveHeight1.Normalize();
            
            IWave waveHeight2 = waveBuilder.Build(random);
            waveHeight2.Normalize();

            waveViewer.BraneMatrixHeight = braneBuilder.Build(waveHeight1, waveHeight2, precision, frameWidth, frameHeight);

            IWave waveHue1 = waveBuilder.Build(random);
            waveHue1.Normalize();

            IWave waveHue2 = waveBuilder.Build(random);
            waveHue2.Normalize();

            waveViewer.BraneMatrixHue = braneBuilder.Build(waveHue1, waveHue2, precision, frameWidth, frameHeight);

            Application.Run(waveViewer);
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