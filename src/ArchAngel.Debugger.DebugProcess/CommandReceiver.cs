using System;
using ArchAngel.Debugger.DebugProcess;
using ArchAngel.Interfaces;
namespace ArchAngel.Debugger.Common
{
    /// <summary>
    /// This class is used to load and run code to be debugged. It is used
    /// by passing ICommand objects to its ExecuteCommand function. To set the
    /// object up, use a LoadAssembliesCommand. If this command is not run first,
    /// many other commands will fail to run.
    /// </summary>
    public class CommandReceiver : MarshalByRefObject
    {
        /// <summary>
        /// Used to flag whether the object has been set up yet.
        /// </summary>
        internal bool Loaded = false;
		/// <summary>
		/// The project we use to run the debug project.
		/// </summary>
    	internal IWorkbenchProject CurrentProject;

        /// <summary>
        /// Execute some code in the app domain this object is running in.
        /// </summary>
        /// <param name="c">The command to execute. Calls the RunCommand() method on c. Cannot be null.</param>
        /// <returns>The output of the c.RunCommand(this) call. True if the command was sucessful.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if c is null</exception>
        public bool ExecuteCommand(ICommand c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("c");
            }
            return c.RunCommand(this);
        }

        /// <summary>
        /// We need to override this method to return null so that it stays in
        /// memory till the process exits. If the object is garbage collected then
        /// we loose valuable state information.
        /// </summary>
        /// <returns>null</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}