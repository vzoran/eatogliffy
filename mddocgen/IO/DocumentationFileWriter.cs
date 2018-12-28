using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        private const string IMAGE_FOLDER = @"auto/images/";
        private const string FRAGMENT_FOLDER = @"auto/fragments/";
        private const string IMAGE_INDEX_FRAGMENT = "img_index.md";
        private const string MASTER_DOC = "master.md";

        // it is needed to force UTF-8 wothout BOM
        private UTF8Encoding uTF8Encoding = new UTF8Encoding(false);

        private string MasterFullPath {
            get
            {
                return targetFolder + MASTER_DOC;
            }
        }

        private string ImageIndexFullPath
        {
            get
            {
                return targetFolder + FRAGMENT_FOLDER + IMAGE_INDEX_FRAGMENT;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="targetFolderName">Root of the generated documentation</param>
        public DocumentationFileWriter(string targetFolderName)
        {
            this.targetFolder = targetFolderName;
            if(!targetFolder.EndsWith(@"\"))
            {
                targetFolder += @"\";
            }
        }

        /// <inheritdoc />
        public ImageReference CreateImageReference(long itemId, string itemName)
        {
            ImageReference imageReference;
            string fileName = createFileName("I", itemId, itemName, FileType.Image);

            imageReference.imageID = "I" + itemId.ToString().PadLeft(5, '0');
            imageReference.fullImagePath = targetFolder + IMAGE_FOLDER + fileName;

            string referenceLine = String.Format("[{0}]: {1} \"{2}\" ", imageReference.imageID, IMAGE_FOLDER.Replace(@"\", "/") + fileName, itemName);
            
            File.AppendAllLines(ImageIndexFullPath, new List<string>() { referenceLine });

            return imageReference;
        }

        /// <inheritdoc />
        public string WriteFragment(string prefix, long itemId, string itemName, string fragment)
        {
            string fileName = createFileName(prefix, itemId, itemName, FileType.Fragment);

            if(fragment != null)
            {
                File.WriteAllText(targetFolder + FRAGMENT_FOLDER + fileName, fragment);
            }
            
            return FRAGMENT_FOLDER + fileName;
        }

        /// <inheritdoc />
        public void CleanUp(bool isFull)
        {
            if(isFull)
            {
                if (Directory.Exists(targetFolder + IMAGE_FOLDER))
                {
                    Directory.Delete(targetFolder + IMAGE_FOLDER, true);
                }

                if (Directory.Exists(targetFolder + FRAGMENT_FOLDER))
                {
                    Directory.Delete(targetFolder + FRAGMENT_FOLDER, true);
                }

                if (File.Exists(MasterFullPath))
                {
                    File.Delete(MasterFullPath);
                }

                Directory.CreateDirectory(targetFolder + IMAGE_FOLDER);
                Directory.CreateDirectory(targetFolder + FRAGMENT_FOLDER);
            } else {
                File.Delete(ImageIndexFullPath);
            }
        }

        /// <inheritdoc />
        public void FinalizeMaster()
        {
            WriteToMasterDoc(FRAGMENT_FOLDER + IMAGE_INDEX_FRAGMENT, true);
        }

        /// <inheritdoc />
        public void AddMetaInfo(string key, string value)
        {
            WriteToMasterDoc(String.Format("{0}: {1}  " + Environment.NewLine, key, value), false);
        }

        /// <summary>
        /// Writes string to the master.md file
        /// </summary>
        /// <param name="textContent"></param>
        /// <param name="isRef"></param> 
        public void WriteToMasterDoc(string textContent, bool isRef = true, int intend = 0)
        {
            if(isRef)
            {
                textContent = String.Format("{0}{{{{{1}}}}}" + Environment.NewLine, new String('#', intend) + (intend > 0 ? " " : String.Empty), textContent);
            }

            File.AppendAllText(this.MasterFullPath, textContent, uTF8Encoding);
        }

        private string createFileName(string prefix, long itemId, string itemName, FileType fileType)
        {
            return String.Format("{0}{1}_{2}.{3}", 
                prefix.ToUpper().Trim(), 
                itemId.ToString().PadLeft(5, '0'), 
                itemName.Replace(" ", "_").Replace("/", "").ToLower().Trim(),
                fileType == FileType.Fragment ? "md" : "png");
        }
    }
}
