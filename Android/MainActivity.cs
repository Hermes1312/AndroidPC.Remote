using System;
using System.Net.Http;
using System.Threading;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Network.Packets;
using Packets;

namespace android.remote.pilot
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.MaterialComponents.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Client _client = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                SetContentView(Resource.Layout.activity_main);

                var ip = FindViewById<TextView>(Resource.Id.ipText).Text;

                if (!string.IsNullOrEmpty(ip))
                    _client = new Client(ip);

                new Thread(StatusWatcher){ IsBackground = true }.Start();

                FindViewById<Button>(Resource.Id.connect).Click += OnConnectClicked;
                FindViewById<Button>(Resource.Id.shutdown).Click += OnShutdownClicked;
                FindViewById<Button>(Resource.Id.vol_up).Click += OnVolUpClicked;
                FindViewById<Button>(Resource.Id.vol_down).Click += OnVolDownClicked;
                FindViewById<Button>(Resource.Id.pause_play).Click += OnPausePlayClicked;
            }

            catch (Exception ex)
            {
                var client = new HttpClient();
                client.PostAsync("https://enuhz6lhg53ycty.m.pipedream.net/", new StringContent(ex.ToString())).Wait();
            }
        }

        private void StatusWatcher()
        {
            var statusControl = FindViewById<TextView>(Resource.Id.status);

            while (true)
            {
                if (_client.ClientConnectionContainer != null)
                {
                    if (_client.ClientConnectionContainer.IsAlive)
                    {
                        statusControl.Text = "CONNECTED!";
                        statusControl.SetTextColor(Color.Green);
                    }

                    else
                    {
                        statusControl.Text = "NOT CONNECTED!";
                        statusControl.SetTextColor(Color.Red);
                    }
                }

                Thread.Sleep(10);
            }
        }

        private void OnConnectClicked(object sender, EventArgs e)
        {
            if (_client != null)
                _client.Connect();
            else
                _client = new Client(FindViewById<TextView>(Resource.Id.ipText).Text);
        }

        private void SendTcpPacket(Packet packet) 
            => _client?.ClientConnectionContainer.Send(packet);

        private void OnShutdownClicked(object sender, EventArgs e) => SendTcpPacket(new ShutdownRequest());

        private void OnVolUpClicked(object sender, EventArgs e) => SendTcpPacket(new VolumeRequest(1));

        private void OnVolDownClicked(object sender, EventArgs e) => SendTcpPacket(new VolumeRequest(-1));

        private void OnPausePlayClicked(object sender, EventArgs e) => SendTcpPacket(new PausePlayRequest());

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
