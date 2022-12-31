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

            return GetFiles(path);
        }

        private static List<FileInfo> GetFiles(string currentDirPath)
        {
            List <FileInfo> files = new List<FileInfo>();
            DirectoryInfo currentFolder = new DirectoryInfo(currentDirPath);

            //get all files in current directory
            FileInfo[] currentFiles = currentFolder.GetFiles();
            files.AddRange(currentFiles);
            //get all directories in current directory
            DirectoryInfo[] dirs = currentFolder.GetDirectories();

            //get all files from subDirectories
            foreach (var dir in dirs)
            {
                files.AddRange(GetFiles(dir.FullName));
            }

            return files;
        }
    }
    
}