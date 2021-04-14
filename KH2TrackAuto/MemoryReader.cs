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
        public static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        Process process;
        IntPtr processHandle;
        public bool Hooked;
        public MemoryReader()
        {
            try
            {
                process = Process.GetProcessesByName("KINGDOM HEARTS II FINAL MIX")[0];
                processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            }
            catch (IndexOutOfRangeException e)
            {
                Hooked = false;
                return;
            }
                Hooked = true;

        }

        public byte[] ReadMemory(Int32 address, int bytesToRead)
        {
            try
            {
                if (process.HasExited)
                {
                    process = Process.GetProcessesByName("KINGDOM HEARTS II FINAL MIX")[0];
                    processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                }
                int bytesRead = 0;
                byte[] buffer = new byte[bytesToRead];

                ProcessModule processModule = process.MainModule;

                ReadProcessMemory((int)processHandle, processModule.BaseAddress.ToInt64() + address, buffer, buffer.Length, ref bytesRead);
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