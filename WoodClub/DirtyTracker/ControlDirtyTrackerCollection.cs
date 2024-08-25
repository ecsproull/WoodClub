using System.Collections.Generic;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Borrowed code that tracks dirty (edited) data on a form.
	/// </summary>
	/// <seealso cref="System.Collections.Generic.List&lt;WoodClub.ControlDirtyTracker&gt;" />
	public class ControlDirtyTrackerCollection : List<ControlDirtyTracker>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControlDirtyTrackerCollection"/> class.
		/// </summary>
		public ControlDirtyTrackerCollection() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlDirtyTrackerCollection"/> class.
		/// </summary>
		/// <param name="frm">The FRM.</param>
		public ControlDirtyTrackerCollection(Form frm) : base()
		{
			// initialize to the controls on the passed in form
			AddControlsFromForm(frm);
		}

		/// <summary>
		/// Adds the controls from form.
		/// </summary>
		/// <param name="frm">The FRM.</param>
		public void AddControlsFromForm(Form frm)
		{
			AddControlsFromCollection(frm.Controls);
		}

		/// <summary>
		/// Adds the controls from collection.
		/// Recursive routine to inspect each control and add to the collection accordingly.
		/// </summary>
		/// <param name="collection">The collection.</param>
		public void AddControlsFromCollection(Control.ControlCollection collection)
		{
			foreach (Control c in collection)
			{
				// if the control is supported for dirty tracking, add it
				if (ControlDirtyTracker.IsControlTypeSupported(c))
					this.Add(new ControlDirtyTracker(c));

				// Recursively apply to inner collections
				if (c.HasChildren)
					AddControlsFromCollection(c.Controls);
			}
		}

		/// <summary>
		/// Gets the list of dirty controls.
		/// </summary>
		/// <returns></returns>
		public List<Control> GetListOfDirtyControls()
		{
			List<Control> list = new List<Control>();

			foreach (ControlDirtyTracker c in this)
			{
				if (c.DetermineIfDirty())
					list.Add(c.Control);
			}

			return list;
		}

		/// <summary>
		/// Resets the color of the back.
		/// </summary>
		/// <param name="c">The c.</param>
		public void ResetBackColor(Control c)
		{
			List<Control> list = new List<Control>();

			foreach (ControlDirtyTracker dt in this)
			{
				if (dt.Control == c)
				{
					dt.RestoreBackColor();
				}
			}
		}

		/// <summary>
		/// Marks all controls as clean.
		/// </summary>
		public void MarkAllControlsAsClean()
		{
			foreach (ControlDirtyTracker c in this)
				c.EstablishValueAsClean();
		}
	}
}
