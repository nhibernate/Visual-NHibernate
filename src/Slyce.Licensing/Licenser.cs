using System;
using System.Collections.Generic;
using System.Text;

namespace Slyce.Licensing
{
    class Licenser
    {
        internal static bool ContinueWithTrial = false;

        public static bool CanLaunch()
        {
            if (!License.Status.Licensed)
            {
                frmMain form = new frmMain();
                form.ShowDialog();
            }
            return ContinueWithTrial;
        }
    }
}
