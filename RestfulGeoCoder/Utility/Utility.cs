using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using RestfulGeoCoder.GISObjects.Response;
using Location = RestfulGeoCoder.GISObjects.Response.Location;

namespace RestfulGeoCoder.Utility
{
    /// <summary>
    /// Contains helpful methods for everyday tasks
    /// </summary>
    public class Utility
    {
        private const string UsZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
        private const string CaZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
        private const String States = "|AL|AK|AS|AZ|AR|CA|CO|CT|DE|DC|FM|FL|GA|GU|HI|ID|IL|IN|IA|KS|KY|LA|ME|MH|MD|MA|MI|MN|MS|MO|MT|NE|NV|NH|NJ|NM|NY|NC|ND|MP|OH|OK|OR|PW|PA|PR|RI|SC|SD|TN|TX|UT|VT|VI|VA|WA|WV|WI|WY|";

        /// <summary>
        /// Converts the RootResponse object from the REST call into a DataTable
        /// </summary>
        /// <typeparam name="T">The type of object to convert</typeparam>
        /// <param name="rootResponseObject">The object to convert</param>
        /// <returns>The DataTable</returns>
        public static DataTable ConvertRootResponseToDataTable<T>(RootResponseObject rootResponseObject)
        {
            DataTable responseTable = new DataTable();
            responseTable.Columns.Add("EID");
            responseTable.Columns.Add("PointX");
            responseTable.Columns.Add("PointY");
            responseTable.Columns.Add("score");
            responseTable.Columns.Add("address");
            responseTable.Columns.Add("Loc_name");
            responseTable.Columns.Add("Status");
            responseTable.Columns.Add("Match_Addr");
            responseTable.Columns.Add("Addr_Type");
            responseTable.Columns.Add("PlaceName");
            responseTable.Columns.Add("Place_addr");
            responseTable.Columns.Add("Rank");
            responseTable.Columns.Add("AddBldg");
            responseTable.Columns.Add("AddNum");
            responseTable.Columns.Add("AddNumFrom");
            responseTable.Columns.Add("AddNumTo");
            responseTable.Columns.Add("Side");
            responseTable.Columns.Add("StPreDir");
            responseTable.Columns.Add("StPreType");
            responseTable.Columns.Add("StName");
            responseTable.Columns.Add("StType");
            responseTable.Columns.Add("StDir");
            responseTable.Columns.Add("StAddr");
            responseTable.Columns.Add("Nbrhd");
            responseTable.Columns.Add("City");
            responseTable.Columns.Add("Subregion");
            responseTable.Columns.Add("Region");
            responseTable.Columns.Add("Postal");
            responseTable.Columns.Add("PostalExt");
            responseTable.Columns.Add("Country");
            responseTable.Columns.Add("LangCode");
            responseTable.Columns.Add("Distance");
            responseTable.Columns.Add("X");
            responseTable.Columns.Add("Y");
            responseTable.Columns.Add("DisplayX");
            responseTable.Columns.Add("DisplayY");
            responseTable.Columns.Add("Xmin");
            responseTable.Columns.Add("Xmax");
            responseTable.Columns.Add("Ymin");
            responseTable.Columns.Add("Ymax");

            foreach (Location location in rootResponseObject.locations)
            {
                if ((location.location.x > 0 || location.location.x < 0) && (location.location.y > 0 || location.location.y < 0))
                {
                    responseTable.Rows.Add(new object[]
                    {
                        location.attributes.ResultID, 
                        location.location.x, 
                        location.location.y,
                        location.score,
                        location.address,
                        location.attributes.Loc_name,
                        location.attributes.Status,
                        location.attributes.Match_addr,
                        location.attributes.Addr_type,
                        location.attributes.PlaceName,
                        location.attributes.Place_addr,
                        location.attributes.Rank,
                        location.attributes.AddBldg,
                        location.attributes.AddNum,
                        location.attributes.AddNumFrom,
                        location.attributes.AddNumTo,
                        location.attributes.Side,
                        location.attributes.StPreDir,
                        location.attributes.StPreType,
                        location.attributes.StName,
                        location.attributes.StType,
                        location.attributes.StDir,
                        location.attributes.StAddr,
                        location.attributes.Nbrhd,
                        location.attributes.City,
                        location.attributes.Subregion,
                        location.attributes.Region,
                        location.attributes.Postal,
                        location.attributes.PostalExt,
                        location.attributes.Country,
                        location.attributes.LangCode,
                        location.attributes.Distance,
                        location.attributes.X,
                        location.attributes.Y,
                        location.attributes.DisplayX,
                        location.attributes.DisplayY,
                        location.attributes.Xmin,
                        location.attributes.Xmax,
                        location.attributes.Ymin,
                        location.attributes.Ymax
                    });
                }
            }

            return responseTable;
        }

        /// <summary>
        /// Determines whether a street address is formatted correctly using a regular expression
        /// </summary>
        /// <param name="address">the address to validate</param>
        /// <returns>true or false</returns>
        public static bool IsValidStreetAddress(string address)
        {
            const string exp = @"\d{1,3}.?\d{0,3}\s[a-zA-Z]{2,30}\s[a-zA-Z]{2,15}";
            var regex = new Regex(exp);
            return regex.IsMatch(address);
        }

        /// <summary>
        /// Validates that a given string is a known US state abbreviation
        /// </summary>
        /// <param name="state">The string to validate against</param>
        /// <returns>True or false</returns>
        public static bool IsStateAbbreviation(String state)
        {
            if (state.ToUpper() == "MARYLAND")
            {
                return true;
            }
            if (state.Length == 2 && States.IndexOf(state, StringComparison.Ordinal) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates zip code for US or Canadian addresses
        /// </summary>
        /// <param name="zipCode">The zip code to validate</param>
        /// <returns>True or false</returns>
        public static bool IsUsorCanadianZipCode(string zipCode)
        {
            bool validZipCode = Regex.Match(zipCode, UsZipRegEx).Success && (!Regex.Match(zipCode, CaZipRegEx).Success);
            if (zipCode == "00000")
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        /// <summary>
        /// Converts a generic list to a DataTable.
        /// </summary>
        /// <typeparam name="T">The index of objects</typeparam>
        /// <param name="data">The list of data</param>
        /// <returns>The DataTable</returns>
        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];

            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }

            return table;
        }
    }
}