using Newtonsoft.Json.Linq;

namespace WebDriverDemo.Security
{
    public class Credentials
    {
        public JObject SecretsObject { get; set; }
        
        private string _secretsJson = File.ReadAllText("Secrets.json");

        public Credentials()
        {
            SecretsObject = JObject.Parse(_secretsJson);
        }
    }
}