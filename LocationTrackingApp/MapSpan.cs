namespace LocationTrackingApp
{
    internal class MapSpan
    {
        private Position position;
        private double v1;
        private double v2;

        public MapSpan(Position position, double v1, double v2)
        {
            this.position = position;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}