using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MWC.BL.Managers {
	/// <summary>
	/// Central point for triggering data update from server
	/// to our local SQLite db
	/// </summary>
	public static class UpdateManager {
		private static object locker = new object();
		
		public static event EventHandler UpdateStarted = delegate {};
		public static event EventHandler UpdateFinished = delegate {};
		
		public static event EventHandler UpdateUserGroupsStarted = delegate {};
		public static event EventHandler UpdateUserGroupsFinished = delegate {};

		/// <summary>
		/// Gets or sets a value indicating whether the data is updating.
		/// </summary>
		/// <value>
		/// <c>true</c> if the data is updating; otherwise, <c>false</c>.
		/// </value>
		public static bool IsUpdating
		{
			get { return isUpdating; }
			set { isUpdating = value; }
		}
		public static bool IsUpdatingUserGroups
		{
			get { return isUpdatingUserGroups; }
			set { isUpdatingUserGroups = value; }
		}
		private static bool isUpdatingUserGroups = false;
		private static bool isUpdating = false;
		
		static UpdateManager ()
		{
		}
		
		/// <summary>
		/// Console.WriteLines only emitted in DEBUG mode, so they won't occur
		/// on a real device in the Distribution version
		/// </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] arg)
		{
			Console.WriteLine(format, arg);
		}
		
		public static bool HasDataAlready {
			get {
				return MWC.DL.MwcDatabase.CountTable<Session>() > 0;
			}
		}

		public static void UpdateFromFile(string xmlString)
		{
			WriteLine ("### Updating all data from local file");

			// make this a critical section to ensure that access is serial
			lock (locker) {
				isUpdating = true;
				UpdateStarted (null, EventArgs.Empty);
				var ea = new UpdateFinishedEventArgs (UpdateType.SeedData, false);
				
				var c = MWC.SAL.MWCSiteParser.DeserializeConference (xmlString);
				if (c != null) {
					if (SaveToDatabase (c)) {
						ea.Success = true;
					}
				}
				UpdateFinished (null, ea);
				isUpdating = false;
			}
		}
		/// <summary>
		/// Updates all conference data from the cloud. Sets UpdateManager.IsUpdating
		/// to true while updating. serialized, thread-safe access.
		/// (although, the webclient request is async...)
		/// </summary
		public static void UpdateConference()
		{
			WriteLine ("### Updating all data from cloud; _isUpdating = true");
				
			// make this a critical section to ensure that access is serial
			lock (locker) {
				isUpdating = true;
				UpdateStarted (null, EventArgs.Empty);
				var ea = new UpdateFinishedEventArgs (UpdateType.Conference, false);

				var siteParser = new MWC.SAL.MWCSiteParser();
				siteParser.GetConference (Constants.ConferenceDataUrl,  
					delegate  {
						var c = siteParser.ConferenceData;

						if (c == null) {
							WriteLine ("xxx No conference data downloaded, skipping");
						} else {
							if (SaveToDatabase (c)) {
								ea.Success = true;
							}
						}
						UpdateFinished (null, ea);
						isUpdating = false;
					}
				);
			}
		}

		public static void UpdateUserGroups()
		{
			WriteLine ("### Updating usergroups data from cloud; _isUpdating = true");
				
			// make this a critical section to ensure that access is serial
			lock (locker) {
				isUpdatingUserGroups = true;
				UpdateUserGroupsStarted (null, EventArgs.Empty);
				var ea = new UpdateFinishedEventArgs (UpdateType.Exhibitors, false);

				var siteParser = new MWC.SAL.MWCSiteParser();
				siteParser.GetUserGroups (Constants.UserGroupsDataUrl,  
					delegate {
						var c = siteParser.UserGroups;

						if (c == null) {
							WriteLine ("xxx No conference data downloaded, skipping");
						} else {
							if (SaveToDatabase (c)) {
								ea.Success = true;
							}
						}
						UpdateUserGroupsFinished (null, ea);
						isUpdatingUserGroups = false;
					}
				);
			}
		}

		static bool SaveToDatabase(Conference c)
		{
			bool success = false;
			try  {
				WriteLine ("yyy SAVING new conference data to sqlite");
			
				if (c.Speakers.Count > 0) {
					DAL.DataManager.DeleteSpeakers ();
					DAL.DataManager.SaveSpeakers (c.Speakers);
				}
				if (c.Sessions.Count > 0) {
					DAL.DataManager.DeleteSessions ();
					DAL.DataManager.DeleteSessionSpeakers ();
					var speakers = SessionManager.GenerateKeysAndSpeakers (c.Sessions);
					DAL.DataManager.SaveSessions (c.Sessions);
					DAL.DataManager.SaveSessionSpeakers (speakers);
				}
				//if (c.Exhibitors.Count > 0) {
				//	DAL.DataManager.DeleteExhibitors ();
				//	DAL.DataManager.SaveExhibitors (c.Exhibitors);	
				//}
				if (c.UserGroups.Count > 0) {
					DAL.DataManager.DeleteUserGroups ();
					DAL.DataManager.SaveUserGroups (c.UserGroups);
				}
				success = true;
			} catch (Exception ex) {
				WriteLine ("xxx SAVING conference to sqlite failed " + ex.Message);
			}
			return success;
		}
		static bool SaveToDatabase(List<UserGroup> usergroups)
		{
			bool success = false;
			try  {
				WriteLine ("yyy SAVING new usergroups data to sqlite");
				if (usergroups.Count > 0) {
					DAL.DataManager.DeleteUserGroups ();
					DAL.DataManager.SaveUserGroups (usergroups);	
				}
				success = true;
			} catch (Exception) {
				WriteLine ("xxx SAVING usergroups to sqlite failed");
			}
			return success;
		}
	}
}