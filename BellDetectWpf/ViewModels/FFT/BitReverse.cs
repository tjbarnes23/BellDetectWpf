namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        // Do bit reversal of specified number of places of an uint. For example, 1101 bit-reversed is 1011
        // Bits in the uint that are higher than numBits are left unchanged
        // x parameter is the uint to be bit-reversed
        // numBits parameter is number of bits in the number to be reversed (counted from the lowest bit)
        public static uint BitReverse(uint x, uint numBits)
        {
            uint y = 0;

            for (uint i = 0; i < numBits; i++)
            {
                y <<= 1;
                y |= x & 0x0001;
                x >>= 1;
            }

            return y;
        }
    }
}
