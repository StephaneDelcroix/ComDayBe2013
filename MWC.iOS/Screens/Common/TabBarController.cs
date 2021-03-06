using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace MWC.iOS.Screens.Common {
	public class TabBarController : UITabBarController {
		UIViewController homeScreen = null;
		UINavigationController homeNav, speakerNav, sessionNav;
		DialogViewController speakersScreen, sessionsScreen, twitterFeedScreen, userGroupsScreen, /*newsFeedScreen, exhibitorsScreen,*/ favoritesScreen;
		Screens.Common.Map.MapScreen mapScreen;
		Screens.Common.About.AboutXamarinScreen aboutScreen;
		UIViewController mi8Screen;
		UISplitViewController speakersSplitView, sessionsSplitView, userGroupsSplitView ,/*exhibitorsSplitView,*/ twitterSplitView/*, newsSplitView*/;
		
		public TabBarController ()
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// home tab
			if (AppDelegate.IsPhone) {
				homeScreen = new Screens.iPhone.Home.HomeScreen();
				homeScreen.Title = "Schedule";
			} else {
				homeScreen = new Screens.iPhone.Home.HomeScreen();
			}
			homeNav = new UINavigationController();
			homeNav.PushViewController ( homeScreen, false );			
			homeNav.Title = "Schedule";
			homeNav.TabBarItem = new UITabBarItem("Schedule"
										, UIImage.FromBundle("Images/Tabs/schedule.png"), 0);
			
			// speakers tab
			if (AppDelegate.IsPhone) {
				speakersScreen = new Screens.iPhone.Speakers.SpeakersScreen();			
				speakerNav = new UINavigationController();
				speakerNav.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
				speakerNav.PushViewController ( speakersScreen, false );
			} else {
				speakersSplitView = new MWC.iOS.Screens.iPad.Speakers.SpeakerSplitView();
				speakersSplitView.TabBarItem = new UITabBarItem("Speakers"
											, UIImage.FromBundle("Images/Tabs/speakers.png"), 1);
			}

			// sessions
			if (AppDelegate.IsPhone) {
				sessionsScreen = new Screens.iPhone.Sessions.SessionsScreen();
				sessionNav = new UINavigationController();
				sessionNav.TabBarItem = new UITabBarItem("Sessions"
											, UIImage.FromBundle("Images/Tabs/sessions.png"), 2);
				sessionNav.PushViewController ( sessionsScreen, false );
			} else {
				sessionsSplitView = new MWC.iOS.Screens.iPad.Sessions.SessionSplitView();
				sessionsSplitView.TabBarItem = new UITabBarItem("Sessions"
											, UIImage.FromBundle("Images/Tabs/sessions.png"), 2);		
			}
			// maps tab
			mapScreen = new Screens.Common.Map.MapScreen();
			mapScreen.TabBarItem = new UITabBarItem("Map"
										, UIImage.FromBundle("Images/Tabs/maps.png"), 3);
			
			if (AppDelegate.IsPhone) {
				// exhibitors
				//exhibitorsScreen = new Screens.iPhone.Exhibitors.ExhibitorsScreen();
				//exhibitorsScreen.TabBarItem = new UITabBarItem("Exhibitors"
				//							, UIImage.FromBundle("Images/Tabs/exhibitors.png"), 4);

				// user groups
				userGroupsScreen = new Screens.iPhone.UserGroups.UserGroupsScreen ();
				userGroupsScreen.TabBarItem = new UITabBarItem ("User Groups", UIImage.FromBundle ("Images/Tabs/exhibitors.png"), 4);


				// twitter feed
				twitterFeedScreen = new MWC.iOS.Screens.iPhone.Twitter.TwitterScreen();
				twitterFeedScreen.TabBarItem = new UITabBarItem("Twitter"
											, UIImage.FromBundle("Images/Tabs/twitter.png"), 5);
				
				// news
				//newsFeedScreen = new MWC.iOS.Screens.Common.News.NewsScreen();
				//newsFeedScreen.TabBarItem =  new UITabBarItem("News"
				//							, UIImage.FromBundle("Images/Tabs/rss.png"), 6);
			} else {
				// iPad
				// exhibitors
				
				//exhibitorsSplitView = new Screens.iPad.Exhibitors.ExhibitorSplitView();
				//exhibitorsSplitView.TabBarItem = new UITabBarItem("Exhibitors"
				//							, UIImage.FromBundle("Images/Tabs/exhibitors.png"), 4);
				userGroupsSplitView = new Screens.iPad.UserGroups.UserGroupsSplitView ();
				userGroupsSplitView.TabBarItem = new UITabBarItem ("User Groups", UIImage.FromBundle ("Images/Tabs/exhibitors.png"), 4);
				
				// twitter feed
				twitterSplitView = new Screens.iPad.Twitter.TwitterSplitView();
				twitterSplitView.TabBarItem = new UITabBarItem("Twitter"
											, UIImage.FromBundle("Images/Tabs/twitter.png"), 5);

				// news
				//newsSplitView = new MWC.iOS.Screens.iPad.News.NewsSplitView();
				//newsSplitView.TabBarItem =  new UITabBarItem("News"
				//							, UIImage.FromBundle("Images/Tabs/rss.png"), 6);
			}
		
			// favorites (only required on iPhone)
			if (AppDelegate.IsPhone) {
				favoritesScreen = new MWC.iOS.Screens.iPhone.Favorites.FavoritesScreen();
				favoritesScreen.TabBarItem =  new UITabBarItem("Favorites"
											, UIImage.FromBundle("Images/Tabs/favorites.png"), 6);
			}
			mi8Screen = new Screens.Common.About.AboutMi8Screen ();
			mi8Screen.TabBarItem = new UITabBarItem ("About Mobile Inception"
			                                       , UIImage.FromBundle ("Images/Tabs/mi8.png"), 7);

			// about tab
			aboutScreen = new Screens.Common.About.AboutXamarinScreen();
			aboutScreen.TabBarItem = new UITabBarItem("About Xamarin"
										, UIImage.FromBundle("Images/Tabs/about.png"), 8);
			
			UIViewController[] viewControllers;
			// create our array of controllers
			if (AppDelegate.IsPhone) {
				viewControllers = new UIViewController[] {
					homeNav,
					speakerNav,
					sessionNav,
					mapScreen,
					//exhibitorsScreen,
					userGroupsScreen,
					twitterFeedScreen,
					//newsFeedScreen,
					favoritesScreen,
					mi8Screen,
					aboutScreen
				};
			} else {	// IsPad
				viewControllers = new UIViewController[] {
					homeNav,
					speakersSplitView,
					sessionsSplitView,
					mapScreen,
					//exhibitorsSplitView,
					userGroupsSplitView,
					twitterSplitView,
					//newsSplitView,	
					// NOTE: removed Favorites
					mi8Screen,
					aboutScreen
				};
			}
			
			// attach the view controllers
			ViewControllers = viewControllers;
			
			// tell the tab bar which controllers are allowed to customize. 
			// if we don't set  it assumes all controllers are customizable. 
			// if we set to empty array, NO controllers are customizable.
			CustomizableViewControllers = new UIViewController[] {};
			
			// set our selected item
			SelectedViewController = homeNav;
		}
		
		public void ShowSessionDay(int day)
		{
			// WARNING: ORDER IS IMPORTANT, call ShowDay() before setting index (which causes ViewWillAppear)
			var sv = sessionsSplitView as MWC.iOS.Screens.iPad.Sessions.SessionSplitView;
			sv.ShowDay (day);
			SelectedIndex = 2; // Sessions
		}

		/// <summary>
		/// Only allow iPad application to rotate, iPhone is always portrait
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
			if (AppDelegate.IsPad)
	            return true;
			else
				return toInterfaceOrientation == UIInterfaceOrientation.Portrait;
        }
	}
}