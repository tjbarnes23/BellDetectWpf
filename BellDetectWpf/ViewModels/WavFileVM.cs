namespace BellDetectWpf.ViewModels
{
    public static class WavFileVM
    {
        public static uint FileSize { get; set; }
        public static uint FormatParametersSize { get; set; }
        public static ushort WavType { get; set; }
        public static ushort NumChannels { get; set; }
        public static uint DataRate { get; set; }
        public static ushort BlockAlignment { get; set; }
        public static ushort BitsPerSamplePerChannel { get; set; }
        public static uint DataSizeBytes { get; set; }
        public static string FilePathName { get; set; }
    }
}
