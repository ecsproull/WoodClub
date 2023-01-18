using System;
//
//  Handle errors from Pull SDK
//
namespace WoodClub
{
	static class LogError
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
					(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		static int errorNum = 0;
		static String errMsg = "";
		static String errCode = "";
		static public void ErrorHandler(String source, int err)
		{
			errorNum = err;
			errMsg = source + ": ";
			switch (errorNum)
			{
				case -1:
					errMsg += "Command not sent successfully";
					break;
				case -2:
					errMsg += "Command had no response";
					break;
				case -3:
					errMsg += "Buffer too small";
					break;
				case -4:
					errMsg += "Decompression failed???";    // Unkown error
					break;
				case -5:
					errMsg += "Read data length not correct";
					break;
				case -6:
					errMsg += "Expected length error???";    // Unknown error
					break;
				case -7:
					errMsg += "Command Repeated";
					break;
				case -8:
					errMsg += "Connection not authorized";
					break;
				case -9:
					errMsg += "Data Error: CRC result failure";
					break;
				case -10:
					errMsg += "Data Error: PullSDK failed";
					break;
				case -11:
					errMsg += "Data Parameter Error";
					break;
				case -12:
					errMsg += "Command failure";
					break;
				case -13:
					errMsg += "Command Error: command not available";
					break;
				case -14:
					errMsg += "Communication Password not correct";     // Not used
					break;
				case -15:
					errMsg += "Fail to write file";
					break;
				case -16:
					errMsg += "Fail to read to file";
					break;
				case -17:
					errMsg += "The file does not exist";
					break;
				case -99:
					errMsg += "Unknown SDK error";
					break;
				case -100:
					errMsg += "Table structure does not exist";
					break;
				case -101:
					errMsg += "Condition field does not exist in table structure";
					break;
				case -102:
					errMsg += "Total number of fields not consistant";
					break;
				case -103:
					errMsg += "Sequence of fields not consistant";
					break;
				case -104:
					errMsg += "Real Time Event data error";
					break;
				case -105:
					errMsg += "Data errors during data resolution";
					break;
				case -106:
					errMsg += "Data overflow";
					break;
				case -107:
					errMsg += "Failed to get table structure";
					break;
				case -108:
					errMsg += "Invalid options";
					break;
				case -201:
					errMsg += "Load Library failure";
					break;
				case -202:
					errMsg += "Failed to invoke interface";
					break;
				case -203:
					errMsg += "Communication initialization failed";
					break;
				case -207:
					errMsg += "Command failed...";
					break;
				case -301:
					errMsg += "TCP/IP version error";
					break;
				case -302:
					errMsg += "Incorrect version number";
					break;
				case -303:
					errMsg += "Failed to get protocol type";
					break;
				case -304:
					errMsg += "Invalid Socket";
					break;
				case -305:
					errMsg += "Socket error";
					break;
				case -306:
					errMsg += "Host error";
					break;
				case -307:
					errMsg += "Connection attempt failed";
					break;
				case -10054:
					errMsg += "Connection reset by peer";
					break;
				default:
					errCode = errorNum.ToString();
					errMsg += "Undocumented SDK error: " + errCode;
					break;
			}
			log.Error(errMsg);
		}
	}
}
