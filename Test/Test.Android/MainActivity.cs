using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using Xamarin.Forms;
using Google;
using Android.Content;

namespace Test.Droid
{
    [Activity(Label = "Journal App", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected static string DB_NAME = "Baza_de_date.db";

        private void copyDataBase(string DB_PATH)
        {
            var myInput = ApplicationContext.Assets.Open("Baza_de_date.db");
            string outFileName = DB_PATH;
            var myOutput = new FileStream(outFileName, FileMode.OpenOrCreate);
            byte[] buffer = new byte[1024];
            int length;
            while ((length = myInput.Read(buffer, 0, 1024)) > 0)
                myOutput.Write(buffer, 0, length);
            myOutput.Flush();
            myOutput.Close();
            myInput.Close();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.SaddleBrown);
            string fileName = "Baza_de_date.db";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(folderPath, fileName);
            copyDataBase(completePath);
            LoadApplication(new App(completePath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}