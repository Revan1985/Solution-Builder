
using Newtonsoft.Json;

namespace SolutionBuilder.Model
{
    public sealed class SolutionConfiguration
    {
        [JsonProperty("SourcePath", Order = 0, Required = Required.DisallowNull)]
        public string SourcePath { get; set; } = string.Empty;

        [JsonProperty("OutputPath", Order = 1, Required = Required.DisallowNull)]
        public string OutputPath { get; set; } = string.Empty;

        [JsonProperty("TemporaryPath", Order = 2, Required = Required.DisallowNull)]
        public string TemporaryPath { get; set; } = string.Empty;

        [JsonProperty("BuildConfiguration", Order = 3, Required = Required.DisallowNull)]
        public string Configuration { get; set; } = "Release";

        [JsonProperty("BuildPlatform", Order = 4, Required = Required.DisallowNull)]
        public string Platform { get; set; } = "AnyCpu";


        [JsonProperty("Configuration", Order = 2, Required = Required.Always)]
        public List<Installation> Installations
        {
            get;
            set;
        } = new ();

        public static bool TryParse(string json, out SolutionConfiguration? configuration)
        {
            configuration = default;
            if (string.IsNullOrEmpty(json)) { return false; }

            configuration = JsonConvert.DeserializeObject<SolutionConfiguration>(json);
            if (configuration == null) { return false; }

            foreach (Installation installation in configuration.Installations)
            {
                installation._parent = configuration;
                foreach (Command command in installation.Commands)
                {
                    command._parent = installation;
                }
            }

            return true;
        }
    }
}
