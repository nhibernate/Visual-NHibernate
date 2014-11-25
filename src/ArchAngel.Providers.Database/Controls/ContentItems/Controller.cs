using System;
using System.Collections.Generic;
using System.Text;

namespace ArchAngel.Providers.Database.Controls.ContentItems
{
    public class Controller : ArchAngel.Interfaces.Controls.ContentItems.Controller
    {
        public static Model.AppConfig TheAppConfig = null;
        public static ArchAngel.Providers.Database.BLL.Database TheBllDatabase = null;
        private ArchAngel.Providers.Database.BLL.Database _bllDatabase;
            
        /// <summary>
        /// Saves the page/object(s) state to file eg: xml, binary serialization etc
        /// </summary>
        /// <param name="filename"></param>
        public override void Save(string filename)
        {
            throw new NotImplementedException("Save() has not yet been implemented for the DatabaseProvider.");
        }

        /// <summary>
        /// Reads the page/object(s) state from file eg: xml, binary serialization etc
        /// </summary>
        /// <param name="filename"></param>
        public override void Open(string filename)
        {
            throw new NotImplementedException("Open() has not yet been implemented for the DatabaseProvider.");
        }

        public ArchAngel.Providers.Database.BLL.Database BllDatabase
        {
            get { return _bllDatabase; }
            set { _bllDatabase = value; }
        }


    }
}
