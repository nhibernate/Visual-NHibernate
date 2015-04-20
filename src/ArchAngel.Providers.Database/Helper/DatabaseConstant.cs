namespace ArchAngel.Providers.Database.Helper
{
    public sealed class DatabaseConstant
    {
        public sealed class IndexType
        {
            private IndexType()
            {
            }

            public static string Unique
            {
                get { return "Unique"; }
            }

            public static string Check
            {
                get { return "Check"; }
            }

            public static string None
            {
                get { return "None"; }
            }

            public static string ForeignKey
            {
                get { return "ForeignKey"; }
            }

            public static string PrimaryKey
            {
                get { return "PrimaryKey"; }
            }
        }

        public sealed class KeyType
        {
            private KeyType()
            {
            }

            public static string Primary
            {
                get { return "Primary"; }
            }

            public static string Foreign
            {
                get { return "Foreign"; }
            }

            public static string Unique
            {
                get { return "Unique"; }
            }

            public static string None
            {
                get { return "None"; }
            }
        }
    }
}
