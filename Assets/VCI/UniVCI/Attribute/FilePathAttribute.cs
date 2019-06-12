using UnityEngine;

namespace VCI
{
#if UNITY_EDITOR
    public class FilePathAttribute : PropertyAttribute
    {

        public string extensionFilter;
        public FilePathAttribute(string extensionFilter = "")
        {
            if (string.IsNullOrEmpty(extensionFilter))
            {
                extensionFilter = "";
            }
            this.extensionFilter = extensionFilter;
        }
    }
#endif
}




