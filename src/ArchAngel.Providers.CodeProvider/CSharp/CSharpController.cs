using System;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;
using InterfaceAccessor = ArchAngel.Providers.CodeProvider.DotNet.InterfaceAccessor;
using UsingStatement = ArchAngel.Providers.CodeProvider.DotNet.UsingStatement;

namespace ArchAngel.Providers.CodeProvider.CSharp
{
	[Serializable]
	public class CSharpController : Controller
	{
		public CSharpController()
		{
			Root = new CodeRoot(this);
			dict = new Dictionary<Type, Func<IPrintable, IPrinter>>()
					   {
						   {typeof (Attribute), bc => new AttributePrinter(bc as Attribute)},
						   {typeof (AttributeSection), bc => new AttributeSectionPrinter(bc as AttributeSection)},
						   {typeof (BaseConstruct), bc => new BaseConstructPrinter(bc as BaseConstruct)},
						   {typeof (Class), bc => new ClassPrinter(bc as Class)},
						   {typeof (CodeRoot), bc => new CodeRootPrinter(bc as CodeRoot)},
						   {typeof (Constant), bc => new ConstantPrinter(bc as Constant)},
						   {typeof (Constructor), bc => new ConstructorPrinter(bc as Constructor)},
						   {typeof (DataType), bc => new DataTypePrinter(bc as DataType)},
						   {typeof (Delegate), bc => new DelegatePrinter(bc as Delegate)},
						   {typeof (Destructor), bc => new DestructorPrinter(bc as Destructor)},
						   {typeof (EmptyPlaceholder), bc => new EmptyPlaceholderPrinter(bc as EmptyPlaceholder)},
						   {typeof (Enumeration), bc => new EnumerationPrinter(bc as Enumeration)},
						   {typeof (Event), bc => new EventPrinter(bc as Event)},
						   {typeof (Field), bc => new FieldPrinter(bc as Field)},
						   {typeof (Function), bc => new FunctionPrinter(bc as Function)},
						   {typeof (Indexer), bc => new IndexerPrinter(bc as Indexer)},
						   {typeof (Interface), bc => new InterfacePrinter(bc as Interface)},
						   {typeof (InterfaceAccessor), bc => new InterfaceAccessorPrinter(bc as InterfaceAccessor)},
						   {typeof (InterfaceEvent), bc => new InterfaceEventPrinter(bc as InterfaceEvent)},
						   {typeof (InterfaceIndexer), bc => new InterfaceIndexerPrinter(bc as InterfaceIndexer)},
						   {typeof (InterfaceMethod), bc => new InterfaceMethodPrinter(bc as InterfaceMethod)},
						   {typeof (InterfaceProperty), bc => new InterfacePropertyPrinter(bc as InterfaceProperty)},
						   {typeof (Namespace), bc => new NamespacePrinter(bc as Namespace)},
						   {typeof (Operator), bc => new OperatorPrinter(bc as Operator)},
						   {typeof (Parameter), bc => new ParameterPrinter(bc as Parameter)},
						   {typeof (Property), bc => new PropertyPrinter(bc as Property)},
						   {typeof (PropertyAccessor), bc => new PropertyAccessorPrinter(bc as PropertyAccessor)},
						   {typeof (Region), bc => new RegionPrinter(bc as Region)},
						   {typeof (RegionStart), bc => new RegionStartPrinter(bc as RegionStart)},
						   {typeof (RegionEnd), bc => new RegionEndPrinter(bc as RegionEnd)},
						   {typeof (Struct), bc => new StructPrinter(bc as Struct)},
						   {typeof (UsingStatement), bc => new UsingStatementPrinter(bc as UsingStatement)},
						   {typeof (Enumeration.EnumMember), bc => new EnumerationPrinter.EnumMemberPrinter(bc as Enumeration.EnumMember)}
					   };
		}

		public override void Reset()
		{
			Root = new CodeRoot(this);
		}
	}
}
