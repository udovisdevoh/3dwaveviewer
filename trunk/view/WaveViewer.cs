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
        private BraneMatrix braneMatrixHeight;

        private BraneMatrix braneMatrixHue;

        private int frameWidth;

        private int frameHeight;

        private Pen pen = new Pen(Color.White);

        private double precision;
        #endregion

        #region Constructor
        public WaveViewer(int frameWidth, int frameHeight, double precision)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.precision = precision;
            InitializeComponent();
        }
        #endregion

        #region Protected Methods
        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            Graphics graphics = paintEvent.Graphics;
            Draw(graphics, braneMatrixHeight, braneMatrixHue);
        }
        #endregion

        #region Private Methods
        private void Draw(Graphics graphics, BraneMatrix braneMatrixHeight, BraneMatrix braneMatrixHue)
        {
            double currentWaveCount = 0.0;
            double previousWaveCount = 0.0;
            SingleWaveViewModel previousSingleWaveModel = null;
            int counter = 0;
            foreach (SingleWaveViewModel singleWaveModelHeight in braneMatrixHeight)
            {
                DrawSingleWaveModel(graphics, singleWaveModelHeight, braneMatrixHue[counter], previousSingleWaveModel, currentWaveCount, previousWaveCount);
               
                previousSingleWaveModel = singleWaveModelHeight;
                previousWaveCount = currentWaveCount;

                currentWaveCount += precision;
                counter++;
            }
        }

        private void DrawSingleWaveModel(Graphics graphics, SingleWaveViewModel singleWaveModelHeight, SingleWaveViewModel singleWaveModelHue, SingleWaveViewModel previousSingleWaveModel, double currentWaveCount, double previousWaveCount)
        {
            int x, y, previousX = -1, previousY = -1;
            int previousWaveModelX = -1, previousWaveModelY = -1;
            double hue;
            for (double wavePosition = -0.5; wavePosition <= 1.5; wavePosition += precision)
            {
                BuildDrawingPositions(singleWaveModelHeight, wavePosition, currentWaveCount, out x, out y);

                hue = BuildHue(singleWaveModelHue, wavePosition, currentWaveCount);
                
                if (previousSingleWaveModel != null)
                    BuildDrawingPositions(previousSingleWaveModel, wavePosition, previousWaveCount, out previousWaveModelX, out previousWaveModelY);

                pen.Color = BuildPenColor(y, hue);

                if (previousX != -1)
                    graphics.DrawLine(pen, previousX, previousY, x, y);

                if (previousSingleWaveModel != null)
                    graphics.DrawLine(pen, previousWaveModelX, previousWaveModelY, x, y);

                previousX = x;
                previousY = y;
            }
        }

        private double BuildHue(SingleWaveViewModel singleWaveModelHue, double wavePosition, double currentWaveCount)
        {
            int x = (int)(wavePosition * (double)frameWidth);
            int y = (int)(currentWaveCount * (double)(frameHeight));

            y -= x / 2;

            int key = x + frameWidth / 2;

            double hue = singleWaveModelHue[key];

            hue *= 2;

            while (hue < 0.0)
                hue += 255.0;
            while (hue > 255.0)
                hue -= 255.0;

            return hue;
        }

        private void BuildDrawingPositions(SingleWaveViewModel singleWaveModel, double wavePosition, double currentWaveCount, out int x, out int y)
        {
            int heightOffset;

            x = (int)(wavePosition * (double)frameWidth);
            y = (int)(currentWaveCount * (double)(frameHeight));

            y -= x / 2;

            int key = x + frameWidth / 2;
            heightOffset = (int)singleWaveModel[key];


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

        private Color BuildPenColor(int height, double hue)
        {
            int lightness = (int)((double)(height) / frameHeight * 255);
            if (lightness < 0)
                lightness = 0;
            else if (lightness > 255)
                lightness = 255;

            return ColorFromHSV(hue, 0.5, (double)lightness / 255.0);
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        #endregion

        #region Properties
        public BraneMatrix BraneMatrixHeight
        {
            get { return braneMatrixHeight; }
            set { braneMatrixHeight = value; }
        }

        public BraneMatrix BraneMatrixHue
        {
            get { return braneMatrixHue; }
            set { braneMatrixHue = value; }
        }

        public int FrameWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public double Precision
        {
            get { return precision; }
            set { precision = value; }
        }
        #endregion
    }
}
