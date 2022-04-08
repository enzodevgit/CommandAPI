using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;
namespace CommandAPI.Controllers;
[Route("api/command")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    public CommandController(ICommandRepository commandRepository)
    {
        _commandRepository = commandRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Command>> GetAllCommands()
    {
        IEnumerable<Command> commandItems = _commandRepository.GetAllCommands();
        return Ok(commandItems);
    }

    [HttpGet("{id}")]
    public ActionResult<Command> GetCommandById(int id)
    {
        Command commandItem = _commandRepository.GetCommandById(id);
        if (commandItem == null)
        {
            return NotFound();
        }
        return Ok(commandItem);
    }
}