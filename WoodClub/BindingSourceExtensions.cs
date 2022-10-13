
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
