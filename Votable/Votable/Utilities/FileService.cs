using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Votable.Utilities
{
    public class FileService
    {
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
    }
}
