-- Run against the WoodClub database, after CreateSavedEmail.sql has already
-- been run (dbo.SavedEmail must exist first).
-- Links each Communications row back to the SavedEmail "sent copy" it was
-- part of, so events (e.g. dropped/blocked) can be queried directly by the
-- list send that caused them, rather than only by Subject/SentAt matching.

ALTER TABLE dbo.Communications
    ADD SavedEmailId INT NULL REFERENCES dbo.SavedEmail(SavedEmailId) ON DELETE SET NULL;
