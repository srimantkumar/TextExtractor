using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
// import namespaces
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

// Import namespaces
namespace ML
{
    class OCRModel
    {

        private static ComputerVisionClient cvClient;
        public async Task<List<string>> TextExtractionMethod(string command)
        {
            List<string> stringList = new List<string>();
            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string cogSvcEndpoint = configuration["CognitiveServicesEndpoint"];
                string cogSvcKey = configuration["CognitiveServiceKey"];

                // Authenticate Azure AI Vision client
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(cogSvcKey);
                cvClient = new ComputerVisionClient(credentials)
                {
                    Endpoint = cogSvcEndpoint
                };


                // Menu for text reading functions
                //Console.WriteLine("1: Use Read API for image\n2: Use Read API for document\n3: Read handwriting\nAny other key to quit");
                //Console.WriteLine("Enter a number:");
                string imageFile;
                switch (command)
                {
                    case "1":
                        imageFile = "ML/images/OneCard.jpeg";
                        stringList = await GetTextRead(imageFile);
                        break;
                    case "2":
                        imageFile = "ML/images/Rome.pdf";
                        stringList = await GetTextRead(imageFile);
                        break;
                    case "3":
                        imageFile = "ML/images/Rome.pdf";
                        stringList = await GetTextRead(imageFile);
                        break;
                    default:
                        break;
                }
                return stringList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return stringList;
            }
        }

        static async Task<List<string>> GetTextRead(string imageFile)
        {
            // Use Read API to read text in image
            using (var imageData = File.OpenRead(imageFile))
            {    
                List<string> stringList = new List<string>();
                var readOp = await cvClient.ReadInStreamAsync(imageData);

                // Get the async operation ID so we can check for the results
                string operationLocation = readOp.OperationLocation;
                string operationId = operationLocation.Substring(operationLocation.Length - 36);

                // Wait for the asynchronous operation to complete
                ReadOperationResult results;
                do
                {
                    Thread.Sleep(1000);
                    results = await cvClient.GetReadResultAsync(Guid.Parse(operationId));
                }
                while ((results.Status == OperationStatusCodes.Running ||
                        results.Status == OperationStatusCodes.NotStarted));

                // If the operation was successfully, process the text line by line
                if (results.Status == OperationStatusCodes.Succeeded)
                {
                    var textUrlFileResults = results.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in textUrlFileResults)
                    {
                        foreach (Line line in page.Lines)
                        {
                            Console.WriteLine(line.Text);
                            stringList.Add(line.Text);
                            
                            // Uncomment the following line if you'd like to see the bounding box 
                            //Console.WriteLine(line.BoundingBox);
                        }
                    }
                }
                return stringList;
            }  
        }
    }
}
