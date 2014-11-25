using System.Text;

namespace ArchAngel.Providers.Database.Controls.FilterWizard
{
    public partial class ucFilterSummary : Interfaces.Controls.ContentItems.ContentItem
    {
    	readonly FormFilter2 Owner;

        public ucFilterSummary(FormFilter2 owner)
        {
            InitializeComponent();
            HasNext = false;
            HasPrev = true;
            HasFinish = true;
            PageHeader = "Filter Summary";
            PageDescription = "Check that all the information is correct.";
            Owner = owner;
        }

        public override bool Back()
        {
            return true;
        }

        public override void OnDisplaying()
        {
            StringBuilder sbFilterColumns = new StringBuilder(1000);
            StringBuilder sbOrderByColumns = new StringBuilder(1000);

            foreach (Model.Filter.FilterColumn filterColumn in Owner.TheFilter.FilterColumns)
            {
                sbFilterColumns.AppendFormat(@"
                    <tr><td>Alias: '{0}', Column: {1} {2} {3}</td></tr>",
                                                                                     filterColumn.Alias,
                                                                                     filterColumn.Column == null ? "NULL" : string.Format("{0} [{1}]", filterColumn.Column.Alias, filterColumn.Column.Name),
                                                                                     filterColumn.LogicalOperator,
                                                                                     filterColumn.CompareOperator);
            }
            foreach (Model.Filter.OrderByColumn orderByColumn in Owner.TheFilter.OrderByColumns)
            {
                sbOrderByColumns.AppendFormat(@"
                    <tr><td>Column [{0}] {1}</td></tr>",
                                                                     orderByColumn.Column == null ? "NULL" : orderByColumn.Column.Alias,
                                                                     orderByColumn.SortOperator);
            }
            StringBuilder sb = new StringBuilder(3000);

            sb.AppendFormat(@"
            <html>
            <style>
                *
                {{
                    font-family:Verdana,Arial;
                    font-size:xx-small;
                    background-color:AliceBlue;
                    padding:0;
                    margin:0;
                }}
                h2
                {{
                    font-size:x-small;
                    background-color:LightSteelBlue;
                }}
            </style>
            <body>
                <h2>Summary of Filter</h2>
                <table>
                    <tr><td style=""font-weight:bold;"">Name:</td><td>{0}</td></tr>
                    <tr><td style=""font-weight:bold;"">Alias:</td><td>{1}</td></tr>
                    <tr><td style=""font-weight:bold;"">Returns:</td><td>{6}</td></tr>
                    <tr><td style=""font-weight:bold;"">{4} Filter Columns:</td><td><table border=""1"">{2}</table></td></tr>
                    <tr><td style=""font-weight:bold;"">{5} Order By Columns:</td><td><table border=""1"">{3}</table></td></tr>
                </table>
            </body>
            </html>", 
                    Owner.TheFilter.Name, 
                    Owner.TheFilter.Alias, 
                    sbFilterColumns, 
                    sbOrderByColumns, 
                    Owner.TheFilter.FilterColumns.Length, 
                    Owner.TheFilter.OrderByColumns.Length, 
                    Owner.TheFilter.IsReturnTypeCollection ? "Collection" : "Single Result" );
            webBrowser1.DocumentText = sb.ToString();
        }
    }
}
