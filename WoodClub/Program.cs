using System;
using System.Windows.Forms;

/// <summary>
/// Application for viewing and managing member data in the club database.
/// Each member has a lot of associated data including things like Machine Permissions,
/// Lockers, Door access and much more. The program also includes data reporting.
/// </summary>
namespace WoodClub
{
	/// <summary>
	/// Main class.
	/// </summary>
	static class Program
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// The main entry point for the application. The initial Form is MainMembers.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainMembers());
		}
	}
}
