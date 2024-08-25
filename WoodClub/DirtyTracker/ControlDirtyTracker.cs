using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Borrowed code that tracks dirty controls on a form.
	/// </summary>
	public class ControlDirtyTracker
	{
		private Control _control;
		private string _cleanValue;
		private Color _backColor;

		/// <summary>
		/// Gets the control.
		/// </summary>
		/// <value>
		/// The control.
		/// </value>
		public Control Control { get { return _control; } }

		/// <summary>
		/// Gets the clean value.
		/// </summary>
		/// <value>
		/// The clean value.
		/// </value>
		public string CleanValue { get { return _cleanValue; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="ControlDirtyTracker"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <exception cref="System.NotSupportedException"></exception>
		public ControlDirtyTracker(Control control)
		{
			// if the control type is not one that is supported, throw an exception
			if (ControlDirtyTracker.IsControlTypeSupported(control))
				_control = control;
			else
				throw new NotSupportedException(
					string.Format("The control type for '{0}' is not supported by the ControlDirtyTracker class."
					  , control.Name)
					);

		}

		/// <summary>
		/// Establishes the value as clean.
		/// </summary>
		public void EstablishValueAsClean()
		{
			_cleanValue = GetControlCurrentValue();
			_backColor = _control.BackColor;
		}


		/// <summary>
		/// Determine if the current control value is considered "dirty". 
		/// i.e. if the current control value is different than the one
		/// remembered as "clean"
		/// /// </summary>
		/// <returns>true if dirty, else false.</returns>
		public bool DetermineIfDirty()
		{
			// compare the remembered "clean value" to the current value;
			// if they are the same, the control is still clean;
			// if they are different, the control is considered dirty.
			return (string.Compare(_cleanValue, GetControlCurrentValue(), false) != 0);
		}



		//////////////////////////////////////////////////////////////////////////////////////
		// developers may modify the following two methods to extend support for other types
		//////////////////////////////////////////////////////////////////////////////////////

		// static class utility method; return whether or not the control type 
		// of the given control is supported by this class;
		// developers may modify this to extend support for other types
		public static bool IsControlTypeSupported(Control ctl)
		{
			// list of types supported
			if (ctl is TextBox) return true;
			if (ctl is CheckBox) return true;
			if (ctl is ComboBox) return true;
			if (ctl is ListBox) return true;

			// ... add additional types as desired ...                       

			// not a supported type
			return false;
		}


		// private method to determine the current value (as a string) of the control;
		// if the control is not supported, return a NotSupported exception
		// developers may modify this to extend support for other types
		private string GetControlCurrentValue()
		{
			if (_control is TextBox)
				return (_control as TextBox).Text;

			if (_control is CheckBox)
				return (_control as CheckBox).Checked.ToString();

			if (_control is ComboBox)
				return (_control as ComboBox).Text;

			if (_control is ListBox)
			{
				// for a listbox, create a list of the selected indexes
				StringBuilder val = new StringBuilder();
				ListBox lb = (_control as ListBox);
				ListBox.SelectedIndexCollection coll = lb.SelectedIndices;
				for (int i = 0; i < coll.Count; i++)
					val.AppendFormat("{0};", coll[i]);

				return val.ToString();
			}

			// ... add additional types as desired ...

			return "";
		}

		public void RestoreBackColor()
		{
			_control.BackColor = _backColor;
		}
	}
}
