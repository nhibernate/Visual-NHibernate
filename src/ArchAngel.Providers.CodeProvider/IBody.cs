namespace ArchAngel.Providers.CodeProvider
{
    public interface IBody
    {
        /// <summary>
        /// The body text of the construct. Does not include child nodes, and should only be
        /// used on leaf nodes.
        /// </summary>
        string BodyText { get; set; }
    }
}