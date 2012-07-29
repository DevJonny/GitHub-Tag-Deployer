using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using RestSharp;

namespace GitHub_Tag_Deployer {

    class FileUtil {

        public void unpackTag(string tagUrl, string unpackDirectory) {

            bool firstDir = true;
            string firstDirPath = "";

            var tagFile = getTagFilePath(tagUrl, unpackDirectory);

            using (ZipFile tag = ZipFile.Read(tagFile)) {
                foreach (ZipEntry entry in tag) {
                    if (firstDir) {
                        firstDirPath = entry.FileName;
                        firstDir = false;
                    }
                    entry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            DirectoryCopy(unpackDirectory + "\\" + firstDirPath, unpackDirectory);

            Directory.Delete(unpackDirectory + "\\" + firstDirPath, true);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName) {

            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files) {
                string temppath = Path.Combine(destDirName, file.Name);
                Directory.CreateDirectory(destDirName);
                file.CopyTo(temppath, true);
            }

            foreach (DirectoryInfo subdir in dirs) {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }

        static byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string getTagFilePath(string tagUrl, string deploymentDir) {

            string tagName = "";
            
            if (deploymentDir.EndsWith("\\")) {
                tagName = deploymentDir + tagUrl.Substring(tagUrl.LastIndexOf("/"));
            } else {
                tagName = deploymentDir + "\\" + tagUrl.Substring(tagUrl.LastIndexOf("/")+1);
            }            

            return tagName + ".zip";
        }
    }
}
