using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRSGeoCoder.Model
{
	public class AddressModel
	{
		public int ID { get; set; }
		public string Address { get; set; }
		public string Zip { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
		public string City { get; set; }
		public string State { get; set; }
	}
}
