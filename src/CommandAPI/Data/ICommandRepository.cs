using CommandAPI.Models;
using System.Collections.Generic;

namespace CommandAPI.Data;
public interface ICommandRepository
{
    bool SaveChanges();
    IEnumerable<Command> GetAllCommands();
    Command GetCommandById(int id);
    void CreateCommand(Command cmd);
    void DeleteCommand(Command cmd);
    void UpdateCommand(Command cmd);
    
}