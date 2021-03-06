﻿using System;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using EA;
using EaToGliffy.Gliffy.Model;
using System.Windows.Forms;
using EaToGliffy.Gliffy.Builder.Core;

namespace EaToGliffy
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
            return "EaToGliffy.AddIn - connected";
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
                case menuNameExportSelected:
                    BuildCurrent(repository);
                    break;

                case menuNameExportAll:
                    MessageBox.Show("This function is under construction.", "Info", MessageBoxButtons.OK);
                    break;

                case menuNameDebug:
                    BuildDebug(repository);
                    break;
            }
        }

        private void BuildDebug(Repository repository)
        {
            try
            {
                System.IO.File.WriteAllText("d:\\repository.json", new JavaScriptSerializer()
                        .Serialize(repository));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + ex.StackTrace);
                throw;
            }
        }

        private void BuildCurrent(Repository repository)
        {

            try
            {
                DiagramBuilder diagramBuilder = new DiagramBuilder();
                GliffyDiagram gliffyDiagram = diagramBuilder
                    .WithContentType(DiagramBuilder.DEFAULT_CONTENT_TYPE)
                    .WithVersion(DiagramBuilder.DEFAULT_VERSION)
                    .FromActiveDiagram(repository)
                    .Build()
                    .GetDiagram();

                var json = new JavaScriptSerializer()
                    .Serialize(gliffyDiagram);

                System.IO.File.WriteAllText("d:\\temp.Gliffy", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " " + ex.StackTrace);
                throw;
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
           GC.WaitForPendingFinalizers();
        }
    }
}
