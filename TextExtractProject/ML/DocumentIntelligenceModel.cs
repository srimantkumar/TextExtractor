using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using TextExtractProject.DTO;
using Newtonsoft.Json;

namespace ML
{
    class DocumentIntelligenceModel
    {
        public async Task<string> ModelAnalyzer(IFormFile formFile)
        {
            // Get config settings from AppSettings
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            string? endpoint = configuration["ModelAnalyzerServiceEndPoint"];;
            string? apiKey = configuration["ModelAnalyzerServiceKey"];

            if (!string.IsNullOrEmpty(endpoint) || !string.IsNullOrEmpty(apiKey)) 
            {
                AzureKeyCredential credential = new AzureKeyCredential(apiKey);
                DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

                string modelId = "your_model_id";
                //string filePath = "";
                //if (command == 1)
                //    filePath = "ML/images/SanjayAdharCard.jpeg";
                //else if (command == 2)
                //    filePath = "ML/images/AdharCard.jpeg";
                Stream imageStream = FileToStream(formFile);

                AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, modelId, imageStream);
                AnalyzeResult result = operation.Value;

  
                foreach (AnalyzedDocument document in result.Documents)
                {
                    string[] keyClasses =  new string[5];
                    int i = 0;

                    foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                    {
                        string fieldName = fieldKvp.Key;
                        DocumentField field = fieldKvp.Value;

                        Console.WriteLine($"Field '{fieldName}': ");
                        Console.WriteLine($"  Content: '{field.Content}'");
                        Console.WriteLine($"  Confidence: '{field.Confidence}'");
                        keyClasses[i++] = field.Content;
                    }

                    var adharDetails = new AdharDetails
                    {
                        FullName = keyClasses[0],
                        DOB = keyClasses[1],
                        Gender = keyClasses[2],
                        Address = keyClasses[3],
                        AdharNumber = keyClasses[4]
                    };
                    string jsonString = JsonConvert.SerializeObject(adharDetails);
                    TestDeserialization(jsonString);
                    return jsonString;
                }
            }
            else
            {
                Console.WriteLine("One of the value is Null or Empty");
                Console.WriteLine("EndPoint : " + endpoint);
                Console.WriteLine("Api Key : " + apiKey);
                return "Error Occured. Something went Wrong!!!\"";
            }

            return "Something went Wrong!!!";
        }

        Stream FileToStream(IFormFile formFile)
        {
            // Open the file as a FileStream
            //FileStream fileStream = File.OpenRead(filePath);
    
            // Create a MemoryStream to copy the file content
            MemoryStream memoryStream = new MemoryStream();

            // Copy the FileStream content to the MemoryStream
            formFile.CopyTo(memoryStream);

            // Rewind the MemoryStream to the beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Return the MemoryStream as a Stream
            return memoryStream;
        }


        Stream FileToStream(string filePath)
        {
            // Open the file as a FileStream
            FileStream fileStream = File.OpenRead(filePath);

            // Create a MemoryStream to copy the file content
            MemoryStream memoryStream = new MemoryStream();

            // Copy the FileStream content to the MemoryStream
            fileStream.CopyTo(memoryStream);

            // Rewind the MemoryStream to the beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Close the original FileStream
            fileStream.Close();

            // Return the MemoryStream as a Stream
            return memoryStream;
        }

        public void TestDeserialization(string jsonString)
        {
            AdharDetails adharDetails = JsonConvert.DeserializeObject<AdharDetails>(jsonString);
            var toString = adharDetails.ToString();
            Console.WriteLine(toString);
        }
    }
}