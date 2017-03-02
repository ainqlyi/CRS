namespace RestfulGeoCoder.GISObjects.Request
{
    /// <summary>
    /// Defines the properties for a record's attributes.
    /// </summary>
    public class Attributes
    {
        public int OBJECTID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
    }
}