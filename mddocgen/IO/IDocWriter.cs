namespace MdDocGenerator.IO
{
    /// <summary>
    /// Struct for storing all image references
    /// </summary>
    public struct ImageReference
    {
        /// <summary>
        /// ID of the image in image catalog file
        /// </summary>
        public string imageID;

        /// <summary>
        /// Full path of the image, where expected to be saved
        /// </summary>
        public string fullImagePath;
    }

    /// <summary>
    /// Interface for writing document fragment
    /// </summary>
    public interface IDocWriter
    {
        /// <summary>
        /// Persist a fragment
        /// </summary>
        /// <param name="prefix">A prefix to separate different kinds of fragment such as diagram or package</param>
        /// <param name="itemId">Id of the source item in EA</param>
        /// <param name="itemName">Name of the source item in EA. Could be ambigous.</param>
        /// <param name="fragment">Text of the fragment</param>
        /// <returns>Fragment reference</returns>
        string WriteFragment(string prefix, long itemId, string itemName, string fragment);

        /// <summary>
        /// Generates a proper reference to an image fragment.
        /// </summary>
        /// <param name="itemId">Id of the source item in EA</param>
        /// <param name="itemName">Name of the source item in EA. Could be ambigous.</param>
        /// <returns>Image reference</returns>
        ImageReference CreateImageReference(long itemId, string itemName);

        /// <summary>
        /// Initialize the target (if needed)
        /// </summary>
        void Initialize();

        /// <summary>
        /// Write a single line to the master document
        /// </summary>
        /// <param name="textContent">The content will be written</param>
        /// <param name="isRef">Is a reference line? In this case custom formatting  will be used</param>
        /// <param name="intend">Intendation</param>
        void WriteToMasterDoc(string textContent, bool isRef = true, int intend = 0);

        /// <summary>
        /// Finalize master document
        /// </summary>
        void FinalizeMaster();

        /// <summary>
        /// Write metadata to the master document
        /// </summary>
        /// <param name="key">Meta key</param>
        /// <param name="value">Meta data</param>
        void AddMetaInfo(string key, string value);
    }
}