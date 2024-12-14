using Microsoft.Maui.Controls.Maps;
 namespace LocationTrackingApp
    {
        public partial class MainPage : ContentPage
        {
            // Location tracking interval (in milliseconds)
            private const int TrackingInterval = 10000; // 10 seconds
            private Microsoft.Maui.Controls.Maps.Map locationMap = new Microsoft.Maui.Controls.Maps.Map();

            public MainPage()
            {
                InitializeComponent();
            }

            public object PinType { get; private set; }
        public object initialPosition { get; private set; }

        private void InitializeComponent()
            {
                throw new NotImplementedException();
            }

            // Start button click handler
            private async void OnStartTrackingClicked(object sender, EventArgs e)
            {
                // Request permissions to access the location
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Denied", "Location permission is required", "OK");
                    return;
                }

            // Start tracking the location
            StartLocationTracking(initialPosition);
            }

        private void StartLocationTracking(object initialPosition)
        {
            throw new NotImplementedException();
        }

        // Method to start location tracking
        private async void StartLocationTracking(MapSpan initialPosition)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location != null)
                    {
                    // Set initial position of the map
                    var initialPosition = new MapSpan(new Position(location.Latitude, location.Longitude), 0.01, 0.01);
                    object value = locationMap.MoveToRegion(initialPosition);
                    }

                    // Periodically update location
                    Device.StartTimer(TimeSpan.FromMilliseconds(TrackingInterval), () =>
                    {
                        TrackCurrentLocation();
                        return true; // Continue the timer
                    });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Location tracking failed: {ex.Message}", "OK");
                }
            }

            // Method to get and show current location
            private async void TrackCurrentLocation()
            {
                try
                {
                    var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));
                    if (location != null)
                    {
                        // Update map with new location
                        var currentPosition = new Position(location.Latitude, location.Longitude);
                        var pin = new CustomPin { Label = "You are here", Position = currentPosition, Type = PinType.Place };
                        locationMap.Pins.Clear();
                    locationMap.Pins.Add(pin);

                        // Update the map region
                        var mapSpan = new MapSpan(currentPosition, 0.01, 0.01);
                    object value = locationMap.MoveToRegion(mapSpan);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Unable to track location: {ex.Message}", "OK");
                }
            }
        }

    internal class CustomPin
    {
        public string Label { get; set; }
        public Position Position { get; set; }
        public object Type { get; set; }
    }
}
