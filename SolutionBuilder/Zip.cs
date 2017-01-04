using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;

namespace SolutionBuilder
{
  /// <summary>
  /// Utility class for Zip Creation
  /// </summary>
 public class Zip
  {
    /// <summary>
    /// Create archive from root
    /// </summary>
    /// <param name="folder">Root</param>
    /// <param name="ZipFile">The file to create</param>
    /// <param name="useZip64">64 bit?</param>
    /// <returns>true or false</returns>
    public static Boolean CreateFileFromFolder(String folder, String ZipFile, Boolean useZip64 = false, Int32 CompressionLevel = 1)
    {
      if (String.IsNullOrEmpty(folder)) { throw new ArgumentNullException("folder"); }
      if (String.IsNullOrEmpty(ZipFile)) { throw new ArgumentNullException("ZipFile"); }
      ZipOutputStream oZipStream = null;

      if (CompressionLevel < 1) { CompressionLevel = 1; }
      if (CompressionLevel > 9) { CompressionLevel = 9; }

      try
      {
        oZipStream = new ZipOutputStream(File.Create(ZipFile));
        //oZipStream.UseZip64 = useZip64 ? UseZip64.On : UseZip64.Off;
        oZipStream.SetLevel(CompressionLevel);

        DirectoryInfo di = new DirectoryInfo(folder);
        ArrayList arr = GenerateFileList(di.FullName, new[] { ".log" });
        Int32 TrimLength = di.FullName.Length - di.Name.Length;

        ZipEntry oZipEntry = null;
        FileStream oStream = null;

        Byte[] oBuffer = null;

        foreach (String file in arr)
        {
          oZipEntry = new ZipEntry(file.Remove(0, TrimLength));
          oZipStream.PutNextEntry(oZipEntry);

          if (!file.EndsWith(@"/"))
          {
            oStream = File.OpenRead(file);
            oBuffer = new Byte[oStream.Length];
            oStream.Read(oBuffer, 0, oBuffer.Length);
            oZipStream.Write(oBuffer, 0, oBuffer.Length);
            oStream.Close();
            oStream.Dispose();
          }
        }
      }
      catch (Exception)
      {
        return false;
      }
      finally
      {
        oZipStream.Finish();
        oZipStream.Close();
      }

      return true;
    }

    /// <summary>
    /// Generate a list of all file starting from the root...
    /// </summary>
    /// <param name="directory">The directory</param>
    /// <returns>ArrayList of files</returns>
    public static ArrayList GenerateFileList(String directory, params String[] excludeFormat)
    {
      if (String.IsNullOrEmpty(directory)) { throw new ArgumentNullException("directory"); }

      Boolean empty = true;
      ArrayList files = new ArrayList();

      foreach (String file in Directory.GetFiles(directory))
      {
        if (excludeFormat != null && excludeFormat.Length > 0)
        {
          foreach (String e in excludeFormat)
          {
            if (!file.EndsWith(e, StringComparison.InvariantCultureIgnoreCase))
            {
              files.Add(file);
              empty = false;
            }
          }
        }
        else
        {
          files.Add(file);
          empty = false;
        }
      }

      if (empty)
      {
        if (Directory.GetDirectories(directory).Length == 0)
        {
          files.Add(directory + @"/");
        }
      }

      foreach (String dirs in Directory.GetDirectories(directory))
      {
        foreach (Object obj in GenerateFileList(dirs))
        {
          files.Add(obj);
        }
      }
      return files;
    }
  }


  //public interface IArchive
  //{
  //  Boolean Add(FileInfo fi);
  //  Boolean Add(DirectoryInfo di);
  //  Boolean Add(Stream stream);

  //  Boolean Archive();
  //}

  //class ZipArchive : IArchive
  //{
  //  public ZipArchive() { }

  //  public Boolean Add(FileInfo fi) { throw new NotImplementedException(); }
  //  public Boolean Add(DirectoryInfo di) { throw new NotImplementedException(); }
  //  public Boolean Add(Stream stream) { throw new NotImplementedException(); }

  //  public Boolean Archive() { throw new NotImplementedException(); }
  //}
}
