using System.Windows.Forms;

namespace WoodClub
{
	public static class BindingSourceExtensions
	{
		public static int MemberIdentifier(this BindingSource sender)
		{
			return ((MemberRoster)sender.Current).id;
		}
	}

}
