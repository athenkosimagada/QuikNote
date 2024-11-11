using ErrorOr;
using QuikNote.Models;

namespace QuikNote.Services.Interface;

public interface INoteService 
{
    ErrorOr<Created> CreateNote(Note note);
    ErrorOr<Deleted> DeleteNote(Guid id);
    ErrorOr<Note> GetNote(Guid id);
    ErrorOr<Updated> UpdateNote(Note note);
}