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
        private IWave wave1;

        private IWave wave2;

        private Pen blackPen = new Pen(Color.Black);

        private Pen whitePen = new Pen(Color.White);

        private Pen currentPen;
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
            //ViewTerrain(paintEvent);
            //ViewSurface(paintEvent);
            ViewTerrain3d(paintEvent);            
        }
        #endregion

        #region Private Methods
        private void ViewTerrain3d(PaintEventArgs paintEvent)
        {
            Graphics graphics = paintEvent.Graphics;

            double wave1Height, wave2Height;
            int x, y, previousX = -1, previousY = -1;

            for (double wave1Position = 0.0; wave1Position <= 1.0; wave1Position += 0.01)
            {
                wave1Height = wave1.GetYValueAt(wave1Position) + 1.0;

                x = (int)(wave1Position * 640.0);
                y = (int)(wave1Height * 240.0);
                
                if (y > 480)
                    y = 480;
                else if (y < 0)
                    y = 0;

                if (x > 640)
                    x = 640;
                else if (x < 0)
                    x = 0;

                if (previousX!= -1)
                    graphics.DrawLine(blackPen, previousX, previousY, x,y);

                previousX = x;
                previousY = y;
            }
        }

        private void ViewSurface(PaintEventArgs paintEvent)
        {
            Graphics gfx = paintEvent.Graphics;

            double realX, realY;
            double z1, z2;
            double brightnessDouble;
            int brightness;

            for (int pixelX = 0; pixelX < 800; pixelX++)
            {
                realX = (double)(pixelX) / 800.0 * 2.0;
                z1 = wave2.GetYValueAt(realX);
                
                for (int pixelY = 0; pixelY < 600; pixelY++)
                {
                    realY = (double)(pixelY) / 600.0 * 2.0;
                    z2 = wave2.GetYValueAt(realY);

                    brightnessDouble = ((z1 + z2) / 2.0 + 0.5) * 255.0;
                    if (brightnessDouble > 255.0)
                        brightnessDouble = 255.0;
                    else if (brightnessDouble < 0)
                        brightnessDouble = 0;

                    brightness = (int)brightnessDouble;

                    blackPen = new Pen(Color.FromArgb(255,brightness,brightness,brightness));

                    gfx.DrawRectangle(blackPen, pixelX, pixelY, 1, 1);
                }
            }
        }

        private void ViewTerrain(PaintEventArgs paintEvent)
        {
            Graphics gfx = paintEvent.Graphics;

            int yOrigin = this.Height / 2;
            int yOrigin2 = this.Height / 2;
            int y, y2;
            double realX, realX2;

            int[] previousLineHeight = new int[800];

            for (int x2 = 0; x2 < 400; x2++)
            {
                realX2 = (double)(x2) / 800.0 * 2.0;
                y2 = yOrigin2 + (int)(wave2.GetYValueAt(realX2) * yOrigin2 * 0.8);


                int previousPointY = -1;
                int previousPointX = -1;
                for (int x = 0; x < 800; x++)
                {
                    realX = (double)(x) / 800.0 * 2.0;
                    y = yOrigin + (int)(wave1.GetYValueAt(realX) * yOrigin * 0.8);
                    y -= y2;


                    if ( x % 2 == 0)
                        gfx.DrawLine(whitePen, previousPointX + x2 * 2, previousLineHeight[x] - x / 2, x + x2 * 2, y + x2 * 2 - x / 2);
                    else
                        gfx.DrawLine(blackPen, previousPointX + x2 * 2, previousLineHeight[x] - x / 2, x + x2 * 2, y + x2 * 2 - x / 2);
                    previousLineHeight[x] = y + x2 * 2;

                    if (previousPointX != -1)
                    {
                        if (x2 % 2 == 0)
                            gfx.DrawLine(blackPen, previousPointX + x2 * 2 , previousPointY + x2 * 2 - 1 - x / 2, x + x2 * 2, y + x2 * 2 - 1 - x / 2);
                        else
                            gfx.DrawLine(whitePen, previousPointX + x2 * 2, previousPointY + x2 * 2 - 1 - x / 2, x + x2 * 2, y + x2 * 2 - 1 - x / 2);
                    }
                    previousPointY = y;
                    previousPointX = x;
                }
            }
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
