-- Run against the WoodClub database, after CreateSavedEmail.sql has already
-- been run (dbo.SavedEmail must exist first).
-- Holds the attached files for a saved/sent email so reopening it restores
-- its attachments too.

CREATE TABLE dbo.SavedEmailAttachment
(
    SavedEmailAttachmentId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    SavedEmailId           INT               NOT NULL REFERENCES dbo.SavedEmail(SavedEmailId) ON DELETE CASCADE,
    FileName                NVARCHAR(255)     NOT NULL,
    MimeType                NVARCHAR(255)     NOT NULL,
    Content                 VARBINARY(MAX)    NOT NULL
);
