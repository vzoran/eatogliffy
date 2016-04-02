using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.tools
{
    public class BuilderTools
    {
        public static string COLOR_DEFAULT = "#FFFFFF";
        public static string COLOR_BLACK = "#000000";

        public static string hexConverter(int color)
        {
            return hexConverter(color, null);
        }

        public static string hexConverter(int color, string overrideDefault)
        {
            var b = ((color >> 16) & 0xff);
            var g = ((color >> 8) & 0xff);
            var r = (color & 0xff);

            string colorStr = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

            if (colorStr.Equals(COLOR_DEFAULT) && !String.IsNullOrEmpty(overrideDefault))
            {
                colorStr = overrideDefault;
            }

            return colorStr;
        }
    }
}
