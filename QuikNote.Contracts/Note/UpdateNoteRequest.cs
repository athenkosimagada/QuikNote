namespace QuikNote.Contracts.Note;

public record UpdateNoteRequest(
    string Title,
    string Content,
    string Tags,
    DateTime CreatedDateTime,
    DateTime LastModifiedDateTime
);