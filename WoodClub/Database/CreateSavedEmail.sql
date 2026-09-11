-- Run against the WoodClub database (matches the style of dbo.MailingList).
-- Holds both manually-saved drafts and automatic copies made when an email is sent.

CREATE TABLE dbo.SavedEmail
(
    SavedEmailId    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Subject         NVARCHAR(255)     NOT NULL,   -- also shown as the title in the Open Email list
    BodyHtml        NVARCHAR(MAX)     NOT NULL,
    FromAddress     NVARCHAR(255)     NOT NULL,
    MailingListId   INT               NULL REFERENCES dbo.MailingList(MailingListId) ON DELETE SET NULL,
    SendToAll       BIT               NOT NULL DEFAULT (0),
    ExtraAddresses  NVARCHAR(MAX)     NULL,
    IsSent          BIT               NOT NULL DEFAULT (0),   -- 1 = auto-saved copy of a sent email, 0 = manually saved draft
    CreatedAt       DATETIME2(7)      NOT NULL DEFAULT (SYSUTCDATETIME()),
    CreatedBy       NVARCHAR(200)     NULL
);
