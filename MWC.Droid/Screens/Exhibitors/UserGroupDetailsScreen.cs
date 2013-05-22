using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MWC.BL;

namespace MWC.Android.Screens {
    [Activity(Label = "UserGroup", ScreenOrientation = ScreenOrientation.Portrait)]
    public class UserGroupDetailsScreen : BaseScreen, MonoTouch.Dialog.Utilities.IImageUpdated {
		UserGroup usergroup;
        ImageView imageview;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ExhibitorDetailsScreen);

            var id = Intent.GetIntExtra("UserGroupID", -1);

            if (id >= 0) {
                usergroup = BL.Managers.UserGroupManager.GetUserGroup(id);
                if (usergroup != null) {
                    FindViewById<TextView>(Resource.Id.NameTextView).Text = usergroup.Name;
                    //FindViewById<TextView>(Resource.Id.CountryTextView).Text = usergroup.FormattedCityCountry;
                    //FindViewById<TextView>(Resource.Id.LocationTextView).Text = usergroup.Locations;
                    if (!String.IsNullOrEmpty(usergroup.Overview))
                        FindViewById<TextView>(Resource.Id.DescriptionTextView).Text = usergroup.Overview;
                    else
                        FindViewById<TextView>(Resource.Id.DescriptionTextView).Text = "No background information available.";
                    // now do the image
                    imageview = FindViewById<ImageView>(Resource.Id.ExhibitorImageView);
                    var uri = new Uri(usergroup.ImageUrl);
					Console.WriteLine("usergroup.ImageUrl " + usergroup.ImageUrl);
                    try {
                        var drawable = MonoTouch.Dialog.Utilities.ImageLoader.DefaultRequestImage(uri, this);
                        if (drawable != null)
                            imageview.SetImageDrawable(drawable);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.ToString());
                    }
                } else {   // shouldn't happen...
                    FindViewById<TextView>(Resource.Id.NameTextView).Text = "UserGroup not found: " + id;
                }
            }
        }

        public void UpdatedImage(Uri uri)
        {
            Console.WriteLine("usergroup.ImageUrl CALLBACK ");
            RunOnUiThread(() => {
                var drawable = MonoTouch.Dialog.Utilities.ImageLoader.DefaultRequestImage(uri, this);
                imageview.SetImageDrawable(drawable);
            });
        }
    }
}