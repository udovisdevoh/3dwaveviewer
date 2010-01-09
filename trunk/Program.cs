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
            waveViewer = new WaveViewer();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Start the application
        /// </summary>
        private void Start()
        {
            Random random = new Random();

            IWave wave1 = waveBuilder.Build(random);
            //wave1 = waveBuilder.BuildSinePlusSquare();
            wave1.Normalize();
            
            IWave wave2 = waveBuilder.Build(random);
            //wave2 = waveBuilder.BuildSawPlusSine();
            wave2.Normalize();

            BraneMatrix braneMatrix = braneBuilder.Build(wave1, wave2, precision, frameWidth, frameHeight);

            waveViewer.Precision = precision;
            waveViewer.FrameWidth = frameWidth;
            waveViewer.FrameHeight = frameHeight;

            waveViewer.BraneMatrix = braneMatrix;

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