using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using QuikNote.Contracts.Note;
using QuikNote.Models;
using QuikNote.ServiceErrors;
using QuikNote.Services.Interface;

namespace QuikNote.Controllers;

public class NotesController : ApiController
{
    private readonly INoteService _noteService;
    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpPost]
    public IActionResult CreateNote(CreateNoteRequest request)
    {
       ErrorOr<Note> noteRequestResult = Note.Create(
            request.Title,
            request.Content,
            request.Tags);

        if(noteRequestResult.IsError)
        {
            return Problem(noteRequestResult.Errors);
        }

        Note note = noteRequestResult.Value;
        ErrorOr<Created> createNoteResult = _noteService.CreateNote(note);
        return createNoteResult.Match(
            created => CreatedAtAction(
                actionName: nameof(GetNote),
                routeValues: new { id = note.Id},
                value: MapNoteResponse(note)
            ),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetNote(Guid id)
    {
        ErrorOr<Note> getNoteResult = _noteService.GetNote(id);

        return getNoteResult.Match(
            note => Ok(MapNoteResponse(note)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateNote(Guid id, UpdateNoteRequest request)
    {
        ErrorOr<Note> noteRequestResult = Note.Create(
            request.Title,
            request.Content,
            request.Tags,
            request.CreatedDateTime);

        if(noteRequestResult.IsError)
        {
            return Problem(noteRequestResult.Errors);
        }

        Note note = noteRequestResult.Value;
        ErrorOr<Updated> updateNoteResult = _noteService.UpdateNote(note);
        return updateNoteResult.Match(
            updated => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteNote(Guid id)
    {
        ErrorOr<Deleted> deleteNoteResult = _noteService.DeleteNote(id);
        return deleteNoteResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private static NoteResponse MapNoteResponse(Note note)
    {
        return new NoteResponse(
                note.Id,
                note.Title,
                note.Content,
                note.Tags,
                note.CreatedDateTime,
                note.LastModifiedDateTime);
    }
}