using System.Text;
using ask;
using Newtonsoft.Json;


// Change the console foreground color to blue
Console.ForegroundColor = ConsoleColor.Blue;
// Write a prompt for the user input
Console.Write("What command do you use to ");
// Reset the console foreground color back to the default
Console.ResetColor();

// Get the user input
var question = Console.ReadLine();

// Check if the user input is not empty
if (question.Length > 0)
{
    // Create a new HttpClient object for making API requests
    HttpClient client = new HttpClient();
    // Add an authorization header to the request
    client.DefaultRequestHeaders.Add("authorization", "Bearer TOKEN");

    // Create an object to hold the request content (the prompt)
    ContentObj obj = new ContentObj()
    {
        model = "text-davinci-003", // Specify the OpenAI model to use
        prompt = $"What command do you use to {question}? Return this answer command between quotes", // Set the prompt for the request
        temperature = 1, // Set the temperature for the request
        max_tokens = 200// Set the maximum number of tokens to generate in the response
    };

    // Serialize the request content object to a JSON string
    var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

    // Set the number of API request attempts
    int max_attempts = 3;

    // Repeat the API request until either a response is received or the maximum number of attempts is reached
    while (max_attempts > 0)
    {
        // Make the API request
        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

        // Read the response content as a string
        string responseString = await response.Content.ReadAsStringAsync();

        try
        {
            // Deserialize the response content as a dynamic object
            var dynamicData = JsonConvert.DeserializeObject<dynamic>(responseString);
            // Use the GuessCommand function to extract the guessed command from the response
            string guess = GuessCommand(dynamicData!.choices[0].text);
            // Change the console foreground color to green
            Console.ForegroundColor = ConsoleColor.Green;
            // Write the guessed command to the console
            Console.WriteLine($"---> I think the command is: {guess}");
            // Reset the console foreground color back to the default
            Console.ResetColor();
            // Break out of the loop since a response has been received
            break;

        }
        catch (Exception e)
        {
            // Check if this is the last attempt
            if (max_attempts == 1)
            {
                // Change the console foreground color to red
                Console.ForegroundColor = ConsoleColor.Red;
                // Write an error message to the console
                Console.WriteLine($"---> Sorry, I can't help you this time. There is an problem with OpenAI API! {e.Message}<---");
                // Write an error object to the console
                Console.WriteLine(responseString);
                // Reset the console foreground color back to the default
                Console.ResetColor();
            }
            else
            {
                // Write an warning message to the console
                Console.WriteLine($"---> Oh, I found some errors... but I'm trying to get your answer again ;) <---");
            }

            max_attempts--;
        }
    }
}
else
{
    Console.WriteLine("---> You need to provide some input <---");
}

static string GuessCommand(string raw)
{
    // Finds the index of the last line break in `raw`
    var lastIndex = raw.LastIndexOf('\n');

    // Extracts the substring from `lastIndex + 1` to the end of `raw` and removes any quotes
    string guess = raw.Substring(lastIndex + 1).Replace("\"", "");

    // Sets the extracted string as the system clipboard text
    TextCopy.ClipboardService.SetText(guess);

    // Returns the processed string
    return guess;
}