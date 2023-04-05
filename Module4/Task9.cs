using System;

namespace Module4
{
    public static class Task9
    {
        #region Constants
        #region Messages
        private const string consoleDateInputRequestMessage = "Enter date in format dd.mm.yyyy hh:mm:ss: ";
        private const string consolIncorrectInputErrorMessage = "Incorrect input";
        private const string commandsInfo = 
            "Commands: exit - close apllication; rollback - roll back changes to specified date time";
        private const string trackModOnMessage = "Track mod: on";
        private const string trackModOffMessage = "Track mod: off";
        private const string incorrectCommandMessage = "Incorrect command";
        private const string rollBackFailMessage = "Cannot roll back changes for: ";
        private const string exitCommand = "exit";
        private const string rollBackCommand = "rollback";
        #endregion
        #region Directories, pathes
        private const string dataPath = @"E:\Data";
        private static readonly DirectoryInfo dataVersionsDirectory = new DirectoryInfo(@"E:\DataVersions");
        #endregion
        #endregion

        public static void Run()
        {
            FileSystemWatcher watcher = InitializeSystemWatcher();
            Console.WriteLine(commandsInfo);
            StartConsoleDialog(watcher);
        }

        private static void StartConsoleDialog(FileSystemWatcher watcher)
        {
            while (true)
            {
                Console.WriteLine(trackModOnMessage);
                string? command = Console.ReadLine();
                switch (command)
                {
                    case null:
                        Console.WriteLine(incorrectCommandMessage);
                        continue;
                    case exitCommand:
                        return;
                    case rollBackCommand:
                        RollBackConsoleCommand(watcher);
                        continue;
                    default:
                        Console.WriteLine(incorrectCommandMessage);
                        continue;
                }
            }
        }

        private static void RollBackConsoleCommand(FileSystemWatcher watcher)
        {
            Console.WriteLine(trackModOffMessage);
            ReadDateTimeUntilCorrect(consoleDateInputRequestMessage, consolIncorrectInputErrorMessage, out DateTime dateTime);
            RollbackChanges(dateTime, watcher);
        }

        private static FileSystemWatcher InitializeSystemWatcher()
        {
            var watcher = new FileSystemWatcher(dataPath, "*.txt");
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += WatcherChanged;
            watcher.Created += WatcherCreated;
            return watcher;
        }

        private static void WatcherCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was created", e.Name);
            CreateFileBackup(sender, e);
        }

        private static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was changed", e.Name);
            CreateFileBackup(sender, e);
        }

        private static void RollbackChanges(DateTime dateTime, FileSystemWatcher watcher)
        {
            watcher.EnableRaisingEvents = false;
            foreach (var file in Directory.EnumerateFiles(dataPath))
            {
                RollBackChanges(file, dateTime);
            }
            watcher.EnableRaisingEvents = true;
        }

        private static void CreateFileBackup(object sender, FileSystemEventArgs e)
        {
            var fileVersionsDirectory = 
                dataVersionsDirectory.CreateSubdirectory(Path.GetFileNameWithoutExtension(e.FullPath));
            File.Copy(e.FullPath, fileVersionsDirectory.FullName + @"\" + 
                                  IDFromDate(DateTime.Now).ToString() + 
                                  Path.GetExtension(e.FullPath));
        }

        private static void RollBackChanges(string filePath, DateTime backupDateTime)
        {
            var fileVersionsDirectoryPath = dataVersionsDirectory.FullName + @"\" +
                Path.GetFileNameWithoutExtension(filePath);
            try
            {
                foreach (var fileVersion in Directory.EnumerateFiles(fileVersionsDirectoryPath))
                {
                    if (long.TryParse(Path.GetFileNameWithoutExtension(fileVersion), out long fileID) &&
                        fileID >= IDFromDate(backupDateTime))
                    {
                        File.Copy(fileVersion, filePath, true);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine(rollBackFailMessage + filePath);
            }
        }

        private static long IDFromDate(DateTime dateTime)
        {
            return long.MaxValue - dateTime.Ticks;
        }

        private static void ReadDateTimeUntilCorrect(string consoleInputRequestMessage, string consolErrorMessage,
            out DateTime dateTime)
        {
            while (!TryReadDateTime(consoleInputRequestMessage, consolErrorMessage, out dateTime)) ;
        }

        private static bool TryReadDateTime(string consoleInputRequestMessage, string consolErrorMessage, 
            out DateTime dateTime)
        {
            Console.Write(consoleInputRequestMessage);
            if (DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                return true;
            }
            Console.Write(consolErrorMessage);
            return false;
        }

    }
}
