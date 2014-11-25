using System.IO;

namespace ArchAngel.Designer
{
    internal class Branding
    {
        private static bool IsInitialized;
        private static string _FormTitle = System.Windows.Forms.Application.ProductName;
        private static string _SplashImageFile = "";

        private static void Initialize()
        {
            if (!IsInitialized)
            {
                string brandingFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "branding.settings");

                if (File.Exists(brandingFile))
                {
                    string text = Slyce.Common.Utility.ReadTextFile(brandingFile);

                    string formTitle = GetInnerXml(text, "designer_form_title");

                    if (!string.IsNullOrEmpty(formTitle))
                    {
                        _FormTitle = formTitle;
                    }
                    string splashFile = GetInnerXml(text, "designer_splash_image");

                    if (!string.IsNullOrEmpty(splashFile))
                    {
                        splashFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), splashFile);

                        if (File.Exists(splashFile))
                        {
                            _SplashImageFile = splashFile;
                        }
                    }
                }
                IsInitialized = true;
            }
        }

        internal static string FormTitle
        {
            get
            {
                Initialize();
                return _FormTitle;
            }
        }

        internal static string SplashImageFile
        {
            get
            {
                Initialize();
                return _SplashImageFile;
            }
        }


        private static string GetInnerXml(string xml, string elementName)
        {
            int startPos = xml.IndexOf(string.Format("<{0}>", elementName));

            if (startPos >= 0)
            {
                startPos += elementName.Length + 2;
                int endPos = xml.IndexOf(string.Format("</{0}>", elementName), startPos);

                if (endPos > startPos)
                {
                    return xml.Substring(startPos, endPos - startPos);
                }
            }
            return "";
        }
    }
}
