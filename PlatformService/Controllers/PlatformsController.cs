using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PlatformsController : ControllerBase
   {
      private readonly IPlatformRepo _repository;
      private readonly IMapper _mapper;

      public PlatformsController(IPlatformRepo repository, IMapper mapper)
      {
         _repository = repository;
         _mapper = mapper;
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
      public ActionResult<PlatformCreateDto> CreatePlatform(PlatformCreateDto platformCreateDto)
      {
         var platformModel = _mapper.Map<Platform>(platformCreateDto);
         _repository.CreatePlatform(platformModel);
         var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
         return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
      }
   }
}