using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;
namespace CommandAPI.Controllers;
[Route("api/command")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandRepository _commandRepository;
    private readonly IMapper _mapper;
    public CommandController(ICommandRepository commandRepository, IMapper mapper)
    {
        _commandRepository = commandRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
        IEnumerable<Command> commandItems = _commandRepository.GetAllCommands();
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    [HttpGet("{id}")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        Command commandItem = _commandRepository.GetCommandById(id);
        if (commandItem == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }
}