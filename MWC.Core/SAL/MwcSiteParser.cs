using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using MWC.BL;

namespace MWC.SAL
{
	public class MWCSiteParser
	{
		static MWCSiteParser ()
		{
		}
		public MWCSiteParser ()
		{
		}
		public Conference ConferenceData { get; set; }
		public List<UserGroup> UserGroups { get; set; }

		public void GetConference (string url, Action action)
		{			
			var webClient = new WebClient ();
			Debug.WriteLine ("Get remote data for conference");
			webClient.DownloadStringCompleted += (sender, e) =>
			{
				try 
				{
					var r = e.Result;
					ConferenceData = DeserializeConference (r);
				} catch (Exception ex) {
					Debug.WriteLine ("ERROR deserializing downloaded conference XML: " + ex);
				}
				action();
			};
			webClient.Encoding = System.Text.Encoding.UTF8;
			webClient.DownloadStringAsync (new Uri (url));
		}

		public void GetUserGroups (string url, Action action)
		{			
			var webClient = new WebClient ();
			Debug.WriteLine ("Get remote data for usergroups");
			webClient.DownloadStringCompleted += (sender, e) =>
			{
				try 
				{
					var r = e.Result;
                    UserGroups = DeserializeUserGroups (r);
				} catch (Exception ex) {
					Debug.WriteLine ("ERROR deserializing downloaded usergroups XML: " + ex);
				}
				action();
			};
			webClient.Encoding = System.Text.Encoding.UTF8;
			webClient.DownloadStringAsync (new Uri (url));
		}
		
		internal static Conference DeserializeConference (string xml)
		{
			Conference confData = null;
			try {
				var serializer = new XmlSerializer(typeof(Conference));
				var sr = new StringReader(xml);
				confData = (Conference)serializer.Deserialize(sr);
			} catch (Exception ex) {
				//try again, but skip the BOM
				try {
					var serializer = new XmlSerializer(typeof(Conference));
					var sr = new StringReader(xml);
					sr.Read ();//Skip BOM
					confData = (Conference)serializer.Deserialize(sr);
				} catch (Exception ex2) {
					Debug.WriteLine ("ERROR deserializing downloaded conference XML: " + ex2);
				}
			}
			return confData;
		}
		
		internal static List<UserGroup> DeserializeUserGroups (string xml)
		{
            Conference confData = null;
			try {
                var serializer = new XmlSerializer (typeof (Conference));
				var sr = new StringReader(xml);
                confData = (Conference)serializer.Deserialize (sr);
			} catch (Exception ex) {
				//try again, but skip the BOM
				try { 
					var serializer = new XmlSerializer (typeof (Conference));
					var sr = new StringReader(xml);
					sr.Read (); //Skip the BOM
					confData = (Conference)serializer.Deserialize (sr);
				} catch (Exception ex2) {
					Debug.WriteLine ("ERROR deserializing downloaded usergroups XML: " + ex2);
				}
			}
			return confData.UserGroups;
		}
	}
}