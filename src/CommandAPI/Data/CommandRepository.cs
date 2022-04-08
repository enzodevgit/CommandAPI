using CommandAPI.Models;
using System.Collections.Generic;
namespace CommandAPI.Data;
public class CommandRepository : ICommandRepository
{
    private readonly CommandContext _context;
    public CommandRepository(CommandContext context)
    {
        _context = context;
    }
    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
        return _context.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
        return _context.Commands.FirstOrDefault(x => x.Id == id);
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}