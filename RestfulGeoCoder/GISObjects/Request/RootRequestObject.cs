using System.Collections.Generic;

namespace RestfulGeoCoder.GISObjects.Request
{
    /// <summary>
    /// Defines the root object which contains all records.
    /// </summary>
    public class RootRequestObject
    {
        public List<Record> records = new List<Record>();
    }
}