using System;
using System.IO;
using Ionic.Zip;


namespace GitHub_Tag_Deployer
{

    class FileUtil
    {
        public void unpackTag(string tagUrl, string unpackDirectory)
        {

            bool firstDir = true;
            string firstDirPath = "";

            var tagFile = getTagFilePath(tagUrl, unpackDirectory);
            
            using (ZipFile tag = ZipFile.Read(tagFile))
            {
                foreach (ZipEntry entry in tag)
                {
                    if (firstDir)
                    {
                        firstDirPath = entry.FileName;
                        firstDir = false;
                    }
                    entry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            DirectoryCopy(unpackDirectory + "\\" + firstDirPath, unpackDirectory);

            FileInfo f = new FileInfo(tagFile);
            f.Delete();
        }

        private void DirectoryCopy(string sourceDirName, string destDirName)
        {

            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                Directory.CreateDirectory(destDirName);
                file.CopyTo(temppath, true);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string getTagFilePath(string tagUrl, string deploymentDir)
        {

            string tagName = "";

            if (deploymentDir.EndsWith("\\"))
            {
                tagName = deploymentDir + tagUrl.Substring(tagUrl.LastIndexOf("/"));
            }
            else
            {
                tagName = deploymentDir + "\\" + tagUrl.Substring(tagUrl.LastIndexOf("/") + 1);
            }

            return tagName;
        }

        public void BackupFolder(string sourceFolder, string destFolder)
        {
            string backupFolder = "Backup" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            backupFolder = backupFolder.Replace(":", "").Replace("-", "").Replace(" ", "");

            string backupFolderPath = destFolder + @"\" + backupFolder;
            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolderPath);

            foreach (string dirPath in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourceFolder, backupFolderPath));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(sourceFolder, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourceFolder, backupFolderPath));
        }

        public void DeleteExistingFiles(string deployDirectory)
        {
            foreach (string folder in Directory.GetDirectories(deployDirectory))
            {
                DirectoryInfo d = new DirectoryInfo(folder);
                d.Delete(true);
            }

            foreach (string file in Directory.GetFiles(deployDirectory))
            {
                FileInfo f = new FileInfo(file);
                f.Delete();
            }
        }
    }
}
