using System;

namespace Resonant
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Resonant())
                game.Run();
        }
    }
}
