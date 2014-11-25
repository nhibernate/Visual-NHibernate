namespace ArchAngel.Providers.Database.AccessDAL
{
    public class AccessBase
    {
        private readonly string _fileName;

        public string FileName
        {
            get { return _fileName; }
        }

        public AccessBase(string fileName)
        {
            _fileName = fileName;
        }
    }
}
