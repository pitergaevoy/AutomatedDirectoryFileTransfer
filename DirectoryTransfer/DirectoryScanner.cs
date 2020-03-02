using System;
using System.IO;
using System.Threading.Tasks;

namespace DirectoryTransfer
{
    public class DirectoryScanner
    {
        public static async void DirectoryListener(string directoryForScan, string searchPattern, string moveToThisDirectory)
        {
            while (true)
            {
                var directoryInfo = new DirectoryInfo(directoryForScan);

                if (directoryInfo.Exists)
                {
                    foreach (var fileInfo in directoryInfo.GetFiles(searchPattern))
                    {
                        var oldFileName = fileInfo.Name;
                        var newFileName = oldFileName;

                        if (DirectoryExistFile(moveToThisDirectory, oldFileName))
                        {
                            var oldFileNameWithoutExt =
                                oldFileName.Substring(0, oldFileName.Length - fileInfo.Extension.Length);

                            newFileName = oldFileNameWithoutExt + "copy" + fileInfo.Extension;
                        }

                        var destFileName = Path.Combine(moveToThisDirectory, newFileName);

                        try
                        {
                            fileInfo.MoveTo(destFileName, true);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }

                await Task.Delay(500);
            }
        }

        private static bool DirectoryExistFile(string targetDirectory, string fileName)
        {
            var directoryInfo = new DirectoryInfo(targetDirectory);

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Name == fileName)
                    return true;
            }

            return false;
        }
    }
}
