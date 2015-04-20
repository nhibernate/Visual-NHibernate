using System.Collections.Generic;
// References to ArchAngel specific libraries
using ArchAngel.Providers.Database.Model;
using ArchAngel.Providers.Database.IDAL;

namespace ArchAngel.Providers.Database.BLL
{
    public class StoredProcedure : ScriptBLL
    {
        protected List<Model.StoredProcedure> _scriptObjects = new List<Model.StoredProcedure>();

        public StoredProcedure(DatabaseTypes dalAssemblyName, ConnectionStringHelper connectionString)
            : base(dalAssemblyName, connectionString)//, Model.StoredProcedure.StoredProcedurePrefixes)
        {
            IStoredProcedure dal = DALFactory.DataAccess.CreateStoredProcedure(DalAssemblyName, ConnectionString);

            Model.StoredProcedure[] storedProcedures = dal.GetStoredProcedures();
            //InitiateAlias(storedProcedures);
            //this.ErrorMessages.AddRange(dal.
            InitialCreateFilters(storedProcedures);

            foreach (Model.StoredProcedure sp in storedProcedures)
            {
                foreach (string error in sp.Errors)
                {
                    ErrorMessages.Add(error);
                }
            }
            _scriptObjects = new List<Model.StoredProcedure>(storedProcedures);
        }

        public Model.StoredProcedure[] StoredProcedures
        {
            get { return _scriptObjects.ToArray(); }
        }

        public void FillStoredProcedureColumns(Model.StoredProcedure storedProcedure)
        {
            IStoredProcedure dal = DALFactory.DataAccess.CreateStoredProcedure(DalAssemblyName, ConnectionString);
            dal.FillStoredProcedureColumns(storedProcedure);
        }

        private static void InitialCreateFilters(IList<Model.StoredProcedure> storedProcedures)
        {
            foreach (Model.StoredProcedure storedProcedure in storedProcedures)
            {
                // Get All Filter
                Filter filter = new Filter(storedProcedure.Name, false, storedProcedure, true, false, false, "", null);
                storedProcedure.AddFilter(filter);
            }
        }
    }
}