using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdDocGenerator.IO
{
    public interface IDocWriter
    {
        void WritePackageContent(long itemId, string itemName, string itemString);
        void WriteImageReference(long itemId, string itemName);
        string GetImageFolder(long itemId, string itemName);

    }
}
