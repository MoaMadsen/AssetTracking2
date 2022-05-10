namespace AssetTracking2
{
    internal class AssetItem
    {
        // private int _endofdate;

        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public int PurchaseDate { get; set; }
        public int EndOfLife { get; set; }
       /* {
            get => _endofdate;
            set
            {
                _endofdate = PurchaseDate + 30000;
            }
        }*/
        public double Price { get; set; }
    }

    class Computer : AssetItem
    {
        public Computer(string brand, string model, string office, int purchase, double price)
        {
            Type = "computer";
            Brand = brand;
            Model = model;
            Office = office;
            PurchaseDate = purchase;
            EndOfLife = PurchaseDate + 30000;
            Price = price;
        }
    }
    class Mobile : AssetItem
    {
        public Mobile(string brand, string model, string office, int purchase, double price)
        {
            Type = "mobile";
            Brand = brand;
            Model = model;
            Office = office;
            PurchaseDate = purchase;
            EndOfLife = PurchaseDate + 30000;
            Price = price;
        }
    }
}