using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Interfaces.Template
{
	public class File
	{
		public File()
		{
			Iterator = IteratorTypes.None;
			Script = new Script();
			CompileErrors = new List<System.CodeDom.Compiler.CompilerError>();
			Encoding = Encoding.Unicode;
		}

		public string Name { get; set; }
		public Encoding Encoding { get; set; }
		public int Id { get; set; }
		public IteratorTypes Iterator { get; set; }
		public Folder ParentFolder { get; set; }
		public Script Script { get; set; }
		public List<System.CodeDom.Compiler.CompilerError> CompileErrors;
	}
}
