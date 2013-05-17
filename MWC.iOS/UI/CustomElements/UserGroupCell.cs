using System;
using System.Drawing;
using MonoTouch.Dialog.Utilities;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MWC.iOS.UI.CustomElements {
	/// <summary>
	/// Custom Exhibitor cell. Used to display exhibitor info.
	/// </summary>
	public class UserGroupCell : UITableViewCell, IImageUpdated {
		BL.UserGroup usergroup;
		// control declarations
		protected UILabel nameLabel;

		protected UIImageView logoImageView;
		int cellTextLeft = 8 + 44 + 13;
		
		/// <summary>
		/// Gets the reuse identifier.
		/// </summary>
		/// <value>
		/// The reuse identifier.
		/// </value>
		public override string ReuseIdentifier
		{
			get { return cellId; }
		}
		static NSString cellId = new NSString("UserGroupCell");
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MWC.iOS.UI.CustomElements.ExhibitorCell"/> class.
		/// </summary>
		public UserGroupCell (BL.UserGroup showUserGroup) : base (new RectangleF (0, 0, 320, 66 ) )
		{
			usergroup = showUserGroup;
	
			// create the control and add it to the view
			nameLabel = new UILabel ( new RectangleF ( cellTextLeft, 7, 231, 27 ) ); //9->7,23->25
			nameLabel.Font = UIFont.FromName ( "Helvetica-Light", AppDelegate.Font16pt );
			nameLabel.BackgroundColor = UIColor.FromWhiteAlpha (0f, 0f);
			AddSubview (nameLabel);

			
			logoImageView = new UIImageView ( new RectangleF ( 8, 8, 44, 44 ) ) {ContentMode = UIViewContentMode.ScaleAspectFit};
			AddSubview(logoImageView);
		}
	
		public void UpdateCell (BL.UserGroup showExhibitor)
		{
			usergroup = showExhibitor;
			nameLabel.Text = usergroup.Name;

		
			var u = new Uri(usergroup.ImageUrl);
			logoImageView.Image = ImageLoader.DefaultRequestImage(u, this);
		}
	
		public void UpdatedImage (Uri uri)
		{
			logoImageView.Image = ImageLoader.DefaultRequestImage(uri, this);
		}
	}
}