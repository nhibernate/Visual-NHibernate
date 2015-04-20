using System;
using ArchAngel.Providers;
using ArchAngel.Providers.CodeProvider.CSharp;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Slyce.IntelliMerge.Controller;
using Slyce.IntelliMerge.UnitTesting;
using Attribute=ArchAngel.Providers.CodeProvider.DotNet.Attribute;
using Delegate = ArchAngel.Providers.CodeProvider.DotNet.Delegate;
using Version=Slyce.IntelliMerge.Controller.Version;
using ArchAngel.Providers.CodeProvider.DotNet;
using DataType=ArchAngel.Providers.CodeProvider.DotNet.DataType;

namespace Specs_For_CodeRootMap.GetMergedBaseConstruct
{
    [TestFixture]
    public class Template_Changes : Merge.GetMergedCodeRootBase
    {
        private const string DataType1 = "MyDataType1";
        private const string DataType2 = "MyDataType2";
        private const string Modifier1 = "MyModifier1";
        private const string Modifier2 = "MyModifier2";

        [SetUp]
        public void Setup()
        {
            controller = new CSharpController();
			controller.Reorder = true;
        }

        [Test]
        public void Attribute_Added_Positional_Attribute()
        {
            const string positionalAttrName = "MyPositionalAttrName1";
            const string attrName = "MyAttributeName1";
            string expectedResult = String.Format("{0}({1})", attrName, positionalAttrName);
            Attribute merged1 = new Attribute(controller);
            Attribute merged2 = new Attribute(controller);
            Attribute merged3 = new Attribute(controller);

            Attribute changing = new Attribute(controller);
            changing.Name = attrName;
            changing.PositionalArguments.Add(positionalAttrName);

            Attribute unchanging = new Attribute(controller);
            unchanging.Name = attrName;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Attribute_Added_Positional_Attributes()
        {
            const string positionalAttrName1 = "MyPositionalAttrName1";
            const string positionalAttrName2 = "MyPositionalAttrName2";
            const string attrName = "MyAttributeName1";
            string expectedResult = String.Format("{0}({1}, {2})", attrName, positionalAttrName1, positionalAttrName2);
            Attribute merged1 = new Attribute(controller);
            Attribute merged2 = new Attribute(controller);
            Attribute merged3 = new Attribute(controller);

            Attribute changing = new Attribute(controller);
            changing.Name = attrName;
            changing.PositionalArguments.Add(positionalAttrName1);
            changing.PositionalArguments.Add(positionalAttrName2);

            Attribute unchanging = new Attribute(controller);
            unchanging.Name = attrName;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Attribute_Added_Named_Attribute()
        {
            const string attributeName = "MyNamedAttribute1";
            const string attributeValue = "MyNamedAttributeValue1";
            const string attrName = "MyAttributeName1";
            string expectedResult = String.Format("{0}({1} = {2})",attrName,attributeName,attributeValue);
            Attribute merged1 = new Attribute(controller);
            Attribute merged2 = new Attribute(controller);
            Attribute merged3 = new Attribute(controller);

            Attribute changing = new Attribute(controller);
            changing.Name = attrName;
            changing.NamedArguments.Add(new Attribute.NamedArgument(attributeName, attributeValue));
           
            Attribute unchanging = new Attribute(controller);
            unchanging.Name = attrName;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Class_Removed_Base_Name()
        {
            const string className = "MyClassName1";
            const string baseName = "MyBaseName1";
            string expectedResult = String.Format("class {0}\n{{\n\n}}".Replace("\n", Environment.NewLine), className);
            Class merged1 = new Class(controller, className);
            Class merged2 = new Class(controller, className);
            Class merged3 = new Class(controller, className);

            Class user = new Class(controller, className);
            Class prevgen = new Class(controller, className);
            Class newgen = new Class(controller, className);
            user.BaseNames.Add(baseName);
            prevgen.BaseNames.Add(baseName);

            Merge_And_Assert_Removing(merged1, merged2, merged3, user, prevgen, newgen, expectedResult);
        }

        [Test]
        public void Class_Removed_Generic_Type()
        {
            const string className = "MyClassName1";
            const string genericType = "MyGenericType1";
            string expectedResult = String.Format("class {0}\n{{\n\n}}".Replace("\n", Environment.NewLine), className);
            Class merged1 = new Class(controller, className);
            Class merged2 = new Class(controller, className);
            Class merged3 = new Class(controller, className);

            Class user = new Class(controller, className);
            Class prevgen = new Class(controller, className);
            Class newgen = new Class(controller, className);
            user.GenericTypes.Add(genericType);
            prevgen.GenericTypes.Add(genericType);

            Merge_And_Assert_Removing(merged1, merged2, merged3, user, prevgen, newgen, expectedResult);
        }

        [Test]
        public void Class_Added_Modifier()
        {
            const string className = "MyClassName1";
            string expectedResult = String.Format("{0} class {1}\n{{\n\n}}".Replace("\n",Environment.NewLine), Modifier2, className);
            Class merged1 = new Class(controller, className);
            Class merged2 = new Class(controller, className);
            Class merged3 = new Class(controller, className);

            Class changing = new Class(controller, className);
            changing.Modifiers.Add(Modifier2);
            Class unchanging = new Class(controller, className);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Class_Added_IsPartial()
        {
            const string className = "MyClassName1";
            string expectedResult = String.Format("partial class {0}",className);
            Class merged1 = new Class(controller, className);
            Class merged2 = new Class(controller, className);
            Class merged3 = new Class(controller, className);

            Class changing = new Class(controller, className);
            changing.IsPartial = true;
            Class unchanging = new Class(controller, className);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constant_Changed_Type_And_Expression()
        {
            const string constName = "MyConstName1";
            const string expresssion1 = "MyExpression1";
            const string expresssion2 = "MyExpression2";
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);
            string expectedResult = String.Format("const {0} {1} = {2}", DataType2, constName, expresssion2);

            Constant merged1 = new Constant(controller);
            Constant merged2 = new Constant(controller);
            Constant merged3 = new Constant(controller);

            Constant changing = new Constant(controller, constName, type2, expresssion2);
            Constant unchanging = new Constant(controller, constName, type1, expresssion1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constant_Added_Modifier()
        {
            const string constName = "MyConstName1";
            const string expresssion = "MyExpression1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} const {1} {2} = {3}", Modifier1, DataType1, constName, expresssion);

            Constant merged1 = new Constant(controller);
            Constant merged2 = new Constant(controller);
            Constant merged3 = new Constant(controller);

            Constant changing = new Constant(controller, constName, type1, expresssion);
            changing.Modifiers.Add(Modifier1);
            Constant unchanging = new Constant(controller, constName, type1, expresssion);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constructor_Added_Modifier()
        {
            const string className = "MyClassName1";
            const string parameterName1 = "MyParameterName1";
            string expectedResult = String.Format("{0} {1}({2} {3})", Modifier1, className, DataType1, parameterName1);

            Constructor merged1 = new Constructor(controller);
            Constructor merged2 = new Constructor(controller);
            Constructor merged3 = new Constructor(controller);

            Constructor changing = new Constructor(controller, className);
            changing.Parameters.Add(new Parameter(controller, DataType1, parameterName1));
            changing.Modifiers.Add(Modifier1);
            Constructor unchanging = new Constructor(controller, className);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, parameterName1));

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constructor_Added_Modifiers()
        {
            const string className = "MyClassName1";
            const string parameterName1 = "MyParameterName1";
            string expectedResult = String.Format("{0} {1} {2}({3} {4})", Modifier1, Modifier2, className, DataType1, parameterName1);

            Constructor merged1 = new Constructor(controller);
            Constructor merged2 = new Constructor(controller);
            Constructor merged3 = new Constructor(controller);

            Constructor changing = new Constructor(controller, className);
            changing.Parameters.Add(new Parameter(controller, DataType1, parameterName1));
            changing.Modifiers.Add(Modifier1);
            changing.Modifiers.Add(Modifier2);
            Constructor unchanging = new Constructor(controller, className);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, parameterName1));

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constructor_Renamed_Parameter()
        {
            const string className = "MyClassName1";
            const string parameterName1 = "MyParameterName1";
            const string parameterName2 = "MyParameterName2";
            string expectedResult = String.Format("{0} {1}({2} {3})", Modifier1, className, DataType1, parameterName2);

            Constructor merged1 = new Constructor(controller);
            Constructor merged2 = new Constructor(controller);
            Constructor merged3 = new Constructor(controller);

            Constructor changing = new Constructor(controller, className);
            changing.Parameters.Add(new Parameter(controller, DataType1, parameterName2));
            changing.Modifiers.Add(Modifier1);
            Constructor unchanging = new Constructor(controller, className);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, parameterName1));
            unchanging.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constructor_Changed_Parameter_DataType()
        {
            const string className = "MyClassName1";
            const string parameterName1 = "MyParameterName1";
            string expectedResult = String.Format("{0} {1}({2} {3})", Modifier1, className, DataType2, parameterName1);

            Constructor merged1 = new Constructor(controller);
            Constructor merged2 = new Constructor(controller);
            Constructor merged3 = new Constructor(controller);

            Constructor changing = new Constructor(controller, className);
            changing.Parameters.Add(new Parameter(controller, DataType2, parameterName1));
            changing.Modifiers.Add(Modifier1);
            Constructor unchanging = new Constructor(controller, className);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, parameterName1));
            unchanging.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Constructor_Added_Initialiser()
        {
            const string className = "MyClassName1";
            const string initName = "MyInitializerName1";
            const string initArgs1 = "MyInitializerArgs1";
            const string initArgs2 = "MyInitializerArgs2";
            string expectedResult = String.Format("{0}() : {1}({2}, {3})\n{{ }}".Replace("\n",Environment.NewLine), className, initName, initArgs1, initArgs2);

            Constructor merged1 = new Constructor(controller);
            Constructor merged2 = new Constructor(controller);
            Constructor merged3 = new Constructor(controller);

            Constructor changing = new Constructor(controller, className);
            changing.InitializerType = initName;
            changing.InitializerArguments.Add(initArgs1);
            changing.InitializerArguments.Add(initArgs2);
            Constructor unchanging = new Constructor(controller, className);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

		[Test]
		public void Destructor_Added_Extern()
		{
			const string className = "MyClassName1";
			string expectedResult = String.Format("extern ~{0}();".Replace("\n", Environment.NewLine), className);

			Destructor merged1 = new Destructor(controller);
			Destructor merged2 = new Destructor(controller);
			Destructor merged3 = new Destructor(controller);

			Destructor changing = new Destructor(controller, className);
			changing.IsExtern = true;
			Destructor unchanging = new Destructor(controller, className);

			Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
		}

        [Test]
        public void Delegate_Changed_ReturnType()
        {
            const string delegateName = "MyEnumerationName1";
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);
            string expectedResult = String.Format("{0} delegate {1} {2}", Modifier1, type2, delegateName);

            Delegate merged1 = new Delegate(controller);
            Delegate merged2 = new Delegate(controller);
            Delegate merged3 = new Delegate(controller);

            Delegate changing = new Delegate(controller, delegateName, type2, Modifier1);
            Delegate unchanging = new Delegate(controller, delegateName, type1, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Delegate_Added_Modifier()
        {
            const string delegateName = "MyEnumerationName1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} {1} delegate {2} {3}", Modifier1, Modifier2, type1, delegateName);

            Delegate merged1 = new Delegate(controller);
            Delegate merged2 = new Delegate(controller);
            Delegate merged3 = new Delegate(controller);

            Delegate changing = new Delegate(controller, delegateName, type1, Modifier1);
            changing.Modifiers.Add(Modifier2);
            Delegate unchanging = new Delegate(controller, delegateName, type1, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Delegate_Added_GenericType()
        {
            const string delegateName = "MyEnumerationName1";
            const string genericTypeName = "MyGenericTypeName1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} delegate {1} {2}<{3}>", Modifier1, type1, delegateName,genericTypeName);

            Delegate merged1 = new Delegate(controller);
            Delegate merged2 = new Delegate(controller);
            Delegate merged3 = new Delegate(controller);

            Delegate changing = new Delegate(controller, delegateName, type1, Modifier1);
            changing.GenericType = genericTypeName;
            Delegate unchanging = new Delegate(controller, delegateName, type1, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Delegate_Changed_GenericType()
        {
            const string delegateName = "MyEnumerationName1";
            const string genericTypeName1 = "MyGenericTypeName1";
            const string genericTypeName2 = "MyGenericTypeName2";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} delegate {1} {2}<{3}>", Modifier1, type1, delegateName, genericTypeName2);

            Delegate merged1 = new Delegate(controller);
            Delegate merged2 = new Delegate(controller);
            Delegate merged3 = new Delegate(controller);

            Delegate changing = new Delegate(controller, delegateName, type1, Modifier1);
            changing.GenericType = genericTypeName2;
            Delegate unchanging = new Delegate(controller, delegateName, type1, Modifier1);
            unchanging.GenericType = genericTypeName1;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Enumeration_Added_EnumBase()
        {
            const string enumName = "MyEnumerationName1";
            const string enumBase = "MyEnumBase1";
            string expectedResult = String.Format("{0} enum {1} : {2}", Modifier1, enumName, enumBase);

            Enumeration merged1 = new Enumeration(controller, enumName);
            Enumeration merged2 = new Enumeration(controller, enumName);
            Enumeration merged3 = new Enumeration(controller, enumName);

            Enumeration changing = new Enumeration(controller, enumName);
            changing.Modifiers.Add(Modifier1);
            changing.EnumBase = enumBase;
            Enumeration unchanging = new Enumeration(controller, enumName);
            unchanging.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Enumeration_Changed_EnumBase()
        {
            const string enumName = "MyEnumerationName1";
            const string enumBase1 = "MyEnumBase1";
            const string enumBase2 = "MyEnumBase2";
            string expectedResult = String.Format("{0} enum {1} : {2}", Modifier1, enumName, enumBase2);

            Enumeration merged1 = new Enumeration(controller, enumName);
            Enumeration merged2 = new Enumeration(controller, enumName);
            Enumeration merged3 = new Enumeration(controller, enumName);

            Enumeration changing = new Enumeration(controller, enumName);
            changing.Modifiers.Add(Modifier1);
            changing.EnumBase = enumBase2;
            Enumeration unchanging = new Enumeration(controller, enumName);
            unchanging.Modifiers.Add(Modifier1);
            unchanging.EnumBase = enumBase1;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }


        [Test]
        public void Enumeration_Added_Modifier()
        {
            const string enumName = "MyEnumerationName1";
            string expectedResult = String.Format("{0} enum {1}",Modifier1, enumName );

            Enumeration merged1 = new Enumeration(controller, enumName);
            Enumeration merged2 = new Enumeration(controller, enumName);
            Enumeration merged3 = new Enumeration(controller, enumName);

            Enumeration changing = new Enumeration(controller, enumName);
            changing.Modifiers.Add(Modifier1);
            Enumeration unchanging = new Enumeration(controller, enumName);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Enumeration_Changed_Modifier()
        {
            const string enumName = "MyEnumerationName1";
            string expectedResult = String.Format("{0} enum {1}", Modifier2, enumName);

            Enumeration merged1 = new Enumeration(controller, enumName);
            Enumeration merged2 = new Enumeration(controller, enumName);
            Enumeration merged3 = new Enumeration(controller, enumName);

            Enumeration changing = new Enumeration(controller, enumName);
            changing.Modifiers.Add(Modifier2);
            Enumeration unchanging = new Enumeration(controller, enumName);
            unchanging.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Enumeration_Added_Modifiers()
        {
            const string enumName = "MyEnumerationName1";
            string expectedResult = String.Format("{0} {1} enum {2}", Modifier1, Modifier2, enumName);

            Enumeration merged1 = new Enumeration(controller, enumName);
            Enumeration merged2 = new Enumeration(controller, enumName);
            Enumeration merged3 = new Enumeration(controller, enumName);

            Enumeration changing = new Enumeration(controller, enumName);
            changing.Modifiers.Add(Modifier1);
            changing.Modifiers.Add(Modifier2);
            Enumeration unchanging = new Enumeration(controller, enumName);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void EnumMember_ChangedValue()
        {
            const string memberName = "MyMemberName1";
            const string memberValue1 = "MyValue1";
            const string memberValue2 = "MyValue2";
            string expectedResult = String.Format("{0} = {1}", memberName, memberValue2);

            Enumeration.EnumMember merged1 = new Enumeration.EnumMember(controller, memberName);
            Enumeration.EnumMember merged2 = new Enumeration.EnumMember(controller, memberName);
            Enumeration.EnumMember merged3 = new Enumeration.EnumMember(controller, memberName);

            Enumeration.EnumMember changing = new Enumeration.EnumMember(controller, memberName, memberValue2);
            Enumeration.EnumMember unchanging = new Enumeration.EnumMember(controller, memberName, memberValue1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Event_Changed_DataType()
        {
            const string name = "MyEvent1";
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);
            string expectedResult = String.Format("{0} event {1} {2}", Modifier1, DataType2, name);

            Event merged1 = new Event(controller);
            Event merged2 = new Event(controller);
            Event merged3 = new Event(controller);

            Event changing = new Event(controller, type2, name, Modifier1);
            Event unchanging = new Event(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Event_Changed_Modifier()
        {
            const string name = "MyEvent1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} event {1} {2}", Modifier2, DataType1, name);

            Event merged1 = new Event(controller);
            Event merged2 = new Event(controller);
            Event merged3 = new Event(controller);

            Event changing = new Event(controller, type1, name, Modifier2);
            Event unchanging = new Event(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Event_Added_Modifier()
        {
            const string name = "MyEvent1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} {1} event {2} {3}", Modifier1, Modifier2, DataType1, name);

            Event merged1 = new Event(controller);
            Event merged2 = new Event(controller);
            Event merged3 = new Event(controller);

            Event changing = new Event(controller, type1, name, Modifier1);
            changing.Modifiers.Add(Modifier2);
            Event unchanging = new Event(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Event_Added_Accessors()
        {
            const string name = "MyEvent1";
            const string addAccessorText = "{ // add accessor }";
            const string removeAccessorText = "{ // remove accessor }";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} event {1} {2}\n{{\n\tadd{3}\tremove{4}}}".Replace("\n",Environment.NewLine),Modifier1, DataType1, name, addAccessorText, removeAccessorText);

            Event merged1 = new Event(controller);
            Event merged2 = new Event(controller);
            Event merged3 = new Event(controller);

            Event changing = new Event(controller, type1, name, Modifier1);
            changing.AddAccessorText = addAccessorText;
            changing.RemoveAccessorText = removeAccessorText;
            Event unchanging = new Event(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Field_Changed_Modifier()
        {
            const string name = "MyField1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} {1} {2}", Modifier2, type1, name);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type1, name, Modifier2);
            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Field_Added_Modifier()
        {
            const string name = "MyField1";
            DataType type1 = new DataType(controller, DataType1);
            string expectedResult = String.Format("{0} {1} {2} {3}", Modifier1, Modifier2, type1, name);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type1, name, Modifier1);
            changing.Modifiers.Add(Modifier2);
            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Field_Added_Preceeding_Comment()
        {
            const string name = "MyField1";
            const string comment = "My comment goes here...";
            DataType type1 = new DataType(controller, DataType1);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type1, name, Modifier1);
            changing.Comments.PreceedingComments.Add(comment);
            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, comment);
        }

        [Test]
        public void Field_Added_Preceeding_Comments()
        {
            const string name = "MyField1";
            const string comment1 = "My first comment goes here...";
            const string comment2 = "My second comment goes here...";
            const string comment3 = "My third comment goes here...";
            string expectedResult = String.Format("{0}\n{1}\n{2}".Replace("\n",Environment.NewLine),comment1,comment2,comment3);
            DataType type1 = new DataType(controller, DataType1);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type1, name, Modifier1);
            changing.Comments.PreceedingComments.Add(comment1);
            changing.Comments.PreceedingComments.Add(comment2);
            changing.Comments.PreceedingComments.Add(comment3);
            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Field_Added_Trailing_Comment()
        {
            const string name = "MyField1";
            const string comment = "My comment goes here...";
            DataType type1 = new DataType(controller, DataType1);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type1, name, Modifier1);
            changing.Comments.TrailingComment = comment;
            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, comment);
        }

        [Test]
        public void Field_Changed_Return_Type()
        {
            const string name = "MyField1";
            string expectedResult = String.Format("{0} {1} {2}", Modifier1, DataType2, name);
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Field merged1 = new Field(controller);
            Field merged2 = new Field(controller);
            Field merged3 = new Field(controller);

            Field changing = new Field(controller, type2, name, Modifier1);

            Field unchanging = new Field(controller, type1, name, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Function_Added_Modifier()
        {
            const string name = "MyFunction1";
            const string paramName = "MyParameter1";
            string expectedResult = String.Format("{0} {1} {2}", Modifier1, DataType2, name);
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            Function changing = new Function(controller, name, type2);
            changing.Parameters.Add(new Parameter(controller, DataType1, paramName));
            changing.Modifiers.Add(Modifier1);
            Function unchanging = new Function(controller, name, type1);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, paramName));

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Function_Changed_Modifier()
        {
            const string name = "MyFunction1";
            const string paramName = "MyParameter1";
            string expectedResult = String.Format("{0} {1} {2}", Modifier2, DataType2, name);
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            Function changing = new Function(controller, name, type2);
            changing.Parameters.Add(new Parameter(controller, DataType1, paramName));
            changing.Modifiers.Add(Modifier2);
            Function unchanging = new Function(controller, name, type1);
            unchanging.Parameters.Add(new Parameter(controller, DataType1, paramName));
            unchanging.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Function_Added_Comments()
        {
            const string name = "MyFunction1";
            const string comment = "My Comment goes here...";
            DataType type1 = new DataType(controller, DataType1);

            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            Function changing = new Function(controller, name, type1);
            Function unchanging = new Function(controller, name, type1);
            changing.Comments.PreceedingComments.Add(comment);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, comment);
          
        }

        [Test]
        public void Function_Changed_Return_Type()
        {
            const string name = "MyFunction1";
            string expectedResult = String.Format("{0} {1}", DataType2, name);
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            Function changing = new Function(controller, name, type2);
            Function unchanging = new Function(controller, name, type1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Indexer_Changed_ReturnType()
        {
            string expectedResult = String.Format("{0} this []", DataType2);
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Indexer merged1 = new Indexer(controller);
            Indexer merged2 = new Indexer(controller);
            Indexer merged3 = new Indexer(controller);

            Indexer changing = new Indexer(controller, type2);
            Indexer unchanging = new Indexer(controller, type1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

		[Test]
		public void Interface_Added_Modifier()
		{
            const string name = "MyInterface1";
            string expectedResult = String.Format("{0} interface {1}", Modifier1, name );
            Interface merged1 = new Interface(controller, name);
            Interface merged2 = new Interface(controller, name);
            Interface merged3 = new Interface(controller, name);

            Interface changing = new Interface(controller, name);
            Interface unchanging = new Interface(controller, name);

            changing.Modifiers.Add(Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
		}

        [Test]
        public void Interface_Changed_Modifier()
        {
            const string name = "MyInterface1";
            string expectedResult = String.Format("{0} interface {1}", Modifier2, name);
            Interface merged1 = new Interface(controller, name);
            Interface merged2 = new Interface(controller, name);
            Interface merged3 = new Interface(controller, name);

            Interface changing = new Interface(controller, name, Modifier1);
            Interface unchanging = new Interface(controller, name, Modifier1);

            changing.Modifiers[0] = Modifier2;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Interface_Added_InterfaceBase()
        {
            const string name = "MyInterface1";
            const string baseName = "InterfaceBase1";
            string expectedResult = String.Format("interface {0} : {1}", name, baseName);
            Interface merged1 = new Interface(controller, name);
            Interface merged2 = new Interface(controller, name);
            Interface merged3 = new Interface(controller, name);

            Interface changing = new Interface(controller, name); 
            Interface unchanging = new Interface(controller, name);

            changing.InterfaceBase = baseName;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
           
        }

        [Test]
        public void Interface_Changed_InterfaceBase()
        {
            const string name = "MyInterface1";
            const string baseName1 = "InterfaceBase1";
            const string baseName2 = "InterfaceBase2";
            string expectedResult = String.Format("interface {0} : {1}", name, baseName2);
            Interface merged1 = new Interface(controller, name);
            Interface merged2 = new Interface(controller, name);
            Interface merged3 = new Interface(controller, name);

            Interface changing = new Interface(controller, name);
            Interface unchanging = new Interface(controller, name);

            unchanging.InterfaceBase = baseName1; 
            changing.InterfaceBase = baseName2;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void InterfaceAccessor_Add_Modifier()
        {

            InterfaceAccessor merged1 = new InterfaceAccessor(controller);
            InterfaceAccessor merged2 = new InterfaceAccessor(controller);
            InterfaceAccessor merged3 = new InterfaceAccessor(controller);

            const string name = "MyName1";

            InterfaceProperty property1 = new InterfaceProperty(controller, name, new DataType(controller, DataType2));
            InterfaceProperty property2 = new InterfaceProperty(controller, name, new DataType(controller, DataType2));

            string expectedResult = String.Format("{0} {1} get;", Modifier1, Modifier2);

			InterfaceAccessor changing = new InterfaceAccessor(controller, InterfaceAccessor.AccessorTypes.Get, Modifier1);
			InterfaceAccessor unchanging = new InterfaceAccessor(controller, InterfaceAccessor.AccessorTypes.Get, Modifier1);

            property1.AddChild(changing);
            property2.AddChild(unchanging);

            changing.Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void InterfaceAccessor_Change_Modifier()
        {

            InterfaceAccessor merged1 = new InterfaceAccessor(controller);
            InterfaceAccessor merged2 = new InterfaceAccessor(controller);
            InterfaceAccessor merged3 = new InterfaceAccessor(controller);

            const string name = "MyName1";

            InterfaceProperty property1 = new InterfaceProperty(controller, name, new DataType(controller, DataType2));
            InterfaceProperty property2 = new InterfaceProperty(controller, name, new DataType(controller, DataType2));

            string expectedResult = String.Format("{0} get;", Modifier2);

			InterfaceAccessor changing = new InterfaceAccessor(controller, InterfaceAccessor.AccessorTypes.Get, Modifier2);
			InterfaceAccessor unchanging = new InterfaceAccessor(controller, InterfaceAccessor.AccessorTypes.Get, Modifier1);

            property1.AddChild(changing);
            property2.AddChild(unchanging);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void InterfaceEvent_Change_DataType()
        {
            InterfaceEvent merged1 = new InterfaceEvent(controller);
            InterfaceEvent merged2 = new InterfaceEvent(controller);
            InterfaceEvent merged3 = new InterfaceEvent(controller);

            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            const string name = "MyName1";
            const string expectedResult = "event " + DataType2 + " " + name;

            InterfaceEvent changing = new InterfaceEvent(controller, name, type2, false);
            InterfaceEvent unchanging = new InterfaceEvent(controller, name, type1, false);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void InterfaceEvent_Add_HasNewKeyword()
        {
            InterfaceEvent merged1 = new InterfaceEvent(controller);
            InterfaceEvent merged2 = new InterfaceEvent(controller);
            InterfaceEvent merged3 = new InterfaceEvent(controller);
            
            DataType type1 = new DataType(controller, DataType1);

            const string name = "MyName1";
            const string expectedResult = "new event " + DataType1 + " " + name;

            InterfaceEvent changing = new InterfaceEvent(controller, name, type1, true);
            InterfaceEvent unchanging = new InterfaceEvent(controller, name, type1, false);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceIndexer_Change_DataType()
        {

            InterfaceIndexer merged1 = new InterfaceIndexer(controller);
            InterfaceIndexer merged2 = new InterfaceIndexer(controller);
            InterfaceIndexer merged3 = new InterfaceIndexer(controller);

            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            const string expectedResult = DataType2 + " this []";
            InterfaceIndexer unchanging = new InterfaceIndexer(controller, type1, false);
            InterfaceIndexer changing = new InterfaceIndexer(controller, type2, false);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceIndexer_Add_HasNewKeyword()
        {
            InterfaceIndexer merged1 = new InterfaceIndexer(controller);
            InterfaceIndexer merged2 = new InterfaceIndexer(controller);
            InterfaceIndexer merged3 = new InterfaceIndexer(controller);

            DataType type1 = new DataType(controller, DataType1);
            const string expectedResult = "new " + DataType1 + " this []";
            InterfaceIndexer unchanging = new InterfaceIndexer(controller, type1, false);
            InterfaceIndexer changing = new InterfaceIndexer(controller, type1, true);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceMethod_Change_DataType()
        {

            const string name = "MyInterfaceMethod1";
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);
            const string expectedResult = DataType2 + " " + name;

            InterfaceMethod merged1 = new InterfaceMethod(controller, name);
            InterfaceMethod merged2 = new InterfaceMethod(controller, name);
            InterfaceMethod merged3 = new InterfaceMethod(controller, name);

            InterfaceMethod unchanging = new InterfaceMethod(controller, name, type1);
            InterfaceMethod changing = new InterfaceMethod(controller, name, type2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceMethod_Add_HasNewKeyword()
        {
            const string name = "MyInterfaceMethod1";
            DataType type1 = new DataType(controller, DataType1);
            const string expectedResult = "new " + DataType1 + " " + name;

            InterfaceMethod merged1 = new InterfaceMethod(controller, name);
            InterfaceMethod merged2 = new InterfaceMethod(controller, name);
            InterfaceMethod merged3 = new InterfaceMethod(controller, name);

            InterfaceMethod unchanging = new InterfaceMethod(controller, name, type1);
            InterfaceMethod changing = new InterfaceMethod(controller, name, type1);
            changing.HasNewKeyword = true;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceProperty_Change_DataType()
        {
            const string name = "MyInterfaceProperty1";
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);
            const string expectedResult = DataType2 + " " + name;

            InterfaceProperty merged1 = new InterfaceProperty(controller, name);
            InterfaceProperty merged2 = new InterfaceProperty(controller, name);
            InterfaceProperty merged3 = new InterfaceProperty(controller, name);

            InterfaceProperty unchanging = new InterfaceProperty(controller, name, type1);
            InterfaceProperty changing = new InterfaceProperty(controller, name, type2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void InterfaceProperty_Add_HasNewKeyword()
        {
            const string name = "InterfaceProperty1";
            DataType type = new DataType(controller, DataType1);
            const string expectedResult = "new " + DataType1 + " " + name;

            InterfaceProperty merged1 = new InterfaceProperty(controller,name);
            InterfaceProperty merged2 = new InterfaceProperty(controller, name);
            InterfaceProperty merged3 = new InterfaceProperty(controller, name);

            InterfaceProperty unchanging = new InterfaceProperty(controller, name, type);
            InterfaceProperty changing = new InterfaceProperty(controller, name, type);
            changing.HasNewKeyword = true;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Operator_Change_DataType()
        {
            Operator merged1 = new Operator(controller);
            Operator merged2 = new Operator(controller);
            Operator merged3 = new Operator(controller);
            const string name = "MyOperator";
            const string modifier = "MyModifier";
            const string expectedResult = DataType2 + " operator " + name;
            DataType type1 = new DataType(controller, DataType1);
            DataType type2 = new DataType(controller, DataType2);

            Operator unchanging = new Operator(controller, name, type1, modifier);
            Operator changing = new Operator(controller, name, type2, modifier);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Operator_Change_Modifier()
        {
            Operator merged1 = new Operator(controller);
            Operator merged2 = new Operator(controller);
            Operator merged3 = new Operator(controller);
            const string name = "MyOperator";
            DataType type = new DataType(controller, DataType1);
            const string expectedResult = Modifier2 + " " + DataType1 + " operator " + name;

            Operator changing = new Operator(controller, name, type, Modifier2);
            Operator unchanging = new Operator(controller, name, type, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Operator_Add_Modifier()
        {
            Operator merged1 = new Operator(controller);
            Operator merged2 = new Operator(controller);
            Operator merged3 = new Operator(controller);
            const string name = "MyOperator";
            DataType type = new DataType(controller, DataType1);
            const string expectedResult = Modifier1 + " " + Modifier2 + " " + DataType1 + " operator " + name;

            Operator changing = new Operator(controller, name, type, Modifier1);
            Operator unchanging = new Operator(controller, name, type, Modifier1);
            changing.Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
            
        }

        [Test]
        public void Parameter_Add_DataType()
        {
            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            const string name = "MyFunction1";
            const string parameterName = "i";
            DataType returnType = new DataType(controller, "void");
            Parameter param = new Parameter(controller, "int", parameterName);
            string expectedResult = string.Format("void {0}({1} {2})", new object[] { name, DataType1, parameterName });

            Function unchanging = new Function(controller, name, returnType, param);
            Function changing = new Function(controller, name, returnType, new Parameter(controller, DataType1, parameterName));

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Parameter_Change_Modifiers()
        {
            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            const string name = "MyFunction1";
            DataType dataType = new DataType(controller, DataType1);
            Parameter param = new Parameter(controller, "int", "i");
            string expectedResult = string.Format("{0} {1}({2} {3} int i)", new object[] { DataType1, name, Modifier1, Modifier2 });

            Function changing = new Function(controller, name, dataType, param);
            Function unchanging = new Function(controller, name, dataType, param);
            changing.Parameters[0].Modifiers.Add(Modifier1);
            changing.Parameters[0].Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
           
        }

        [Test]
        public void Parameter_Change_IsParams()
        {
            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller); 
            Function merged3 = new Function(controller);

            const string Name = "MyFunction1";
            DataType dataType = new DataType(controller, DataType1);
            Parameter param = new Parameter(controller, "int", "i");
            string expectedResult = string.Format("{0} {1}(params int i)", new object[]{DataType1,Name});

            Function unchanging = new Function(controller, Name, dataType, param);
            Function changing = new Function(controller, Name, dataType, param);
            changing.Parameters[0].IsParams = true;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }


        [Test]
        public void Parameter_Change_IsParams_And_Modifiers()
        {
            Function merged1 = new Function(controller);
            Function merged2 = new Function(controller);
            Function merged3 = new Function(controller);

            const string Name = "MyFunction1";
            DataType dataType = new DataType(controller, DataType1);
            Parameter param = new Parameter(controller, "int", "i");
            string expectedResult = string.Format("{0} {1}({2} {3} params int i)", new object[] {DataType1, Name, Modifier1, Modifier2 });

            Function unchanging = new Function(controller, Name, dataType, param);
            Function changing = new Function(controller, Name, dataType, param);
            changing.Parameters[0].IsParams = true;
            changing.Parameters[0].Modifiers.Add(Modifier1);
            changing.Parameters[0].Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Property_Change_DataType()
        {
            Property merged1 = new Property(controller);
            Property merged2 = new Property(controller);
            Property merged3 = new Property(controller);

            const string Name = "MyProperty1";
            DataType OldDataType = new DataType(controller, DataType1);
            DataType NewDataType = new DataType(controller, DataType2);
            const string PropertyModifier = Modifier1;
            string expectedResult = PropertyModifier + " " + NewDataType + " " + Name;

            Property unchanging = new Property(controller, Name, OldDataType, PropertyModifier);
            Property changing = new Property(controller, Name, NewDataType, PropertyModifier);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);

        }

        [Test]
        public void Property_Change_Modifier()
        {
            Property merged1 = new Property(controller);
            Property merged2 = new Property(controller);
            Property merged3 = new Property(controller);

            const string Name = "MyProperty1";
            DataType MyDataType = new DataType(controller, DataType2);
            const string expectedResult = Modifier2 + " " + DataType2 + " " + Name;

            Property unchanging = new Property(controller, Name, MyDataType, Modifier1);
            Property changing = new Property(controller, Name, MyDataType, Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Property_Add_Modifier()
        {
            Property merged1 = new Property(controller);
            Property merged2 = new Property(controller);
            Property merged3 = new Property(controller);

            const string Name = "MyProperty1";
            DataType MyDataType = new DataType(controller, DataType2);
            const string expectedResult = Modifier1 + " " + Modifier2 + " " + DataType2 + " " + Name;

            Property unchanging = new Property(controller, Name, MyDataType, Modifier1);
            Property changing = new Property(controller, Name, MyDataType, Modifier1);
            changing.Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void PropertyAccessor_Change_Modifier()
        {
            PropertyAccessor merged1 = new PropertyAccessor(controller);
            PropertyAccessor merged2 = new PropertyAccessor(controller);
            PropertyAccessor merged3 = new PropertyAccessor(controller);
            const string AccessorName = "MyAccessor";
            const string expectedResult = Modifier1 + " get";

            PropertyAccessor changing = new PropertyAccessor(controller, PropertyAccessor.AccessorTypes.Get, AccessorName);
			PropertyAccessor unchanging = new PropertyAccessor(controller, PropertyAccessor.AccessorTypes.Get, AccessorName);
            changing.Modifier = Modifier1;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Struct_Add_BaseNames()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);
            const string Name = "MyStructName";
            const string OldBaseName = "MyBaseName1";
            const string NewBaseName = "MyBaseName2";
            const string GenericTypeName = "MyGenericType";
            const string expectedResult =
                Modifier1 + " struct " + Name + "<" + GenericTypeName + "> : " + OldBaseName + ", " + NewBaseName;

            Struct changing = new Struct(controller, Name, OldBaseName, GenericTypeName, Modifier1);
            Struct unchanging= new Struct(controller, Name, OldBaseName, GenericTypeName, Modifier1);
            changing.BaseNames.Add(NewBaseName);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Struct_Change_BaseNames()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);
            const string Name = "MyStructName";
            const string OldBaseName = "MyBaseName1";
            const string NewBaseName = "MyBaseName2";
            const string GenericTypeName = "MyGenericType";
            const string expectedResult = Modifier1 + " struct " + Name + "<" + GenericTypeName + "> : " + NewBaseName;

            Struct unchanging = new Struct(controller, Name, OldBaseName, GenericTypeName, Modifier1);
            Struct changing = new Struct(controller, Name, NewBaseName, GenericTypeName, Modifier1);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Struct_Add_GenericTypes()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);
            const string Name = "MyStructName";
            const string BaseName = "MyBaseName1";
            const string GenericTypeName1 = "MyGenericType1";
            const string GenericTypeName2 = "MyGenericType2";
            const string expectedResult =
                Modifier1 + " struct " + Name + "<" + GenericTypeName1 + ", " + GenericTypeName2 + "> : " + BaseName;

            Struct changing = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier1);
            Struct unchanging = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier1);
            changing.GenericTypes.Add(GenericTypeName2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Struct_Change_GenericTypes()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);
            const string Name = "MyStructName";
            const string BaseName = "MyBaseName1";
            const string GenericTypeName1 = "MyGenericType1";
            const string GenericTypeName2 = "MyGenericType2";
            const string expectedResult = Modifier2 + " struct " + Name + "<" + GenericTypeName2 + "> : " + BaseName;

            Struct unchanging = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier2);
            Struct changing = new Struct(controller, Name, BaseName, GenericTypeName2, Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void Struct_Add_Modifier()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);

            const string Name = "MyStructName";
            const string BaseName = "MyBaseName1";
            const string GenericTypeName1 = "MyGenericType1";
            const string expectedResult =
                Modifier1 + " " + Modifier2 + " struct " + Name + "<" + GenericTypeName1 + "> : " + BaseName;

            Struct unchanging = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier1);
            Struct changing = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier1);
            changing.Modifiers.Add(Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }


        [Test]
        public void Struct_Change_Modifier()
        {
            Struct merged1 = new Struct(controller);
            Struct merged2 = new Struct(controller);
            Struct merged3 = new Struct(controller);

            const string Name = "MyStructName";
            const string BaseName = "MyBaseName1";
            const string GenericTypeName1 = "MyGenericType1";
            const string expectedResult = Modifier2 + " struct " + Name + "<" + GenericTypeName1 + "> : " + BaseName;

            Struct unchanging = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier1);
            Struct changing = new Struct(controller, Name, BaseName, GenericTypeName1, Modifier2);

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
            
        }

        [Test]
        public void UsingStatement_Change_Alias()
        {
            UsingStatement merged1 = new UsingStatement(controller);
            UsingStatement merged2 = new UsingStatement(controller);
            UsingStatement merged3 = new UsingStatement(controller);

            const string Alias1 = "MyAlias1";
            const string Alias2 = "MyAlias2";
            const string Value = "MyValue1";

            UsingStatement unchanging = new UsingStatement(controller, Alias1, Value);
            UsingStatement changing = new UsingStatement(controller, Alias2, Value);

            const string expectedResult = "using " + Alias2 + " = " + Value;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        [Test]
        public void UsingStatement_Change_Value()
        {
            UsingStatement merged1 = new UsingStatement(controller);
            UsingStatement merged2 = new UsingStatement(controller);
            UsingStatement merged3 = new UsingStatement(controller);

            const string Alias = "MyAlias1";
            const string Value1 = "MyValue1";
            const string Value2 = "MyValue2";

            UsingStatement unchanging = new UsingStatement(controller, Alias, Value1);
            UsingStatement changing = new UsingStatement(controller, Alias, Value2);

            const string expectedResult = "using " + Alias + " = " + Value2;

            Merge_And_Assert(merged1, merged2, merged3, changing, unchanging, expectedResult);
        }

        public void Merge_And_Assert(BaseConstruct merged1, BaseConstruct merged2, BaseConstruct merged3, BaseConstruct changed, BaseConstruct unchanged, string expectedResult)
        {
            Assert.That(merged1.CustomMergeStep(changed, unchanged, unchanged), Is.EqualTo(true), "CustomMergeStep returned false when the user version had changed");
            string result = merged1.ToString();
            Assertions.StringContains(result, expectedResult, 1, "\nUser change");

            Assert.That(merged2.CustomMergeStep(unchanged, changed, unchanged), Is.EqualTo(true), "CustomMergeStep returned false when the template version had changed");
            result = merged2.ToString();
            Assertions.StringContains(result, expectedResult, 1, "\nTemplate Change");

            Assert.That(merged3.CustomMergeStep(changed, changed, unchanged), Is.EqualTo(true), "CustomMergeStep returned false when the template and users version had both changed");
            result = merged3.ToString();
            Assertions.StringContains(result, expectedResult, 1, "\nTemplate and User changes, with both changes the same");
        }

        public void Merge_And_Assert_Removing(BaseConstruct merged1, BaseConstruct merged2, BaseConstruct merged3, BaseConstruct user, BaseConstruct prevgen, BaseConstruct newgen, string expectedResult)
        {
            Assert.That(merged1.CustomMergeStep(user, newgen, prevgen), Is.EqualTo(true));
            string result = merged1.ToString();
            Assertions.StringContains(result, expectedResult, 1, "\nTesting removal of a construct in template");
        }
    }
}
