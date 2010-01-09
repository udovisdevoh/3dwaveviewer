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
        private int width = 800;

        private int height = 600;

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
            Graphics graphics = paintEvent.Graphics;

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
