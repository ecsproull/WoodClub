using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class SyncToQB : Form
	{
		private Dictionary<string, string> lockerItemMap;
		private bool cancel = false;

		public SyncToQB()
		{
			InitializeComponent();
			lockerItemMap = new Dictionary<string, string>();
			lockerItemMap.Add("FS", "X11");
			lockerItemMap.Add("LC", "X25");
			lockerItemMap.Add("SC", "X24");
			lockerItemMap.Add("UC", "X17");
			lockerItemMap.Add("WL", "X20");
			lockerItemMap.Add("X", "X18");
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			closeButton.Enabled = false;
			string[] clubMemberSubTypes = { "X06A", "X06B", "X06C", "X06D", "X06E", "X06F" };
			List<Tuple<string, string, string>> map = new List<Tuple<string, string, string>>();
			QBFunctions qbf = new QBFunctions();
			try
			{
				using (WoodClubEntities context = new WoodClubEntities())
				{
					qbf.connectToQB();
					try
					{
						int count = 0;
						List<MemberRoster> members = (from m in context.MemberRosters
													  select m).ToList();

						foreach (MemberRoster member in members)
						{
							if (member.Badge == "20001" || this.cancel)
							{
								continue;
							}

							List<Locker> lockers = (from l in context.Lockers
													where l.Badge == member.Badge && !l.LockerTitle.StartsWith("I")
													select l).OrderBy(x => x.Code).ToList();
							string customerType = "Club Member:";
							string lockerText = "...";


							if (lockers.Count > 1)
							{
								customerType += "M00";
								int lockerCount = 0;
								foreach (Locker locker in lockers)
								{
									if (lockerCount++ == 0)
									{
										lockerText = locker.LockerTitle;
									}
									else
									{
										lockerText += ", " + locker.LockerTitle;
									}
								}
							}
							else if (lockers.Count == 1)
							{
								if (lockers[0].Code.ToLower() != "i")
								{
									customerType += lockerItemMap[lockers[0].Code];
									lockerText = lockers[0].LockerTitle;
								}
							}
							else
							{
								customerType += clubMemberSubTypes[(int)(count++ / 100)];
							}


							map.Add(new Tuple<string, string, string>(member.Badge, customerType, lockerText));
						}

						syncProgressBar.Maximum = map.Count;
						syncProgressBar.Step = 1;
						foreach (Tuple<string, string, string> tuple in map)
						{
							if (this.cancel)
							{
								syncProgressBar.PerformStep();
								continue;
							}

							string response = qbf.processRequestFromQB(qbf.buildCustomerQueryRqXML(
								new string[] { "ListID", "EditSequence", "CustomerTypeRef" }, tuple.Item1, "", "", "", true));

							CustomerIdData customerIdData = qbf.parseCustomerQueryRsShort(response);
							if (string.IsNullOrEmpty(customerIdData.ListId) || string.IsNullOrEmpty(customerIdData.EditSequence))
							{
								MessageBox.Show("CustomerFailed : " + tuple.Item1);
								continue;
							}
							string customerModXml = qbf.BuildCustomerMod(customerIdData, tuple.Item2);
							response = qbf.processRequestFromQB(customerModXml);

							response = qbf.processRequestFromQB(qbf.buildDataExtMod(tuple.Item1, "Locker#", tuple.Item3));

							MemberRoster mm = (from m in context.MemberRosters
											   where m.Badge == tuple.Item1
											   select m).FirstOrDefault();
							response = qbf.processRequestFromQB(qbf.buildDataExtMod(tuple.Item1, "Customer Name", mm.FirstName + " " + mm.LastName));

							syncProgressBar.PerformStep();

						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					finally
					{
						qbf.disconnectFromQB();
						closeButton.Enabled = true;
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				qbf.disconnectFromQB();
				closeButton.Enabled = true;
			}
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
