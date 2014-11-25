using System.Collections.Generic;
using ArchAngel.Interfaces.ITemplate;

namespace ArchAngel.Interfaces
{
	public interface IScriptBaseObject
	{
		List<IUserOption> Ex { get; set; }
		void AddUserOption(IUserOption userOption);
		object GetUserOptionValue(string name);

		/// <summary>
		/// Gets the value of the named User Option and casts it to T.
		/// If the value cannot be casted to T, this will return default(T).
		/// </summary>
		/// <typeparam name="T">The type of the User Option</typeparam>
		/// <param name="name">The name of the User Option to serach for.</param>
		/// <returns>The value of the named User Option, or default(T).</returns>
		T GetUserOptionValue<T>(string name);
		bool HasUserOption(string name);
	}

}

