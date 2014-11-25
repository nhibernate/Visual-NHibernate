using System;
using System.Collections.Generic;
// References to PetShop2005 specific libraries
using PetShop2005.Model;
using PetShop2005.SQLServerDAL;

namespace PetShop2005.BLL
{
    /// <summary>
    /// A business component used to manage AlphabeticalListOfProducts
    /// The PetShop2005.Model.AlphabeticalListOfProduct is used to store
    /// serializable information about a specific AlphabeticalListOfProduct
    /// </summary>
    public sealed partial class AlphabeticalListOfProduct
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        private AlphabeticalListOfProduct()
        {
        }
        
        // Get an instance of the AlphabeticalListOfProduct DAL using the DALFactory
        // Making this static will cache the DAL instance after the initial load
		private readonly static SQLServerDAL.AlphabeticalListOfProduct dal = new SQLServerDAL.AlphabeticalListOfProduct();

        /// <summary>
        /// Get a list of AlphabeticalListOfProducts
        /// </summary>
        /// <returns>Arraylist of AlphabeticalListOfProducts</returns>
        public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts()
        {
            // Run a search against the data store
            return dal.GetAlphabeticalListOfProducts();
        }
		
        /// <summary>
        /// Get a list of sorted AlphabeticalListOfProducts
        /// </summary>
        /// <returns>Arraylist of AlphabeticalListOfProducts</returns>
        public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProductsSorted(string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression)) 
            {
                return GetAlphabeticalListOfProducts();
            }
		
            System.ComponentModel.ListSortDirection listSortDirection = System.ComponentModel.ListSortDirection.Ascending;
            string[] sortExpressionParams = sortExpression.Split(' ');
            string sortFieldName = sortExpressionParams[0];
            if (sortExpressionParams.Length > 1) 
            {
                string direction = sortExpressionParams[1];
                if (direction.ToUpper() == "DESC") 
                {
                    listSortDirection = System.ComponentModel.ListSortDirection.Descending;
                }
            }
            // Run a search against the data store
            List<AlphabeticalListOfProductInfo> alphabeticalListOfProducts = (List<AlphabeticalListOfProductInfo>) dal.GetAlphabeticalListOfProducts();
		
            Model.SortComparer<AlphabeticalListOfProductInfo> comparer = new Model.SortComparer<AlphabeticalListOfProductInfo>(sortFieldName, listSortDirection);
            alphabeticalListOfProducts.Sort(comparer);
        
            return (IList<AlphabeticalListOfProductInfo>) alphabeticalListOfProducts;
        }
		
        /// <summary>
        /// Get a list of AlphabeticalListOfProducts
        /// </summary>
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>Arraylist of AlphabeticalListOfProducts</returns>
        public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProducts(System.Data.SqlClient.SqlTransaction trans)
        {
            // Run a search against the data store
            return dal.GetAlphabeticalListOfProducts(trans);
        }
		
        /// <summary>
        /// Get a list of sorted AlphabeticalListOfProducts
        /// </summary>
        /// <param name="trans">Transaction to run select within</param>
        /// <returns>Arraylist of AlphabeticalListOfProducts</returns>
        public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProductsSorted(string sortExpression, System.Data.SqlClient.SqlTransaction trans)
        {
            if (string.IsNullOrEmpty(sortExpression)) 
            {
                return GetAlphabeticalListOfProducts(trans);
            }
        
            System.ComponentModel.ListSortDirection listSortDirection = System.ComponentModel.ListSortDirection.Ascending;
            string[] sortExpressionParams = sortExpression.Split(' ');
            string sortFieldName = sortExpressionParams[0];
            if (sortExpressionParams.Length > 1) 
            {
                string direction = sortExpressionParams[1];
                if (direction.ToUpper() == "DESC")
                {
                    listSortDirection = System.ComponentModel.ListSortDirection.Descending;
                }
            }
            // Run a search against the data store
            List<AlphabeticalListOfProductInfo> alphabeticalListOfProducts = (List<AlphabeticalListOfProductInfo>) dal.GetAlphabeticalListOfProducts(trans);
        
            Model.SortComparer<AlphabeticalListOfProductInfo> comparer = new Model.SortComparer<AlphabeticalListOfProductInfo>(sortFieldName, listSortDirection);
            alphabeticalListOfProducts.Sort(comparer);
        
            return (IList<AlphabeticalListOfProductInfo>) alphabeticalListOfProducts;
        }
		
        /// <summary>
        /// Get a filted list of AlphabeticalListOfProducts
        /// </summary>
        /// <param name="fieldName">Database Field to filter on</param>
        /// <param name="operatorValue">SQL boolean operator (like, =, <, >, <>, >=, <=)</param>
        /// <param name="fieldValue">Data to search for</param>
        /// <returns>Arraylist of AlphabeticalListOfProducts</returns>
        public static IList<AlphabeticalListOfProductInfo> GetAlphabeticalListOfProductsByFilter(string fieldName, string operatorValue, string fieldValue, string sortExpression) 
        {
            // Return new if the string is empty
            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(operatorValue) || string.IsNullOrEmpty(fieldValue)) 
            {
                return GetAlphabeticalListOfProductsSorted(sortExpression);
            }
            
            if (string.IsNullOrEmpty(sortExpression)) 
            {
                return dal.GetAlphabeticalListOfProductsByFilter(fieldName, operatorValue, fieldValue);
            }
            
            System.ComponentModel.ListSortDirection listSortDirection = System.ComponentModel.ListSortDirection.Ascending;
            string[] sortExpressionParams = sortExpression.Split(' ');
            string sortFieldName = sortExpressionParams[0];
            if (sortExpressionParams.Length > 1) 
            {
                string direction = sortExpressionParams[1];
                if (direction.ToUpper() == "DESC")
                {
                    listSortDirection = System.ComponentModel.ListSortDirection.Descending;
                }
            }
            // Run a search against the data store
            List<AlphabeticalListOfProductInfo> alphabeticalListOfProducts = (List<AlphabeticalListOfProductInfo>) dal.GetAlphabeticalListOfProductsByFilter(fieldName, operatorValue, fieldValue);
            
            Model.SortComparer<AlphabeticalListOfProductInfo> comparer = new Model.SortComparer<AlphabeticalListOfProductInfo>(sortFieldName, listSortDirection);
            alphabeticalListOfProducts.Sort(comparer);
            
            return (IList<AlphabeticalListOfProductInfo>) alphabeticalListOfProducts;
        }
    }
}