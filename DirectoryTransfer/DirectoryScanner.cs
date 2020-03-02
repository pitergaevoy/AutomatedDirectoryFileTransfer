using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DirectoryTransfer
{
    public class DirectoryScanner
    {
        private bool _stopped;

        public void DirectoryListener(List<ScannerUnit> units)
        {
            _stopped = false;
            if (units == null)
                return;

            foreach (var scannerUnit in units) 
                ListenerUnit(scannerUnit);
        }
        private async void ListenerUnit(ScannerUnit scannerUnit)
        {
            while (!_stopped)
            {
                var directoryInfo = new DirectoryInfo(scannerUnit.DirectoryForScan);

                if (directoryInfo.Exists)
                {
                    foreach (var fileInfo in directoryInfo.GetFiles(scannerUnit.SearchPattern))
                    {
                        var oldFileName = fileInfo.Name;
                        var newFileName = oldFileName;

                        string moveToThisDirectory = scannerUnit.DirectoryForTransfer;
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

                await Task.Delay(1000);
            }
        }

        public void Stop()
        {
            _stopped = true;
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
