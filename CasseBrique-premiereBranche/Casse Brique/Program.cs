#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

using CasseBriqueTitleMenu;

namespace Casse_Brique
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        //private static MainWindow titleMenu = new MainWindow(PlaySolo);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //titleMenu.Show();
            using (var game = new Game1())
                game.Run();
        }
        /*
        //[STAThread]
        public static void PlaySolo()
        {
            titleMenu.Hide();
            using (var game = new Game1())
                game.Run();
            Main();
        }

        public static void WaitPlaySolo()
        {
            while (titleMenu.Response != UserAction.PlaySolo);
        }*/
    }
#endif
}