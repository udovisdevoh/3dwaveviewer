using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3dWaves
{
    class BraneBuilder
    {
        #region Fields
        private double precision;

        private int frameWidth;

        private int frameHeight;
        #endregion

        #region Public Methods
        public BraneMatrix Build(IWave wave1, IWave wave2, double precision, int frameWidth, int frameHeight)
        {
            this.precision = precision;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            BraneMatrix braneMatrix = new BraneMatrix();

            SingleWaveViewModel singleWaveModel;
            for (double currentWaveCount = 0.0; currentWaveCount <= 2.0; currentWaveCount += precision)
            {
                singleWaveModel = BuildSingleWaveModel(wave1, wave2, currentWaveCount);
                braneMatrix.Add(singleWaveModel);
            }

            return braneMatrix;
        }
        #endregion

        #region Private Methods
        private SingleWaveViewModel BuildSingleWaveModel(IWave primaryWave, IWave secondaryWave, double currentWaveCount)
        {
            double minimumPosition = -0.5;
            double maximumPosition = 1.5;
            int totalCount = (int)((maximumPosition - minimumPosition) / precision);

            SingleWaveViewModel singleWaveModel = new SingleWaveViewModel();

            int x;
            int heightOffset;
            for (double wavePosition = minimumPosition; wavePosition <= maximumPosition; wavePosition += precision)
            {
                x = (int)(wavePosition * (double)frameWidth);

                heightOffset = (int)(primaryWave.GetYValueAt(wavePosition) * 50.0);
                heightOffset += (int)(secondaryWave.GetYValueAt(currentWaveCount) * 50.0);

                int key = x + frameWidth / 2;

                singleWaveModel[key] = heightOffset;
            }
            return singleWaveModel;
        }
        #endregion
    }
}
