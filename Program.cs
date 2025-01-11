using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DotNetEnv;

class Program
{
    static async Task Main(string[] args)
    {
        bool programIsRunning = true;
        Console.WriteLine("Crypto App is running...");
        Console.WriteLine("Fetching top cryptocurrencies...");
        // Load environment variables from .env file
        Env.Load();

        if (Environment.GetEnvironmentVariable("COINGECKO_API_KEY") == null || Environment.GetEnvironmentVariable("COINGECKO_API_KEY") == "")
        {
            Console.WriteLine("API key not found. Please set the COINGECKO_API_KEY environment variable.");
            return;
        }

        // Get the API key from environment variables
        string apiKey = Environment.GetEnvironmentVariable("COINGECKO_API_KEY");

        // API URL for fetching cryptocurrency data
        string apiUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd";

        // Create an HttpClient instance
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Set the API key in the request headers
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);

                // Send a GET request to the API
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a list of CryptoData objects
                var cryptoList = JsonConvert.DeserializeObject<CryptoData[]>(responseBody);
                if (cryptoList == null)
                {
                    Console.WriteLine("No data found.");
                    return;
                }

                while (programIsRunning)
                {
                Console.WriteLine("Would you like to see a particular cryptocurrency? (y/n) or top 100 (t) or quit (q)");
                string input = Console.ReadLine();
                    // Clean up the input
                    input = input.ToLower().Trim();
                    string currency = "usd";

                    if (input == "q")
                    {
                        programIsRunning = false;
                    }
                    else if (input == "t")
                    {
                        // Display the top 100 cryptocurrencies
                        foreach (var crypto in cryptoList)
                        {
                            Console.WriteLine($"Name: {crypto.Name} | Symbol: {crypto.Symbol} | Current Price: ${crypto.CurrentPrice}");
                        }
                    }
                    else if (input == "y")
                    {
                        Console.WriteLine("Enter the cryptocurrency name: for example , bitcoin or solana or ripple");
                        
                        string coinName = Console.ReadLine();
                        // checks if null and also if contains numbers
                        if (!string.IsNullOrEmpty(coinName) && !coinName.Any(char.IsDigit)) {
                            coinName = coinName.ToLower().Trim();
                            await GetLastPrice(client, coinName, currency);
                        } else {
                            Console.WriteLine("Invalid input");
                        }
                    }

                    // Console.WriteLine("Would you like to see another cryptocurrency? (y/n) or top 100 (t) or quit (q)");
                    // input = Console.ReadLine();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }

    public async static Task GetLastPrice(HttpClient client, string coinName, string currency = "usd")
    {
        string apiKey = Environment.GetEnvironmentVariable("COINGECKO_API_KEY");
        string singleCryptoUrl = $"https://api.coingecko.com/api/v3/simple/price?ids={coinName}&vs_currencies=usd";
        // include the header in the request
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);
        try
        {
            // Send a GET request to the API
            HttpResponseMessage response = await client.GetAsync(singleCryptoUrl);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response to a JObject
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseBody);


            //get thejson object property : usd
            var price = jsonResponse[coinName.ToLower()]?["usd"];
            if (price == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine($"The current price of {coinName} is: ${price}");
            // Extract the price from the JSON response




        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
    }
}
