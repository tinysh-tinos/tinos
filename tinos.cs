using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiNOS
{
    internal class Program
    {
        static string main_path = @"C:\TiNOS";
        static string data_path = @"C:\TiNOS\data";
        static string data_user_path = Path.Combine(data_path, "data.csv");
        static string current_path = main_path;
        static string currentUser = null;

        static void Main(string[] args)
        {
            Console.WriteLine("TINOS");

            if (!Directory.Exists(main_path) || !File.Exists(data_user_path))
            {
                Setup();
            }
            else
            {
                Login();
            }

            while (true)
            {
                Console.Write(current_path + " > ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();
                string[] arguments = parts.Skip(1).ToArray();

                switch (command)
                {
                    case "help":
                        Help();
                        break;
                    case "clear":
                    case "cls":
                        Console.Clear();
                        break;
                    case "echo":
                        Echo(arguments);
                        break;
                    case "whoami":
                        WhoAmI();
                        break;
                    case "date":
                        Date();
                        break;
                    case "time":
                        Time();
                        break;
                    case "dir":
                    case "ls":
                        Dir(arguments);
                        break;
                    case "cd":
                        Cd(arguments);
                        break;
                    case "mkdir":
                        Mkdir(arguments);
                        break;
                    case "rmdir":
                        Rmdir(arguments);
                        break;
                    case "del":
                    case "rm":
                        Del(arguments);
                        break;
                    case "create":
                        Create(arguments);
                        break;
                    case "read":
                        Read(arguments);
                        break;
                    case "write":
                        Write(arguments);
                        break;
                    case "open":
                        Open(arguments);
                        break;
                    case "start":
                        Start(arguments);
                        break;
                    case "edit":
                        Edit(arguments);
                        break;
                    case "move":
                        Move(arguments);
                        break;
                    case "copy":
                        Copy(arguments);
                        break;
                    case "find":
                        Find(arguments);
                        break;
                    case "shutdown":
                        Shutdown();
                        return;
                    case "reboot":
                        Reboot();
                        return;
                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }

        static void Setup()
        {
            Directory.CreateDirectory(main_path);
            Directory.CreateDirectory(data_path);

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(data_user_path, false, Encoding.UTF8))
            {
                writer.WriteLine("Username;Password");
                writer.WriteLine(username + ";" + password);
            }

            currentUser = username;
        }

        static void Login()
        {
            while (true)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                string[] lines = File.ReadAllLines(data_user_path);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(';');
                    if (data[0] == username && data[1] == password)
                    {
                        currentUser = username;
                        return;
                    }
                }
                Console.WriteLine("Invalid username or password.");
            }
        }

        static void Help()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help      - Show help");
            Console.WriteLine("  clear/cls - Clear screen");
            Console.WriteLine("  echo      - Print text");
            Console.WriteLine("  whoami    - Show current user");
            Console.WriteLine("  date      - Show date");
            Console.WriteLine("  time      - Show time");
            Console.WriteLine("  dir/ls    - List directory contents");
            Console.WriteLine("  cd        - Change directory");
            Console.WriteLine("  mkdir     - Create directory");
            Console.WriteLine("  rmdir     - Remove directory");
            Console.WriteLine("  del/rm    - Delete file");
            Console.WriteLine("  create    - Create file");
            Console.WriteLine("  read      - Read file");
            Console.WriteLine("  write     - Write to file");
            Console.WriteLine("  open      - Open file with default program");
            Console.WriteLine("  start     - Open file with specified program");
            Console.WriteLine("  edit      - Open file in Notepad");
            Console.WriteLine("  move      - Move file/directory");
            Console.WriteLine("  copy      - Copy file");
            Console.WriteLine("  find      - Search for files");
            Console.WriteLine("  shutdown  - Shut down the system");
            Console.WriteLine("  reboot    - Reboot the system");
        }

        static void Echo(string[] args)
        {
            Console.WriteLine(string.Join(" ", args));
        }

        static void WhoAmI()
        {
            Console.WriteLine(currentUser);
        }

        static void Date()
        {
            Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
        }

        static void Time()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

        static void Dir(string[] args)
        {
            string path = args.Length > 0 ? ResolvePath(args[0]) : current_path;

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory not found.");
                return;
            }

            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (string dir in dirs)
            {
                Console.WriteLine("[DIR]  " + Path.GetFileName(dir));
            }
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                Console.WriteLine("[FILE] " + Path.GetFileName(file) + " (" + fi.Length + " bytes)");
            }
        }

        static void Cd(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(current_path);
                return;
            }

            string newPath = ResolvePath(args[0]);

            if (Directory.Exists(newPath))
            {
                current_path = newPath;
            }
            else
            {
                Console.WriteLine("Directory not found.");
            }
        }

        static void Mkdir(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify directory name.");
                return;
            }

            string path = ResolvePath(args[0]);
            Directory.CreateDirectory(path);
        }

        static void Rmdir(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify directory name.");
                return;
            }

            string path = ResolvePath(args[0]);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                Console.WriteLine("Directory not found.");
            }
        }

        static void Del(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify file name.");
                return;
            }

            string path = ResolvePath(args[0]);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static void Create(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify file name.");
                return;
            }

            string path = ResolvePath(args[0]);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                Console.WriteLine("File created.");
            }
            else
            {
                Console.WriteLine("File already exists.");
            }
        }

        static void Read(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify file name.");
                return;
            }

            string path = ResolvePath(args[0]);

            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static void Write(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: write <file> <text>");
                return;
            }

            string path = ResolvePath(args[0]);
            string text = string.Join(" ", args.Skip(1));

            File.WriteAllText(path, text);
            Console.WriteLine("Text written.");
        }

        static void Open(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: open <file>");
                return;
            }

            string path = ResolvePath(args[0]);

            if (File.Exists(path))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                    Console.WriteLine("Opening file...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error opening file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static void Start(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: start <program> <file>");
                return;
            }

            string program = args[0];
            string path = ResolvePath(args[1]);

            if (File.Exists(path))
            {
                try
                {
                    Process.Start(program, path);
                    Console.WriteLine("Opening file with " + program + "...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error opening file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static void Edit(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: edit <file>");
                return;
            }

            string path = ResolvePath(args[0]);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            try
            {
                Process.Start("notepad.exe", path);
                Console.WriteLine("Opening in Notepad...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening Notepad: " + ex.Message);
            }
        }

        static void Move(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: move <source> <destination>");
                return;
            }

            string source = ResolvePath(args[0]);
            string dest = ResolvePath(args[1]);

            if (File.Exists(source))
            {
                File.Move(source, dest);
                Console.WriteLine("File moved.");
            }
            else if (Directory.Exists(source))
            {
                Directory.Move(source, dest);
                Console.WriteLine("Directory moved.");
            }
            else
            {
                Console.WriteLine("Source not found.");
            }
        }

        static void Copy(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: copy <source> <destination>");
                return;
            }

            string source = ResolvePath(args[0]);
            string dest = ResolvePath(args[1]);

            if (File.Exists(source))
            {
                File.Copy(source, dest, true);
                Console.WriteLine("File copied.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        static void Find(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify file name to search for.");
                return;
            }

            string searchPattern = args[0];
            string[] files = Directory.GetFiles(current_path, searchPattern, SearchOption.AllDirectories);

            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            if (files.Length == 0)
            {
                Console.WriteLine("No files found.");
            }
        }

        static void Shutdown()
        {
            Console.WriteLine("System shutting down...");
        }

        static void Reboot()
        {
            Console.WriteLine("Rebooting system...");
            current_path = main_path;
        }

        static string ResolvePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            return Path.GetFullPath(Path.Combine(current_path, path));
        }
    }
}
