namespace WoodClub
{
    /// <summary>
    /// A single file attached to an outgoing email as a real attachment (not
    /// one of the inline base64 images embedded directly in the HTML body -
    /// those go through a separate path in <see cref="Forms.MailComposer"/>).
    /// </summary>
    public class EmailAttachment
    {
        public string FileName { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }
    }
}
