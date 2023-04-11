namespace Module4
{
    public static class Task9
    {
        #region Constants
        #region Messages
        private const string ConsoleDateInputRequestMessage = "Enter date in format dd.mm.yyyy hh:mm:ss: ";
        private const string ConsolIncorrectInputErrorMessage = "Incorrect input";
        private const string CommandsInfo =
            "Commands: exit - close apllication; rollback - roll back changes to specified date time";
        private const string TrackModOnMessage = "Track mod: on";
        private const string TrackModOffMessage = "Track mod: off";
        private const string IncorrectCommandMessage = "Incorrect command";
        private const string ExitCommand = "exit";
        private const string RollBackCommand = "rollback";
        #endregion
        #region Pathes
        private const string DataPath = @"E:\Data";
        private const string DataBackupsPath = @"E:\DataVersions";
        private const string txtExtension = "*.txt";
        #endregion
        #endregion
        #region Pathes
        private static readonly DirectoryInfo DataDirectory = new DirectoryInfo(DataPath);
        private static readonly DirectoryInfo DataBackupsDirectory = new DirectoryInfo(DataBackupsPath);
        #endregion

        public static void Run()
        {
            FileSystemWatcher watcher = InitializeSystemWatcher();
            Console.WriteLine(CommandsInfo);
            StartConsoleDialog(watcher);
        }

        private static void CopyAllTxtFilesIncludingSubdirectories(DirectoryInfo sourceDirectory,
                                                                   DirectoryInfo destinationDirectory)
        {
            foreach (var file in sourceDirectory.EnumerateFiles(txtExtension, SearchOption.AllDirectories))
            {
                if (file.Directory is DirectoryInfo)
                {
                    var relativePath = Path.GetRelativePath(sourceDirectory.FullName, file.Directory.FullName);
                    file.CopyTo(Path.Combine(destinationDirectory.CreateSubdirectory(relativePath).FullName, file.Name),
                                overwrite: true);
                }
            }
        }

        private static void StartConsoleDialog(FileSystemWatcher watcher)
        {
            while (true)
            {
                watcher.EnableRaisingEvents = true;
                Console.WriteLine(TrackModOnMessage);
                switch (Console.ReadLine())
                {
                    case ExitCommand:
                        return;
                    case RollBackCommand:
                        RollBackConsoleCommand(watcher);
                        continue;
                    default:
                        Console.WriteLine(IncorrectCommandMessage);
                        continue;
                }
            }
        }

        private static void RollBackConsoleCommand(FileSystemWatcher watcher)
        {
            watcher.EnableRaisingEvents = false;
            Console.WriteLine(TrackModOffMessage);
            DateTime dateTime = ReadDateTimeUntilCorrect(ConsoleDateInputRequestMessage, ConsolIncorrectInputErrorMessage);
            RollbackChanges(dateTime);
        }

        private static FileSystemWatcher InitializeSystemWatcher()
        {
            var watcher = new FileSystemWatcher(DataPath, txtExtension);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += WatcherChanged;
            watcher.Created += WatcherCreated;
            watcher.Deleted += WatcherDeleted;
            watcher.Renamed += WatcherRenamed;
            watcher.NotifyFilter = NotifyFilters.FileName;
            return watcher;
        }

        private static void WatcherRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was renamed", e.Name);
            CreateDataBackup();
        }

        private static void WatcherDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was deleted", e.Name);
            CreateDataBackup();
        }

        private static void WatcherCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was created", e.Name);
            CreateDataBackup();
        }

        private static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File \"{0}\" was changed", e.Name);
            CreateDataBackup();
        }

        private static void RollbackChanges(DateTime backupDateTime)
        {
            foreach (var backupDirectory in DataBackupsDirectory.EnumerateDirectories())
            {
                if (long.TryParse(backupDirectory.Name, out long fileID) && fileID >= IDFromTicks(backupDateTime))
                {
                    DeleteAllUnbackupedTxtFiles(backupDirectory);
                    CopyAllTxtFilesIncludingSubdirectories(backupDirectory, DataDirectory);
                    return;
                }
            }
        }

        private static void DeleteAllUnbackupedTxtFiles(DirectoryInfo backupDirectory)
        {
            HashSet<string> backupFilesRelativePathes = GetFilesRelativePathes(backupDirectory);
            foreach (var file in DataDirectory.EnumerateFiles("*txt", SearchOption.AllDirectories))
            {
                if (!backupFilesRelativePathes.Contains(Path.GetRelativePath(DataDirectory.FullName, file.FullName)))
                {
                    file.Delete();
                }
            }
        }

        private static HashSet<string> GetFilesRelativePathes(DirectoryInfo directory)
        {
            HashSet<string> filesRelativePathes = new HashSet<string>(directory.EnumerateFiles().Count());
            foreach (var file in directory.EnumerateFiles(txtExtension, SearchOption.AllDirectories))
            {
                filesRelativePathes.Add(Path.GetRelativePath(directory.FullName, file.FullName));
            }

            return filesRelativePathes;
        }

        private static void CreateDataBackup()
        {
            CopyAllTxtFilesIncludingSubdirectories(DataDirectory,
                DataBackupsDirectory.CreateSubdirectory(IDFromTicks(DateTime.Now).ToString()));
        }

        private static long IDFromTicks(DateTime dateTime)
        {
            return long.MaxValue - dateTime.Ticks;
        }

        private static DateTime ReadDateTimeUntilCorrect(string consoleInputRequestMessage, string consolErrorMessage)
        {
            while (true)
            {
                if (TryReadDateTime(consoleInputRequestMessage, consolErrorMessage, out DateTime dateTime))
                {
                    return dateTime;
                }
            }
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
