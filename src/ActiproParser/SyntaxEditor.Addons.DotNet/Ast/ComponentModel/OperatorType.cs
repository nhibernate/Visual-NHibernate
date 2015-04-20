using System;
using System.Collections;

namespace ActiproSoftware.SyntaxEditor.Addons.DotNet.Ast {

	/// <summary>
	/// Specifies the type of an operator.
	/// </summary>
	public enum OperatorType {

		/// <summary>
		/// No valid operator type.
		/// </summary>
		None,

		/// <summary>
		/// An addition operator.
		/// </summary>
		Addition,

		/// <summary>
		/// A subtraction operator.
		/// </summary>
		Subtraction,
		
		/// <summary>
		/// A multiplication operator.
		/// </summary>
		Multiply,  // NOTE: Would prefer to call Multiplication but that's now what is defined by Microsoft for op_XXX

		/// <summary>
		/// A division operator.
		/// </summary>
		Division,

		/// <summary>
		/// A modulus operator.
		/// </summary>
		Modulus,

		/// <summary>
		/// A bitwise AND operator.
		/// </summary>
		BitwiseAnd,

		/// <summary>
		/// A bitwise OR operator.
		/// </summary>
		BitwiseOr,

		/// <summary>
		/// A bitwise XOR operator.
		/// </summary>
		ExclusiveOr,

		/// <summary>
		/// A negation operator.
		/// </summary>
		Negation,

		/// <summary>
		/// A bitwise complement operator.
		/// </summary>
		OnesComplement,

		/// <summary>
		/// A less than operator.
		/// </summary>
		LessThan,

		/// <summary>
		/// A greater than operator.
		/// </summary>
		GreaterThan,

		/// <summary>
		/// An pre-increment operator.
		/// </summary>
		PreIncrement,

		/// <summary>
		/// An pre-decrement operator.
		/// </summary>
		PreDecrement,

		/// <summary>
		/// An post-increment operator.
		/// </summary>
		PostIncrement,

		/// <summary>
		/// An post-decrement operator.
		/// </summary>
		PostDecrement,

		/// <summary>
		/// A left shift operator.
		/// </summary>
		LeftShift,

		/// <summary>
		/// A right shift operator.
		/// </summary>
		RightShift,

		/// <summary>
		/// An equality operator.
		/// </summary>
		Equality,

		/// <summary>
		/// An inequality operator.
		/// </summary>
		Inequality,

		/// <summary>
		/// A less than or equal operator.
		/// </summary>
		LessThanOrEqual,

		/// <summary>
		/// A greater than or equal operator.
		/// </summary>
		GreaterThanOrEqual,

		/// <summary>
		/// A true operator.
		/// </summary>
		True,

		/// <summary>
		/// A false operator.
		/// </summary>
		False,

		/// <summary>
		/// An implicit cast operator.
		/// </summary>
		Implicit,

		/// <summary>
		/// An explicit cast operator.
		/// </summary>
		Explicit,

		/// <summary>
		/// A conditional OR binary operator.
		/// </summary>
		ConditionalOr,

		/// <summary>
		/// A conditional AND binary operator.
		/// </summary>
		ConditionalAnd,
		
		/// <summary>
		/// A pointer indirection operator.
		/// </summary>
		PointerIndirection,

		/// <summary>
		/// An address-of operator.
		/// </summary>
		AddressOf,

		/// <summary>
		/// A null coalescing operator.
		/// </summary>
		NullCoalescing,

		/// <summary>
		/// A reference equality operator (Is).
		/// Used in Visual Basic only.
		/// </summary>
		ReferenceEquality,

		/// <summary>
		/// A reference inequality operator (IsNot).
		/// Used in Visual Basic only.
		/// </summary>
		ReferenceInequality,
		
		/// <summary>
		/// A like operator (Like).
		/// Used in Visual Basic only.
		/// </summary>
		Like,

		/// <summary>
		/// A concatenation operator (&amp;).
		/// Used in Visual Basic only.
		/// </summary>
		StringConcatenation,

		/// <summary>
		/// An integer division operator (\).
		/// Used in Visual Basic only.
		/// </summary>
		IntegerDivision,

		/// <summary>
		/// An exponentiation operator (^).
		/// Used in Visual Basic only.
		/// </summary>
		Exponentiation,

		/// <summary>
		/// A mid-assignment operator.
		/// Used in Visual Basic only.
		/// </summary>
		Mid,

		/// <summary>
		/// An XML descendant operator (...).
		/// Used in Visual Basic only.
		/// </summary>
		XmlDescendant,

	}
}
