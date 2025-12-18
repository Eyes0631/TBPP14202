using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentSet.CSFileTool
{
    public class FileTool
    {
        public bool GetFolderFileList(string folder, ref List<string> FileList) //取得檔案列表
        {
            if (FileList == null) { return false; }
            if (System.IO.Directory.Exists(folder) == false) { return false; }

            FileList.Clear();
            FileList.AddRange(System.IO.Directory.GetDirectories(folder));
            FileList.AddRange(System.IO.Directory.GetFiles(folder));
            return true;
        }

        public bool GetFullFolderFileList(string FileFormat, ref List<string> FileList, bool IncludeRoot = true) //取得檔案列表 (包含次資料夾)
        {
            string folder = ExtractFileFormatFolder(FileFormat);
            if (folder.Length < 3) { return false; }
            if (System.IO.Directory.Exists(folder) == false) { return false; }
            if (FileList == null) { return false; }
            FileList.Clear();

            List<string> slFiles = new List<string>();
            List<string> slTmp = new List<string>();
            if (folder.Substring(folder.Length - 1, 1) != "\\")
            {
                folder += "\\";
            }
            if (IncludeRoot)
            {
                FileList.Add(folder);
            }

            //取得資料夾中所有檔案列表
            if (GetFolderFileList(folder, ref slFiles))
            {
                string strFileName;
                for (int i = 0; i < slFiles.Count; i++)
                {
                    strFileName = slFiles[i];
                    if (strFileName != "." && strFileName != "..") //if (strFileName.Length() > 2)
                    {
                        //strFileName = folder + strFileName;
                        if (System.IO.File.Exists(strFileName)) //檔案
                        {
                            FileList.Add(strFileName);
                        }
                        else if (System.IO.Directory.Exists(strFileName)) //資料夾
                        {
                            if (GetFullFolderFileList(strFileName, ref slTmp, IncludeRoot))
                            {
                                FileList.AddRange(slTmp);
                            }
                        }
                    }
                }
            }

            if (FileFormat.IndexOf("*") >= 0)
            {
                for (int i = 0; i < FileList.Count; i++)
                {
                    if (CheckPathFormatString(FileList[i], FileFormat) == false)
                    {
                        FileList.RemoveAt(i);
                        i--;
                    }
                }
            }

            //結束
            return true; //((IncludeRoot==true && Files->Count>1) || (IncludeRoot==false && Files->Count>0));
        }

        public string ExtractFileFormatFolder(string FileFormat) //取得檔案格式存在的路徑
        {
            string folder = FileFormat;
            if (folder.IndexOf("*") >= 0)
            {
                folder = System.IO.Directory.GetParent(folder.Substring(0, folder.IndexOf("*") + 1)).FullName;
            }
            return folder;
        }

        public bool CheckPathFormatString(string CheckString, string FormatString) //檢查路徑格式
        {
            if (FormatString.IndexOf("*") < 0)
            {
                return (CheckString.IndexOf(FormatString) == 0);
            }

            //拆解格式字串, 以*號作分隔
            int postmp = FormatString.IndexOf("**");
            while (postmp >= 0)
            {
                FormatString = FormatString.Remove(postmp, 1);
                postmp = FormatString.IndexOf("**");
            }

            List<string> sltmp = new List<string>();
            sltmp.Clear();
            postmp = FormatString.IndexOf("*");
            while (postmp >= 0)
            {
                sltmp.Add(FormatString.Substring(0, postmp));
                FormatString = FormatString.Remove(0, postmp + 1);
                postmp = FormatString.IndexOf("*");
            }
            if (FormatString.Length > 0)
            {
                sltmp.Add(FormatString);
            }

            //比對
            bool bret = true;
            for (int i = 0; i < sltmp.Count; i++)
            {
                int ipos = CheckString.IndexOf(sltmp[i]);
                if ((i == 0 && ipos == 0) || (i > 0 && ipos > 0))
                {
                    CheckString = CheckString.Remove(0, ipos + sltmp[i].Length);
                }
                else
                {
                    bret = false;
                    break;
                }
            }

            return bret;
        }

        public bool IsFileModifiedInDays(string FileName, bool CheckBefore, int Days) //檢查檔案日期
        {
            if (System.IO.File.Exists(FileName))
            {
                DateTime filedt = System.IO.File.GetLastWriteTime(FileName);
                int fileday = filedt.Year * 365 + filedt.DayOfYear;
                int nowday = DateTime.Now.Year * 365 + DateTime.Now.DayOfYear;

                if (CheckBefore == true && fileday < (nowday - Days))
                { //CheckBefore==true, 檢查檔案更新日期是否為指定天數之前
                    return true;
                }
                if (CheckBefore == false && fileday >= (nowday - Days))
                { //CheckBefore==false, 檢查檔案更新日期是否為指定天數之後
                    return true;
                }
            }
            return false;
        }

        public bool GetFileListInDays(string FileFormat, int InDays, ref List<string> FileList) //取得設定天數以內的檔案列表
        {
            if (System.IO.Directory.Exists(ExtractFileFormatFolder(FileFormat)) == false) { return false; }
            FileList.Clear();
            List<string> slFiles = new List<string>();

            string strFileName;
            if (GetFullFolderFileList(FileFormat, ref slFiles))
            {
                for (int i = 0; i < slFiles.Count; i++)
                {
                    strFileName = slFiles[i];
                    if (System.IO.File.Exists(strFileName) && IsFileModifiedInDays(strFileName, false, InDays))
                    {
                        FileList.Add(strFileName);
                    }
                }
            }
            return (FileList.Count > 0);
        }

        public void DeleteFullDirFiles(string FileFormat, int DaysBefore)   //刪除舊檔案 (包含次資料夾)
        {
            if (System.IO.Directory.Exists(ExtractFileFormatFolder(FileFormat)) == false) { return; }
            List<string> slFiles = new List<string>();
            List<string> sltmp = new List<string>();

            string strFileName;
            if (GetFullFolderFileList(FileFormat, ref slFiles))
            {
                for (int i = slFiles.Count - 1; i >= 0; i--) //刪除檔案
                {
                    strFileName = slFiles[i];
                    if (System.IO.File.Exists(strFileName) && IsFileModifiedInDays(strFileName, true, DaysBefore))
                    {
                        System.IO.File.Delete(strFileName);
                    }
                }
                for (int i = slFiles.Count - 1; i >= 0; i--) //刪除資料夾
                {
                    strFileName = slFiles[i];
                    if (System.IO.Directory.Exists(strFileName) && GetFullFolderFileList(strFileName, ref sltmp, false))
                    {
                        if (sltmp.Count == 0) //if (sltmp->Count==1 && sltmp->Strings[0]==strFileName)
                        {
                            System.IO.Directory.Delete(strFileName);
                        }
                    }
                }
            }
        }

        public string GetRarToolPath()   //RAR 執行檔路徑
        {
            string sRet = "C:\\Program Files\\WinRAR\\WinRar.exe";
            if (System.IO.File.Exists(sRet))
            {
                return sRet;
            }

            sRet = Environment.ExpandEnvironmentVariables("%ProgramFiles%"); //"C:\\Program Files (x86)"
            sRet += "\\WinRAR\\WinRar.exe"; //"C:\\Program Files (x86)\\WinRAR\\WinRar.exe"
            if (System.IO.File.Exists(sRet))
            {
                return sRet;
            }
            return "";
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string ReadConfigFileString(string FileName, string section, string key, string defvalue)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, defvalue, temp, 255, FileName);
            return temp.ToString();
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int WinExec(string exeName, int operType);

        public void BackupSetupFiles() //壓縮指定內容
        {
            List<string> sltmp = new List<string>();
            string inifile = System.IO.Directory.GetParent(System.Windows.Forms.Application.ExecutablePath) + "\\RarParam.ini";

            try
            {
                string SaveToFolder = ReadConfigFileString(inifile, "Basic", "SaveToPath", "D:\\");
                string SaveToFileName = "";
                if (System.IO.Directory.Exists(SaveToFolder) == false)
                {
                    System.IO.Directory.CreateDirectory(SaveToFolder);
                }
                if (System.IO.Directory.Exists(SaveToFolder))
                {
                    string rarfilepath = GetRarToolPath(); //RAR 執行檔路徑
                    if (rarfilepath != "" && System.IO.File.Exists(rarfilepath))
                    {
                        SaveToFileName = SaveToFolder + "\\"
                            + ReadConfigFileString(inifile, "Basic", "FileNameTitle", "") + "_"
                            + DateTime.Now.ToString(ReadConfigFileString(inifile, "Basic", "FileNameTimeFormat", "yyyy-MM-dd-HH-mm-ss-ff")) + ".rar";

                        //sCmd = sCmd + " a -r " + SaveToFileName; //sCmd = sCmd + " a -r " + SaveToFileName + " " + ExtractFilePath(Application->ExeName) + "*.ini";   //"*.ini D:\\*.ini";
                        string argumentcmd = "a -r -dh " + SaveToFileName; //sCmd = sCmd + " a -r " + SaveToFileName + " " + ExtractFilePath(Application->ExeName) + "*.ini";   //"*.ini D:\\*.ini";
                        bool HaveData = false;

                        int suffix = 1;
                        string BackupDataPath = ReadConfigFileString(inifile, "DataSource", "Dir" + string.Format("{0:D2}", suffix), "");
                        int BackupInDays = int.Parse(ReadConfigFileString(inifile, "DataSource", "Day" + string.Format("{0:D2}", suffix), "-1"));
                        while (BackupDataPath != "")
                        {
                            if (System.IO.Directory.Exists(ExtractFileFormatFolder(BackupDataPath)))
                            {
                                if (BackupInDays < 0)
                                { //壓縮完整資料夾
                                    argumentcmd = argumentcmd + " " + BackupDataPath;
                                    HaveData = true;
                                }
                                else
                                { //依據設定天數產生檔案列表
                                    if (GetFileListInDays(BackupDataPath, BackupInDays, ref sltmp))
                                    {
                                        for (int i = 0; i < sltmp.Count; i++)
                                        {
                                            if (System.IO.File.Exists(sltmp[i]))
                                            {
                                                argumentcmd = argumentcmd + " " + sltmp[i];
                                                HaveData = true;
                                            }
                                        }
                                    }
                                }
                            }

                            //下一筆設定
                            suffix++;
                            BackupDataPath = ReadConfigFileString(inifile, "DataSource", "Dir" + string.Format("{0:D2}", suffix), "");
                            BackupInDays = int.Parse(ReadConfigFileString(inifile, "DataSource", "Day" + string.Format("{0:D2}", suffix), "-1"));
                        }

                        if (HaveData)
                        {
                            //使用System.Diagnostics.Process
                            System.Diagnostics.Process executefile = new System.Diagnostics.Process();
                            executefile.StartInfo.FileName = rarfilepath;
                            executefile.StartInfo.Arguments = argumentcmd;
                            executefile.Start();

                            //使用WinExec
                            //string scmd = rarfilepath + " " + argumentcmd;
                            //WinExec(scmd, 5);
                        }
                    }
                }
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                //prc.StartInfo.FileName = SaveToFolder;
                prc.StartInfo.FileName = SaveToFolder;
                prc.StartInfo.Arguments = @"/select, " + SaveToFolder;
                //prc.StartInfo.WorkingDirectory = SaveToFolder;
                prc.Start();

                
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.Message);
            }

        }

        public void Dispose()
        { 

        }
    }
}
