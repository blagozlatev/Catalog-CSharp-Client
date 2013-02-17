namespace MiniatureBottleWPFDesktopClient.Nomenclatures
{
    public static class Constants
    {
        public static class Image
        {
            public static int AngleRotation { get { return 90; } }
            public static int FullRotation { get { return 360; } }
            public static int NullRotation { get { return 0; } }
            public static double ScaleImageStep { get { return 0.2; } }
            public static int DivisorForCenterOfImage { get { return 2; } }
            public static int MinimumZoomPercentage { get { return 30; } }
            public static int MaximumZoomPercentage { get { return 400; } }
        }

        public static class General
        {
            public static int Zero { get { return 0; } }
            public static int One { get { return 1; } }
            public static int Hundred { get { return 100; } }
        }
    }
}
