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

            DrawHalfBrane(graphics, wave1, wave2, false);
            DrawHalfBrane(graphics, wave2, wave1, true);

            /*
            double wave1Height, wave2Height;
            int x, y, previousX = -1, previousY = -1;

            for (double wave1Position = 0.0; wave1Position <= 1.0; wave1Position += 0.01)
            {
                wave1Height = wave1.GetYValueAt(wave1Position) + 1.0;

                x = (int)(wave1Position * (double)width);
                y = (int)(wave1Height * (double)(height /2));

                if (y > height)
                    y = height;
                else if (y < 0)
                    y = 0;

                if (x > width)
                    x = width;
                else if (x < 0)
                    x = 0;

                if (previousX!= -1)
                    graphics.DrawLine(whitePen, previousX, previousY, x,y);

                previousX = x;
                previousY = y;
            }
            */
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Draw half of a brain
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="wave">source wave</param>
        /// <param name="isInverted">whether the picture is horizontally mirrored</param>
        private void DrawHalfBrane(Graphics graphics, IWave primaryWave, IWave secondaryWave, bool isInverted)
        {
            for (double currentWaveCount = 0.0; currentWaveCount <= 2.0; currentWaveCount += precision)
                DrawString(graphics, primaryWave, secondaryWave, isInverted, currentWaveCount);
        }

        private void DrawString(Graphics graphics, IWave primaryWave, IWave secondaryWave, bool isInverted, double currentWaveCount)
        {
            int x, y, previousX = -1, previousY = -1;
            double waveHeight;
            int heightOffset;
            for (double wavePosition = -0.5; wavePosition <= 1.5; wavePosition += precision)
            {
                waveHeight = primaryWave.GetYValueAt(wavePosition) + 1.0;

                x = (int)(wavePosition * (double)frameWidth);
                y = (int)(currentWaveCount * (double)(frameHeight));

                if (isInverted)
                    y -= frameHeight - (x / 2);
                else
                    y -= x / 2;

                if (isInverted)
                {
                    heightOffset = (int)(primaryWave.GetYValueAt(1.0 - wavePosition) * 50.0);
                    heightOffset += (int)(secondaryWave.GetYValueAt(1.0 - currentWaveCount) * 50.0);
                }
                else
                {
                    heightOffset = (int)(primaryWave.GetYValueAt(wavePosition) * 50.0);
                    heightOffset += (int)(secondaryWave.GetYValueAt(currentWaveCount) * 50.0);
                }

                pen.Color = BuildPenColor(y);

                if (isInverted)
                    x -= y - (frameWidth / 2);
                else
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

                if (previousX != -1)
                    graphics.DrawLine(pen, previousX, previousY, x, y);

                previousX = x;
                previousY = y;
            }
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
