using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyMegaConverter 
{
    internal class MainViewModel : BindableObject
    {
        private string[] _currencies = { "shish", "shoah"};

        public string[] Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private string _current;

        public string Current
        {
            get => _current;
            set
            {
                _current = value;
                OnPropertyChanged(nameof(Current));
            }
        }

        private ExchangeRates _exchangeRates;

        public MainViewModel()
        {
            var uri = new Uri($"https://www.cbr-xml-daily.ru/daily_json.js");
            var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            _exchangeRates = JsonSerializer.Deserialize<ExchangeRates>(result);
            Currencies = _exchangeRates.Valute.Keys.ToArray();
            Current = Currencies[0];
        }
    }
}
