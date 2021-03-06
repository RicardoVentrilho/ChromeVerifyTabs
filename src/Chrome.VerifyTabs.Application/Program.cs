﻿using System.Diagnostics;
using static System.Console;

namespace Chrome.VerifyTabs.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tabName = args[(int)OrdinationEnum.FIRST];
            var url = args[(int)OrdinationEnum.SECOND];

            var chromeProcess = new ChromeProcess();

            if (chromeProcess.VerifyTab(tabName))
            {
                WriteLine("Found tab...");
                Process.Start("chrome", url);
            }

#if DEBUG
            ReadKey();
#endif
        }
    }
}
