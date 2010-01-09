using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3dWaves
{
    public partial class WaveViewer : Form
    {
        #region Fields
        private int frameWidth = 800;

        private int frameHeight = 600;

        private IWave wave1;

        private IWave wave2;

        private Pen blackPen = new Pen(Color.Black);

        private Pen whitePen = new Pen(Color.White);

        private Pen pen = new Pen(Color.White);

        private double precision = 0.01;
        #endregion

        #region Constructor
        public WaveViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Protected Methods
        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            Graphics graphics = paintEvent.Graphics;

            List<double[]> braneMatrix = BuildBraneMatrix(wave1, wave2);
            DrawBraneMatrix(graphics, braneMatrix);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build brane matrix
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="wave">source wave</param>
        private List<double[]> BuildBraneMatrix(IWave primaryWave, IWave secondaryWave)
        {
            List<double[]> braneMatrix = new List<double[]>();

            double[] singleWaveModel;
            for (double currentWaveCount = 0.0; currentWaveCount <= 2.0; currentWaveCount += precision)
            {
                singleWaveModel = BuildSingleWaveModel(primaryWave, secondaryWave, currentWaveCount);
                braneMatrix.Add(singleWaveModel);
            }

            return braneMatrix;
        }

        private void DrawBraneMatrix(Graphics graphics, List<double[]> braneMatrix)
        {
            double currentWaveCount = 0.0;
            double previousWaveCount = 0.0;
            double[] previousSingleWaveModel = null;
            foreach (double[] singleWaveModel in braneMatrix)
            {
                DrawSingleWaveModel(graphics, singleWaveModel, previousSingleWaveModel, currentWaveCount, previousWaveCount);
                currentWaveCount += precision;
                
                previousSingleWaveModel = singleWaveModel;
                previousWaveCount = currentWaveCount;
            }
        }

        private double[] BuildSingleWaveModel(IWave primaryWave, IWave secondaryWave, double currentWaveCount)
        {
            double minimumPosition = -0.5;
            double maximumPosition = 1.5;
            int totalCount = (int)((maximumPosition - minimumPosition) / precision);

            double[] singleWaveModel = new double[totalCount *8];

            int x;
            int heightOffset;
            for (double wavePosition = minimumPosition; wavePosition <= maximumPosition; wavePosition += precision)
            {
                x = (int)(wavePosition * (double)frameWidth);

                heightOffset = (int)(primaryWave.GetYValueAt(wavePosition) * 50.0);
                heightOffset += (int)(secondaryWave.GetYValueAt(currentWaveCount) * 50.0);

                int key = x+ frameWidth/2;

                singleWaveModel[key] = heightOffset;
            }
            return singleWaveModel;
        }

        private void DrawSingleWaveModel(Graphics graphics, double[] singleWaveModel, double[] previousSingleWaveModel, double currentWaveCount, double previousWaveCount)
        {
            int x, y, previousX = -1, previousY = -1;
            int previousWaveModelX = -1, previousWaveModelY = -1;
            for (double wavePosition = -0.5; wavePosition <= 1.5; wavePosition += precision)
            {
                BuildDrawingPositions(singleWaveModel, wavePosition, currentWaveCount, out x, out y);
                
                if (previousSingleWaveModel != null)
                    BuildDrawingPositions(previousSingleWaveModel, wavePosition, previousWaveCount, out previousWaveModelX, out previousWaveModelY);

                

                if (previousX != -1)
                    graphics.DrawLine(pen, previousX, previousY, x, y);

                if (previousSingleWaveModel != null)
                    graphics.DrawLine(pen, previousWaveModelX, previousWaveModelY,x ,y);

                previousX = x;
                previousY = y;
            }
        }

        private void BuildDrawingPositions(double[] singleWaveModel, double wavePosition, double currentWaveCount, out int x, out int y)
        {
            int heightOffset;

            x = (int)(wavePosition * (double)frameWidth);
            y = (int)(currentWaveCount * (double)(frameHeight));

            y -= x / 2;

            int key = x + frameWidth / 2;
            heightOffset = (int)singleWaveModel[key];


            pen.Color = BuildPenColor(y);

            x += y - (frameWidth / 2);

            y += heightOffset;

            if (y > frameHeight)
                y = frameHeight;
            else if (y < 0)
                y = 0;

            if (x > frameWidth)
                x = frameWidth;
            else if (x < 0)
                x = 0;
        }

        private Color BuildPenColor(int height)
        {
            int lightness = (int)((double)(height) / frameHeight * 255);
            if (lightness < 0)
                lightness = 0;
            else if (lightness > 255)
                lightness = 255;
            return Color.FromArgb(lightness, lightness, lightness);
        }
        #endregion

        #region Properties
        public IWave Wave1
        {
            get { return wave1; }
            set { wave1 = value; }
        }

        public IWave Wave2
        {
            get { return wave2; }
            set { wave2 = value; }
        }
        #endregion
    }
}
