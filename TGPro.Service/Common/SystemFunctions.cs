using System;
using System.IO;

namespace TGPro.Service.Common
{
    public class SystemFunctions
    {
        public static string BlankProductImageCaption(string name)
        {
            return LowerStringHasUnderscore(name) + "_blank_image";
        }

        public static string ProductImageCaption(string name, int identity)
        {
            return LowerStringHasUnderscore(name) + "_image_" + identity;
        }

        public static string LowerString(string inputString)
        {
            return inputString.ToLower();
        }

        public static string LowerStringHasUnderscore(string inputString)
        {
            return inputString.ToLower().Replace(" ", "_");
        }

        public static string LowerStringHasDash(string inputString)
        {
            return inputString.ToLower().Replace(" ", "-");
        }

        public static string GenerateFileName(string inputFileName, string inputOriginalFileName)
        {
            return $"{inputFileName}-{Guid.NewGuid()}{Path.GetExtension(inputOriginalFileName)}";
        }
    }
}
