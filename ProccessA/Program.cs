using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using Share;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    // Process A:
    static void Main(string[] args)
    {
        using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
        {

            bool mutexCreated;
            var franco = new Person("franco Pettigrosso", 26);
            Mutex mutex = new Mutex(true, "testmapmutex", out mutexCreated);
            IFormatter Formatter =  new BinaryFormatter();

            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(1);
            }
                //https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
                //https://www.red-gate.com/simple-talk/dotnet/net-development/sharing-caring-using-memory-mapped-files-net/

            mutex.ReleaseMutex();

            Console.WriteLine("Start Process B and press ENTER to continue.");
            Console.ReadLine();

            Console.WriteLine("Start Process C and press ENTER to continue.");
            Console.ReadLine();

            mutex.WaitOne();
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                Console.WriteLine("Process A says: {0}", reader.ReadBoolean());
                Console.WriteLine("Process B says: {0}", reader.ReadBoolean());
                Console.WriteLine("Process C says: {0}", reader.ReadBoolean());
            }
            mutex.ReleaseMutex();
        }
    }
}