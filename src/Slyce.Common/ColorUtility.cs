// See: http://www.bobpowell.net/RGBHSB.htm
using System;
using System.Drawing;


namespace Slyce.Common
{
    public class Colors
    {
        private static Color _baseColor = Color.FromKnownColor(KnownColor.ActiveCaption);
        public static Color ThemeColor = Color.FromKnownColor(KnownColor.ActiveCaption);
        private static Color _backgroundColor = Color.Empty;
		  private static Color _backgroundColor2 = Color.Empty;
        private static Color _fadingTitleLightColor = Color.Empty;
        private static Color _fadingTitleDarkColor = Color.Empty;

        public static Color GetBaseColorVariant(double brightness)
        {
            return ChangeBrightness(BaseColor, brightness);
        }

        // See: http://www.codeproject.com/tips/JbColorContrast.asp?df=100&forumid=39709&exp=0&select=875951#xx875951xx
        //-------------------------------------------------------------------------//
        // Create a color that contrast a given color                              //
        // This algorithm is taken from my research on color science               //
        //-------------------------------------------------------------------------//
        //public static Color GetContrastingColor(Color color, int nThreshold)
        //{
        //    int HUE_UNDEF = Convert.ToInt32("0x000000FF", 16);
        //    int nOrigLum, nCalcLum, nLoop;
        //    int nHue, nSat, nBri;
        //    int nRed, nGreen, nBlue;

        //    nRed = color.R;
        //    nGreen = color.G;
        //    nBlue = color.B;
        //    RGBHSL.HSL hsl = RGBHSL.RGB_to_HSL(color);
        //    nOrigLum = hsl.L;

        //    if (hsl.H == HUE_UNDEF)
        //    {
        //        nRed = nGreen = nBlue = 0;
        //        if (hsl.L < 50) nRed = nGreen = nBlue = 255;
        //    }
        //    else
        //    {
        //        nHue = (hsl.H + 120) % 240;
        //        nLoop = 20;
        //        while (nLoop)
        //        {
        //            Color newColor = RGBHSL.HSL_to_RGB(
        //            SfxHSBtoRGB(nHue, nSat, nBri, &nRed, &nGreen, &nBlue);
        //            nCalcLum = LUMINANCE(nRed, nGreen, nBlue);
        //            if (abs(nOrigLum - nCalcLum) >= nThreshold) break;
        //            if (nOrigLum <= 50)
        //            {
        //                nSat -= 5;
        //                if (nSat < 0) nSat += 5;
        //                nBri += 10;
        //                if (nBri > 100) nBri = 100;
        //            }
        //            else
        //            {
        //                nSat += 5;
        //                if (nSat > 100) nSat = 100;
        //                nBri -= 5;
        //                if (nBri < 10) nBri = 10;
        //            }
        //            nLoop--;
        //        }
        //    }
        //    return Color.FromArgb(nRed, nGreen, nBlue);
        //}

        public static Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                // Reset dependent colours
                _backgroundColor = Color.Empty;
                _fadingTitleDarkColor = Color.Empty;
                _fadingTitleLightColor = Color.Empty;
            }
        }

        public static Color IdealTextColor(Color backgroundColor)
        {
            // See: http://www.codeproject.com/useritems/IdealTextColor.asp
            const int nThreshold = 105;
            int bgDelta = Convert.ToInt32((backgroundColor.R * 0.299) + (backgroundColor.G * 0.587) + (backgroundColor.B * 0.114));
            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }

        public static double GetBrightness(Color color)
        {
            return RGBHSL.RGB_to_HSL(color).L;
        }

        /// <summary>
        /// 
        /// </summary>
		/// <param name="color"></param>
		/// <param name="brightness">Valid values are 0.0 to 1.0. 0 is dark, 1 is light.</param>
        /// <returns></returns>
        public static Color ChangeBrightness(Color color, double brightness)
        {
            if (brightness < 0 || brightness > 1)
            {
                throw new InvalidOperationException("Brightness parameter must have a value between 0 and 1.");
            }

            return RGBHSL.SetBrightness(color, brightness);
        }

        public static Color BackgroundColor
        {
            get
            {
                if (_backgroundColor == Color.Empty)
                {
						 DevComponents.DotNetBar.ColorScheme cs = new DevComponents.DotNetBar.ColorScheme(DevComponents.AdvTree.eColorSchemeStyle.Office2007);
						 _backgroundColor = cs.PanelBackground;
                    //_backgroundColor = GetBaseColorVariant(0.90);
                }
                return _backgroundColor;
            }
        }

		  public static Color BackgroundColor2
		  {
			  get
			  {
				  if (_backgroundColor2 == Color.Empty)
				  {
					  DevComponents.DotNetBar.ColorScheme cs = new DevComponents.DotNetBar.ColorScheme(DevComponents.AdvTree.eColorSchemeStyle.Office2007);
					  _backgroundColor2 = cs.PanelBackground2;
					  //_backgroundColor = GetBaseColorVariant(0.90);
				  }
				  return _backgroundColor2;
			  }
		  }

        public static Color BackgroundColorDark
        {
            get
            {
                if (_fadingTitleDarkColor == Color.Empty)
                {
                    _fadingTitleDarkColor = GetBaseColorVariant(0.2);
                }
                return _fadingTitleDarkColor;
            }
        }

        public static Color FadingTitleLightColor
        {
            get
            {
                if (_fadingTitleLightColor == Color.Empty)
                {
                    _fadingTitleLightColor = GetBaseColorVariant(0.7);
                }
                return _fadingTitleLightColor;
            }
        }

        public static Color FadingTitleDarkColor
        {
            get
            {
                if (_fadingTitleDarkColor == Color.Empty)
                {
                    _fadingTitleDarkColor = GetBaseColorVariant(0.3);
                }
                return _fadingTitleDarkColor;
            }
        }

    }

    public class RGBHSL
    {
        public class HSL
        {
            public HSL()
            {
                _h = 0;
                _s = 0;
                _l = 0;
            }

            double _h;
            double _s;
            double _l;

            public double H
            {
                get { return _h; }
                set
                {
                    _h = value;
                    _h = _h > 1 ? 1 : _h < 0 ? 0 : _h;
                }
            }

            public double S
            {
                get { return _s; }
                set
                {
                    _s = value;
                    _s = _s > 1 ? 1 : _s < 0 ? 0 : _s;
                }
            }

            public double L
            {
                get { return _l; }
                set
                {
                    _l = value;
                    _l = _l > 1 ? 1 : _l < 0 ? 0 : _l;
                }
            }
        }

    	/// <summary> 
        /// Sets the absolute brightness of a colour 
        /// </summary> 
        /// <param name="c">Original colour</param> 
        /// <param name="brightness">The luminance level to impose</param> 
        /// <returns>an adjusted colour</returns> 
        public static Color SetBrightness(Color c, double brightness)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.L = brightness;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Modifies an existing brightness level 
        /// </summary> 
        /// <remarks> 
        /// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1 
        /// </remarks> 
        /// <param name="c">The original colour</param> 
        /// <param name="brightness">The luminance delta</param> 
        /// <returns>An adjusted colour</returns> 
        public static Color ModifyBrightness(Color c, double brightness)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.L *= brightness;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Sets the absolute saturation level 
        /// </summary> 
        /// <remarks>Accepted values 0-1</remarks> 
        /// <param name="c">An original colour</param> 
        /// <param name="Saturation">The saturation value to impose</param> 
        /// <returns>An adjusted colour</returns> 
        public static Color SetSaturation(Color c, double Saturation)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.S = Saturation;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Modifies an existing Saturation level 
        /// </summary> 
        /// <remarks> 
        /// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1 
        /// </remarks> 
        /// <param name="c">The original colour</param> 
        /// <param name="Saturation">The saturation delta</param> 
        /// <returns>An adjusted colour</returns> 
        public static Color ModifySaturation(Color c, double Saturation)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.S *= Saturation;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Sets the absolute Hue level 
        /// </summary> 
        /// <remarks>Accepted values 0-1</remarks> 
        /// <param name="c">An original colour</param> 
        /// <param name="Hue">The Hue value to impose</param> 
        /// <returns>An adjusted colour</returns> 
        public static Color SetHue(Color c, double Hue)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.H = Hue;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Modifies an existing Hue level 
        /// </summary> 
        /// <remarks> 
        /// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1 
        /// </remarks> 
        /// <param name="c">The original colour</param> 
        /// <param name="Hue">The Hue delta</param> 
        /// <returns>An adjusted colour</returns> 
        public static Color ModifyHue(Color c, double Hue)
        {
            HSL hsl = RGB_to_HSL(c);
            hsl.H *= Hue;
            return HSL_to_RGB(hsl);
        }

        /// <summary> 
        /// Converts a colour from HSL to RGB 
        /// </summary> 
        /// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks> 
        /// <param name="hsl">The HSL value</param> 
        /// <returns>A Color structure containing the equivalent RGB values</returns> 
        public static Color HSL_to_RGB(HSL hsl)
        {
            double r = 0, g = 0, b = 0;
            double temp1, temp2;

            if (hsl.L == 0)
            {
                r = g = b = 0;
            }
            else
            {
                if (hsl.S == 0)
                {
                    r = g = b = hsl.L;
                }
                else
                {
                    temp2 = ((hsl.L <= 0.5) ? hsl.L * (1.0 + hsl.S) : hsl.L + hsl.S - (hsl.L * hsl.S));
                    temp1 = 2.0 * hsl.L - temp2;

                    double[] t3 = new double[] { hsl.H + 1.0 / 3.0, hsl.H, hsl.H - 1.0 / 3.0 };
                    double[] clr = new double[] { 0, 0, 0 };

                    for (int i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                            t3[i] += 1.0;
                        if (t3[i] > 1)
                            t3[i] -= 1.0;

                        if (6.0 * t3[i] < 1.0)
                            clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                        else if (2.0 * t3[i] < 1.0)
                            clr[i] = temp2;
                        else if (3.0 * t3[i] < 2.0)
                            clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                        else
                            clr[i] = temp1;
                    }
                    r = clr[0];
                    g = clr[1];
                    b = clr[2];
                }
            }
            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
        }

        /// <summary> 
        /// Converts RGB to HSL 
        /// </summary> 
        /// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks> 
        /// <param name="c">A Color to convert</param> 
        /// <returns>An HSL value</returns> 
        public static HSL RGB_to_HSL(Color c)
        {
            HSL hsl = new HSL {H = (c.GetHue()/360.0), L = c.GetBrightness(), S = c.GetSaturation()};
        	return hsl;
        }
    }
}