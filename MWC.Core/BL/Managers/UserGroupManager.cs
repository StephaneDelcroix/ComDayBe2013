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
		
		internal static void UpdateUserGroupData(IList<UserGroup> usergroups)
		{
			DAL.DataManager.DeleteUserGroups();
			DAL.DataManager.SaveUserGroups (usergroups); //SAL.MwcSiteParser.GetExhibitors ());
		}

		public static IList<UserGroup> GetUserGroups ()
		{
            var iusergroups = DAL.DataManager.GetUserGroups();
            return iusergroups.ToList(); 
		}
		
		public static UserGroup GetUserGroup (int usergroupID)
		{
			return DAL.DataManager.GetUserGroup (usergroupID);
		}

        public static UserGroup GetUserGroupWithName (string usergroupName)
        {
			return DAL.DataManager.GetUserGroupWithName (usergroupName);
        }
	}
}

