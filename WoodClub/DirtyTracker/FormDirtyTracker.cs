using System.Collections.Generic;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Borrowed code that tracks dirty controls on a form.
	/// Used by member <see cref="Editor"/>
	/// </summary>
	public class FormDirtyTracker
	{
		private Form _frmTracked;
		private ControlDirtyTrackerCollection _controlsTracked;
	
		/// <summary>
		/// Gets a value indicating whether this instance is dirty.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
		/// </value>
		public bool IsDirty
		{
			get
			{
				List<Control> dirtyControls
					= _controlsTracked.GetListOfDirtyControls();

				return (dirtyControls.Count > 0);
			}
		}

		/// <summary>
		/// Resets the color of the control back. When a data item
		/// on the form has been changed it changes the background color.
		/// This set it back to the original color.
		/// </summary>
		/// <param name="c">The c.</param>
		public void resetControlBackColor(Control c)
		{
			_controlsTracked.ResetBackColor(c);
		}

		/// <summary>
		/// Gets the list of dirty controls.
		/// </summary>
		/// <returns></returns>
		public List<Control> GetListOfDirtyControls()
		{
			return _controlsTracked.GetListOfDirtyControls();
		}


		/// <summary>
		/// Marks as clean.
		/// </summary>
		public void MarkAsClean()
		{
			_controlsTracked.MarkAllControlsAsClean();
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="FormDirtyTracker"/> class.
		/// </summary>
		/// <param name="frm">The FRM.</param>
		public FormDirtyTracker(Form frm)
		{
			_frmTracked = frm;
			_controlsTracked = new ControlDirtyTrackerCollection(frm);
		}
	}
}
