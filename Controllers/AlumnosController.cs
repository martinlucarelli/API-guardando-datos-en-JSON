

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace API_guardando_datos_en_JSON;

[ApiController]
[Route("[controller]")]

public class AlumnosController : ControllerBase
{

   private readonly IAlumnoService _alumnoService;

    public AlumnosController(IAlumnoService alumnoService)
    {
        _alumnoService = alumnoService;
    }


    [HttpGet]

    public IEnumerable<Alumno> Get()
    {
        return _alumnoService.Get();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Alumno a)
    {
        _alumnoService.Save(a);
        
        return Ok();
    }

    [HttpDelete("{id}")]

    public IActionResult Delete(Guid id)
    {

      _alumnoService.Delete(id);

        return Ok();
    }




}