using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharp.Demo
{
    public class FilesGenerator
    {
        public static List<string> CopyFile(string filePath, int nb, string outputDir)
        {
            var dirInfo = new DirectoryInfo(outputDir);
            List<string> files = new List<string>();

            dirInfo.GetFiles().ToList().ForEach(f => f.Delete());

            for (int indexFile = 0; indexFile < nb; indexFile++)
            {
                string outPutFileName = Path.Combine(outputDir, string.Format("{0}.{1}{2}", Path.GetFileNameWithoutExtension(filePath), indexFile, Path.GetExtension(filePath)));
                File.Copy(filePath, outPutFileName, true);
            }

            return Directory.GetFiles(outputDir).ToList();
        }
    }
}