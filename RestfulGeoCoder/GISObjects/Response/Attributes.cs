namespace RestfulGeoCoder.GISObjects.Response
{
    /// <summary>
    /// Attributes object to include in json response REST call to ArcGIS Server
    /// </summary>
    public class Attributes
    {
        public int ResultID { get; set; }
        public string Loc_name { get; set; }
        public string Status { get; set; }
        public float Score { get; set; }
        public string Match_addr { get; set; }
        public string Addr_type { get; set; }
        public string Type { get; set; }
        public string PlaceName { get; set; }
        public string Place_addr { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        public string Rank { get; set; }
        public string AddBldg { get; set; }
        public string AddNum { get; set; }
        public string AddNumFrom { get; set; }
        public string AddNumTo { get; set; }
        public string Side { get; set; }
        public string StPreDir { get; set; }
        public string StPreType { get; set; }
        public string StName { get; set; }
        public string StType { get; set; }
        public string StDir { get; set; }
        public string StAddr { get; set; }
        public string Nbrhd { get; set; }
        public string City { get; set; }
        public string Subregion { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
        public string PostalExt { get; set; }
        public string Country { get; set; }
        public string LangCode { get; set; }
        public int Distance { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double DisplayX { get; set; }
        public double DisplayY { get; set; }
        public double Xmin { get; set; }
        public double Xmax { get; set; }
        public double Ymin { get; set; }
        public double Ymax { get; set; }
    }
}