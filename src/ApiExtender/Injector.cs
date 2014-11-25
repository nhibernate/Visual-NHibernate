using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using ArchAngel.Interfaces;
using ArchAngel.Providers.CodeProvider;
using ArchAngel.Providers.CodeProvider.CSharp;
using ArchAngel.Providers.CodeProvider.DotNet;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace ApiExtender
{
	public static class Injector
	{
		//public static string DllFile { get; set; }

		//public static string ProjectFile { get; set; }
		internal static bool RunningCommandLine = true;

		private static void WriteStatus(string text)
		{
			if (RunningCommandLine)
			{
				Console.Write(text);
			}
			else
			{
				frmMain.Instance.StatusBox.AppendText(text);
				frmMain.Instance.StatusBox.ScrollToCaret();
			}
		}

		private static void WriteLineStatus(string text)
		{
			if (RunningCommandLine)
			{
				Console.WriteLine(text);
			}
			else
			{
				frmMain.Instance.StatusBox.AppendText(text + Environment.NewLine);
				frmMain.Instance.StatusBox.ScrollToCaret();
			}
		}

		internal static bool SignAssembly(string filename, string keyFilename, out string standardOutput)
		{
			ProcessStartInfo psi = new ProcessStartInfo(@"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\sn.exe");
			psi.Arguments = string.Format("-R \"{0}\" \"{1}\"", filename, keyFilename);

			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			var proc = Process.Start(psi);

			standardOutput = proc.StandardOutput.ReadToEnd();

			// Wait for a maximum of 30 seconds.
			proc.WaitForExit(30000);

			if (proc.ExitCode != 0)
			{
				return false;
			}

			return true;
		}

		private static string GetFullyQualifiedDisplayName(TypeDefinition type, MethodDefinition method)
		{
			StringBuilder sbParams = new StringBuilder();

			foreach (ParameterDefinition parm in method.Parameters)
			{
				string modifier = parm.Attributes.ToString().ToLower();

				if (modifier == "none")
				{
					modifier = "";
				}
				if (modifier.Length > 0)
				{
					modifier += " ";
				}
				string typeName = parm.ParameterType.GetOriginalType().FullName;

				if (typeName.LastIndexOf("/") > typeName.LastIndexOf("."))
				{
					if (typeName.LastIndexOf(".") > 0)
					{
						typeName = typeName.Substring(typeName.LastIndexOf(".") + 1).Replace("/", ".");
					}
					else
					{
						typeName = typeName.Replace("/", ".");
					}
				}
				else
				{
					typeName = parm.ParameterType.GetOriginalType().Name;
				}

				if (parm.ParameterType.Name.LastIndexOf("[]") == parm.ParameterType.Name.Length - 2)
				{
					typeName += "[]";
				}
				sbParams.AppendFormat("{0}{1},", modifier, typeName);
			}
			string result = string.Format("{0}.{1}({2})", type.FullName.Replace("/", "."), method.Name, sbParams.ToString().Trim(new char[] { ',' })).Replace(" ", "");
			return result;
		}

		public static bool Run(string dllFile, string projectFile)
		{
			if (!File.Exists(projectFile))
			{
				WriteLineStatus("Project file not found.");
				return false;
			}
			if (!File.Exists(dllFile))
			{
				WriteLineStatus("Compiled DLL not found.");
				return false;
			}
			try
			{
				List<Function> functions = GetFunctionsWithAttribute(projectFile, "ApiExtension");


				AssemblyDefinition assembly =
					AssemblyFactory.GetAssembly(
						dllFile);

				assembly.MainModule.LoadSymbols();

				StringBuilder sb = new StringBuilder();

				// Gets all types which are declared in the Main Module of the assembly
				foreach (TypeDefinition type in assembly.MainModule.Types)
				{
					// Writes the full name of a type
					sb.AppendLine(type.FullName);

					// Gets all methods of the current type
					foreach (MethodDefinition method in type.Methods)
					{
						if (method.CustomAttributes.Count > 0)
						{
							string attributeName = method.CustomAttributes[0].Constructor.DeclaringType.Name;

							if (attributeName == "ApiExtensionAttribute")
							{
								bool matchingFunctionFound = false;

								List<Function> possibleMathes = new List<Function>();

								// Find the matching function in the source-code functions
								foreach (Function function in functions)
								{
									if (function.Name != method.Name)
									{
										continue;
									}
									bool isMatch = false;
									string signature1 = function.FullyQualifiedDisplayNameWithBCLTypes.Replace(".Fields.Constructors.Properties.Functions.Inner Classes", "").Replace(".Fields.Constructors.Properties.Functions", "").Replace(".Fields.Constructors.Functions", "").Replace(".Constructors.Functions", "").Replace(".Functions", "").Replace(" ", "").ToLower();
									string signature2 = GetFullyQualifiedDisplayName(type, method).ToLower();

									if (function.Name == method.Name)
									{
										//string gg = "";
									}
									isMatch = signature1 == signature2;

									if (isMatch)
									{
										possibleMathes.Add(function);
									}
									else
									{
										// It might still be a match, but parameter types might be qualified differently
										//if (function.FullyQualifiedName.IndexOf(type.FullName.Replace("/", ".Fields.Constructors.Properties.Functions.Inner Classes.")) == 0 && method.Name == function.Name && method.Parameters.Count == function.Parameters.Count)
										string name1 = function.FullyQualifiedName.Replace(".Fields.Constructors.Properties.Functions.Inner Classes", "").Replace(".Fields.Constructors.Properties.Functions", "").Replace(".Fields.Constructors.Functions", "").Replace(".Constructors.Functions", "").Replace(".Functions", "");
										string name2 = string.Format("{0}.{1}", type.FullName, method.Name).Replace("/", ".");

										if (name1 == name2 && method.Parameters.Count == function.Parameters.Count)
										{
											bool parametersMatch = true;

											for (int paramCounter = 0; paramCounter < method.Parameters.Count; paramCounter++)
											{
												if (method.Parameters[paramCounter].Name != function.Parameters[paramCounter].Name)
												{
													parametersMatch = false;
													break;
												}
											}
											if (parametersMatch)
											{
												if (!method.IsPublic)
												{
													WriteLineStatus(string.Format("Function is not public: {0}.{1}", type.FullName, method.Name));
													return false;
												}
												possibleMathes.Add(function);
											}
										}
									}
								}
								if (possibleMathes.Count == 0)
								{
									WriteLineStatus(string.Format("Function not found in source-code: {0}.{1}", type.FullName, method.Name));
									return false;
								}
								else if (possibleMathes.Count == 1)
								{
									// No need to perform any further checks
									matchingFunctionFound = true;
									StringBuilder xmlComments = new StringBuilder(100);

									foreach (string xmlComment in possibleMathes[0].XmlComments)
									{
										xmlComments.AppendLine(xmlComment);
									}
									string functionBody = Slyce.Common.Utility.StandardizeLineBreaks(possibleMathes[0].BodyText,
																									 Slyce.Common.Utility.LineBreaks.Unix).
										Replace("\n", "");
									functionBody = CleanFunctionBodyEnds(functionBody);

									if (method.CustomAttributes[0].ConstructorParameters.Count == 0)
									{
										method.CustomAttributes.RemoveAt(0);
										CustomAttribute ca = new CustomAttribute(assembly.MainModule.Import(
																					typeof(ArchAngel.Interfaces.Attributes.ApiExtensionAttribute).
																						GetConstructor(
																						new Type[] { typeof(string), typeof(string) })));

										ca.ConstructorParameters.Clear();
										ca.ConstructorParameters.Add(xmlComments.ToString());
										ca.ConstructorParameters.Add(functionBody);

										method.CustomAttributes.Add(ca);
									}
									else if (method.CustomAttributes[0].ConstructorParameters.Count == 2)
									{
										method.CustomAttributes[0].ConstructorParameters[0] = xmlComments.ToString();
										method.CustomAttributes[0].ConstructorParameters[1] = functionBody;
									}
									else
									{
										WriteLineStatus("FAILED - ApiExtensionAttribute has unexpected number of arguments: " + method.Name);
										return false;
									}
									WriteLineStatus("Processed: " + method.Name);
								}
								else
								{
									bool parametersMatch = true;
									matchingFunctionFound = false;

									foreach (Function function in possibleMathes)
									{
										for (int paramCounter = 0; paramCounter < method.Parameters.Count; paramCounter++)
										{
											if (method.Parameters[paramCounter].ParameterType.Name != function.Parameters[paramCounter].DataType)
											{
												parametersMatch = false;
												break;
											}
										}
										if (parametersMatch)
										{
											matchingFunctionFound = true;
											method.CustomAttributes[0].ConstructorParameters[0] = function.Comments.PreceedingComments;
											method.CustomAttributes[0].ConstructorParameters[1] =
												CleanFunctionBodyEnds(Slyce.Common.Utility.StandardizeLineBreaks(function.BodyText, Slyce.Common.Utility.LineBreaks.Unix).Replace(
													"\n", ""));
											break;
										}
									}
									if (!matchingFunctionFound)
									{
										WriteLineStatus("Many possible matching functions found in source-code: " + method.Name);
										return false;
									}
								}
								//}

								if (method.Body == null)
								{
									continue;
								}
								// Gets the CilWorker of the method for working with CIL instructions
								CilWorker worker = method.Body.CilWorker;
								FieldReference projectInstance = assembly.MainModule.Import(typeof(SharedData).GetField("CurrentProject"));
								//MethodReference projectInstance =
								//    assembly.MainModule.Import(typeof(SharedData).GetMethod("get_CurrentProject"));
								MethodReference callApiExtFunctionMethodRef =
									assembly.MainModule.Import(typeof(IWorkbenchProject).GetMethod("CallApiExtensionFunction"));

								// Create local variables
								VariableDefinition archAngelApiExtResult = new VariableDefinition(assembly.MainModule.Import(typeof(object)));
								VariableDefinition parameters1 = new VariableDefinition(assembly.MainModule.Import(typeof(object[])));
								VariableDefinition parameters2 = new VariableDefinition(assembly.MainModule.Import(typeof(object[])));

								int localVarIndexOffset = method.Body.Variables.Count;
								method.Body.Variables.Add(archAngelApiExtResult);
								method.Body.Variables.Add(parameters1);
								method.Body.Variables.Add(parameters2);

								// Get the first instruction of the current method
								Instruction firstInstruction = method.Body.Instructions[0];
								// Arguments in a non-static method are one-based, because arg[0] is 'this'.
								int argIndexOffset = method.IsStatic ? 0 : 1;

								#region Initialize all "out" arguments to null
								for (int argIndex = 0; argIndex < method.Parameters.Count; argIndex++)
								{
									ParameterDefinition arg = method.Parameters[argIndex];
									int realArgIndex = argIndex + argIndexOffset;

									if (arg.IsOut)
									{
										// Load the argument
										switch (realArgIndex)
										{
											case 0:
												method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_0));
												break;
											case 1:
												method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_1));
												break;
											case 2:
												method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_2));
												break;
											case 3:
												method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_3));
												break;
											default:
												method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_S, arg));
												break;
										}
										// Set it to null
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldnull));
										// Store the ref object
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stind_Ref));
									}
								}
								#endregion

								#region Initialize object array

								#region Size array to number of arguments
								switch (method.Parameters.Count)
								{
									case 0:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_0));
										break;
									case 1:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_1));
										break;
									case 2:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_2));
										break;
									case 3:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_3));
										break;
									case 4:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_4));
										break;
									case 5:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_5));
										break;
									case 6:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_6));
										break;
									case 7:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_7));
										break;
									case 8:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_8));
										break;
									default:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_S, (sbyte)method.Parameters.Count));
										//method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_S, method.Parameters[method.Parameters.Count - 1]));
										break;

								}
								#endregion

								// Create a new array of objects, load the object array
								method.Body.CilWorker.InsertBefore(firstInstruction,
																   worker.Create(OpCodes.Newarr, assembly.MainModule.Import(typeof(object))));

								// Create new object[] - must be declared as a local variable
								// parameters2 is the third variable we added
								switch (localVarIndexOffset + 2)
								{
									case 0:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_0));
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_0));
										break;
									case 1:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_1));
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_1));
										break;
									case 2:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_2));
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_2));
										break;
									case 3:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_3));
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_3));
										break;
									default:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_S, parameters2));
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_S, parameters2));
										break;

								}
								#endregion

								// Load all method arguments into the object array
								for (int argIndex = 0; argIndex < method.Parameters.Count; argIndex++)
								{
									int realArgIndex = argIndex + argIndexOffset;
									ParameterDefinition arg = method.Parameters[argIndex];

									// Specify the array insertion index.
									switch (argIndex)
									{
										case 0:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_0));
											break;
										case 1:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_1));
											break;
										case 2:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_2));
											break;
										case 3:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_3));
											break;
										case 4:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_4));
											break;
										case 5:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_5));
											break;
										case 6:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_6));
											break;
										case 7:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_7));
											break;
										case 8:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_8));
											break;
										default:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldc_I4_S, (sbyte)argIndex));
											break;
									}
									// Load the argument
									switch (realArgIndex)
									{
										case 0:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_0));
											break;
										case 1:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_1));
											break;
										case 2:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_2));
											break;
										case 3:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_3));
											break;
										default:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldarg_S, arg));
											break;
									}
									if (arg.ParameterType.IsValueType)
									{
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Box, arg.ParameterType));
									}
									// If the parameter is out or ref
									if (arg.IsOut)
									{
										// Load as indirect reference
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldind_Ref));
									}
									// Replace the current array object with the new object
									method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stelem_Ref));

									// Assign the new object
									// parameters2 is the third variable we added
									switch (localVarIndexOffset + 2)
									{
										case 0:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_0));
											break;
										case 1:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_1));
											break;
										case 2:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_2));
											break;
										case 3:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_3));
											break;
										default:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_S, parameters2));
											break;
									}
								}
								// POP (parameters variable) OFF STACK AND STORE IN LOCAL VARIABLE AT INDEX 1 (object[] parameters)
								// parameters1 is the third variable we added
								switch (localVarIndexOffset + 1)
								{
									case 0:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_0));
										break;
									case 1:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_1));
										break;
									case 2:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_2));
										break;
									case 3:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_3));
										break;
									default:
										method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Stloc_S, parameters1));
										break;
								}
								// Call the ApiExtension function
								//// NEW
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldsfld, projectInstance));
								// END NEW
								//method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Call, projectInstance));
								method.Body.CilWorker.InsertBefore(firstInstruction,
																   worker.Create(OpCodes.Ldstr, method.DeclaringType.FullName + "." + method.Name));
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloca_S, archAngelApiExtResult));
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloca_S, parameters1));
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Callvirt, callApiExtFunctionMethodRef));
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Brfalse_S, firstInstruction));

								if (method.ReturnType.ReturnType.FullName != "System.Void")
								{
									switch (localVarIndexOffset)
									{
										case 0:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_0));
											break;
										case 1:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_1));
											break;
										case 2:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_2));
											break;
										case 3:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_3));
											break;
										default:
											method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ldloc_S, archAngelApiExtResult));
											break;
									}
									//method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Castclass, method.ReturnType.ReturnType));
									method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Unbox_Any, method.ReturnType.ReturnType));
								}
								method.Body.CilWorker.InsertBefore(firstInstruction, worker.Create(OpCodes.Ret));
								method.Body.InitLocals = true;
							}
							//}// Added
						}
						//Import the modifying type into the AssemblyDefinition of //"MyLibrary"
						assembly.MainModule.Import(type);
					}
				}
				//Save the modified "MyLibrary" assembly
				string newFilename = GetNewFilename(dllFile);
				AssemblyFactory.SaveAssembly(assembly, newFilename);

				// Copy the existing pdb sideways
				string sourceDirectory = Path.GetDirectoryName(dllFile);
				string targetDirectory = Path.GetDirectoryName(newFilename);
				string sourcePdbFilePath = Path.Combine(sourceDirectory, Path.GetFileNameWithoutExtension(dllFile) + ".pdb");
				string tempPdbFilePath = Path.Combine(sourceDirectory, Path.GetFileNameWithoutExtension(dllFile) + "_temp.pdb");
				string targetPdbFilePath = Path.Combine(targetDirectory, Path.GetFileNameWithoutExtension(dllFile) + "_EXT.pdb");
				File.Copy(sourcePdbFilePath, tempPdbFilePath);

				assembly.MainModule.SaveSymbols();
				if (File.Exists(targetPdbFilePath))
					File.Delete(targetPdbFilePath);
				File.Move(sourcePdbFilePath, targetPdbFilePath);
				File.Move(tempPdbFilePath, sourcePdbFilePath);

				WriteLineStatus("Finished");
				return true;
			}
			catch (Exception ex)
			{
				WriteLineStatus("Error: " + ex.Message);
				return false;
			}
		}

		public static string GetNewFilename(string dllFile)
		{
			return Path.Combine(Path.GetDirectoryName(dllFile),
								string.Format("{0}_EXT{1}", Path.GetFileNameWithoutExtension(dllFile),
											  Path.GetExtension(dllFile)));
		}

		private static string CleanFunctionBodyEnds(string body)
		{
			char[] chars = new char[] { '\n', '\r', '\t' };
			body = body.Trim(chars);

			if (body.Length > 0 && body[0] == '{' && body[body.Length - 1] == '}')
			{
				body = body.Substring(1, body.Length - 2);
			}
			return body;
		}

		public static void WriteFunctionBodiesAsXml(string projectFilePath, string dllPath)
		{
			string xmlpath = Path.ChangeExtension(dllPath, "functions.xml");
			string xml = GetFunctionBodiesAsXml(projectFilePath, dllPath);
			File.WriteAllText(xmlpath, xml);
		}

		public static string GetFunctionBodiesAsXml(string projectFilePath, string dllPath)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				using (XmlWriter writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = Encoding.UTF8 }))
				{
					if (writer == null) throw new Exception("Could not create XmlWriter");

					writer.WriteStartDocument();
					writer.WriteStartElement("FunctionInformation");
					writer.WriteAttributeString("version", "1");
					foreach (FunctionInformation funcInfo in GetAllFunctionInformations(projectFilePath, dllPath))
					{
						if (funcInfo == null) continue;

						writer.WriteStartElement("Function");

						writer.WriteAttributeString("type", funcInfo.MethodInfo.DeclaringType.FullName);
						writer.WriteAttributeString("name", funcInfo.MethodInfo.Name);
						foreach (ParameterDefinition paramInfo in funcInfo.MethodInfo.Parameters)
						{
							writer.WriteElementString("Parameter", paramInfo.ParameterType.FullName);
						}

						writer.WriteElementString("BodyText", funcInfo.Body);
						writer.WriteElementString("XmlComments", funcInfo.XmlComments);
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
					writer.WriteEndDocument();

					writer.Close();
				}

				return Encoding.UTF8.GetString(ms.ToArray());
			}
		}

		private static IEnumerable<FunctionInformation> GetAllFunctionInformations(string projectFilePath, string dllPath)
		{
			AssemblyDefinition assembly = AssemblyFactory.GetAssembly(dllPath);
			var functionsFromSourceCode = GetFunctionsWithAttribute(projectFilePath, "ApiExtension");
			foreach (TypeDefinition type in assembly.MainModule.Types)
			{
				// Gets all methods of the current type
				foreach (MethodDefinition method in type.Methods)
				{
					if (method.CustomAttributes.Count > 0)
					{
						string attributeName = method.CustomAttributes[0].Constructor.DeclaringType.Name;

						if (attributeName == "ApiExtensionAttribute")
						{
							yield return SearchFunctions(functionsFromSourceCode, type, method);
						}
					}
				}
			}
		}

		private class FunctionInformation
		{
			public string XmlComments;
			public string Body;
			public MethodDefinition MethodInfo;

			public FunctionInformation(string body, string xmlComments, MethodDefinition methodInfo)
			{
				XmlComments = xmlComments;
				Body = body;
				MethodInfo = methodInfo;
			}
		}

		private static FunctionInformation SearchFunctions(IEnumerable<Function> functions, TypeDefinition type, MethodDefinition method)
		{
			List<Function> possibleMathes = new List<Function>();

			// Find the matching function in the source-code functions
			foreach (Function function in functions)
			{
				if (function.Name != method.Name)
				{
					continue;
				}
				string signature1 = function.FullyQualifiedDisplayNameWithBCLTypes.Replace(".Fields.Constructors.Properties.Functions.Inner Classes", "").Replace(".Fields.Constructors.Properties.Functions", "").Replace(".Fields.Constructors.Functions", "").Replace(".Constructors.Functions", "").Replace(".Functions", "").Replace(" ", "").ToLower();
				string signature2 = GetFullyQualifiedDisplayName(type, method).ToLower();

				bool isMatch = signature1 == signature2;

				if (isMatch)
				{
					possibleMathes.Add(function);
				}
				else
				{
					// It might still be a match, but parameter types might be qualified differently
					//if (function.FullyQualifiedName.IndexOf(type.FullName.Replace("/", ".Fields.Constructors.Properties.Functions.Inner Classes.")) == 0 && method.Name == function.Name && method.Parameters.Count == function.Parameters.Count)
					string name1 = function.FullyQualifiedName.Replace(".Fields.Constructors.Properties.Functions.Inner Classes", "").Replace(".Fields.Constructors.Properties.Functions", "").Replace(".Fields.Constructors.Functions", "").Replace(".Constructors.Functions", "").Replace(".Functions", "");
					string name2 = string.Format("{0}.{1}", type.FullName, method.Name).Replace("/", ".");

					if (name1 == name2 && method.Parameters.Count == function.Parameters.Count)
					{
						bool parametersMatch = true;

						for (int paramCounter = 0; paramCounter < method.Parameters.Count; paramCounter++)
						{
							if (method.Parameters[paramCounter].Name != function.Parameters[paramCounter].Name)
							{
								parametersMatch = false;
								break;
							}
						}
						if (parametersMatch)
						{
							if (!method.IsPublic)
							{
								WriteLineStatus(string.Format("Function is not public: {0}.{1}", type.FullName, method.Name));
								return null;
							}
							possibleMathes.Add(function);
						}
					}
				}
			}
			if (possibleMathes.Count == 0)
			{
				WriteLineStatus(string.Format("Function not found in source-code: {0}.{1}", type.FullName, method.Name));
				return null;
			}
			else if (possibleMathes.Count == 1)
			{
				// No need to perform any further checks
				StringBuilder xmlComments = new StringBuilder(100);

				foreach (string xmlComment in possibleMathes[0].XmlComments)
				{
					xmlComments.AppendLine(xmlComment);
				}
				string functionBody = Slyce.Common.Utility.StandardizeLineBreaks(possibleMathes[0].BodyText,
																				 Slyce.Common.Utility.LineBreaks.Unix).
					Replace("\n", "");
				functionBody = CleanFunctionBodyEnds(functionBody);

				WriteLineStatus("Processed: " + method.Name);
				return new FunctionInformation(functionBody, xmlComments.ToString(), method);
			}
			else
			{
				bool parametersMatch = true;

				foreach (Function function in possibleMathes)
				{
					for (int paramCounter = 0; paramCounter < method.Parameters.Count; paramCounter++)
					{
						if (method.Parameters[paramCounter].ParameterType.Name != function.Parameters[paramCounter].DataType)
						{
							parametersMatch = false;
							break;
						}
					}
					if (parametersMatch)
					{
						string functionBody =
							CleanFunctionBodyEnds(Slyce.Common.Utility.StandardizeLineBreaks(function.BodyText, Slyce.Common.Utility.LineBreaks.Unix).Replace(
								"\n", ""));
						return new FunctionInformation(functionBody, string.Join("\n", function.Comments.PreceedingComments.ToArray()), method);
					}
				}

				WriteLineStatus("Many possible matching functions found in source-code: " + method.Name);
				return null;
			}
		}

		/// <summary>
		/// Gets all files in referenced in a project.
		/// </summary>
		/// <param name="csprojPath"></param>
		/// <returns></returns>
		private static List<Function> GetFunctionsWithAttribute(string csprojPath, string attributeText)
		{
			List<Function> functions = new List<Function>();
			XmlDocument doc = new XmlDocument();
			doc.Load(csprojPath);
			XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
			nsManager.AddNamespace("ms", "http://schemas.microsoft.com/developer/msbuild/2003");

			foreach (XmlNode node in doc.SelectNodes("ms:Project/ms:ItemGroup/ms:Compile/@Include", nsManager))
			{
				string file = Slyce.Common.RelativePaths.GetFullPath(Path.GetDirectoryName(csprojPath), node.InnerText);

				if (Slyce.Common.Utility.ReadTextFile(file).IndexOf(attributeText) >= 0)
				{
					functions.AddRange(GetFunctionsInFileWithAttribute(file, attributeText));
				}
			}
			return functions;
		}

		private static void csf_RaiseError(string fileName, string procedureName, string description, string originalText, int lineNumber, int startPos, int length)
		{
			//MessageBox.Show(string.Format("Error occurred. \nFile: {0}\nProcedure: {1}\nDescription: {2}", fileName, procedureName, description));
		}

		private static List<Function> GetFunctionsInFileWithAttribute(string file, string attributeName)
		{
			List<Function> functions = new List<Function>();
			string code = Slyce.Common.Utility.ReadTextFile(file);
			CSharpParser csf = new CSharpParser();
			csf.FormatSettings.CommentLinesAsCommentBlock = true;
			csf.ParseCode(code);

			if (csf.ErrorOccurred)
			{
				// TODO: what do we do here?
				throw new Exception("Parser error. Please contact support@slyce.com");
			}

			CodeRoot root = (CodeRoot)csf.CreatedCodeRoot;

			foreach (Namespace ns in root.Namespaces)
			{
				foreach (Class c in ns.Classes)
				{
					functions.AddRange(GetFunctionsInClassWithAttribute(c, attributeName));
				}
			}
			foreach (Class c in root.Classes)
			{
				functions.AddRange(GetFunctionsInClassWithAttribute(c, attributeName));
			}
			return functions;
		}

		private static List<Function> GetFunctionsInClassWithAttribute(Class theClass, string attributeName)
		{
			List<Function> functions = new List<Function>();

			foreach (Function function in theClass.Functions)
			{
				foreach (ArchAngel.Providers.CodeProvider.DotNet.Attribute att in function.Attributes)
				{
					if (att.Name.IndexOf(attributeName) < 0) continue;

					functions.Add(function);
					break;
				}
			}
			foreach (Class innerClass in theClass.InnerClasses)
			{
				functions.AddRange(GetFunctionsInClassWithAttribute(innerClass, attributeName));
			}
			return functions;
		}

	}
}
