namespace TheHomeOffice.Api.Domain.Models
{
    public class Address
    {
        public string AddressName { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}