using System.Runtime.InteropServices;

namespace AreWeThereAlreadyLol
{
    static class Program
    {
        static void Main(string[] args)
        {

        }

        [DllImport(@"C:\ExampleDLL.dll")]
        private static extern uint Addition(uint a, uint b);
    }
}
