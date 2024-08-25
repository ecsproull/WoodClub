using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Extends the BindingSource.
	/// </summary>
	public static class BindingSourceExtensions
	{
		/// <summary>
		/// Members identifier.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <returns></returns>
		public static int MemberIdentifier(this BindingSource sender)
		{
			return ((MemberRoster)sender.Current).id;
		}
	}

}
