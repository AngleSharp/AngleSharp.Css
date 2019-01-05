namespace AngleSharp.Css.Tests.Mocks
{
    sealed class MockRenderDevice : IRenderDevice
    {
        public DeviceCategory Category
        {
            get;
            set;
        }

        public int ColorBits
        {
            get;
            set;
        }

        public int DeviceHeight
        {
            get;
            set;
        }

        public int DeviceWidth
        {
            get;
            set;
        }

        public int Frequency
        {
            get;
            set;
        }

        public bool IsGrid
        {
            get;
            set;
        }

        public bool IsInterlaced
        {
            get;
            set;
        }

        public bool IsScripting
        {
            get;
            set;
        }

        public int MonochromeBits
        {
            get;
            set;
        }

        public int Resolution
        {
            get;
            set;
        }

        public int ViewPortHeight
        {
            get;
            set;
        }

        public int ViewPortWidth
        {
            get;
            set;
        }

        public IBrowsingContext Context
        {
            get;
            set;
        }
    }
}
