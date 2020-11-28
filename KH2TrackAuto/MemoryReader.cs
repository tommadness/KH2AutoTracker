using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace KH2TrackAuto
{
    public class MemoryReader
    {
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        Int32 address;
        int bytesToRead;
        public MemoryReader(Int32 addr, int bytes)
        {
            address = addr;
            bytesToRead = bytes;

        }

        public byte[] ReadMemory()
        {
            Process process = new Process();
            try
            {
                process = Process.GetProcessesByName("pcsx2")[0];
                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                int bytesRead = 0;
                byte[] buffer = new byte[bytesToRead]; 

                ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesRead);
                //Array.Reverse(buffer, 0, buffer.Length);
                return buffer;
            }
            catch (IndexOutOfRangeException e)
            {
                return new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };
            }
        }
    }
}