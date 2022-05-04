using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using AutoMapper;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
namespace CommandAPI.Controllers;
[Route("api/command")]
[ApiController]
public class CommandController : ControllerBase
{
    //Random change
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

    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        Command commandItem = _commandRepository.GetCommandById(id);
        if (commandItem == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
        Command commandModel = _mapper.Map<Command>(commandCreateDto);
        _commandRepository.CreateCommand(commandModel);
        _commandRepository.SaveChanges();

        CommandReadDto commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
        return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        Command commandModelFromRepo = _commandRepository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }

        _mapper.Map(commandUpdateDto, commandModelFromRepo);
        _commandRepository.UpdateCommand(commandModelFromRepo);
        _commandRepository.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        Command commandModelFromRepo = _commandRepository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        
        CommandUpdateDto commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);


        patchDoc.ApplyTo(commandToPatch, ModelState);

        if (!TryValidateModel(commandToPatch))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(commandToPatch, commandModelFromRepo);
        _commandRepository.UpdateCommand(commandModelFromRepo);
        _commandRepository.SaveChanges();
        
        return NoContent();
    }


    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        Command commandModelFromRepo = _commandRepository.GetCommandById(id);
        if (commandModelFromRepo == null)
        {
            return NotFound();
        }
        _commandRepository.DeleteCommand(commandModelFromRepo);
        _commandRepository.SaveChanges();
        return NoContent();
    }
}