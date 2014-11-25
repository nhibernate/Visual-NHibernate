using System.Collections.Generic;

namespace Demo.Providers.Test
{
    public class School : ArchAngel.Interfaces.ScriptBaseObject
    {
    	private List<Pupil> _pupils = new List<Pupil>();

    	public string Name { get; set; }

    	public List<Pupil> Pupils
        {
            get { return _pupils; }
            set { _pupils = value; }
        }

    }
}
