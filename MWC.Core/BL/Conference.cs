using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace MWC.BL
{
	public class Conference
	{
		public Conference ()
		{
			Speakers = new List<Speaker>();
			Sessions = new List<Session>();
			UserGroups = new List<UserGroup>();
		}

		[XmlElement("sp")]
		public List<Speaker> Speakers { get; set; }
		[XmlElement("se")]
		public List<Session> Sessions { get; set; }
		//[XmlElement("ex")]
		//public List<Exhibitor> Exhibitors { get; set; }
		[XmlElement("ug")]
		public List<UserGroup> UserGroups { get; set; }
	}
}