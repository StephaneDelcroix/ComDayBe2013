using System;
using System.Collections.Generic;

namespace MWC {
	public class Constants {

		public Constants ()
		{
		}
		
		public static DateTime StartDateMin = new DateTime ( 2013, 06, 20, 0, 0, 0 );
		public static DateTime EndDateMax = new DateTime ( 2013, 06, 20, 23, 59, 59 );

//		public const string NewsBaseUrl = "news.google.com"; // for Reachability test
//		public const string NewsUrl = "http://news.google.com/news?q=mobile%20world%20congress&output=rss";
		public const string TwitterUrl = "http://search.twitter.com/search.atom?q=%40comdaybe&show-user=true&rpp=30&result-type=recent";
		
		public const string ConferenceDataBaseUrl = "www.mi8.be";
		public const string ConferenceDataUrl = "http://www.mi8.be/comdaybe/conference.xml";
		public const string UserGroupsDataUrl = "http://www.mi8.be/comdaybe/usergroups.xml";

		public const float MapPinLatitude = 51.026551f;
		public const float MapPinLongitude = 4.5002f;
		public const string MapPinTitle = "Utopolis Mechelen";
		public const string MapPinSubtitle = "Spuibeekstraat 5 2800 Mechelen";
		
//		public const string AboutUrlLinkedIn = "http://www.linkedin.com/company/xamarin";
//		public const string AboutUrlStackOverflow = "http://stackoverflow.com/questions/tagged/monotouch";
		public const string AboutUrlTwitter = "https://twitter.com/#!/mobileInception";
//		public const string AboutUrlYouTube = "http://www.youtube.com/xamarinhq";
//		public const string AboutUrlFacebook = "http://www.facebook.com/pages/Xamarin/283148898401104";
//      public const string AboutUrlBlogRss = "http://blog.xamarin.com/feed/";
        public const string AboutMailto = "info@mi8.be";
        public const string AboutTwitter = "@mobileInception";

		public const string AboutText = @"MobileInception is your unique partner to the mobile world. We create trully native cross-platform mobile application. http://www.mobile-inception.com/";
	}
}