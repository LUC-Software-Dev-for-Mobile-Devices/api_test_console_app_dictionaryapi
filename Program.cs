using System.Net.Http.Headers;
using System.Text.Json;

internal class Program
{
    List<RootClass> dictionaryData = new List<RootClass>();
    static string lookUpWord;
    private static void Main(string[] args)
    {
       Task.Run(async () =>
         {
             var result = await getDictionary();
             //Console.WriteLine(result);

         }).Wait();
    }

    public static async Task<string> getDictionary()
    {

        Console.WriteLine("Please enter the word you would like to look up?");
        lookUpWord = Console.ReadLine();
        
        string URL = $"https://www.dictionaryapi.com/api/v3/references/sd2/json/{lookUpWord}?key=003be8fe-2207-4a5e-85f8-15e8a9ac542b"; //String interpolation

        HttpClient httpClient = new HttpClient();


        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(URL); //Sends a GET Request 
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.api+json");
            string jsonContent = await response.Content.ReadAsStringAsync();
            //List<Card> cardList = JsonSerializer.Deserialize<List<Card>>(jsonContent);

            List<RootClass> returnedData = JsonSerializer.Deserialize<List<RootClass>>(jsonContent);  //Returns an array of objects

            
            //Only pulling data for the first object
            Console.WriteLine("Offensive:" + returnedData[0].meta.offensive); // offensive or not
            Console.WriteLine(returnedData[0].shortdef[0]);  //Short Definition


            return jsonContent;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;

        }
    }




    public class RootClass
    {
        public Meta meta { get; set; }
        public int hom { get; set; }
        public Hwi hwi { get; set; }
        public string fl { get; set; }
        public List<Def> def { get; set; }
        public History history { get; set; }
        public List<string> shortdef { get; set; }
        public List<In> ins { get; set; }
    }


    public class Def
    {
        public List<List<List<object>>> sseq { get; set; }
    }

    public class History
    {
        public string pl { get; set; }
        public List<List<string>> pt { get; set; }
    }

    public class Hwi
    {
        public string hw { get; set; }
        public List<Pr> prs { get; set; }
    }

    public class In
    {
        public string @if { get; set; }
    }

    public class Meta
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string sort { get; set; }
        public string src { get; set; }
        public string section { get; set; }
        public List<string> stems { get; set; }
        public bool offensive { get; set; }
    }

    public class Pr
    {
        public string mw { get; set; }
        public Sound sound { get; set; }
    }



    public class Sound
    {
        public string audio { get; set; }
    }
}