using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Casse_Brique
{
    class MapReader
    {
        
        private static string line;
        public static string[] tabLine = new string[1];
        private static string[][] split = new string[15][];
        private static string directory = Directory.GetCurrentDirectory();
        private static int y = 0;

    private static void readLigneFichier(int level)
    {

    System.IO.StreamReader file = new System.IO.StreamReader(directory+"\\level\\level" + level +".txt");

    while((line = file.ReadLine()) != null)
    {
        tabLine[y] = line;
        y++;

    }
    file.Close();
    }

    private static void SplitLigne()
    {
        for (int i = 0; i < tabLine.Length; i++)
        {
            split[i] = tabLine[i].Split(' ');
        }

    }
        
    public static string[][] getMap(int level)
    {
        readLigneFichier(level);
        SplitLigne();
        return split;
    }

    }
}
