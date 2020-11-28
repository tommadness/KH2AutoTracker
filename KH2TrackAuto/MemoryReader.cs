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
                byte[] buffer = new byte[bytesToRead]; //'Hello World!' takes 12*2 bytes because of Unicode 

                // 0x0046A3B8 is the address where I found the string, replace it with what you found
                ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesRead);
                return buffer;
            }
            catch (IndexOutOfRangeException e)
            {
                return new byte[0];
            }
        }
    }
}