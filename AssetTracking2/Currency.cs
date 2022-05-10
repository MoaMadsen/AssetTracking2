namespace AssetTracking2
{
    internal class Currency
    {
        public Currency(string country, string shorten, double rate)
        {
            Country = country;
            Shorten = shorten;
            Rate = rate;
        }
        public string Country { get; set; }
        public string Shorten { get; set; }
        public double Rate { get; set; }
        

    }
}