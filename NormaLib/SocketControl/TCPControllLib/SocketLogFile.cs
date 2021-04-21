using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NormaLib.SocketControl.TCPControllLib
{
    public static class SocketLogFile
    {
        private static string file_name = "SocketLogFile.log";
        public static async Task WriteExceptionAsync(Exception ex)
        {
            string time = DateTime.Now.ToString("o");
            string text = "";
            if (ex.GetType() == typeof(SocketException))
            {
                SocketException sEx = ex as SocketException;
                text = $"Message = {sEx.Message}; Type = {sEx.GetType().Name}; ExceptionCode = {sEx.ErrorCode};";
            }else if (ex.GetType() == typeof(IOException))
            {
                IOException ioEx = ex as IOException;
                text = $"Message = {ioEx.Message}; Type = {ioEx.GetType().Name}; ExceptionCode = 0x{ioEx.HResult.ToString("x8")};";
            }
            else
                text = $"Message = {ex.Message}; Type = {ex.GetType().Name};";
            using (StreamWriter file = new StreamWriter(file_name, append: true))
            {
                await file.WriteLineAsync($"[{time}][Exception] {text}\n");
            } 
        }

        public static async Task WriteSocketMessageAsync(string message)
        {
            string time = DateTime.Now.ToString("o");
            using (StreamWriter file = new StreamWriter(file_name, append: true))
            {
                await file.WriteLineAsync($"[{time}][Message] {message}\n");
            }

        }


    }
}
