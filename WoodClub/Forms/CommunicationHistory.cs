using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
    /// <summary>
    /// Lists every communication sent to a member (matched on their Badge in
    /// <see cref="Communication.Recipient"/>), each expandable to show the
    /// SendGrid delivery/open/click/bounce events recorded for it in
    /// <see cref="CommunicationEvent"/>.
    /// </summary>
    public partial class CommunicationHistory : Form
    {
        private static readonly TimeZoneInfo PhoenixTimeZone = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");

        private readonly string badge;

        public CommunicationHistory(string badge, string memberName)
        {
            InitializeComponent();
            this.badge = badge;
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                this.Text = "Communications - " + memberName;
            }
        }

        private void CommunicationHistory_Load(object sender, EventArgs e)
        {
            LoadCommunications();
        }

        private void LoadCommunications()
        {
            treeCommunications.Nodes.Clear();

            using (WoodClubEntities context = new WoodClubEntities())
            {
                List<Communication> communications = context.Communications
                    .Where(c => c.Recipient == badge)
                    .OrderByDescending(c => c.SentAt)
                    .ToList();

                List<long> communicationIds = communications.Select(c => c.CommunicationID).ToList();
                ILookup<long, CommunicationEvent> eventsByCommunication = context.CommunicationEvents
                    .Where(ev => communicationIds.Contains(ev.CommunicationID))
                    .ToList()
                    .ToLookup(ev => ev.CommunicationID);

                foreach (Communication communication in communications)
                {
                    string subject = string.IsNullOrWhiteSpace(communication.Subject) ? "(no subject)" : communication.Subject;
                    string sentAt = ToPhoenixTime(communication.SentAt).ToString("MM/dd/yyyy h:mm tt") + " MST";
                    TreeNode commNode = new TreeNode(sentAt + "  -  " + subject)
                    {
                        Tag = communication
                    };

                    List<CommunicationEvent> events = eventsByCommunication[communication.CommunicationID]
                        .OrderBy(ev => ev.EventTime)
                        .ToList();

                    if (events.Count == 0)
                    {
                        commNode.Nodes.Add(new TreeNode("No events recorded"));
                    }
                    else
                    {
                        foreach (CommunicationEvent evt in events)
                        {
                            string when = evt.EventTime.HasValue ? ToPhoenixTime(evt.EventTime.Value).ToString("MM/dd/yyyy h:mm tt") + " MST" : "(unknown time)";
                            TreeNode eventNode = new TreeNode((evt.EventType ?? "(unknown)") + "  -  " + when)
                            {
                                Tag = evt
                            };
                            commNode.Nodes.Add(eventNode);
                        }
                    }

                    treeCommunications.Nodes.Add(commNode);
                }
            }

            if (treeCommunications.Nodes.Count == 0)
            {
                treeCommunications.Nodes.Add(new TreeNode("No communications found for this member."));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Converts a UTC timestamp (as stored in <see cref="Communication.SentAt"/>
        /// and <see cref="CommunicationEvent.EventTime"/>) to America/Phoenix time.
        /// Phoenix does not observe daylight saving, so this is a fixed UTC-7 offset.
        /// </summary>
        private static DateTime ToPhoenixTime(DateTime utc)
        {
            DateTime utcKind = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(utcKind, PhoenixTimeZone);
        }
    }
}
