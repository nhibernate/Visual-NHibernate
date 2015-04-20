using System;

namespace Slyce.IntelliMerge.Controller
{
    /// <summary>
    /// Represents which elements are missing from a MapInfoType object.
    /// </summary>
    [Flags]
    public enum MissingObject
    {
        /// <summary>
        /// No objects are missing.
        /// </summary>
        None = 0,
        /// <summary>
        /// The template object is missing.
        /// </summary>
        NewGen = 1,
        /// <summary>
        /// The user object is missing.
        /// </summary>
        User = 2,
        /// <summary>
        /// The prevgen object is missing.
        /// </summary>
        PrevGen = 4,
        /// <summary>
        /// The template and prevgen objects are missing.
        /// </summary>
        NewGenAndPrevGen = NewGen | PrevGen,
        /// <summary>
        /// The template and user objects are missing.
        /// </summary>
        NewGenAndUser = NewGen | User,
        /// <summary>
        /// All of the objects are missing.
        /// </summary>
        All = NewGen | User | PrevGen
    }
}