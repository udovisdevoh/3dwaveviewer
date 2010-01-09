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
            waveViewer.Wave1 = waveBuilder.Build(random);
            waveViewer.Wave1.Normalize();

            waveViewer.Wave2 = waveBuilder.Build(random);
            waveViewer.Wave2.Normalize();

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