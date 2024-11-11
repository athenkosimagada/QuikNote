using ErrorOr;
using QuikNote.Data;
using QuikNote.Models;
using QuikNote.ServiceErrors;
using QuikNote.Services.Interface;

namespace QuikNote.Services;

public class NoteService : INoteService
{
    private readonly ApplicationDbContext _dbContext;
    public NoteService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public ErrorOr<Created> CreateNote(Note note)
    {
        _dbContext.Notes.Add(note);
        _dbContext.SaveChanges();
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteNote(Guid id)
    {
        ErrorOr<Guid> validateNoteExists = ValidateNoteExists(id);
        if (validateNoteExists.IsError)
        {
            return validateNoteExists.Errors;
        }
        _dbContext.Notes.Remove(_dbContext.Notes.Find(id)!);
        _dbContext.SaveChanges();
        return Result.Deleted;
    }

    public ErrorOr<Note> GetNote(Guid id)
    {
        Note note = _dbContext.Notes.Find(id)!;

        if (note == null)
        {
            return Errors.Note.NotFound;
        }

        return note;
    }

    public ErrorOr<Updated> UpdateNote(Note note)
    {
        Note? noteToUpdate = _dbContext.Notes.Find(note.Id);

        if (noteToUpdate == null)
        {
            return Errors.Note.NotFound;
        }

        noteToUpdate.Title = note.Title;
        noteToUpdate.Content = note.Content;
        noteToUpdate.Tags = note.Tags;
        noteToUpdate.LastModifiedDateTime = note.LastModifiedDateTime;

        _dbContext.Notes.Update(noteToUpdate);
        _dbContext.SaveChanges();
        return Result.Updated;
    }

    private ErrorOr<Guid> ValidateNoteExists(Guid id)
    {
        if (!_dbContext.Notes.Any(note => note.Id == id))
        {
            return Errors.Note.NotFound;
        }
        return id;
    }
}