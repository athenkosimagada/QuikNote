namespace QuikNote.Contracts.Note;

public record NoteResponse(
    Guid Id,
    string Title,
    string Content,
    string Tags,
    DateTime CreatedDateTime,
    DateTime LastModifiedDateTime
);