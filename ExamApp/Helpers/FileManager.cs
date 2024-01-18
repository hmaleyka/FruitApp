using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExamApp.Helpers
{
    public static class FileManager
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckLong(this IFormFile file, int length)
        { 
             return file.Length <=length;
        }
        public static string Upload(this IFormFile file, string envpath, string foldername)
        {
            string filename = file.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length-64);
            }
            filename = Guid.NewGuid().ToString() + filename;

            string path = envpath + foldername + filename;

            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filename;
        }
    }
}
