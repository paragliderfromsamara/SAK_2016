using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// Change this to match your program's normal namespace
namespace NormaLib.Utils
{
    public class IniFile   // revision 10
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static IniFile GetAppSettingsFile()
        {
            IniFile file = new IniFile("appSettings.ini");
            return file;
        }

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void WriteIntArray(string Key, int[] Value, string Section = null)
        {
            Write(Key, $"[{string.Join(",", Value)}]", Section);
        }

        public int[] ReadIntArray(string Key, string Section = null)
        {
            string s = Read(Key, Section);
            if (string.IsNullOrWhiteSpace(s)) return new int[] { };
            try
            {
                List<int> collection = new List<int>();
                string[] sArr;
                s = s.Replace("[", "");
                s = s.Replace("]", "");
                sArr = s.Split(',');
                foreach (var sVal in sArr) collection.Add(Convert.ToInt32(sVal));
                return collection.ToArray();
            }catch
            {
                return new int[] { };
            }
        }

        public int ReadInt(string Key, string Section = null)
        {
            string sv = Read(Key, Section);
            int v = 0;
            int.TryParse(sv, out v);
            return v;
        }

        public float ReadFloat(string Key, string Section = null, float def_value = 0.0f)
        {
            string sv = Read(Key, Section);
            float.TryParse(sv, out def_value);
            return def_value;
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
