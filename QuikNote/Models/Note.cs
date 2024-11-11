using System.ComponentModel.DataAnnotations;
using ErrorOr;
using QuikNote.ServiceErrors;

namespace QuikNote.Models;

public class Note
{
    public const int MinTitleLength = 10;
    public const int MaxTitleLength = 150;
    public const int MinContentLength = 10;
    public const int MinTagsLength = 1;

    [Key]
    public Guid Id { get; private set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    // public Guid UserId { get; private set; }

    private Note() { }
    public Note(
        Guid id,
        string title,
        string content,
        string tags,
        DateTime createdDateTime,
        DateTime lastModifiedDateTime)
    {
        Id = id;
        Title = title;
        Content = content;
        Tags = tags;
        CreatedDateTime = createdDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
    }

    public static ErrorOr<Note> Create(
        string title,
        string content,
        string tags,
        DateTime? createdDateTime = null,
        Guid? id = null
    )
    {
        List<Error> errors = [];
        if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
        {
            errors.Add(Errors.Note.InvalidTitle);
        }

        if (content.Length < MinContentLength)
        {
            errors.Add(Errors.Note.InvalidContent);
        }

        if (tags.Split(',').Length < MinTagsLength)
        {
            errors.Add(Errors.Note.InvalidTags);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Note(
            id ?? Guid.NewGuid(),
            title,
            content,
            tags,
            createdDateTime ?? DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}