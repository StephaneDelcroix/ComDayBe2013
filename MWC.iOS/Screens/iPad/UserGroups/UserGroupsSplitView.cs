using System;
using System.Drawing;
using MonoTouch.UIKit;
using MWC.iOS.Screens.iPhone.UserGroups;

namespace MWC.iOS.Screens.iPad.UserGroups
{
	public class UserGroupsSplitView : UISplitViewController
	{
		UserGroupsScreen _usergroupsList;
		UserGroupDetailsScreen _usergroupDetails;
		
		public UserGroupsSplitView ()
		{
			//View.Bounds = new RectangleF(0,0,UIScreen.MainScreen.Bounds.Width,UIScreen.MainScreen.Bounds.Height);
			Delegate = new SplitViewDelegate();
			
			_usergroupsList = new UserGroupsScreen(this);
			_usergroupDetails = new UserGroupDetailsScreen(-1);
			
			this.ViewControllers = new UIViewController[]
				{_usergroupsList, _usergroupDetails};
		}
		
		public void ShowUserGroup (int exhibitorID, UIViewController usergroupView)
		{
			this.ViewControllers = new UIViewController[]
				{_usergroupsList, usergroupView};

		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
	}

 	public class SplitViewDelegate : UISplitViewControllerDelegate
    {
		public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
		{
			return false;
		}
	}
}