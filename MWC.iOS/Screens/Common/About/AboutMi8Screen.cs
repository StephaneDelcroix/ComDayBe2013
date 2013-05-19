using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MWC.iOS.Screens.Common.About {
	/// <summary>
	/// This screen REPLACES the old XIB version
	/// </summary>
	public class AboutMi8Screen : UIViewController {
		UIWebView webView;

		public AboutMi8Screen ()
		{
			Title = "About Mobile Inception";
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			webView = new UIWebView();

			webView.ShouldStartLoad = 
			delegate (UIWebView webViewParam, NSUrlRequest request, UIWebViewNavigationType navigationType) {
				// view links in a new 'webbrowser' window like about, session & twitter
				if (navigationType == UIWebViewNavigationType.LinkClicked) {
					UIApplication.SharedApplication.OpenUrl (request.Url);
					return false;
				}
				return true;
			};

			Add (webView);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			webView.Frame = new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height);
			webView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			
			NSUrl url = null;
			
			url = new NSUrl("http://www.mobile-inception.com");
			var request = new NSUrlRequest(url);
			webView.LoadRequest(request);
		}
	}
}