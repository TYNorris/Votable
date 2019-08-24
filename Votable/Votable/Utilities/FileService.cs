using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Votable.Utilities
{
    public class FileService
    {
        private static string AppDataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private Assembly assembly { get; set; }
        public FileService()
        {

            assembly = IntrospectionExtensions.GetTypeInfo(typeof(FileService)).Assembly;
        }

        public string GetEmbeddedResource(string filename)
        {
            try
            {
                Stream stream = assembly.GetManifestResourceStream("Votable." + filename);
                string text = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch(FileNotFoundException)
            {
                return null;
            }
        }


        public void WriteToFile(string filename, string content)
        {
            File.WriteAllText(Path.Combine(AppDataPath, filename), content);
        }
        public string ReadFile(string filename)
        {
            var path = Path.Combine(AppDataPath, filename);
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return null;
        }
    }
}
