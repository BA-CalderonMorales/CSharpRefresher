using System.Text;

namespace Fundamentals.DirectoriesAndFiles
{
    class DirectoriesAndFiles
    {
        public static void Lesson()
        {
            // InternalLesson1();
            // InternalLesson2();
            // InternalLesson3();
            // InternalLesson4();
            // InternalLesson5();
            InternalLesson6();
        }

        public static void InternalLesson1()
        {
            // start with admin rights if errors appear
            DirectoryInfo currDir = new DirectoryInfo(".");
            DirectoryInfo myDir = new DirectoryInfo(@"C:\Users\bacm6");

            Console.WriteLine(myDir.FullName);
            Console.WriteLine(myDir.Name);
            Console.WriteLine(myDir.Parent);
            Console.WriteLine(myDir.Attributes);
            Console.WriteLine(myDir.CreationTime);

            DirectoryInfo dataDir = new DirectoryInfo(@"C:\Users\bacm6\source\repos\Fundamentals\DirectoriesAndFiles\MyNewDir");
            dataDir.Create();
            Directory.Delete(@"C:\Users\bacm6\source\repos\Fundamentals\DirectoriesAndFiles"
                , true); // true flag helps with deleting folders with files/sub folders

        }

        private static void InternalLesson2()
        {
            string[] customers =
            {
                "Bob Smith",
                "Sally Smith",
                "Robert Smith"
            };
            string textFilePath = @"C:\Users\bacm6\source\repos\Fundamentals\Fundamentals\DirectoriesAndFiles\testfile1.txt";
            File.WriteAllLines(textFilePath, customers);

            foreach (string cust in File.ReadAllLines(textFilePath))
            {
                Console.WriteLine($"Customer {cust}");
            }
        }
        private static void InternalLesson3()
        {
            DirectoryInfo myDataDir = new DirectoryInfo(@"C:\Users\bacm6\source\repos\Fundamentals\Fundamentals\DirectoriesAndFiles");

            FileInfo[] txtFiles = myDataDir.GetFiles("*.txt",
                SearchOption.AllDirectories);
            Console.WriteLine("Matches : {0}", txtFiles.Length);

            foreach (FileInfo file in txtFiles)
            {
                Console.WriteLine(file.Name);
                Console.WriteLine(file.Length);
            }
        }

        private static void InternalLesson4()
        {
            // couldn't copy directly into \Fundamentals\Fundamentals\Directories, even in admin mode, but this path works.
            string textFilePath = @"C:\Users\bacm6\source\repos\Fundamentals\DirectoriesAndFiles"; // maybe because it was open?
            FileStream fs = File.Open(textFilePath,
                FileMode.Create);
            string randString = "This is a random string";
            byte[] rsByteArray = Encoding.Default.GetBytes(randString);

            fs.Write(rsByteArray, 0, rsByteArray.Length);
            fs.Position = 0;

            byte[] fileByteArray = new byte[rsByteArray.Length];

            for (int i = 0; i < rsByteArray.Length; i++)
            {
                fileByteArray[i] = (byte)fs.ReadByte();
            }

            Console.WriteLine(Encoding.Default.GetString(fileByteArray));

            fs.Close();
        }

        private static void InternalLesson5()
        {
            // same issue here as with FileStream class. But it works with a path outside of the current project.
            string textFilePath = @"C:\Users\bacm6\source\repos\Fundamentals\DirectoriesAndFiles";
            StreamWriter sw = new StreamWriter(textFilePath);

            sw.Write("This is a random ");
            sw.WriteLine("sentence.");
            sw.WriteLine("This is another sentence.");

            sw.Close();

            StreamReader sr = new StreamReader(textFilePath);
            Console.WriteLine("Peek : {0}",
                Convert.ToChar(sr.Peek()));
            Console.WriteLine("1st String : {0}",
                sr.ReadLine());
            Console.WriteLine("Everything Else : {0}",
                sr.ReadToEnd());
        }

        private static void InternalLesson6()
        {
            string textFilePath = @"C:\Users\bacm6\source\repos\Fundamentals\DirectoriesAndFiles";
            FileInfo datFile = new FileInfo(textFilePath);
            BinaryWriter bw = new BinaryWriter(datFile.OpenWrite());
            string randomText = "Some more random text.";
            int myAge = 28;
            double height = 5.7;
            bw.Write(randomText);
            bw.Write(myAge);
            bw.Write(height);
            bw.Close();

            BinaryReader br = new BinaryReader(datFile.OpenRead());
            Console.WriteLine(br.ReadString());
            Console.WriteLine(br.ReadInt32());
            Console.WriteLine(br.ReadDouble());
            br.Close();
        }
    }
}
