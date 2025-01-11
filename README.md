# Crypto Price Tracker

![C#](https://img.shields.io/badge/C%23-%20-green)
![.NET](https://img.shields.io/badge/.NET-5.0-blue)
![License](https://img.shields.io/badge/License-MIT-blue.svg)

🚀 **Crypto Price Tracker** is a simple console application built in C# that fetches and displays the current price of cryptocurrencies using the CoinGecko API. 

## 📜 Features

- Fetches real-time cryptocurrency prices.
- Displays top 100 cryptocurrencies.
- Allows querying for specific cryptocurrency prices.
- Handles API authentication using environment variables.
- Provides user-friendly error messages for invalid inputs.

## 💻 Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed.
- [CoinGecko API Key](https://www.coingecko.com/en/api) (Demo key is also acceptable).

### Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/cryptoprice-tracker.git
    cd cryptoprice-tracker
    ```

2. **Install dependencies:**
    ```sh
    dotnet restore
    ```

3. **Set up environment variables:**
    - Create a `.env` file in the project root:
        ```
        COINGECKO_API_KEY=your_api_key_here
        ```

4. **Run the application:**
    ```sh
    dotnet run
    ```

## 🛠️ Usage

1. The application will fetch and display the top 100 cryptocurrencies.
2. You can query the price of a specific cryptocurrency by entering its name or symbol.
3. Use the options `(y/n/t/q)` as prompted to navigate through the app.

## 📚 Code Overview

### Main Program
The `Program.cs` file handles the main application logic, including fetching cryptocurrency data and user interaction.

### GetLastPrice Method
This method sends an HTTP GET request to fetch the current price of a specified cryptocurrency and handles the response.

## 🔧 Technologies Used

- **C#**
- **.NET 5.0**
- **HttpClient**
- **Newtonsoft.Json** for JSON parsing
- **DotNetEnv** for loading environment variables

## 📄 License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---


