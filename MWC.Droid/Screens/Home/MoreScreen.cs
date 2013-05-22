using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace MWC.Android.Screens
{
    [Activity(Label = "More", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MoreScreen : BaseScreen
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our layout to be the home screen
            this.SetContentView(Resource.Layout.MoreScreen);

            var aboutButton = this.FindViewById<Button>(Resource.Id.AboutButton);
			var mi8Button = this.FindViewById<Button> (Resource.Id.AboutMi8Button);
            //var usergroupsButton = this.FindViewById<Button>(Resource.Id.ExhibitorsButton);
            var favoritesButton = this.FindViewById<Button>(Resource.Id.FavoritesButton);
            //var newsButton = this.FindViewById<Button>(Resource.Id.NewsButton);
            var twitterButton = this.FindViewById<Button>(Resource.Id.TwitterButton);

            aboutButton.Click += delegate
            {
                var intent = new Intent(this, typeof(AboutXamScreen));
                StartActivity(intent);
            };
			mi8Button.Click += delegate {
				var intent = new Intent (this, typeof(AboutMi8Screen));
				StartActivity (intent);
			};
            //usergroupsButton.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(UserGroupsScreen));
            //    StartActivity(intent);
            // };
            favoritesButton.Click += delegate
            {
                var intent = new Intent(this, typeof(FavoritesScreen));
                StartActivity(intent);
            };
            //newsButton.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(NewsScreen));
            //    StartActivity(intent);
            //};
            twitterButton.Click += delegate
            {
                var intent = new Intent(this, typeof(TwitterScreen));
                StartActivity(intent);
            };

        }
    }
}
