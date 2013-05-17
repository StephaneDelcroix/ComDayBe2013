using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MWC.BL;
using MWC.iOS.Screens.iPad.UserGroups;

namespace MWC.iOS.Screens.iPhone.UserGroups {
	/// <summary>
	/// Exhibitors screen. Derives from MonoTouch.Dialog's DialogViewController to do 
	/// the heavy lifting for table population.
	/// </summary>
	/// <remarks>
	/// This class initially inherited from UpdateManagerLoadingDialogViewController
	/// but when we split the data download into two parts, the methods from that
	/// baseclass we duplicated here (due to different eventhandlers)
	/// </remarks>
	public partial class UserGroupsScreen : DialogViewController {
		protected UserGroupDetailsScreen usergroupsDetailsScreen;
		IList<UserGroup> usergroups;
		
		/// <summary>
		/// Set pushing=true so that the UINavCtrl 'back' button is enabled
		/// </summary>
		public UserGroupsScreen () : base (UITableViewStyle.Plain, null, true)
		{
			EnableSearch = true; // requires ExhibitorElement to implement Matches()
		}
		
		UserGroupsSplitView splitView;
		public UserGroupsScreen (UserGroupsSplitView usergroupSplitView) : base (UITableViewStyle.Plain, null)
		{
			splitView = usergroupSplitView;
			EnableSearch = true; // requires ExhibitorElement to implement Matches()
		}

		/// <summary>
		/// Populates the page with exhibitors.
		/// </summary>
		protected void PopulateTable()
		{
			usergroups = BL.Managers.UserGroupManager.GetUserGroups();
			Console.WriteLine ("UserGroups count: {0}", usergroups.Count);
			Root = 	new RootElement ("User Groups") {
				from usergroup in usergroups
                    group usergroup by (usergroup.Index) into alpha
						orderby alpha.Key
						select new Section (alpha.Key) {
						from eachUserGroup in alpha
						   select (Element) new MWC.iOS.UI.CustomElements.UserGroupElement (eachUserGroup, splitView)
			}};
			// hide search until pull-down
			TableView.ScrollToRow (NSIndexPath.FromRowSection (0,0), UITableViewScrollPosition.Top, false);
		}

		public override DialogViewController.Source CreateSizingSource (bool unevenRows)
		{
			return new UserGroupsTableSource (this, usergroups);
		}

		#region UpdatemanagerLoadingDialogViewController copied here, for Exhibitor-specific behaviour
		UI.Controls.LoadingOverlay loadingOverlay;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			BL.Managers.UpdateManager.UpdateUserGroupsStarted += HandleUpdateStarted;
			BL.Managers.UpdateManager.UpdateUserGroupsFinished += HandleUpdateFinished;
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if(BL.Managers.UpdateManager.IsUpdatingUserGroups) {
				if (loadingOverlay == null) {
					loadingOverlay = new MWC.iOS.UI.Controls.LoadingOverlay (View.Frame);
					// because DialogViewController is a UITableViewController,
					// we need to step OVER the UITableView, otherwise the loadingOverlay
					// sits *in* the scrolling area of the table
					if (View.Superview != null) {
						// TODO: see when Superview is null
						View.Superview.Add (loadingOverlay); 
						View.Superview.BringSubviewToFront (loadingOverlay);
					}
				}
				ConsoleD.WriteLine("Waiting for updates to finish before displaying table.");
			} else {
				loadingOverlay = null;
				if (Root == null || Root.Count == 0) {
					ConsoleD.WriteLine("Not updating, populating table.");
					PopulateTable();
				} else ConsoleD.WriteLine ("Exhibitors already populated");
			}
		}
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			BL.Managers.UpdateManager.UpdateUserGroupsFinished -= HandleUpdateFinished; 
		}
		void HandleUpdateStarted(object sender, EventArgs e)
		{
			ConsoleD.WriteLine("Updates starting, need to create overlay.");
			InvokeOnMainThread ( () => {
				if (loadingOverlay == null) {
					loadingOverlay = new MWC.iOS.UI.Controls.LoadingOverlay (TableView.Frame);
					// because DialogViewController is a UITableViewController,
					// we need to step OVER the UITableView, otherwise the loadingOverlay
					// sits *in* the scrolling area of the table
					if (View.Superview != null) { //TODO: see when this is null
						View.Superview.Add (loadingOverlay); 
						View.Superview.BringSubviewToFront (loadingOverlay);
					}
				}
			});
		}
		void HandleUpdateFinished(object sender, EventArgs e)
		{
			ConsoleD.WriteLine("Updates finished, going to populate table.");
			InvokeOnMainThread ( () => {
				PopulateTable ();
				if (loadingOverlay != null)
					loadingOverlay.Hide ();
				loadingOverlay = null;
			});
		}
		#endregion
	}

	/// <summary>
	/// Implement index-slider down right side of tableview
	/// </summary>
	public class UserGroupsTableSource : DialogViewController.SizingSource {
		IList<UserGroup> userGroupList;
		public UserGroupsTableSource (DialogViewController dvc, IList<UserGroup> exhibitors) : base(dvc)
		{
			userGroupList = exhibitors;
		}

		public override string[] SectionIndexTitles (UITableView tableView)
		{
			var sit = from usergroup in userGroupList
                    group usergroup by (usergroup.Index) into alpha
						orderby alpha.Key
						select alpha.Key;
			return sit.ToArray();
		}

//		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			return 65f;
//		}
	}
}