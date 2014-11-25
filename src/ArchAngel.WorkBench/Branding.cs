using System.IO;
using ArchAngel.Common;

namespace ArchAngel.Workbench
{
    internal class Branding
    {
        private static bool IsInitialized = false;
        private static string _FormTitle = System.Windows.Forms.Application.ProductName;
        private static string _SplashImageFile = "";
        private static string _ProductName = "ArchAngel";

		public static ApplicationBrand ProductBranding = ApplicationBrand.ArchAngel;

		static Branding()
		{
			#if VISUAL_NHIBERNATE
            ProductBranding = ApplicationBrand.VisualNHibernate;
			#endif
		}

        private static void Initialize()
        {
            if (!IsInitialized)
            {
            	//LoadfromBrandingFile();

				switch(ProductBranding)
				{
					case ApplicationBrand.ArchAngel:
						_ProductName = "ArchAngel";
						_FormTitle = "ArchAngel";
						break;
					case ApplicationBrand.VisualNHibernate:
						_ProductName = "Visual NHibernate";
						_FormTitle = "Visual NHibernate";
						break;
				}

            	IsInitialized = true;
            }
        }

    	private static void LoadfromBrandingFile()
    	{
    		string brandingFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "branding.settings");

    		if (File.Exists(brandingFile))
    		{
    			string text = Slyce.Common.Utility.ReadTextFile(brandingFile);

    			string formTitle = GetInnerXml(text, "workbench_form_title");

    			if (!string.IsNullOrEmpty(formTitle))
    			{
    				_FormTitle = formTitle;
    			}
    			string productName = GetInnerXml(text, "product_name");

    			if (!string.IsNullOrEmpty(productName))
    			{
    				_ProductName = productName;
    			}
    			string splashFile = GetInnerXml(text, "workbench_splash_image");

    			if (!string.IsNullOrEmpty(splashFile))
    			{
    				splashFile = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), splashFile);

    				if (File.Exists(splashFile))
    				{
    					_SplashImageFile = splashFile;
    				}
    			}
    		}
    	}

    	internal static string FormTitle
        {
            get
            {
                if(!IsInitialized) { Initialize(); }
                return _FormTitle;
            }
        }

        internal static string ProductName
        {
            get
            {
                if (!IsInitialized) { Initialize(); }
                return _ProductName;
            }
        }

        internal static string SplashImageFile
        {
            get
            {
                if (!IsInitialized) { Initialize(); }
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
