namespace ImajeLib
{
    public class InkInfo
    {
        private bool _accuLow;
        private bool _recovMedium;
        private bool _recovHigh;
        private bool _linkHigh;
        private bool _viscoHigh;
        private bool _viscoLow;
        private bool _accuHigh;
        private bool _linkLow;

        public InkInfo(byte[] data)
        {
            this.LoadData(data);
        }

        private void LoadData(byte[] data)
        {
            byte levelByte = data[1];
            this._linkLow = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._accuHigh = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._viscoLow = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._viscoHigh = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._linkHigh = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._recovHigh = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._recovMedium = (levelByte & 1) == 1;
            levelByte >>= 1;
            this._accuLow = (levelByte & 1) == 1;
        }

        public bool AccuLow
        {
            get { return _accuLow; }
            set { _accuLow = value; }
        }

        public bool RecovMedium
        {
            get { return _recovMedium; }
            set { _recovMedium = value; }
        }

        public bool RecovHigh
        {
            get { return _recovHigh; }
            set { _recovHigh = value; }
        }

        public bool LinkHigh
        {
            get { return _linkHigh; }
            set { _linkHigh = value; }
        }

        public bool ViscoHigh
        {
            get { return _viscoHigh; }
            set { _viscoHigh = value; }
        }

        public bool ViscoLow
        {
            get { return _viscoLow; }
            set { _viscoLow = value; }
        }

        public bool AccuHigh
        {
            get { return _accuHigh; }
            set { _accuHigh = value; }
        }

        public bool LinkLow
        {
            get { return _linkLow; }
            set { _linkLow = value; }
        }
    }
}