using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private readonly List<Note> _notes = new List<Note>();

    [HttpGet]
    public IActionResult GetNotes()
    {
        return Ok(_notes);
    }

    [HttpPost]
    public IActionResult AddNote([FromBody] Note note)
    {
        if (note == null)
        {
            return BadRequest("Invalid note data");
        }

        note.Id = Guid.NewGuid().ToString();
        _notes.Add(note);

        return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
    }

    [HttpGet("{id}")]
    public IActionResult GetNoteById(string id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null)
        {
            return NotFound();
        }

        return Ok(note);
    }
}

public class Note
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}