using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace ApiExtender
{
    public class ApiExtender : Task
    {
        [Required]
        public string Assembly { get; set; }
        [Required]
        public string Output { get; set; }
        public string KeyFile { get; set; }

        public override bool Execute()
        {
            if (Injector.Run(Assembly, Output) != true)
            {
                Log.LogError("Could not run extender:");
                Log.LogError(Assembly);
                Log.LogError(Output);
                return false;
            }

            if(KeyFile != null)
            {
                string standardOutput;
                if(Injector.SignAssembly(Output, KeyFile, out standardOutput) == false)
                    Log.LogError(standardOutput);

            }
            return true;
        }
    }
}
