namespace QuikNote.Contracts.Note;

public record CreateNoteRequest(
    string Title,
    string Content,
    string Tags
);