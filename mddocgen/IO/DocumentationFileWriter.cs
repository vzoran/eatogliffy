using System;
using System.Collections.Generic;
using System.IO;

namespace MdDocGenerator.IO
{
    /// <summary>
    /// Supported file types in the documentation package
    /// </summary>
    enum FileType
    {
        Fragment,
        Image
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class DocumentationFileWriter : IDocWriter
    {
        private string targetFolder;
        private const string IMAGE_FOLDER = @"auto\images\";
        private const string FRAGMENT_FOLDER = @"auto\fragments\";
        private const string IMAGE_INDEX_FRAGMENT = "img_index.md";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="targetFolderName">Root of the generated documentation</param>
        public DocumentationFileWriter(string targetFolderName)
        {
            this.targetFolder = targetFolderName;
            if(!targetFolder.EndsWith("\\"))
            {
                targetFolder += "\\";
            }
        }

        /// <inheritdoc />
        public ImageReference CreateImageReference(long itemId, string itemName)
        {
            ImageReference imageReference;
            string fileName = createFileName("I", itemId, itemName, FileType.Image);

            imageReference.imageID = "I" + itemId.ToString().PadLeft(5, '0');
            imageReference.fullImagePath = targetFolder + FRAGMENT_FOLDER + fileName;

            string referenceLine = String.Format("[{0}]: {1} \"{2}\" ", imageReference.imageID, IMAGE_FOLDER.Replace(@"\", "/") + fileName, itemName);
            
            File.AppendAllLines(imageReference.fullImagePath, new List<string>() { FRAGMENT_FOLDER.Replace(@"\", "/") + fileName });

            return imageReference;
        }

        /// <inheritdoc />
        public string WriteFragment(string prefix, long itemId, string itemName, string fragment)
        {
            string fileName = createFileName(prefix, itemId, itemName, FileType.Fragment); 
            File.WriteAllText(targetFolder + FRAGMENT_FOLDER + fileName, fragment);

            return FRAGMENT_FOLDER + fileName;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            if (Directory.Exists(targetFolder + IMAGE_FOLDER))
            {
                Directory.Delete(targetFolder + IMAGE_FOLDER, true);
            }

            if(Directory.Exists(targetFolder + FRAGMENT_FOLDER))
            {
                Directory.Delete(targetFolder + FRAGMENT_FOLDER, true);
            }
            
            Directory.CreateDirectory(targetFolder + IMAGE_FOLDER);
            Directory.CreateDirectory(targetFolder + FRAGMENT_FOLDER);
        }

        private string createFileName(string prefix, long itemId, string itemName, FileType fileType)
        {
            return String.Format("{0}{1}_{2}.{3}", 
                prefix.ToUpper().Trim(), 
                itemId.ToString().PadLeft(5, '0'), 
                itemName.Replace(" ", "_").ToLower().Trim(),
                fileType == FileType.Fragment ? "md" : "png");
        }
    }
}
