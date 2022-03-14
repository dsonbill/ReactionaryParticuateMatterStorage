using System;
using System.Collections.Generic;
using JsonFx.Json;
using System.Runtime.Serialization;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;
using MoonSharp.Interpreter;
using System.Threading;
using UnityEngine;

namespace Computers.Hardware
{
    public class HardDrive
    {
        public int driveNumber { get; private set; }
        public string driveLabel = "";
        public string guid;
        public string filePath { get; set; }
        public string currentWorkingDirectory { get; private set; }
        private JsonReader reader;
        private JsonWriter writer;
        private Directory rootDirectory = new Directory();
        private Directory currentWorkingDirectoryObject;
        private object driveLock = new object();
        private HardDriveWriteOperation writeOp;

        [MoonSharpHidden]
        public HardDrive(int driveNumber, string guid)
        {
            this.driveNumber = driveNumber;
            this.guid = guid;
        }

        [MoonSharpHidden]
        public void Initialize()
        {
            reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
            writer = new JsonWriter(new DataWriterSettings(new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.NoChange, "-")));
            if (filePath == string.Empty) filePath = System.IO.Path.Combine(System.IO.Path.Combine(Environment.CurrentDirectory, "Drives"), guid + ".json");

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    rootDirectory = reader.Read<Directory>(System.IO.File.ReadAllText(filePath));
                }
                catch (Exception e)
                {
                    Debug.Log("Exception while trying to load computer drive file!");
                    rootDirectory.Init(driveNumber.ToString() + ":");
                }
            }
            else rootDirectory.Init(driveNumber.ToString() + ":");

            currentWorkingDirectory = driveNumber.ToString() + ":";
            currentWorkingDirectoryObject = rootDirectory;

            writeOp = new HardDriveWriteOperation();
            writeOp.writeAction = WriteToDisk;
        }

        public string GetFullPath(string relativePath)
        {
            lock (driveLock)
            {
                string path = "";
                if (relativePath.StartsWith("/")) path = relativePath.Substring(1, relativePath.Length - 1);
                else path = relativePath;
                string currentCurrentWorkingDirectory = currentWorkingDirectory;
                currentCurrentWorkingDirectory = HandleDirectoryUp(currentCurrentWorkingDirectory, relativePath, out path);
                if (path != "") path = currentCurrentWorkingDirectory + "/" + path;
                else path = currentCurrentWorkingDirectory;
                return path;
            }
        }

        public string HandleDirectoryUp(string currentDir, string relativePath, out string relativePathOut)
        {
            //if (relativePath.StartsWith("..") && currentDir.Split("/".ToCharArray()).Length > 1)
            //{
            //    string[] parts = currentDir.Split("/".ToCharArray());
            //    currentDir = parts[0];
            //    for (int i = 1; i < parts.Length - 1; i++)
            //    {
            //        currentDir += parts[i];
            //    }
            //}
            //return currentDir;
            lock (driveLock)
            {
                bool manipulated = true;
                string resultDirectory = currentDir;
                relativePathOut = relativePath;
                while (manipulated)
                {
                    resultDirectory = HandleDirectoryUp(resultDirectory, relativePathOut, out manipulated);
                    if (manipulated)
                    {
                        if (relativePathOut.Length > 2) relativePathOut = relativePathOut.Remove(0, 3);
                        else relativePathOut = relativePathOut.Remove(0, 2);
                    }
                }
                return resultDirectory;
            }
        }

        private string HandleDirectoryUp(string currentDir, string relativePath, out bool manipulated)
        {
            lock (driveLock)
            {
                if (relativePath.StartsWith("..") && currentDir.Split("/".ToCharArray()).Length > 1)
                {
                    string[] parts = currentDir.Split("/".ToCharArray());
                    currentDir = parts[0];
                    for (int i = 1; i < parts.Length - 1; i++)
                    {
                        currentDir += "/" + parts[i];
                    }
                    manipulated = true;
                }
                else manipulated = false;
                return currentDir;
            }
        }

        public void ChangeDirectory(string fullPath)
        {
            lock (driveLock)
            {
                if (IsDirectory(fullPath))
                {
                    currentWorkingDirectory = fullPath;
                    currentWorkingDirectoryObject = GetObjectFromPath(fullPath, false) as Directory;
                }
            }
        }

        public void AddDirectory(string fullPath)
        {
            lock (driveLock)
            {
                if (!Exists(fullPath) && ParentExists(fullPath))
                {
                    string[] paths = fullPath.Split("/".ToCharArray());
                    string name = paths[paths.Length - 1];
                    Directory parent = GetObjectFromPath(fullPath, true) as Directory;
                    Directory newdir = new Directory();
                    newdir.Init(name);
                    parent.dirs.Add(name, newdir);
                    HardDriveWriteOperator.Instance.QueueOperation(writeOp);
                }
            }
        }

        public void DeleteDirectory(string fullPath)
        {
            lock (driveLock)
            {
                if (IsDirectory(fullPath))
                {
                    string[] paths = fullPath.Split("/".ToCharArray());
                    string name = paths[paths.Length - 1];
                    Directory parent = GetObjectFromPath(fullPath, true) as Directory;
                    parent.dirs.Remove(name);
                    HardDriveWriteOperator.Instance.QueueOperation(writeOp);
                }
            }
        }

        public void WriteFile(string fullPath, string file)
        {
            lock (driveLock)
            {
                if (ParentExists(fullPath) && !IsDirectory(fullPath))
                {
                    string[] paths = fullPath.Split("/".ToCharArray());
                    string name = paths[paths.Length - 1];
                    Directory parent = GetObjectFromPath(fullPath, true) as Directory;
                    parent.files[name] = file;
                    HardDriveWriteOperator.Instance.QueueOperation(writeOp);
                }
            }
        }

        public void DeleteFile(string fullPath)
        {
            lock (driveLock)
            {
                if (IsFile(fullPath))
                {
                    string[] paths = fullPath.Split("/".ToCharArray());
                    string name = paths[paths.Length - 1];
                    Directory parent = GetObjectFromPath(fullPath, true) as Directory;
                    parent.files.Remove(name);
                    HardDriveWriteOperator.Instance.QueueOperation(writeOp);
                }
            }
        }

        public string ReadFile(string fullPath)
        {
            lock (driveLock)
            {
                if (IsFile(fullPath))
                {
                    return GetObjectFromPath(fullPath, true) as string;
                }
                return null;
            }
        }

        public bool IsFile(string fullPath)
        {
            lock (driveLock)
            {
                object obj = GetObjectFromPath(fullPath, false);
                if (obj == null) return false;
                return (obj.GetType() == typeof(string));
            }
        }

        public bool IsDirectory(string fullPath)
        {
            lock (driveLock)
            {
                object obj = GetObjectFromPath(fullPath, false);
                if (obj == null) return false;
                return (obj.GetType() == typeof(Directory));
            }
        }

        public bool Exists(string fullPath)
        {
            lock (driveLock)
            {
                return (GetObjectFromPath(fullPath, false) != null);
            }
        }

        public bool ParentExists(string fullPath)
        {
            lock (driveLock)
            {
                object obj = GetObjectFromPath(fullPath, true);
                return (obj != null && obj.GetType() == typeof(Directory));
            }
        }

        public string GetDriveJson()
        {
            lock (driveLock)
            {
                return writer.Write(rootDirectory);
            }
        }

        [MoonSharpHidden]
        private object GetObjectFromPath(string fullPath, bool getParent)
        {
            lock (driveLock)
            {
                string[] splitPath = fullPath.Split("/".ToCharArray());
                string drive = splitPath[0]; //Nothing to do with drive number inside the drive
                object currentDepth = rootDirectory;
                for (int i = 1; i < splitPath.Length; i++)
                {
                    if (currentDepth.GetType() == typeof(string)) return null;
                    if (getParent == true && i == splitPath.Length - 1) break;
                    currentDepth = CheckDir(currentDepth, splitPath[i]);
                    if (currentDepth == null) return null;
                }
                return currentDepth;
            }
        }

        [MoonSharpHidden]
        private object CheckDir(object dir, string pathName)
        {
            lock (driveLock)
            {
                Directory d = dir as Directory;
                if (d.dirs.Count > 0 && d.dirs.ContainsKey(pathName)) return d.dirs[pathName];
                if (d.files.Count > 0 && d.files.ContainsKey(pathName)) return d.files[pathName];
                return null;
            }
        }

        [MoonSharpHidden]
        private void WriteToDisk()
        {
            lock (driveLock)
            {
                //if (!System.IO.Directory.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "Drives")))
                //{
                //    try
                //    {
                //        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, "Drives"));
                //    }
                //    catch (Exception e)
                //    {
                //        UnityEngine.Debug.Log("[UTLComputers]Exception while trying to create computer drive directory! " + e.Message);
                //        return;
                //    }
                //}

                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Exception while trying to create computer drive directory!");
                        return;
                    }
                }

                try
                {
                    System.IO.File.WriteAllText(filePath, GetDriveJson());
                }
                catch (Exception e)
                {
                    Debug.Log("Exception while trying to write computer drive to disk!");
                }
            }
        }
    }

    [DataContract]
    public class Directory
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "directories")]
        public Dictionary<string, Directory> dirs { get; set; }
        [DataMember(Name = "files")]
        public Dictionary<string, string> files { get; set; }

        public void Init(string name)
        {
            this.name = name;
            dirs = new Dictionary<string, Directory>();
            files = new Dictionary<string, string>();
        }
    }
}