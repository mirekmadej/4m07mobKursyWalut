using System.Net;
using System.Text.Json;

namespace _4m07mobKursyWalut
{
    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            Currency c1 = new Currency();
            Currency c2 = new Currency();
            double kursZD,kursSD, kursW;

            c1 = deserializujJson(pobierzKurs("usd", dzis));
            c2 = deserializujJson(pobierzKurs("usd", dzis.AddDays(-1)));
            kursZD = (double)c1.rates[0].bid;
            kursSD = (double)c1.rates[0].ask;
            kursW = (double)c2.rates[0].bid;
            lblUSDks.Text = $"Kurs sprzedaży: {kursSD}";
            lblUSDkz.Text = $"Kurs skupu:     {kursZD}";
            if (kursW == kursZD)
                imgUSD.Source = "nc.png";
            else if (kursW > kursZD)
                imgUSD.Source = "down.png";
            else
                imgUSD.Source = "up.png";

            c1 = deserializujJson(pobierzKurs("eur", dzis));
            c2 = deserializujJson(pobierzKurs("eur", dzis.AddDays(-1)));
            kursZD = (double)c1.rates[0].bid;
            kursSD = (double)c1.rates[0].ask;
            kursW = (double)c2.rates[0].bid;
            lblEURks.Text = $"Kurs sprzedaży: {kursSD}";
            lblEURkz.Text = $"Kurs skupu:     {kursZD}";
            if (kursW == kursZD)
                imgEUR.Source = "nc.png";
            else if (kursW > kursZD)
                imgEUR.Source = "down.png";
            else
                imgEUR.Source = "up.png";

            c1 = deserializujJson(pobierzKurs("gbp", dzis));
            c2 = deserializujJson(pobierzKurs("gbp", dzis.AddDays(-1)));
            kursZD = (double)c1.rates[0].bid;
            kursSD = (double)c1.rates[0].ask;
            kursW = (double)c2.rates[0].bid;
            lblGBPks.Text = $"Kurs sprzedaży: {kursSD}";
            lblGBPkz.Text = $"Kurs skupu:     {kursZD}";
            if (kursW == kursZD)
                imgGBP.Source = "nc.png";
            else if (kursW > kursZD)
                imgGBP.Source = "down.png";
            else
                imgGBP.Source = "up.png";

        }
        private Currency deserializujJson(string json)
        {
            return  JsonSerializer.Deserialize<Currency>(json);
        }
        private string pobierzKurs(string waluta, DateTime data)
        {
            string d = data.ToString("yyyy-MM-dd");
            string url = "https://api.nbp.pl/api/exchangerates/rates/c/"+waluta+"/"+d+"/?format=json";
            string json;
            using(var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            return json;
        }


    }

}
