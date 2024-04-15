using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SolutionBuilder.Model;

using System.Linq;

namespace SolutionBuilder
{
    public class CommandConcreteClassResolver : DefaultContractResolver
    {
        protected override JsonConverter? ResolveContractConverter(Type objectType)
        {
            if (typeof(Command).IsAssignableFrom(objectType) && !objectType.IsAbstract) { return null; }
            return base.ResolveContractConverter(objectType);
        }
    }

    public class CommandConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new() { ContractResolver = new CommandConcreteClassResolver(), };

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType) => objectType == typeof(Command);
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<Command?> commands = [];
            JArray jo = JArray.Load(reader);

            for (int i = 0; i < jo.Count; i++)
            {
                JToken token = jo[i];
                switch (token?["Type"]?.Value<string>())
                {
                    case "Replace":
                        {
                            commands.Add(JsonConvert.DeserializeObject<CommandReplace>(token.ToString(), SpecifiedSubclassConversion));
                        }
                        break;
                    case "Replace-CsProj":
                        {
                            commands.Add(JsonConvert.DeserializeObject<CommandReplaceCsProject>(token.ToString(), SpecifiedSubclassConversion));
                        }
                        break;
                    case "Replace-Properties":
                        {
                            CommandReplaceProperties? command = JsonConvert.DeserializeObject<CommandReplaceProperties>(token.ToString(), SpecifiedSubclassConversion);
                            if (command != null && command.Actions != null)
                            {
                                Dictionary<string, CommandAction> actions = new Dictionary<string, CommandAction>(command.Actions);

                                foreach (string key in actions.Keys)
                                {
                                    CommandAction action = command.Actions[key] with { File = "AssemblyInfo.cs" };
                                    command.Actions[key] = action;
                                }
                            }
                            commands.Add(command);
                        }
                        break;
                    case "Replace-References":
                        {
                            CommandReplaceReferences? command = JsonConvert.DeserializeObject<CommandReplaceReferences>(token.ToString(), SpecifiedSubclassConversion);
                            commands.Add(command);
                        }
                        break;
                    case "Insert-Reference":
                        {
                            CommandInsertReference? command = JsonConvert.DeserializeObject<CommandInsertReference>(token.ToString(), SpecifiedSubclassConversion);
                            commands.Add(command);
                        }
                        break;
                    case "Copy":
                        {
                            CommandCopy? command = JsonConvert.DeserializeObject<CommandCopy>(token.ToString(), SpecifiedSubclassConversion);
                            commands.Add(command);
                        }
                        break;
                    case "Build":
                        {
                            commands.Add(JsonConvert.DeserializeObject<CommandBuild>(token.ToString(), SpecifiedSubclassConversion));
                        }
                        break;
                    default: /* skip for now */ break;
                }
            }
            return commands;
        }
    }
}
