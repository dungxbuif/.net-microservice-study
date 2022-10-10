using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PlatformsController : ControllerBase
   {
      private readonly IPlatformRepo _repository;
      private readonly IMapper _mapper;
      private readonly ICommandDataClient _commandDataClient;
      // private readonly IMessageBusClient _messageBusClient;

      public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient
            // IMessageBusClient messageBusClient
            )
      {
         _repository = repository;
         _mapper = mapper;
         _commandDataClient = commandDataClient;
         // _messageBusClient = messageBusClient;
      }

      [HttpGet]
      public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
      {
         var platforms = _repository.GetAllPlatforms();
         return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
      }

      [HttpGet("{id}", Name = "GetPlatformByid")]
      public ActionResult<PlatformReadDto> GetPlatformById(int id)
      {
         var platform = _repository.GetPlatformById(id);
         if (platform == null)
            return NotFound();
         return Ok(_mapper.Map<PlatformReadDto>(platform));
      }

      [HttpPost]
      public async Task<ActionResult<PlatformCreateDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
      {
         var platformModel = _mapper.Map<Platform>(platformCreateDto);
         _repository.CreatePlatform(platformModel);
         _repository.SaveChanges();

         var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

         // Send Sync Message
         try
         {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
         }
         catch (Exception ex)
         {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
         }

         // try
         // {
         //    var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
         //    platformPublishedDto.Event = "Platform_Published";
         //    _messageBusClient.PublishNewPlatform(platformPublishedDto);
         // }
         // catch (Exception ex)
         // {
         //    Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
         // }

         return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
      }

      [HttpDelete("{id}", Name = "DeletePlatformByid")]
      public void DeletePlatformById(int id)
      {
         _repository.DeletePlatformById(id);

      }
   }
}