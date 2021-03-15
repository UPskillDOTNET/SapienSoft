using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SuperMammoth.Models
{
	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class AreaDataPoints
	{
		public AreaDataPoints(int x, double y)
		{
			this.X = x;
			this.Y = y;
			
		}
		//Trying to add a label atribute
		[DataMember(Name = "x")]
		public Nullable<int> X = null;


        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
		public Nullable<double> Y = null;

		
	}
}

