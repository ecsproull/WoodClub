using System;
using System.Runtime.InteropServices;
//
//  PullSDK Entry points defined here
//  Reference: PullDDK interface Version 2.0
//  Helpers for SDK
//  Date: Jan 2012
//
namespace WoodClub
{
    static class PullSDK 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                   (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public IntPtr h = IntPtr.Zero;
        
        // PullSDK wrappers
        internal static class NativeMethods
        {
            [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
            internal static extern int PullLastError();
            //[DllImport("plcommpro.dll", EntryPoint = "ControlDevice")]
            //internal static extern int ControlDevice(IntPtr h, int operationid, int param1, int param2, int param3, int param4, string options);
            //[DllImport("plcommpro.dll", EntryPoint = "SearchDevice")]
            //internal static extern int SearchDevice(string commtype, string address, ref byte buffer);
            //[DllImport("plcommpro.dll", EntryPoint = "ModifyIPAddress")]
            //internal static extern int ModifyIPAddress(string commtype, string address, string buffer);
            //[DllImport("plcommpro.dll", EntryPoint = "GetDeviceFileData")]
            //internal static extern int GetDeviceFileData(IntPtr h, ref byte buffer, ref int buffersize, string filename, string options);
            //[DllImport("plcommpro.dll", EntryPoint = "SetDeviceFileData")]
            //internal static extern int SetDeviceFileData(IntPtr h, string filename, ref byte buffer, int buffersize, string options);
            //[DllImport("plcommpro.dll", EntryPoint = "ProcessBackupData")]
            //internal static extern int ProcessBackupData(byte[] data, int fileLen, ref byte Buffer, int BufferSize);
            [DllImport("plcommpro.dll",EntryPoint = "Connect")]
            internal static extern IntPtr Connect(string tcpip);
            [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
            internal static extern void Disconnect(IntPtr h);
            [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
            internal static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
            [DllImport("plcommpro.dll", EntryPoint = "SetDeviceParam")]
            internal static extern int SetDeviceParam(IntPtr h, string itemvalues);
            [DllImport("plcommpro.dll", EntryPoint = "SetDeviceData")]
            internal static extern int SetDeviceData(IntPtr h, string tablename, string data, string options);
            [DllImport("plcommpro.dll", EntryPoint = "GetDeviceData")]
            internal static extern int GetDeviceData(IntPtr h, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options);
            [DllImport("plcommpro.dll", EntryPoint = "GetDeviceDataCount")]
            internal static extern int GetDeviceDataCount(IntPtr h, string tablename, string filter, string options);
            [DllImport("plcommpro.dll", EntryPoint = "DeleteDeviceData")]
            internal static extern int DeleteDeviceData(IntPtr h, string tablename, string data, string options);
        }
        public static int SDKpullLastError()
        {
            int result = 0;
            try
            {
                result = NativeMethods.PullLastError();
            }
            catch(Exception e)
            {
                log.Error("PullLastError - ", e);
            }
            return result;      
        }
        public static int SDKconnect(string tcpip)   
        {
            int result = 0;
            h = NativeMethods.Connect(tcpip);
            if (h != IntPtr.Zero)
            {
                return 0;
            }
            else
            {
                result = NativeMethods.PullLastError();
                LogError.ErrorHandler("Program: Connect - ", result);
                return result;
            }
        }

            
        public static void SDKdisconnect()
        {
            log.Error("Attempting Disconnect from controller");
           
            if (h != IntPtr.Zero)
            {
                try
                {
                    NativeMethods.Disconnect(h);
                    h = IntPtr.Zero;            // Clear handle
                }
                catch (Exception ex)
                {
                    log.Error("SDKdisconnect Exception", ex);
                }
                
                log.Info("SDKdisconnect completed...");
            }
        }
           
        public static int SDKgetRTLog(ref byte buffer, int bufsize)
        {
            int result = 0;       // Default no error
            try
            {
                if (h != IntPtr.Zero)
                {
                    result = NativeMethods.GetRTLog(h, ref buffer, bufsize);    
                }
            }
            catch (Exception ex)
            {
                log.Error("GetRTLog failed", ex);
            }
            
            return result;
        }
            
        public static int SDKsetDeviceParam(String itemValues)
        {
            int result = -12;       // Command not executed correctly
            if (h != IntPtr.Zero)
            {
                result = NativeMethods.SetDeviceParam(h, itemValues);
            }
            return result;
        }
           
        public static int SDKsetDeviceData(string tablename, string data)
        {
            int result = -12;
            if (h != IntPtr.Zero)
            {
                result = NativeMethods.SetDeviceData(h, tablename, data, "");
            }
            return result;
        }
            
        public static int SDKgetDeviceData(ref byte buffer, int size, string tablename, string filename, string filter)
        {

            return NativeMethods.GetDeviceData(h, ref buffer, size, tablename, filename, filter, "");
        }
            
        public static int SDKgetDeviceDataCount(string tablename, string filter)
        {
            int result = -12;
            if (h != IntPtr.Zero)
            {
                result = NativeMethods.GetDeviceDataCount(h, tablename, filter, null);
            }
            return result;
        }
            
        public static int SDKdeleteDeviceData(string tablename, string data)
        {
            int result = -12;
            if (h != IntPtr.Zero)
            {
                result = NativeMethods.DeleteDeviceData(h, tablename, data, null);
            }
            return result;
        }       
    }
}
