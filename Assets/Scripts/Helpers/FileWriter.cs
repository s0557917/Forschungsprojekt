using System.IO;
using System.Threading.Tasks;

namespace VrPassing.Helpers
{
    public class FileWriter
    {
        public static void WriteToFile(string path, string stringToSerialize)
        {
            FileStream appendStream = File.Open(path, FileMode.Append);

            using (StreamWriter outputFile = new StreamWriter(appendStream))
            {
                outputFile.Write(stringToSerialize);
            }
        }
    }
}
