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
            lblTest.Text = pobierzKurs("usd", dzis);
            lblTest2.Text = pobierzKurs("usd", dzis.AddDays(-1));
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
