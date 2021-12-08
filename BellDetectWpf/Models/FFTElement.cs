namespace BellDetectWpf.Models
{
    public class FFTElement
    {
        // Element for linked list in which we store the input/output data
        // We use a linked list because for sequential access it's faster than array index

        public double Re { get; set; } = 0.0; // Real component
        public double Im { get; set; } = 0.0; // Imaginary component
        public FFTElement Next { get; set; } // Next element in linked list
        public uint RevTarget { get; set; } // Target position post bit-reversal
    }
}
