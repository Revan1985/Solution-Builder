using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  /// <summary>
  /// Root node for Configuration xml
  /// </summary>
  [Serializable()]
  [XmlRoot("Installations")]
  public partial class Configuration
  {
    /// <summary>
    /// Output (where file must be written)
    /// </summary>
    [XmlAttribute("OutputPath")]
    public String Output { get; set; }

    /// <summary>
    /// Source (root folder)
    /// </summary>
    [XmlAttribute("SourcePath")]
    public String Source { get; set; }

    /// <summary>
    /// List of available installations
    /// </summary>
    [XmlElement("Installation")]
    public List<Installation> Installations { get; set; }

    /// <summary>
    /// Load the configuration from an xml.
    /// </summary>
    /// <param name="path">The file to load</param>
    /// <returns>This class if succedded, otherwise null</returns>
    public static Configuration Load(String path)
    {
      String error;
      return Configuration.Load(path, out error);
    }

    /// <summary>
    /// Load the configuration from an xml.
    /// </summary>
    /// <param name="path">The file to load</param>
    /// <param name="error">Any error as out parameter</param>
    /// <returns>This class if succedded, otherwise null</returns>
    public static Configuration Load(String path, out String error)
    {
      Configuration obj = null;
      if (String.IsNullOrEmpty(path)) { throw new ArgumentNullException("path", "path cannot be null"); }
      if (!File.Exists(path)) { throw new FileNotFoundException("Config File not found", path); }
      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
        using (TextReader reader = new StreamReader(path))
        {
          try
          {
            obj = (Configuration)serializer.Deserialize(reader);
            reader.Close();
          }
          catch (Exception e)
          {
            error = e.ToString();
            return null;
          }
        }

        if (obj != null)
        {
          foreach (Installation installation in obj.Installations)
          {
            installation.SetParent(obj);
          }
        }
      }
      catch (Exception e)
      {
        error = e.ToString();
        return null;
      }
      error = String.Empty;
      return obj;
    }
    /// <summary>
    /// Save the Configuration to an xml
    /// </summary>
    /// <param name="path">The file to save</param>
    /// <param name="configuration">Configuration to save</param>
    /// <param name="error">Any error found during saving (populated only on fail)</param>
    /// <returns>True if succedded, false otherwise</returns>
    public static Boolean Save(String path, Configuration configuration, out String error)
    {
      Boolean result = true;
      error = String.Empty;

      if (String.IsNullOrEmpty(path)) { throw new ArgumentNullException("path", "path cannot be null"); }
      if (configuration == null) { throw new ArgumentNullException("configuration", "configuration cannot be null"); }

      XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
      using (TextWriter writer = new StreamWriter(path))
      {
        try
        {
          XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
          ns.Add(String.Empty, String.Empty);

          serializer.Serialize(writer, configuration, ns);
        }
        catch (Exception ex)
        {
          error = ex.Message;
          result = false;
        }
      }

      return result;
    }
  }
}