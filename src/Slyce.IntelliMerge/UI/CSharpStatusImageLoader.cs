using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ArchAngel.Providers.CodeProvider;

namespace Slyce.IntelliMerge.UI
{
	public abstract class ImageLoader
	{
		private const string ResourcePrefix = "Slyce.IntelliMerge.Resources.";
		protected readonly ImageList statusImageList;
		private readonly Image fileImage;

		public enum Status
		{
			Conflict,
			Error,
			Resolved,
			Unknown,
			ExactCopy
		}

		protected ImageLoader()
		{
			statusImageList = new ImageList();

			statusImageList.Images.Add(Status.Conflict.ToString(), LoadImage("StatusImages.conflict.png"));
			statusImageList.Images.Add(Status.Error.ToString(), LoadImage("StatusImages.error.png"));
			statusImageList.Images.Add(Status.Resolved.ToString(), LoadImage("StatusImages.resolved.png"));
			statusImageList.Images.Add(Status.Unknown.ToString(), LoadImage("StatusImages.unknown.png"));
			statusImageList.Images.Add(Status.ExactCopy.ToString(), LoadImage("StatusImages.blank.png"));

			fileImage = LoadImage("VSProject_genericfile.bmp");
		}

		public virtual Image GetFileImage()
		{
			return fileImage;
		}

		protected static Image LoadImage(string fileName)
		{
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourcePrefix + fileName);
			if (stream == null)
				throw new ArgumentException("Could not find image " + fileName + " in the embedded resources");
			return Image.FromStream(stream);
		}

		public abstract Image GetImageForBaseConstruct(IBaseConstruct bc, Status status);
	}

	public class CSharpStatusImageLoader : ImageLoader
	{
		private readonly ImageList cSharpImageList;
		private readonly Image fileImage;

		private enum CSharpConstructType
		{
			Class,
			Field,
			Method,
			Property,
			Namespace,
			Event,
			Constant,
			Operator,
			Struct,
			Interface,
			Enum,
			Delegate
		}

		private readonly Dictionary<CSharpConstructType, Dictionary<Status, Image>> combinedImages;

		public CSharpStatusImageLoader()
		{
			fileImage = LoadImage("CSharp.VSProject_CSCodefile.bmp");

			cSharpImageList = new ImageList();
			combinedImages = new Dictionary<CSharpConstructType, Dictionary<Status, Image>>();

			cSharpImageList.Images.Add(CSharpConstructType.Class.ToString(),	LoadImage("CSharp.VSObject_Class.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Constant.ToString(), LoadImage("CSharp.VSObject_Constant.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Delegate.ToString(), LoadImage("CSharp.VSObject_Delegate.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Enum.ToString(),		LoadImage("CSharp.VSObject_Enum.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Event.ToString(),	LoadImage("CSharp.VSObject_Event.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Field.ToString(),	LoadImage("CSharp.VSObject_Field.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Interface.ToString(),LoadImage("CSharp.VSObject_Interface.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Method.ToString(),	LoadImage("CSharp.VSObject_Method.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Namespace.ToString(),LoadImage("CSharp.VSObject_Namespace.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Operator.ToString(), LoadImage("CSharp.VSObject_Operator.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Property.ToString(), LoadImage("CSharp.VSObject_Properties.bmp"));
			cSharpImageList.Images.Add(CSharpConstructType.Struct.ToString(),	LoadImage("CSharp.VSObject_Structure.bmp"));

			cSharpImageList.TransparentColor = Color.Magenta;
		}

		public override Image GetFileImage()
		{
			return fileImage;
		}

		public override Image GetImageForBaseConstruct(IBaseConstruct bc, Status status)
		{
			if (!(bc is ArchAngel.Providers.CodeProvider.DotNet.BaseConstruct))
				throw new ArgumentException("Given BaseConstruct is not a CSharp construct");
			
			CSharpConstructType consType = GetConsType(bc);
			
			if(combinedImages.ContainsKey(consType) == false)
			{
				// Construct the new image
				combinedImages[consType] = new Dictionary<Status, Image>();
				Bitmap image = CreateImage(consType, status);
				combinedImages[consType].Add(status, image);
				return image;
			}
			else
			{
				Dictionary<Status, Image> dict = combinedImages[consType];

				if(dict.ContainsKey(status))
				{
					return dict[status];
				}
				Bitmap image = CreateImage(consType, status);
				combinedImages[consType].Add(status, image);
				return image;
			}
		}

		private Bitmap CreateImage(CSharpConstructType consType, Status status)
		{
			Bitmap image = new Bitmap(32, 16);
			//Create a new Bitmap and Graphics object to write too.
			Graphics OutPutGraphics = Graphics.FromImage(image);
			OutPutGraphics.Clear(Color.Transparent);

			//Write the two new images at the x and y values
			Image fileTypeImage = cSharpImageList.Images[consType.ToString()];
			if (fileTypeImage == null)
				throw new InvalidOperationException(
					"Cannot find the file type image. The CSharpConstructType enum has been updated without updating the image list.");

			Image statusImage = statusImageList.Images[status.ToString()];
			if (statusImage == null)
				throw new InvalidOperationException(
					"Cannot find the status image. The Status enum has been updated without updating the image list.");

			ImageAttributes attr = new ImageAttributes();
//			attr.SetColorKey(Color.Magenta, Color.Magenta);

			Rectangle dstRect = new Rectangle(0, 0, 16, 16);
			OutPutGraphics.DrawImage(fileTypeImage, dstRect, 0, 0, fileTypeImage.Width, fileTypeImage.Height, GraphicsUnit.Pixel, attr);
			
			dstRect = new Rectangle(16, 0, 16, 16);
			OutPutGraphics.DrawImage(statusImage, dstRect, 0, 0, statusImage.Width, statusImage.Height, GraphicsUnit.Pixel, attr);
			return image;
		}

		private static CSharpConstructType GetConsType(IBaseConstruct bc)
		{
			CSharpConstructType consType;

			if (bc is ArchAngel.Providers.CodeProvider.DotNet.Namespace)
			{
				consType = CSharpConstructType.Namespace;
			}
			else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Class)
			{
				consType = CSharpConstructType.Class;
			}
			else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Field)
			{
				consType = CSharpConstructType.Field;
			}
			else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Delegate)
			{
				consType = CSharpConstructType.Delegate;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Event)
			{
				consType = CSharpConstructType.Event;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Enumeration)
			{
				consType = CSharpConstructType.Enum;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Function)
			{
				consType = CSharpConstructType.Method;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Constructor)
			{
				consType = CSharpConstructType.Method;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.InterfaceMethod)
			{
				consType = CSharpConstructType.Method;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Constant)
			{
				consType = CSharpConstructType.Constant;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Interface)
			{
				consType = CSharpConstructType.Interface;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.InterfaceProperty)
			{
				consType = CSharpConstructType.Property;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.PropertyAccessor)
			{
				consType = CSharpConstructType.Property;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Operator)
			{
				consType = CSharpConstructType.Operator;
			}
            else if (bc is ArchAngel.Providers.CodeProvider.DotNet.Struct)
			{
				consType = CSharpConstructType.Struct;
			}
			else
			{
				consType = CSharpConstructType.Field;
			}
			return consType;
		}
	}
}
