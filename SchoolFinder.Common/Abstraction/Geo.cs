namespace SchoolFinder.Common.Abstraction
{
    public class Geo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
