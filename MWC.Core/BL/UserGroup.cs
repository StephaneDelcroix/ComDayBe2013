//
// UserGroup.cs
//
// Author:
//       Stephane Delcroix <stephane@delcroix.org>
//
// Copyright (c) 2013 S. Delcroix
//
using System.Xml.Serialization;
using System;

namespace MWC.BL
{
	/// <summary>
	/// Represents an Exhibitor at the conference.
	/// </summary>
	public partial class UserGroup : Contracts.BusinessEntityBase
	{
		[XmlAttribute("n")]
		public virtual string Name { get; set; }
		[XmlElement("o")]
		public virtual string Overview { get; set; }
		[XmlAttribute("u")]
		public virtual string Url { get; set; }
		[XmlAttribute("i")]
		public string ImageUrl { get; set; }

		public UserGroup ()
		{
		}

		/// <summary>
		/// Used in the UI to build 'fast scrolling'/'index scrolling'
		/// </summary>
		public string Index {
			get {
				return IsCapitalLetter(Name[0]) ? Name[0].ToString().ToUpper() : "1";
			}
		}
		bool IsCapitalLetter(char startsWith)
		{
			return ((startsWith >= 'A') && (startsWith <= 'Z'))
				|| ((startsWith >= 'a') && (startsWith <= 'z'));
		}
	}
}

