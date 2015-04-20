using System;

namespace ArchAngel.Interfaces.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All,
                   AllowMultiple = true,
                   Inherited = true)]
    public sealed class ArchAngelEditorAttribute : Attribute
    {
    	private Type[] _AlternativeForms = new Type[0];

    	public bool VirtualPropertiesAllowed { get; set; }

		/// <summary>
		/// Whether objects of this class can be used as Iterator objects.
		/// </summary>
    	public bool IsGeneratorIterator { get; set; }

    	public string PreviewDisplayProperty { get; set; }

		/// <summary>
		/// This is set to static text that will be shown when this object is previewed.
		/// The use of this property overrides the PreviewDisplayProperty. 
		/// </summary>
		public string PreviewDisplayName { get; set; }

    	/// <summary>
		/// Alternative representations of this type that are allowed to be used as iterators. Note
		/// that Provider.GetAllObjectsOfType() must cater for these alternative representations. Eg:
		/// Type[], List&lt;Type&gt; etc.
		/// </summary>
		public Type[] AlternativeForms
		{
			get { return _AlternativeForms; }
			set { _AlternativeForms = value; }
		}

        public ArchAngelEditorAttribute()
        {
        }

        public ArchAngelEditorAttribute(bool virtualPropertiesAllowed, bool isGeneratorIterator)
        {
            VirtualPropertiesAllowed = virtualPropertiesAllowed;
            IsGeneratorIterator = isGeneratorIterator;
        }

        public ArchAngelEditorAttribute(bool virtualPropertiesAllowed, bool isGeneratorIterator, Type[] alternativeForms)
        {
            VirtualPropertiesAllowed = virtualPropertiesAllowed;
            IsGeneratorIterator = isGeneratorIterator;
            AlternativeForms = alternativeForms;
        }

        public ArchAngelEditorAttribute(bool virtualPropertiesAllowed, bool isGeneratorIterator, string previewDisplayProperty)
        {
            VirtualPropertiesAllowed = virtualPropertiesAllowed;
            IsGeneratorIterator = isGeneratorIterator;
            PreviewDisplayProperty = previewDisplayProperty;
        }

        public ArchAngelEditorAttribute(bool virtualPropertiesAllowed, bool isGeneratorIterator, string previewDisplayProperty, Type[] alternativeForms)
        {
            VirtualPropertiesAllowed = virtualPropertiesAllowed;
            IsGeneratorIterator = isGeneratorIterator;
            AlternativeForms = alternativeForms;
            PreviewDisplayProperty = previewDisplayProperty;
        }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.All,
                   AllowMultiple = true,
                   Inherited = true)]
    public sealed class ApiExtensionAttribute : Attribute
    {
        private string _DefaultCode = "";
        private string _Description = "";

        public string DefaultCode
        {
            get { return _DefaultCode; }
            set { _DefaultCode = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public ApiExtensionAttribute(string description, string defaultCode)
        {
            _Description = description;
            _DefaultCode = defaultCode;
        }

		public ApiExtensionAttribute()
		{
		}

    }

	[Serializable]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Event,
				   AllowMultiple = false,
				   Inherited = true)]
	public sealed class IntelliSenseVisibilityAttribute : Attribute
	{
		public bool Visible { get; set; }
	}

	/// <summary>
	/// Marker Attribute for indicating that an enum should be available for use in 
	/// ArchAngel Templates as a type for Virtual Properties
	/// </summary>
	[Serializable]
	[AttributeUsage(AttributeTargets.Enum,
				   AllowMultiple = false,
				   Inherited = true)]
	public sealed class TemplateEnumAttribute : Attribute
	{
	}

	/// <summary>
	/// Marker Attribute for indicating that this enum value should be considered
	/// a null value -> this means it will show up in the UI as a blank, and should
	/// be the default value. If this value is chosen by the user, the virtual property
	/// will not be saved in the project, so when the model is reloaded it will be null.
	/// Since enums are value types, the value of the virtual property will be whatever 
	/// default(YourEnumType) returns when you access it, so you should make this the first
	/// value or = 0 if you can.
	/// </summary>
	[Serializable]
	[AttributeUsage(AttributeTargets.Field,
				   AllowMultiple = false,
				   Inherited = false)]
	public sealed class NullValueAttribute : Attribute
	{
	}

}
