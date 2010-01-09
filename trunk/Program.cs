using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _3dWaves
{
    class Program
    {
        #region Parts
        private WaveViewer waveViewer;

        private WaveBuilder waveBuilder = new WaveBuilder();

        private BraneBuilder braneBuilder = new BraneBuilder();
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
            
            waveViewer.BraneMatrix = braneBuilder.Build(wave1, wave2);


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