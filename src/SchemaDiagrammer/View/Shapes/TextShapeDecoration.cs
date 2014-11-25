using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Globalization;

namespace SchemaDiagrammer.View.Shapes
{
	public class TextShapeDecoration : Adorner
	{
		public int TextSize { get; set; }
		public bool CentreText { get; set; }

		public Connection parent;

		public TextShapeDecoration(Connection decoratedShape) :
			base (decoratedShape)
		{
			TextSize = 12;
			parent = decoratedShape;
		}

		#region Text Attached Property

		/// <value>Identifies the Text dependency property</value>
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(TextShapeDecoration),
			                                    new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		
		#endregion

		#region Anchor Attached Property

		// Using a DependencyProperty as the backing store for Anchor.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty AnchorProperty =
			DependencyProperty.Register("Anchor", typeof(Point), typeof(TextShapeDecoration),
			                                    new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));

		public Point Anchor
		{
			get
			{
				return (Point)GetValue(AnchorProperty);	
			}
			set
			{
				SetValue(AnchorProperty, value);
			}
		}

		#endregion

		protected override void OnRender(DrawingContext drawingContext)
		{
			if (parent.Visible == false) return;
			
			string text = Text ?? "";

			if(text == "")
			{
				return;
			}

			FormattedText formattedText = new FormattedText(
				text,
				CultureInfo.CurrentUICulture,
				FlowDirection,
				new Typeface("Verdana"),
				TextSize,
				Brushes.DarkBlue);
			double halfTextWidth = formattedText.Width/2;

			var textLocation = Anchor;
			if(CentreText) textLocation -= new Vector(halfTextWidth, 0);

			Rect rect = new Rect(textLocation - new Vector(5, 5), new Size(formattedText.Width+10, formattedText.Height+10));
			Pen pen = new Pen(Brushes.DarkBlue, 2);

			drawingContext.DrawRoundedRectangle(Brushes.LightBlue, pen, rect, 2, 2);
			drawingContext.DrawText(formattedText, textLocation);
		}	
	}
}