using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArchAngel.Interfaces.Attributes;
using ArchAngel.Interfaces.SchemaDiagrammer;
using Slyce.Common;
using Slyce.Common.EventExtensions;

namespace ArchAngel.Providers.EntityModel.Model.DatabaseLayer
{
	public class NullEntityObject : Table
	{
		internal NullEntityObject()
		{
			Name = "";
		}

	    public override string DisplayName
	    {
            get { return ""; }
	    }

		public void DeleteSelf()
		{
		}
	}
}