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
    }
}