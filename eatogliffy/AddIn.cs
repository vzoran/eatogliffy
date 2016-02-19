using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EA;
using System.Windows.Forms;

namespace eatogliffy
{
    [ComVisible(true)]
    public class AddIn
    {
        private const string menuNameMain = "-&Gliffy Export";
        private const string menuNameDebug = "Debug";
        private const string menuNameExportAll = "Export All";
        private const string menuNameExportSelected = "Export Active Diagram";

        // Called Before EA starts to check Add-In Exists
        public string EA_Connect(Repository repository)
        {
            // nothing special
            return "eatogliffy.AddIn - connected";
        }

        // Called when user Click Add-Ins Menu item.
        public object EA_GetMenuItems(Repository repository, string location, string menuName)
        {
            switch (menuName)
            {
                case "":
                    return menuNameMain;

                case menuNameMain:
                    string[] subMenu = { menuNameDebug, menuNameExportAll, menuNameExportSelected };
                    return subMenu;
            }
            return "";
        }

        // Sets the state of the menu depending if there is
        // an active project or not
        static bool IsProjectOpen(Repository repository)
        {
            try
            {
                return null != repository.Models;
            }
            catch
            {
                return false;
            }
        }

        // Called once Menu has been opened to see what menu
        // items are active.
        public void EA_GetMenuState(Repository repository, string location, string menuName, string itemName,
            ref bool isEnabled, ref bool isChecked)
        {
            isEnabled = IsProjectOpen(repository);
        }

        // Called when user makes a selection in the menu.
        // This is your main exit point to the rest of your Add-in
        public void EA_MenuClick(Repository repository, string location, string menuName, string itemName)
        {
            switch (itemName)
            {
                case "Project statistic...":
                    var count = 0;
                    foreach (Package model in repository.Models)
                        foreach (Package package in model.Packages)
                            count += CountClasses(package);
                    MessageBox.Show("This project contains "
                        + count + " " + (count == 1 ? "class" : "classes"));
                    break;
            }
        }

        private static int CountClasses(Package package)
        {
            var count = 0;
            foreach (Element e in package.Elements)
                if (e.Type == "Class")
                    count++;
            foreach (Package p in package.Packages)
                count += CountClasses(p);
            return count;
        }

        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
