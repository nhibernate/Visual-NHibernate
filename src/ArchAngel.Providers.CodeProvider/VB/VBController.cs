using System;
using System.Collections.Generic;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using Attribute = ArchAngel.Providers.CodeProvider.DotNet.Attribute;
using DataType = ArchAngel.Providers.CodeProvider.DotNet.DataType;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;

namespace ArchAngel.Providers.CodeProvider.VB
{
	public class VBController : Controller
	{
		public VBController()
		{
			Root = new CodeRoot(this);
			dict = new Dictionary<Type, Func<IPrintable, IPrinter>>()
            {
                           {typeof (Attribute), bc => new VBAttributePrinter(bc as Attribute)},
                           {typeof (AttributeSection), bc => new VBAttributeSectionPrinter(bc as AttributeSection)},
                           {typeof (BaseConstruct), bc => new VBBaseConstructPrinter(bc as BaseConstruct)},
                           {typeof (Class), bc => new VBClassPrinter(bc as Class)},
                           {typeof (CodeRoot), bc => new VBCodeRootPrinter(bc as CodeRoot)},
                           {typeof (Constant), bc => new VBConstantPrinter(bc as Constant)},
                           {typeof (Constructor), bc => new VBConstructorPrinter(bc as Constructor)},
                           {typeof (DataType), bc => new VBDataTypePrinter(bc as DataType)},
                           {typeof (Delegate), bc => new VBDelegatePrinter(bc as Delegate)},
                           {typeof (Destructor), bc => new VBDestructorPrinter(bc as Destructor)},
                           {typeof (EmptyPlaceholder), bc => new VBEmptyPlaceholderPrinter(bc as EmptyPlaceholder)},
                           {typeof (Enumeration), bc => new VBEnumerationPrinter(bc as Enumeration)},
                           {typeof (Event), bc => new VBEventPrinter(bc as Event)},
                           {typeof (Field), bc => new VBFieldPrinter(bc as Field)},
                           {typeof (Function), bc => new VBFunctionPrinter(bc as Function)},
                           {typeof (Indexer), bc => new VBIndexerPrinter(bc as Indexer)},
                           {typeof (Interface), bc => new VBInterfacePrinter(bc as Interface)},
                           {typeof (InterfaceAccessor), bc => new VBInterfaceAccessorPrinter(bc as InterfaceAccessor)},
                           {typeof (InterfaceEvent), bc => new VBInterfaceEventPrinter(bc as InterfaceEvent)},
                           {typeof (InterfaceIndexer), bc => new VBInterfaceIndexerPrinter(bc as InterfaceIndexer)},
                           {typeof (InterfaceMethod), bc => new VBInterfaceMethodPrinter(bc as InterfaceMethod)},
                           {typeof (InterfaceProperty), bc => new VBInterfacePropertyPrinter(bc as InterfaceProperty)},
                           {typeof (Namespace), bc => new VBNamespacePrinter(bc as Namespace)},
                           {typeof (Operator), bc => new VBOperatorPrinter(bc as Operator)},
                           {typeof (Parameter), bc => new VBParameterPrinter(bc as Parameter)},
                           {typeof (Property), bc => new VBPropertyPrinter(bc as Property)},
                           {typeof (PropertyAccessor), bc => new VBPropertyAccessorPrinter(bc as PropertyAccessor)},
                           {typeof (Region), bc => new VBRegionPrinter(bc as Region)},
                           {typeof (RegionStart), bc => new VBRegionStartPrinter(bc as RegionStart)},
                           {typeof (RegionEnd), bc => new VBRegionEndPrinter(bc as RegionEnd)},
                           {typeof (Struct), bc => new VBStructPrinter(bc as Struct)},
                           {typeof (UsingStatement), bc => new VBUsingStatementPrinter(bc as UsingStatement)},
                       };
		}

		public override void Reset()
		{
			Root = new CodeRoot(this);
		}
	}
}
