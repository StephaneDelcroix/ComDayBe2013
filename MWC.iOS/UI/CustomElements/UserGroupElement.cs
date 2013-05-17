using System;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using System.Text;

namespace MWC.iOS.UI.CustomElements {
	/// <summary>
	/// Exhibitor element for MonoTouch.Dialog
	/// </summary>
	public class UserGroupElement : Element, IElementSizing {
		MWC.iOS.Screens.iPad.UserGroups.UserGroupsSplitView splitView;
	
		/// <summary>
		/// Gets or sets the exhibitor.
		/// </summary>
		/// <value>
		/// The exhibitor that is used to populate the cell.
		/// </value>
		public BL.UserGroup UserGroup {
			get { return usergroup; }
			set { usergroup = value; }
		}
		protected BL.UserGroup usergroup = null;
		
		/// <summary>
		/// Gets the reuse identifier
		/// </summary>
		protected override MonoTouch.Foundation.NSString CellKey
		{
			get { return cellKey; }
		}
		static NSString cellKey = new NSString("UserGroupCell");
		
		/// <summary>
		/// for iPhone
		/// </summary>
		public UserGroupElement (BL.UserGroup usergroup) : base ("")
		{
			this.usergroup = usergroup;
		}
		/// <summary>
		/// for iPad (SplitViewController)
		/// </summary>
		public UserGroupElement (BL.UserGroup showUserGroup, MWC.iOS.Screens.iPad.UserGroups.UserGroupsSplitView usergroupSplitView) : base ("")
		{
			usergroup = showUserGroup;
			splitView = usergroupSplitView;	// could be null, in current implementation
		}
		
		public override MonoTouch.UIKit.UITableViewCell GetCell (MonoTouch.UIKit.UITableView tv)
		{
			// try and dequeue a cell object to reuse. if one doesn't exist, create a new one
			UserGroupCell cell = tv.DequeueReusableCell (cellKey) as UserGroupCell;
			if (cell == null) {
				cell = new UI.CustomElements.UserGroupCell (usergroup);
			}
			cell.UpdateCell(usergroup);
	
			return cell;
		}
		
		public float GetHeight (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return 65f;
		}
	
		/// <summary>Implement MT.D search on name and company properties</summary>
		public override bool Matches (string text)
		{
			return (usergroup.Name).ToLower ().IndexOf (text.ToLower ()) >= 0;
		}
	
		public override void Selected (DialogViewController dvc, UITableView tableView, MonoTouch.Foundation.NSIndexPath path)
		{
			var eds = new MWC.iOS.Screens.iPhone.UserGroups.UserGroupDetailsScreen (usergroup.ID);
			
			if (splitView != null)
				splitView.ShowUserGroup(usergroup.ID, eds);
			else
				dvc.ActivateController (eds);
		}	
	}
}

