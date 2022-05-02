using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Dtos;
using Xunit;
using CommandAPI.Profiles;
using CommandAPI.Controllers; 
using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandAPI.Test;

public class CommandsControllerTests : IDisposable
{
    Mock<ICommandRepository> mockRepo;
    CommandsProfile realProfile;
    MapperConfiguration configuration;
    IMapper mapper;

    public CommandsControllerTests()
    {
        mockRepo = new Mock<ICommandRepository>();
        realProfile = new CommandsProfile();
        configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
        mapper = new Mapper(configuration);
    }

    public void Dispose()
    {
        mockRepo = null;
        mapper = null;
        configuration = null;
        realProfile = null;
    }

    [Fact]
    public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
    {
        //Arrange 
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
        CommandController controller = new CommandController(mockRepo.Object, mapper);

        //Act
        var result = controller.GetAllCommands();

        //Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }


    private List<Command> GetCommands(int num)
    {
        List<Command> commands = new();
        if (num > 0)
        {
            commands.Add(new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            }); 
        }
        
        return commands;
    } 
}