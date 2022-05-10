using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace AssetTracking2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<AssetItem> assets = new();
            List<Currency> currencyCode = new();

            currencyCode.AddRange(new List<Currency>
            {
                new Currency("USA","USD",1),
                new Currency("Sweden","SEK",9.9521),
                new Currency("Spain","EUR",0.9487),
                new Currency("Denmark","DKK",7.0784),
                new Currency("Germany","EUR",0.9487)
            });

            assets.AddRange(new List<AssetItem>
            {
                new Computer("HP","Elitebook","Spain",20190601,1423),
                new Computer("HP","Elitebook","Sweden",20200316,588),
                new Computer("HP","Elitebook","Sweden",20200316,588),
                new Computer("Asus","W234","Spain",20190821,1200),
                new Computer("Lenovo","Yoga 730","USA",20170421,1035),
                new Computer("Lenovo","Yoga 530","USA",20190521,835),
                new Mobile("iPhone","8","Spain",20181229,970),
                new Mobile("iPhone","11","Spain",20200925,990),
                new Mobile("Motorola","Razr","Sweden",20200315,970),
                new Mobile("iPhone","X","Sweden",20180715,1245)
            });

            var sortedTypeDate = assets.OrderBy(asset => asset.Type).ThenBy(asset => asset.PurchaseDate);
            Write_output(sortedTypeDate, currencyCode);
            // Write_output1(assets, currencyCode);
            var sortedOfficePdate = assets.OrderBy(asset => asset.Office).ThenBy(asset => asset.PurchaseDate);
            Write_output(sortedOfficePdate, currencyCode);
        }




        // write output
        private static void Write_output(IOrderedEnumerable<AssetItem> assets, List<Currency> currencies)
        // private static void Write_output1(List<AssetItem> assets, List<Currency> currencies)
        {
            Console.ResetColor();
            // string mainCurrency = "USD";
            Console.WriteLine("{0," + 30 + "}", "*** Asset Tracking ***");
            Console.WriteLine("Type".PadRight(10) + "Brand".PadRight(10) + "Model".PadRight(10) + "Office".PadRight(10) + "Purchase Date " + "Price in USD " + "Currency " + "Local price today");
            Console.WriteLine("----".PadRight(10) + "-----".PadRight(10) + "-----".PadRight(10) + "------".PadRight(10) + "------------- " + "------------ " + "-------- " + "-----------------");
            foreach (AssetItem a in assets)
            {
                string NewPrice = null;
                string newCurrency = "N/A";
                DateTime Pdate = new(a.PurchaseDate/10000, (a.PurchaseDate/100)%100, a.PurchaseDate%100);
                DateTime Edate = new(a.EndOfLife/ 10000, (a.EndOfLife/ 100) % 100, a.EndOfLife % 100);
                TimeSpan diff = Edate - DateTime.Today;
                var currency = currencies.Where(office => office.Country == a.Office);
                //IEnumerable<Currency> currency = from c in currencies where c.Country == a.Office select c;
                if (currency.Any())
                {
                    foreach (var c in currency)
                    {
                        //    NewPrice = ExchangeRate(a.Price, mainCurrency, c.Shorten);
                        NewPrice = (a.Price * c.Rate).ToString("N2");
                        newCurrency = c.Shorten;
                    }
                }else
                    Console.WriteLine("no country code for "+ a.Office);

                if (diff.Days < 90)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (diff.Days < 180)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                     else
                        Console.ResetColor();
      
                Console.WriteLine(a.Type.PadRight(10) + a.Brand.PadRight(10) + a.Model.PadRight(10) + a.Office.PadRight(10) + Pdate.ToString("yyyy-MM-dd") + a.Price.ToString("N2").PadLeft(12)+newCurrency.PadLeft(10)+NewPrice.PadLeft(12));
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        public static string ExchangeRate(double amout, string fromCurrency, string toCurrency)
        {
            const string url1 = "https://free.currconv.com/api/v7/convert?q=";
            const string url2 = "&compact=ultra&apiKey=8924f16722efb3a8860e";
            string url = url1 + fromCurrency + "_" + toCurrency + url2;
            //string url = "https://free.currconv.com/api/v7/convert?q=USD_PHP&compact=ultra&apiKey=8924f16722efb3a8860e";
            string response = new WebClient().DownloadString(url);
            double Rate;
            int length = response.Length-12;
            if (double.TryParse(response.Substring(11, length), NumberStyles.Any, CultureInfo.InvariantCulture, out Rate))
                Rate= amout * Rate;
            else
                Console.WriteLine("false" + response);

            return Rate.ToString("N2");
        }
    }
}

