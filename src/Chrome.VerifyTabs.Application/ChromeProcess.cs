using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Automation;
using static System.Console;

namespace Chrome.VerifyTabs.Application
{
    public class ChromeProcess
    {
        public bool VerifyTab(string nameTab)
        {
            var processesOfChrome = Process.GetProcessesByName("chrome").ToList();

            if (processesOfChrome.Any())
            {
                WriteLine("Chrome is running...");

                foreach (var process in processesOfChrome)
                {
                    if (process.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }

                    var root = AutomationElement.FromHandle(process.MainWindowHandle);
                    var conditionNewTab = new PropertyCondition(AutomationElement.NameProperty, "Nova guia");
                    var elementNewTab = root.FindFirst(TreeScope.Descendants, conditionNewTab);
                    var treeWalker = TreeWalker.ControlViewWalker;
                    var elementTabStrip = treeWalker.GetParent(elementNewTab);
                    var conditionTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                    foreach (AutomationElement tabItem in elementTabStrip.FindAll(TreeScope.Children, conditionTabItem))
                    {
                        var nameTabItem = tabItem.Current.Name;
                        if (nameTabItem.Equals(nameTab))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                WriteLine("Chrome is not running...");
            }
            return false;
        }
    }
}
