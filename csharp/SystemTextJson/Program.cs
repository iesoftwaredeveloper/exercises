using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Buffers;
using System.Linq;
using System.Collections.Generic;

namespace SystemTextJson
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonDocumentOptions jsonOptions = new JsonDocumentOptions();
            jsonOptions.AllowTrailingCommas = true;
            jsonOptions.CommentHandling = JsonCommentHandling.Skip;
            jsonOptions.MaxDepth = 4;
            //JsonDocument_Parse(jsonOptions);
            //JsonDocument_ParseAsync(jsonOptions).Result.Dispose();
            //JsonDocumentOptions_Example();
            //JsonElement_Example();
            //Utf8JsonReader_Example();
            //Utf8JsonWriter_Example();
            // JsonSerializer_Deserialize();
            // JsonSerializer_Serialize();
        }

        public static void JsonSerializer_Serialize()
        {
            RootObject<Result> root = new RootObject<Result>();
            List<Result> results = new List<Result>();
            results.Add(new Result
            {
                FullName = "Full Name",
                FirstName = "First",
                LastName = "Last",
                Company = "Company 42",
                Email = "email@company42.com",
                LastLogin = DateTime.UtcNow,
                DateJoined = (DateTime.UtcNow).AddDays(-20),
                Groups = new uint[] { 3, 5, 7 },
                Teams = null,
                MemberOf = null,
                Id = 42
            });

            results.Add(new Result
            {
                FullName = "Full Name",
                FirstName = "First",
                LastName = "Last",
                Company = "Company 46",
                Email = "email@company47.com",
                LastLogin = DateTime.Now,
                DateJoined = (DateTime.Now).AddDays(-2),
                Groups = new uint[] { 3, 5, 7 },
                Teams = null,
                MemberOf = null,
                Id = 47
            });

            // Serialize default object to string.
            Console.WriteLine("{0}", JsonSerializer.Serialize(root));

            root = new RootObject<Result>()
            {
                Count = results.Count,
                Next = "page2",
                Previous = null,
                Results = results.ToArray<Result>()
            };

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                WriteIndented = false
            };
            Console.WriteLine("{0}", JsonSerializer.Serialize(root, options));

            // Register a converter and output again.
            options = new JsonSerializerOptions()
            {
                Converters = { new DateTimeConverter() }
            };
            Console.WriteLine("{0}", JsonSerializer.Serialize(root, options));

        }
        public static void JsonSerializer_Deserialize()
        {
            // TODO Add exmample of Overflow JSON. https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-handle-overflow

            // Method 1: Deserialize from string.
            // Step 1: Get the JSON string you want to deserialize.
            JsonDocument document = JsonDocument_ParseAsync(new JsonDocumentOptions());

            // Step 2: Get the Root Element and convert it to a string.
            string json = document.RootElement.ToString();

            // Step 3: Deserialize json
            RootObject<Result> root = JsonSerializer.Deserialize<RootObject<Result>>(json);
            Console.WriteLine(root.Results.Length);
            Console.WriteLine(root.Results[0].FullName);
            Console.WriteLine(root.Results[0].LastLogin);

            // Console.WriteLine("JSON: {0}", json);

            // Method 2: Deserialize from UtfReader
            string path = "Data/users.json";
            byte[] jsonData = File.ReadAllBytes(path);
            Utf8JsonReader reader = new Utf8JsonReader(jsonData);
            root = JsonSerializer.Deserialize<RootObject<Result>>(ref reader);
            Console.WriteLine(root.Results.Length);
            Console.WriteLine(root.Results[1].FullName);
            Console.WriteLine(root.Results[1].LastLogin);

            // Method 3: Read asynchronously from a file
            // Read in the json file.
            path = "Data/users.json";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                root = JsonSerializer.DeserializeAsync<RootObject<Result>>(fs).Result;
                Console.WriteLine(root.Results.Length);
                Console.WriteLine(root.Results[2].FullName);
                Console.WriteLine(root.Results[2].LastLogin);
            }
        }

        public static void Utf8JsonWriter_Example()
        {

        }
        public static void Utf8JsonReader_Example()
        {
            Console.WriteLine("Begin Utf8JsonReader_Example");

            string path = "Data/users.json";
            byte[] jsonData = File.ReadAllBytes(path);
            Utf8JsonReader reader = new Utf8JsonReader(jsonData);

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 0
            Console.Write("{0} ", reader.CurrentDepth); // 0
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 0
            Console.WriteLine("{0}", reader.TokenType); //None

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 1
            Console.Write("{0} ", reader.CurrentDepth); // 0
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 0
            Console.WriteLine("{0}", reader.TokenType); //StartObject

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 12
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 4
            Console.WriteLine("{0}", reader.TokenType); // PropertyName

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 14
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 13
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0}", reader.GetUInt32()); // 9

            Console.WriteLine("No Skip()"); // Does not skip
            reader.Skip();

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 14
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 13
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0}", reader.GetUInt32()); // 9

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 25
            Console.Write("{0} ", reader.CurrentDepth); // 1 
            Console.Write("{0} ", reader.HasValueSequence); //False
            Console.Write("{0} ", reader.TokenStartIndex); // 18
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // next

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 30
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 26
            Console.WriteLine("{0}", reader.TokenType); // Null

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 45
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 34
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // previous

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 64
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 54
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // results

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 66
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 65
            Console.WriteLine("{0} ", reader.TokenType); // StartArray

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 72
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 71
            Console.WriteLine("{0} ", reader.TokenType); // StartObject

            Console.WriteLine(">> Skip() to next object {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 434
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 433
            Console.WriteLine("{0} ", reader.TokenType); // EndObject

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 441
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 440
            Console.WriteLine("{0} ", reader.TokenType); // StartObject

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 460
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 448
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // full_name

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 478
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 461
            Console.Write("{0} ", reader.TokenType); // String
            Console.WriteLine("{0}", reader.GetString()); // Schultz Burgess

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 499
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 486
            Console.WriteLine("{0} ", reader.TokenType); // PropertyName

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 509
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 500
            Console.Write("{0} ", reader.TokenType); // String
            Console.WriteLine("{0}", reader.GetString()); // Preston

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 509
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 500
            Console.Write("{0} ", reader.TokenType); // String
            Console.WriteLine("{0}", reader.GetString()); // Preston

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 509
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 500
            Console.Write("{0} ", reader.TokenType); // String
            Console.WriteLine("{0}", reader.GetString()); // Preston

            // Read the first token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 529
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 517
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // last_name

            Console.WriteLine(">> Skip() to next token {0} = Read()", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 541
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 530
            Console.Write("{0} ", reader.TokenType); // String
            Console.WriteLine("{0}", reader.GetString()); // Wilkinson

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 559
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 549
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // company

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            Console.WriteLine("> Read() {0}", reader.Read());
            Console.WriteLine("> Read() {0}", reader.Read());

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 641
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 628
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // last_login

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 663
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 642
            Console.Write("{0} ", reader.TokenType); // String
            Console.Write("{0} ", reader.GetString());
            Console.Write("{0} ", reader.GetDateTime());
            Console.WriteLine("{0} ", reader.GetDateTimeOffset());

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read()); // date_joined
            Console.WriteLine("> Read() {0}", reader.Read()); // date_joined value

            Console.WriteLine("> Read() {0}", reader.Read()); // groups
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 724
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 715
            Console.Write("{0} ", reader.TokenType); // PropertyName
            Console.WriteLine("{0}", reader.GetString()); // groups

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 726
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 725
            Console.WriteLine("{0} ", reader.TokenType); // StartArray

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read());
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 727
            Console.Write("{0} ", reader.CurrentDepth); // 4
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 726
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0} {1}", reader.TryGetInt32(out int value), value);

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 727
            Console.Write("{0} ", reader.CurrentDepth); // 4
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 726
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0} {1}", reader.TryGetInt32(out value), value);

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read()); //18
            Console.WriteLine("> Read() {0}", reader.Read()); // 35

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 735
            Console.Write("{0} ", reader.CurrentDepth); // 4
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 733
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0} {1}", reader.TryGetInt32(out value), value);

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 735
            Console.Write("{0} ", reader.CurrentDepth); // 4
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 733
            Console.Write("{0} ", reader.TokenType); // Number
            Console.WriteLine("{0} {1}", reader.TryGetInt32(out value), value);

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read()); // EndArray
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 736
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 735
            Console.WriteLine("{0} ", reader.TokenType); // EndArray

            // Read the next token
            Console.WriteLine("> Read() {0}", reader.Read()); // teams
            Console.WriteLine("> Read() {0}", reader.Read()); // BeginArray
            Console.WriteLine("> Read() {0}", reader.Read()); // EndArray
            Console.WriteLine("> Read() {0}", reader.Read()); // member_of
            Console.Write("{0} ", reader.BytesConsumed); // 775
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 763
            Console.WriteLine("{0} {1}", reader.TokenType, reader.GetString()); // PropertyName member_of

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 791
            Console.Write("{0} ", reader.CurrentDepth); // 3
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 790
            Console.WriteLine("{0} ", reader.TokenType); // EndArray

            Console.WriteLine("> Read() {0}", reader.Read()); // id
            Console.WriteLine("> Read() {0}", reader.Read()); // Number
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 812
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 811
            Console.WriteLine("{0} ", reader.TokenType); // EndObject

            Console.WriteLine(">> Skip() to next token {0}", reader.TrySkip()); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 812
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 811
            Console.WriteLine("{0} ", reader.TokenType); // EndObject

            Console.WriteLine("> Read() {0}", reader.Read()); // StartObject

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 819
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 818
            Console.WriteLine("{0} ", reader.TokenType); // StartObject

            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 1187
            Console.Write("{0} ", reader.CurrentDepth); // 2
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 1186
            Console.WriteLine("{0} ", reader.TokenType); // EndObject
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndArray
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 

            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 3385
            Console.Write("{0} ", reader.CurrentDepth); // 1
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 3384
            Console.WriteLine("{0} ", reader.TokenType); // EndArray

            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 3387
            Console.Write("{0} ", reader.CurrentDepth); // 0
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 3386
            Console.WriteLine("{0} ", reader.TokenType); // EndObject

            Console.WriteLine(">> Skip() to next token {0} {1} {2}", reader.TrySkip(), reader.TokenType, reader.TokenStartIndex); // Skips 
            Console.WriteLine("> Read() {0}", reader.Read()); // EndObject
            // Let's get some baselines
            Console.Write("{0} ", reader.BytesConsumed); // 3387
            Console.Write("{0} ", reader.CurrentDepth); // 0
            Console.Write("{0} ", reader.HasValueSequence); // False
            Console.Write("{0} ", reader.TokenStartIndex); // 3386
            Console.WriteLine("{0} ", reader.TokenType); // EndObject
        }

        /// <summary>
        /// Demonstration of the JsonElement
        /// </summary>
        public static async void JsonElement_Example()
        {
            JsonDocument document = JsonDocument_ParseAsync(new JsonDocumentOptions());
            JsonElement rootElement = document.RootElement;
            Console.WriteLine("{0}", rootElement.ValueKind); // Object

            // Get an individual property
            JsonElement element = rootElement.GetProperty("count");
            Console.WriteLine("{0}", element.ValueKind); // Number
            Console.WriteLine("{0}", element.GetUInt32()); // 9

            element = rootElement.GetProperty("next");
            Console.WriteLine("{0} {1}", element.ValueKind, element.GetRawText()); // Null
            try
            {
                Console.WriteLine("{0}", element.GetUInt32()); // Exception
            }

            catch { Console.WriteLine("Exception as expected"); }

            // You can only get properties that are at the same level as the JsonElement
            try
            {
                element = rootElement.GetProperty("full_name");
                Console.WriteLine("{0} {1}", element.ValueKind, element.GetRawText()); // Number
            }
            catch { Console.WriteLine("Exception as expected"); }

            JsonElement elementArray = rootElement.GetProperty("results");
            Console.WriteLine("{0} {1}", elementArray.ValueKind, elementArray.GetArrayLength()); // Array

            // We cannot access an individual property of an object from an Array
            try
            {
                element = elementArray.GetProperty("full_name");
                Console.WriteLine("{0} {1}", element.ValueKind, element.GetRawText()); // Number
            }
            catch { Console.WriteLine("Exception as expected"); }

            // Grab the first element (a JSON Object) in the array using LINQ.
            element = elementArray.EnumerateArray().First();
            Console.WriteLine("{0}", element.ValueKind); // Object

            // Reference an object in the array using Item[index]
            element = elementArray[1];
            Console.WriteLine("{0} {1}", element.ValueKind, element.GetRawText()); // Object
            
            // Access a property of the object.
            Console.WriteLine("{0} {1}", element.GetProperty("full_name").ValueKind, element.GetProperty("full_name").GetRawText()); // Number

            JsonElement.ObjectEnumerator objectEnumerator = rootElement.EnumerateObject();
            foreach (JsonProperty property in objectEnumerator)
            {
                //Console.WriteLine("Element: {0}: {1}", property.Name, property.Value);
            }
            //Console.WriteLine("Item[0] {0}", rootElement);
        }

        /// <summary>
        /// Demostration of JsonDocumentOptions
        /// </summary>
        public static void JsonDocumentOptions_Example()
        {
            // A simple json string
            string json = @"{""count"": 9, /* This is a comment */
      ""next"": null, // This is another comment
      ""previous"": null,
      ""results"": [
          {
            ""full_name"": ""Genevieve Mckenzie"",
            ""first_name"": ""Booker"",
            ""last_name"": ""Morrow"",
            ""company"": ""BIOTICA"",
            ""email"": ""booker.morrow@biotica.ca"",
            ""last_login"": ""2019-01-27T05:41:26"",
            ""date_joined"": ""2015-12-07T22:18:04"",
            ""groups"": [1, 18, 35],
            ""teams"": [],
            ""member_of"": [200, 219, 238],
            ""id"": 0
    },]}"; // Trailing comma

            JsonDocumentOptions options = new JsonDocumentOptions();
            // Default options
            try
            {
                // Should fail due to start of comment /*
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

            options.CommentHandling = JsonCommentHandling.Skip;
            try
            {
                // Should fail due to trailing comma.
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

            options.AllowTrailingCommas = true;
            try
            {
                // Success!
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

            try
            {
                options.CommentHandling = JsonCommentHandling.Allow;
                // Fails
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

            options.MaxDepth = 3;
            try
            {
                // Fails due to depth!
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

            options.MaxDepth = 4;
            try
            {
                // Success!
                JsonDocument.Parse(json, options);
                Console.WriteLine("Default options parsed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}:{1}", ex.GetType(), ex.Message);
            }

        }
        /// <summary>
        /// simple demonstration of the ParseAsync() method
        /// </summary>
        public static JsonDocument JsonDocument_ParseAsync(JsonDocumentOptions options)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;

            // Example usage
            Console.WriteLine("JsonDocument.ParseAsync()");
            // Read in the json file.
            string path = "Data/users.json";
            string jsonString = string.Empty;
            JsonDocument jsonDocument = null;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                jsonDocument = JsonDocument.ParseAsync(fs, options, cancellationToken: cancellationToken).Result;

                JsonElement rootElement = jsonDocument.RootElement;
                Console.WriteLine("Root Element ({0})", rootElement.ValueKind);
            }
            return jsonDocument;
        }

        /// <summary>
        /// Simple demonstration of Parse() method
        /// </summary>
        public static void JsonDocument_Parse(JsonDocumentOptions options)
        {
            // Example usage
            Console.WriteLine("JsonDocument.Parse(string)");

            // Read in the json file.
            string path = "Data/users.json";
            string jsonString = string.Empty;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fs, Encoding.UTF8))
                {
                    jsonString = streamReader.ReadToEnd();
                }
            }

            JsonDocument jsonDocument = JsonDocument.Parse(jsonString, options);
            JsonElement rootElement = jsonDocument.RootElement;
            Console.WriteLine("Root Element ({1}): {0}", rootElement, rootElement.ValueKind);

            // // Enumerate the object
            // JsonElement.ObjectEnumerator objectEnumerator = rootElement.EnumerateObject();
            // foreach (JsonProperty property in objectEnumerator)
            // {
            //     Console.WriteLine("Element: {0}: {1}", property.Name, property.Value);
            // }
            jsonDocument.Dispose();

            // Example usage
            Console.WriteLine("JsonDocument.Parse(Stream)");

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                jsonDocument = JsonDocument.Parse(fs);
            }

            rootElement = jsonDocument.RootElement;
            Console.WriteLine("Root Element ({1}): {0}", rootElement, rootElement.ValueKind);
            jsonDocument.Dispose();

        }
    }


}
