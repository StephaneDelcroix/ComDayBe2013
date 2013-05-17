using System;
using System.Collections.Generic;
using System.Linq;

namespace MWC.BL.Managers
{
	public static class UserGroupManager
	{
		static UserGroupManager ()
		{
		}
		
		internal static void UpdateExhibitorData(IList<UserGroup> exhibitors)
		{
			DAL.DataManager.DeleteUserGroups();
			DAL.DataManager.SaveUserGroups (exhibitors); //SAL.MwcSiteParser.GetExhibitors ());
		}

		public static IList<UserGroup> GetUserGroups ()
		{
            var iexhibitors = DAL.DataManager.GetUserGroups();
            return iexhibitors.ToList(); // new List<Exhibitor>();
		}
		
		public static UserGroup GetUserGroup (int exhibitorID)
		{
			return DAL.DataManager.GetUserGroup (exhibitorID);
		}

        public static UserGroup GetUserGroupWithName (string exhibitorName)
        {
			return DAL.DataManager.GetUserGroupWithName (exhibitorName);
        }
	}
}

