using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Recursion
{
    public class FileFinder
    {
        public static List<FileInfo> Get(string path)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("there is no such directory");

            var files = new List<FileInfo>();
            GetFiles(path, files);
            return files;
        }

        private static void GetFiles(string currentDirPath, List<FileInfo> files)
        {
            DirectoryInfo currentFolder = new DirectoryInfo(currentDirPath);

            //get all files in current directory
            FileInfo[] currentFiles = currentFolder.GetFiles();
            files.AddRange(currentFiles);
            //get all directories in current directory
            DirectoryInfo[] dirs = currentFolder.GetDirectories();

            //get all files from subDirectories
            foreach (var dir in dirs)
            {
                GetFiles(dir.FullName, files);
            }
        }
    }
    
}