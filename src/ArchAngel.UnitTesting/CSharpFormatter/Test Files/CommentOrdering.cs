
namespace SerkoOnline.DataAccess.IDAL
{
    /// <summary>
    /// Interface for the Consultant Data Access Layer
	/// </summary>
    public interface IConsultant
    {
        /// <returns>Arraylist of Consultants</returns>
        /// <summary>
        /// Get a list of Consultants
		/// </summary>
        IList<ConsultantInfo> GetConsultants();
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>Arraylist of Consultants</returns>
        /// <summary>
        /// Get a list of Consultants
		/// </summary>
        IList<ConsultantInfo> GetConsultants(System.Data.SqlClient.SqlTransaction trans);
        /// <param name="consultant">Unique Constraint</param>
        /// <returns>ConsultantInfo</returns>
        /// <summary>
        /// Get a specific Consultant
		/// </summary>
        ConsultantInfo GetConsultant(string consultant);
        /// <param name="consultant">Unique Constraint</param>
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>ConsultantInfo</returns>
        /// <summary>
        /// Get a specific Consultant
		/// </summary>
        ConsultantInfo GetConsultant(string consultant, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="consultantCode">Unique Constraint</param>
        /// <returns>ConsultantInfo</returns>
        /// <summary>
        /// Get a specific Consultant
		/// </summary>
        ConsultantInfo GetConsultantByID(int cD_lConsultant_Code);
        /// <param name="consultantCode">Unique Constraint</param>
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>ConsultantInfo</returns>
        /// <summary>
        /// Get a specific Consultant
		/// </summary>
        ConsultantInfo GetConsultantByID(int cD_lConsultant_Code, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="consultantDesc">Constraint for a Consultant</param>
        /// <returns>Arraylist of Consultants</returns>
        /// <summary>
        /// Get a list of Consultants
		/// </summary>
        IList<ConsultantInfo> GetConsultantsByDescription(string cD_sConsultant_Desc);
        /// <param name="consultantDesc">Constraint for a Consultant</param>
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>Arraylist of Consultants</returns>
        /// <summary>
        /// Get a list of Consultants
		/// </summary>
        IList<ConsultantInfo> GetConsultantsByDescription(string cD_sConsultant_Desc, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="fieldName">Database Field to filter on</param>
        /// <param name="fieldValue">Data to search for</param>
        /// <param name="operatorValue">SQL boolean operator (like, =, <, >, <>, >=, <=)</param>
        /// <returns>Consultant ArrayList</returns>
        /// <summary>
        /// Get a filtered collection of Consultants
		/// </summary>
        IList<ConsultantInfo> GetConsultantsByFilter(string fieldName, string operatorValue, string fieldValue);
        /// <param name="consultant">Consultant to which Consultant_ConsultantGroup are added</param>
        /// <summary>
        /// Fill Consultant_ConsultantGroup related to a specific Consultant
		/// </summary>
        void FillConsultant_ConsultantGroup(ConsultantInfo consultant);
        /// <param name="consultant">ConsultantInfo</param>
        /// <summary>
        /// Insert a new Consultant into the database
		/// </summary>
        void Insert(ConsultantInfo consultant);
        /// <param name="consultant">ConsultantInfo</param>
        /// <summary>
        /// Update a specific Consultant in the database
        /// </summary>
        void Update(ConsultantInfo consultant);
        /// <param name="consultant">ConsultantInfo</param>
        /// <summary>
        /// Delete a specific Consultant from the database
        /// </summary>
        void Delete(ConsultantInfo consultant);
        /// <param name="consultant">ConsultantInfo</param>
        /// <param name="trans">Transaction to run insert within</param>
        /// <summary>
        /// Insert a new Consultant into the database
		/// </summary>
        void Insert(ConsultantInfo consultant, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="consultant">ConsultantInfo</param>
        /// <param name="trans">Transaction to run update within</param>
        /// <summary>
        /// Update a specific Consultant in the database
		/// </summary>
        void Update(ConsultantInfo consultant, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="consultant">ConsultantInfo</param>
        /// <param name="trans">Transaction to run update within</param>
        /// <summary>
        /// Delete a specific Consultant from the database
        /// </summary>
        void Delete(ConsultantInfo consultant, System.Data.SqlClient.SqlTransaction trans);
        /// <param name="customBulkUpdateCallBack">Call back function to execute</param>
        /// <summary>
        /// Bulk Insert/Update/Delete
		/// </summary>
        void BulkUpdate(ConsultantInfo.CustomBulkUpdateCallBack customBulkUpdateCallBack);
        /// <param name="modifiedConsultants">Modified Collection of Consultants</param>
        /// <param name="originalConsultants">Original Collection of Consultants</param>
        /// <summary>
        /// Bulk Insert/Update/Delete
        /// </summary>
        void BulkUpdate(IList<ConsultantInfo> originalConsultants, IList<ConsultantInfo> modifiedConsultants);
    }
}