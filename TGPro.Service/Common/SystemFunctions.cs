using System;
using System.IO;

namespace TGPro.Service.Common
{
    public class SystemFunctions
    {
        public static string DefaultProductImageCaption(string name)
        {
            string caption = LowerStringHasUnderscore(name) + "_Default_Image";
            return caption;
        }

        public static string ProductImageCaption(string name, int identity)
        {
            string caption = LowerStringHasUnderscore(name) + "_Image_" + identity;
            return caption;
        }

        public static string LowerString(string inputString)
        {
            string rString = inputString.ToLower();
            return rString;
        }

        public static string LowerStringHasUnderscore(string inputString)
        {
            string rString = inputString.ToLower().Replace(" ", "_");
            return rString;
        }

        public static string LowerStringHasDash(string inputString)
        {
            string rString = inputString.ToLower().Replace(" ", "-");
            return rString;
        }

        public static string GenerateFileName(string inputFileName, string inputOriginalFileName)
        {
            string rString = $"{inputFileName}-{Guid.NewGuid()}{Path.GetExtension(inputOriginalFileName)}";
            return rString;
        }
    }
}
