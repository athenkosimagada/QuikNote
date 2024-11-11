using ErrorOr;

namespace QuikNote.ServiceErrors;

public static class Errors
{
    public static class Note
    {
        public static Error NotFound => Error.NotFound(
            code: "Note.NotFound",
            description: "Note not found");

        public static Error InvalidTitle => Error.Validation(
            code: "Note.InvalidTitle",
            description: $"Title must be at least {Models.Note.MinTitleLength}" + 
            $" and at most {Models.Note.MaxTitleLength} characters long");

        public static Error InvalidContent => Error.Validation(
            code: "Note.InvalidContent",
            description: $"Content must be at least {Models.Note.MinContentLength} characters long");

        public static Error InvalidTags => Error.Validation(
            code: "Note.InvalidTags",
            description: $"Tags must be at least {Models.Note.MinTagsLength}.");
    }
}