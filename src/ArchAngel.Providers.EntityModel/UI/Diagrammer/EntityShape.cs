using System.Collections.ObjectModel;
using System.Windows;
using ArchAngel.Providers.EntityModel.Model.EntityLayer;
using SchemaDiagrammer.View.Shapes;

namespace ArchAngel.Providers.EntityModel.UI.Diagrammer
{
	public class EntityShape : DiagramShape
	{
        public static readonly DependencyProperty IsPropertyListVisibleProperty =
			DependencyProperty.Register("IsPropertyListVisible", typeof(bool), typeof(EntityShape), new FrameworkPropertyMetadata(false));
		public static readonly DependencyProperty IsInheritedPropertyListVisibleProperty =
			DependencyProperty.Register("IsInheritedPropertyListVisible", typeof(bool), typeof(EntityShape), new FrameworkPropertyMetadata(false));

		public static readonly DependencyProperty HasConcretePropertiesProperty =
			DependencyProperty.Register("HasConcreteProperties", typeof(bool), typeof(EntityShape), new FrameworkPropertyMetadata(false));
		public static readonly DependencyProperty HasInheritedPropertiesProperty =
			DependencyProperty.Register("HasInheritedProperties", typeof(bool), typeof(EntityShape), new FrameworkPropertyMetadata(false));

		private ObservableCollection<Property> properties = new ObservableCollection<Property>();
		private ObservableCollection<Property> inheritedProperties = new ObservableCollection<Property>();

		static EntityShape()
        {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EntityShape), new FrameworkPropertyMetadata(typeof(EntityShape)));
        }

		public EntityShape()
		{
			properties.CollectionChanged += (s, e) => HasConcreteProperties = properties.Count > 0;
			inheritedProperties.CollectionChanged += (s, e) => HasInheritedProperties = inheritedProperties.Count > 0;
		}

        public bool IsPropertyListVisible
        {
            get { return (bool)GetValue(IsPropertyListVisibleProperty); }
            set { SetValue(IsPropertyListVisibleProperty, value); }
        }

		public bool HasConcreteProperties
		{
			get { return (bool)GetValue(HasConcretePropertiesProperty); }
			set { SetValue(HasConcretePropertiesProperty, value); }
		}

		public bool HasInheritedProperties
		{
			get { return (bool)GetValue(HasInheritedPropertiesProperty); }
			set { SetValue(HasInheritedPropertiesProperty, value); }
		}

		public bool IsInheritedPropertyListVisible
		{
			get { return (bool)GetValue(IsInheritedPropertyListVisibleProperty); }
			set { SetValue(IsInheritedPropertyListVisibleProperty, value); }
		}

		public ObservableCollection<Property> Properties
		{
			get { return properties; }
		}

		public ObservableCollection<Property> InheritedProperties
		{
			get { return inheritedProperties; }
		}
	}
}
