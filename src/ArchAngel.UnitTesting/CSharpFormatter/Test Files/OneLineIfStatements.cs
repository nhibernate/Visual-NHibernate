using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Workbench.Tests.CSharpFormatter.Test_Files
{
	class OneLineIfStatements
	{
		public void Method()
		{
			if (true) 
				Method();
		}
	}
}