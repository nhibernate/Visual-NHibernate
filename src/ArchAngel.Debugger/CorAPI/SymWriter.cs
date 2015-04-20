//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
namespace Microsoft.Samples.Debugging.CorSymbolStore
{
	using System;
	using System.Diagnostics.SymbolStore;
	using System.Reflection;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;

	[
		ComImport,
		Guid("ED14AA72-78E2-4884-84E2-334293AE5214"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedWriter
	{
		void DefineDocument([MarshalAs(UnmanagedType.LPWStr)] String url,
								 ref Guid language,
								 ref Guid languageVendor,
								 ref Guid documentType,
								 [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedDocumentWriter RetVal);

		void SetUserEntryPoint(SymbolToken entryMethod);

		void OpenMethod(SymbolToken method);

		void CloseMethod();

		void OpenScope(int startOffset,
						   out int pRetVal);

		void CloseScope(int endOffset);

		void SetScopeRange(int scopeID,
								int startOffset,
								int endOffset);

		void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									int attributes,
									int cSig,
									[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									int addressKind,
									int addr1,
									int addr2,
									int addr3,
									int startOffset,
									int endOffset);

		void DefineParameter([MarshalAs(UnmanagedType.LPWStr)] String name,
								int attributes,
								int sequence,
								int addressKind,
								int addr1,
								int addr2,
								int addr3);

		void DefineField(SymbolToken parent,
						  [MarshalAs(UnmanagedType.LPWStr)] String name,
						  int attributes,
						  int cSig,
						  [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] signature,
						  int addressKind,
						  int addr1,
						  int addr2,
						  int addr3);

		void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									 int attributes,
									 int cSig,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									 int addressKind,
									 int addr1,
									 int addr2,
									 int addr3);

		void Close();

		void SetSymAttribute(SymbolToken parent,
								[MarshalAs(UnmanagedType.LPWStr)] String name,
								int cData,
								[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data);

		void OpenNamespace([MarshalAs(UnmanagedType.LPWStr)] String name);

		void CloseNamespace();

		void UsingNamespace([MarshalAs(UnmanagedType.LPWStr)] String fullName);

		void SetMethodSourceRange(ISymUnmanagedDocumentWriter startDoc,
									 int startLine,
									 int startColumn,
									 ISymUnmanagedDocumentWriter endDoc,
									 int endLine,
									 int endColumn);

		void Initialize(IntPtr emitter,
					   [MarshalAs(UnmanagedType.LPWStr)] String filename,
					   IStream stream,
					   Boolean fullBuild);

		void GetDebugInfo(out ImageDebugDirectory iDD,
							 int cData,
							 out int pcData,
							 [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] data);

		void DefineSequencePoints(ISymUnmanagedDocumentWriter document,
									 int spCount,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] offsets,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] lines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] columns,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endLines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endColumns);

		void RemapToken(SymbolToken oldToken,
							 SymbolToken newToken);

		void Initialize2(IntPtr emitter,
						[MarshalAs(UnmanagedType.LPWStr)] String tempfilename,
						IStream stream,
						Boolean fullBuild,
						[MarshalAs(UnmanagedType.LPWStr)] String finalfilename);

		void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] String name,
							   Object value,
							   int cSig,
							   [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature);

		void Abort();

	}


	[
		ComImport,
		Guid("0B97726E-9E6D-4f05-9A26-424022093CAA"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedWriter2 : ISymUnmanagedWriter
	{
		// ISymUnmanagedWriter interfaces (need to define the base interface methods also, per COM interop requirements)
		new void DefineDocument([MarshalAs(UnmanagedType.LPWStr)] String url,
									 ref Guid language,
									 ref Guid languageVendor,
									 ref Guid documentType,
									 [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedDocumentWriter RetVal);

		new void SetUserEntryPoint(SymbolToken entryMethod);

		new void OpenMethod(SymbolToken method);

		new void CloseMethod();

		new void OpenScope(int startOffset,
						   out int pRetVal);

		new void CloseScope(int endOffset);

		new void SetScopeRange(int scopeID,
								int startOffset,
								int endOffset);

		new void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									int attributes,
									int cSig,
									[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									int addressKind,
									int addr1,
									int addr2,
									int addr3,
									int startOffset,
									int endOffset);

		new void DefineParameter([MarshalAs(UnmanagedType.LPWStr)] String name,
								int attributes,
								int sequence,
								int addressKind,
								int addr1,
								int addr2,
								int addr3);

		new void DefineField(SymbolToken parent,
						  [MarshalAs(UnmanagedType.LPWStr)] String name,
						  int attributes,
						  int cSig,
						  [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] signature,
						  int addressKind,
						  int addr1,
						  int addr2,
						  int addr3);

		new void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									 int attributes,
									 int cSig,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									 int addressKind,
									 int addr1,
									 int addr2,
									 int addr3);

		new void Close();

		new void SetSymAttribute(SymbolToken parent,
								[MarshalAs(UnmanagedType.LPWStr)] String name,
								int cData,
								[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data);

		new void OpenNamespace([MarshalAs(UnmanagedType.LPWStr)] String name);

		new void CloseNamespace();

		new void UsingNamespace([MarshalAs(UnmanagedType.LPWStr)] String fullName);

		new void SetMethodSourceRange(ISymUnmanagedDocumentWriter startDoc,
									 int startLine,
									 int startColumn,
									 ISymUnmanagedDocumentWriter endDoc,
									 int endLine,
									 int endColumn);

		new void Initialize(IntPtr emitter,
					   [MarshalAs(UnmanagedType.LPWStr)] String filename,
					   IStream stream,
					   Boolean fullBuild);

		new void GetDebugInfo(out ImageDebugDirectory iDD,
							 int cData,
							 out int pcData,
							 [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] data);

		new void DefineSequencePoints(ISymUnmanagedDocumentWriter document,
									 int spCount,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] offsets,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] lines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] columns,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endLines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endColumns);

		new void RemapToken(SymbolToken oldToken,
							 SymbolToken newToken);

		new void Initialize2(IntPtr emitter,
						[MarshalAs(UnmanagedType.LPWStr)] String tempfilename,
						IStream stream,
						Boolean fullBuild,
						[MarshalAs(UnmanagedType.LPWStr)] String finalfilename);

		new void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] String name,
							   Object value,
							   int cSig,
							   [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature);

		new void Abort();

		// ISymUnmanagedWriter2 interfaces
		void DefineLocalVariable2([MarshalAs(UnmanagedType.LPWStr)] String name,
									  int attributes,
									  SymbolToken sigToken,
									  int addressKind,
									  int addr1,
									  int addr2,
									  int addr3,
									  int startOffset,
									  int endOffset);

		void DefineGlobalVariable2([MarshalAs(UnmanagedType.LPWStr)] String name,
									   int attributes,
									   SymbolToken sigToken,
									   int addressKind,
									   int addr1,
									   int addr2,
									   int addr3);


		void DefineConstant2([MarshalAs(UnmanagedType.LPWStr)] String name,
								 Object value,
								 SymbolToken sigToken);
	};

	[
		ComImport,
		Guid("12F1E02C-1E05-4B0E-9468-EBC9D1BB040F"),
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		ComVisible(false)
	]
	internal interface ISymUnmanagedWriter3 : ISymUnmanagedWriter2
	{
		// ISymUnmanagedWriter interfaces (need to define the base interface methods also, per COM interop requirements)
		new void DefineDocument([MarshalAs(UnmanagedType.LPWStr)] String url,
									 ref Guid language,
									 ref Guid languageVendor,
									 ref Guid documentType,
									 [MarshalAs(UnmanagedType.Interface)] out ISymUnmanagedDocumentWriter RetVal);

		new void SetUserEntryPoint(SymbolToken entryMethod);

		new void OpenMethod(SymbolToken method);

		new void CloseMethod();

		new void OpenScope(int startOffset,
						   out int pRetVal);

		new void CloseScope(int endOffset);

		new void SetScopeRange(int scopeID,
								int startOffset,
								int endOffset);

		new void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									int attributes,
									int cSig,
									[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									int addressKind,
									int addr1,
									int addr2,
									int addr3,
									int startOffset,
									int endOffset);

		new void DefineParameter([MarshalAs(UnmanagedType.LPWStr)] String name,
								int attributes,
								int sequence,
								int addressKind,
								int addr1,
								int addr2,
								int addr3);

		new void DefineField(SymbolToken parent,
						  [MarshalAs(UnmanagedType.LPWStr)] String name,
						  int attributes,
						  int cSig,
						  [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] signature,
						  int addressKind,
						  int addr1,
						  int addr2,
						  int addr3);

		new void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									 int attributes,
									 int cSig,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature,
									 int addressKind,
									 int addr1,
									 int addr2,
									 int addr3);

		new void Close();

		new void SetSymAttribute(SymbolToken parent,
								[MarshalAs(UnmanagedType.LPWStr)] String name,
								int cData,
								[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data);

		new void OpenNamespace([MarshalAs(UnmanagedType.LPWStr)] String name);

		new void CloseNamespace();

		new void UsingNamespace([MarshalAs(UnmanagedType.LPWStr)] String fullName);

		new void SetMethodSourceRange(ISymUnmanagedDocumentWriter startDoc,
									 int startLine,
									 int startColumn,
									 ISymUnmanagedDocumentWriter endDoc,
									 int endLine,
									 int endColumn);

		new void Initialize(IntPtr emitter,
					   [MarshalAs(UnmanagedType.LPWStr)] String filename,
					   IStream stream,
					   Boolean fullBuild);

		new void GetDebugInfo(out ImageDebugDirectory iDD,
							 int cData,
							 out int pcData,
							 [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] data);

		new void DefineSequencePoints(ISymUnmanagedDocumentWriter document,
									 int spCount,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] offsets,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] lines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] columns,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endLines,
									 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] endColumns);

		new void RemapToken(SymbolToken oldToken,
							 SymbolToken newToken);

		new void Initialize2(IntPtr emitter,
						[MarshalAs(UnmanagedType.LPWStr)] String tempfilename,
						IStream stream,
						Boolean fullBuild,
						[MarshalAs(UnmanagedType.LPWStr)] String finalfilename);

		new void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] String name,
							   Object value,
							   int cSig,
							   [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] signature);

		new void Abort();

		// ISymUnmanagedWriter2 interfaces (need to define the base interface methods also, per COM interop requirements)
		new void DefineLocalVariable2([MarshalAs(UnmanagedType.LPWStr)] String name,
									  int attributes,
									  SymbolToken sigToken,
									  int addressKind,
									  int addr1,
									  int addr2,
									  int addr3,
									  int startOffset,
									  int endOffset);

		new void DefineGlobalVariable2([MarshalAs(UnmanagedType.LPWStr)] String name,
									   int attributes,
									   SymbolToken sigToken,
									   int addressKind,
									   int addr1,
									   int addr2,
									   int addr3);


		new void DefineConstant2([MarshalAs(UnmanagedType.LPWStr)] String name,
								 Object value,
								 SymbolToken sigToken);

		// ISymUnmanagedWriter3 interfaces
		void OpenMethod2(SymbolToken method,
							  int isect,
							  int offset);
		void Commit();
	}

	internal class SymbolWriter : ISymbolWriter2
	{
		ISymUnmanagedWriter m_target;

		private unsafe ISymUnmanagedWriter GetWriter(IntPtr ppUnderlyingWriter)
		{
			// this comes in as double pointer, so need to deference in order to create CCW
			return (ISymUnmanagedWriter)Marshal.GetObjectForIUnknown((IntPtr)(*((void**)ppUnderlyingWriter.ToPointer())));
		}

		public SymbolWriter()
		{
			Guid CLSID_CorSymWriter = new Guid("0AE2DEB0-F901-478b-BB9F-881EE8066788");
			m_target = (ISymUnmanagedWriter)Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID_CorSymWriter));
		}

		public void SetUnderlyingWriter(IntPtr ppUnderlyingWriter)
		{
			m_target = GetWriter(ppUnderlyingWriter);
		}

		public void Initialize(IntPtr emitter, String filename, bool fullBuild)
		{
			m_target.Initialize(emitter, filename, null, fullBuild);
		}

		public void Initialize(Object emitter, String filename, bool fullBuild)
		{
			IntPtr uEmitter = IntPtr.Zero;
			try
			{
				uEmitter = Marshal.GetIUnknownForObject(emitter);
				m_target.Initialize(uEmitter, filename, null, fullBuild);
			}
			finally
			{
				if (uEmitter != IntPtr.Zero)
					Marshal.Release(uEmitter);
			}
		}

		public void Initialize(Object emitter,
						[MarshalAs(UnmanagedType.LPWStr)] String filename,
						IStream stream,
						Boolean fullBuild)
		{
			IntPtr uEmitter = IntPtr.Zero;
			try
			{
				uEmitter = Marshal.GetIUnknownForObject(emitter);
				m_target.Initialize(uEmitter, filename, stream, fullBuild);
			}
			finally
			{
				if (uEmitter != IntPtr.Zero)
					Marshal.Release(uEmitter);
			}
		}

		public void Initialize(Object emitter,
						[MarshalAs(UnmanagedType.LPWStr)] String tempfilename,
						IStream stream,
						Boolean fullBuild,
						[MarshalAs(UnmanagedType.LPWStr)] String finalfilename)
		{
			IntPtr uEmitter = IntPtr.Zero;
			try
			{
				uEmitter = Marshal.GetIUnknownForObject(emitter);
				m_target.Initialize2(uEmitter, tempfilename, stream, fullBuild, finalfilename);
			}
			finally
			{
				if (uEmitter != IntPtr.Zero)
					Marshal.Release(uEmitter);
			}
		}

		public ISymbolDocumentWriter DefineDocument(String url,
										  Guid language,
										  Guid languageVendor,
										  Guid documentType)
		{
			ISymUnmanagedDocumentWriter writer = null;
			m_target.DefineDocument(url, ref language, ref languageVendor, ref documentType, out writer);
			return new SymDocumentWriter(writer);
		}

		public void SetUserEntryPoint(SymbolToken entryMethod)
		{
			m_target.SetUserEntryPoint(entryMethod);
		}

		public void OpenMethod(SymbolToken method)
		{
			m_target.OpenMethod(method);
		}

		public void CloseMethod()
		{
			m_target.CloseMethod();
		}

		public void DefineSequencePoints(ISymbolDocumentWriter document,
								  int[] offsets,
								  int[] lines,
								  int[] columns,
								  int[] endLines,
								  int[] endColumns)
		{
			m_target.DefineSequencePoints(((SymDocumentWriter)document).InternalDocumentWriter, offsets.Length,
										offsets, lines, columns, endLines, endColumns);
		}

		public int OpenScope(int startOffset)
		{
			int ret;
			m_target.OpenScope(startOffset, out ret);
			return ret;
		}

		public void CloseScope(int endOffset)
		{
			m_target.CloseScope(endOffset);
		}

		public void SetScopeRange(int scopeID, int startOffset, int endOffset)
		{
			m_target.SetScopeRange(scopeID, startOffset, endOffset);
		}

		public void DefineLocalVariable(String name,
									FieldAttributes attributes,
									byte[] signature,
									SymAddressKind addressKind,
									int addr1,
									int addr2,
									int addr3,
									int startOffset,
									int endOffset)
		{
			m_target.DefineLocalVariable(name, (int)attributes, signature.Length, signature,
										 (int)addressKind, addr1, addr2, addr3, startOffset, endOffset);
		}

		public void DefineParameter(String name,
								ParameterAttributes attributes,
								int sequence,
								SymAddressKind addressKind,
								int addr1,
								int addr2,
								int addr3)
		{
			m_target.DefineParameter(name, (int)attributes, sequence, (int)addressKind, addr1, addr2, addr3);
		}

		public void DefineField(SymbolToken parent,
								String name,
								FieldAttributes attributes,
								byte[] signature,
								SymAddressKind addressKind,
								int addr1,
								int addr2,
								int addr3)
		{
			m_target.DefineField(parent, name, (int)attributes, signature.Length, signature,
								 (int)addressKind, addr1, addr2, addr3);
		}

		public void DefineGlobalVariable(String name,
									 FieldAttributes attributes,
									 byte[] signature,
									 SymAddressKind addressKind,
									 int addr1,
									 int addr2,
									 int addr3)
		{
			m_target.DefineGlobalVariable(name, (int)attributes, signature.Length, signature,
										  (int)addressKind, addr1, addr2, addr3);
		}

		public void Close()
		{
			m_target.Close();
		}

		public void SetSymAttribute(SymbolToken parent, String name, byte[] data)
		{
			m_target.SetSymAttribute(parent, name, data.Length, data);
		}

		public void OpenNamespace(String name)
		{
			m_target.OpenNamespace(name);
		}

		public void CloseNamespace()
		{
			m_target.CloseNamespace();
		}

		public void UsingNamespace(String fullName)
		{
			m_target.UsingNamespace(fullName);
		}

		public void SetMethodSourceRange(ISymbolDocumentWriter startDoc,
										 int startLine,
										 int startColumn,
										 ISymbolDocumentWriter endDoc,
										 int endLine,
										 int endColumn)
		{
			m_target.SetMethodSourceRange(((SymDocumentWriter)startDoc).InternalDocumentWriter, startLine, startColumn,
										  ((SymDocumentWriter)endDoc).InternalDocumentWriter, endLine, endColumn);
		}

		public byte[] GetDebugInfo(out ImageDebugDirectory iDD)
		{
			int length;
			m_target.GetDebugInfo(out iDD, 0, out length, null);
			byte[] data = new byte[length];
			m_target.GetDebugInfo(out iDD, length, out length, data);
			System.Diagnostics.Debug.Assert(data.Length == length);
			return data;
		}


		public void RemapToken(SymbolToken oldToken,
							SymbolToken newToken)
		{
			m_target.RemapToken(oldToken, newToken);
		}


		public void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] String name,
								Object value,
								byte[] signature)
		{
			m_target.DefineConstant(name, value, signature.Length, signature);
		}

		public void Abort()
		{
			m_target.Abort();
		}

		public void DefineLocalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
										int attributes,
										SymbolToken sigToken,
										int addressKind,
										int addr1,
										int addr2,
										int addr3,
										int startOffset,
										int endOffset)
		{
			((ISymUnmanagedWriter2)m_target).DefineLocalVariable2(name, attributes, sigToken,
										 addressKind, addr1, addr2, addr3, startOffset, endOffset);
		}

		public void DefineGlobalVariable([MarshalAs(UnmanagedType.LPWStr)] String name,
									   int attributes,
									   SymbolToken sigToken,
									   int addressKind,
									   int addr1,
									   int addr2,
									   int addr3)
		{
			((ISymUnmanagedWriter2)m_target).DefineGlobalVariable2(name, attributes, sigToken,
																  addressKind, addr1, addr2, addr3);
		}



		public void DefineConstant([MarshalAs(UnmanagedType.LPWStr)] String name,
								  Object value,
								  SymbolToken sigToken)
		{
			((ISymUnmanagedWriter2)m_target).DefineConstant2(name, value, sigToken);
		}
	}
}
