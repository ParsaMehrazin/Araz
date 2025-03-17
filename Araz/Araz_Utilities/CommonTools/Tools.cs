using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace Utilities
{
    public class Tools
    {
        public static DateTime LastFlightInformationUpdate { get; set; }
        public static long LastFlightInformationTableVersion { get; set; }
        public static List<TimeSpan> FiveMinDurations()
        {
            List<TimeSpan> list = new List<TimeSpan>();
            list.Add(new TimeSpan(0, 5, 0));
            list.Add(new TimeSpan(0, 10, 0));
            list.Add(new TimeSpan(0, 15, 0));
            list.Add(new TimeSpan(0, 20, 0));
            list.Add(new TimeSpan(0, 25, 0));
            list.Add(new TimeSpan(0, 30, 0));
            list.Add(new TimeSpan(0, 35, 0));
            list.Add(new TimeSpan(0, 40, 0));
            list.Add(new TimeSpan(0, 45, 0));
            list.Add(new TimeSpan(0, 50, 0));
            list.Add(new TimeSpan(0, 55, 0));
            list.Add(new TimeSpan(0, 60, 0));
            return list.ToList();
        }
        public static List<TimeSpan> TenMinDurations()
        {
            List<TimeSpan> list = new List<TimeSpan>();
            list.Add(new TimeSpan(0, 10, 0));
            list.Add(new TimeSpan(0, 20, 0));
            list.Add(new TimeSpan(0, 30, 0));
            list.Add(new TimeSpan(0, 40, 0));
            list.Add(new TimeSpan(0, 50, 0));
            list.Add(new TimeSpan(0, 60, 0));
            list.Add(new TimeSpan(0, 70, 0));
            list.Add(new TimeSpan(0, 80, 0));
            list.Add(new TimeSpan(0, 90, 0));
            list.Add(new TimeSpan(0, 100, 0));
            list.Add(new TimeSpan(0, 110, 0));
            list.Add(new TimeSpan(0, 120, 0));
            return list.ToList();
        }

        //public static DateTime? GetStringDateTimeFromDateAndTime(DateTime date, DateTime? time, bool IsLocal)
        //{

        //    if (time == null)
        //        time = new DateTime();
        //    if (!IsLocal)
        //    {

        //        DateTime input = new DateTime(date.Year, date.Month, date.Day, time.Value.Hour, time.Value.Minute, time.Value.Second);

        //        //DataManager.Repository<View_FlightInformation> repository = new DataManager.Repository<View_FlightInformation>();
        //        time = DataManager.Repositories.DARepository.GetSingleValueFromFunctionDateTime("ConvertUTCToLCL",
        //             new DataManager.ServiceOperatorParameter() { Name = "UtcDateTime", Value = (input.ToDateTimeInStringFormat()) });

        //    }
        //    int m = time.Value.Minute;
        //    int h = time.Value.Hour;
        //    int day = date.Day;
        //    int month = date.Month;
        //    int year = date.Year;

        //    DateTime result = new DateTime(year, month, day, h, m, 0);
        //    return result;

        //}

        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;
            try
            {
                FileInfo fInfo = new FileInfo(sPath);
                if (fInfo.Exists)
                {
                    data = new byte[fInfo.Length];
                    data = File.ReadAllBytes(sPath);
                    return data;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                //ERROR
                return data;
            }

        }
        public static void RunFile(byte[] _attachfile, string _filename, string _extension)
        {
            string str = string.Empty;
            if (_filename.Contains(_extension))
                str = string.Format("{0}{1}", Path.GetTempPath(), _filename);
            else
                str = string.Format("{0}{1}.{2}", Path.GetTempPath(), _filename, _extension);
            str = str.Replace('?', 'X');
            if (File.Exists(str))
                File.Delete(str);

            File.WriteAllBytes(str, _attachfile);

            System.Diagnostics.Process.Start(str);

        }


        public static bool ByteCompare(byte[] a1, byte[] a2)
        {
            if (a1 == a2)
            {
                return true;
            }
            if ((a1 != null) && (a2 != null))
            {
                if (a1.Length != a2.Length)
                {
                    return false;
                }
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i] != a2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public static System.Xml.XmlDocument BytesToXML(byte[] bytes)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            MemoryStream ms = new MemoryStream(bytes);
            doc.Load(ms);
            return doc;
        }
        public static void ExtractZipContent(string FileZipPath, string password, string OutputFolder)
        {
            ZipFile file = null;
            try
            {
                FileStream fs = File.OpenRead(FileZipPath);
                file = new ZipFile(fs);

                if (!String.IsNullOrEmpty(password))
                {
                    // AES encrypted entries are handled automatically
                    file.Password = password;
                }

                foreach (ZipEntry zipEntry in file)
                {
                    if (!zipEntry.IsFile)
                    {
                        // Ignore directories
                        continue;
                    }

                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    // 4K is optimum
                    byte[] buffer = new byte[4096];
                    Stream zipStream = file.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(OutputFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (file != null)
                {
                    file.IsStreamOwner = true; // Makes close also shut the underlying stream
                    file.Close(); // Ensure we release resources
                }
            }
        }
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public static void DownloadFile(byte[] _attachfile, string _filename, string _extension)
        {
            //string str = string.Empty;
            //System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            //saveFileDialog1.ShowDialog();

            //if (_filename.Contains(_extension))
            //    str = string.Format("{0}{1}", saveFileDialog1.FileName, _extension);
            //else
            //    str = string.Format("{0}{1}.{2}", saveFileDialog1.FileName, _filename, _extension);
            //str = str.Replace('?', 'X');
            //if (File.Exists(str))
            //    File.Delete(str);

            //File.WriteAllBytes(str, _attachfile);

            //System.Diagnostics.Process.Start(str);

        }
        public static void DownloadFile(byte[] _attachfile, string _filename, string _extension, string _filePath)
        {
            string str = string.Empty;

            if (_filename.Contains(_extension))
                str = string.Format("{0}", _filename);
            else
                str = string.Format("{0}{1}.{2}", _filename, _filename, _extension);
            str = str.Replace('?', 'X');
            str = _filePath + @"\\" + str;
            if (File.Exists(str))
            {
                str = str.Replace(_extension, Guid.NewGuid().ToString());
                str = str + _extension;

            }
            else
            {
                str = str.Replace(_extension, Guid.NewGuid().ToString());
                str = str + _extension;
            }
            File.WriteAllBytes(str, _attachfile);
        }



    }
}
