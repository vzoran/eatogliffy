using System;
using System.Runtime.InteropServices;
using EA;
using System.Windows.Forms;
using MdDocGenerator.Builder;

namespace MdDocGenerator
{
    [ComVisible(true)]
    public class AddIn
    {
        private const string menuNameMain = "-&Create documentation";
        private const string menuNameFull = "-&Full package";
                
        // Called Before EA starts to check Add-In Exists
        public string EA_Connect(Repository repository)
        {
            // nothing special
            return "MDDocGenerator.AddIn - connected";
        }

        // Called when user Click Add-Ins Menu item.
        public object EA_GetMenuItems(Repository repository, string location, string menuName)
        {
            switch (menuName)
            {
                case "":
                    return menuNameMain;

                case menuNameMain:
                    string[] subMenu = { menuNameFull };
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
                case menuNameFull:
                    BuildFullDocumentation(repository);
                    break;

                default:
                    MessageBox.Show(String.Format("Unexpected menu click. [{0}]", itemName), "Error", MessageBoxButtons.OK);
                    break;
            }
        }

        private void BuildFullDocumentation(Repository repository)
        {
            try
            {
                DocumentationBuilder docBuilder = new DocumentationBuilder();
                docBuilder
                    .SetTargetFolder(@"d:\temp")
                    .SetEaRepository(repository)
                    .Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + ex.StackTrace);
                throw;
            }
        }

        ///
        /// EA calls this operation when it exists. Can be used to do some cleanup work.
        ///
        public void EA_Disconnect()
        {
            GC.WaitForPendingFinalizers();
        }
    }
}
