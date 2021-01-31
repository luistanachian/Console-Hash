using System;
using System.IO;
using System.Security.Cryptography;

namespace HashTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test of Hash in C# with MD5");
            Console.WriteLine(string.Empty);

            var directory = new DirectoryInfo(Environment.CurrentDirectory);
            var files = directory.GetFiles();

            Console.WriteLine("Test of Hash with Stream");
            Console.WriteLine("------------------");
            foreach (var file in files)
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var strHash = Hash.Get(stream);
                    Console.WriteLine($"{file.Name} => {strHash}");
                }
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine("Test of Hash with byte[]");
            Console.WriteLine("------------------");
            foreach (var file in files)
            {
                byte[] stream = File.ReadAllBytes(file.FullName);
                var strHash = Hash.Get(stream);
                Console.WriteLine($"{file.Name} => {strHash}");
            }
            Console.Read();
        }
    }

    public static class Hash
    {
        public static string Get(Stream stream)
        {
            var strHash = string.Empty;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(stream);
                strHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            return strHash;
        }
        public static string Get(byte[] stream)
        {
            var strHash = string.Empty;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(stream);
                strHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            return strHash;
        }
    }
}
