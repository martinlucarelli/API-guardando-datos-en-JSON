

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace API_guardando_datos_en_JSON;

[ApiController]
[Route("[controller]")]

public class AlumnosController : ControllerBase
{

    public List<Alumno> listaDeAlumnos;

    public readonly ILogger<AlumnosController> _logger;

    public readonly string _filePath = "F:\\Desktop\\Projects\\API guardando datos en JSON\\Inf\\Alumnos.json"; //aca va el path del archivo que queremos leer

    public AlumnosController(ILogger<AlumnosController> logger)
    {
        _logger=logger;

        if(System.IO.File.Exists(_filePath))
        {
            try
            {
                var json = System.IO.File.ReadAllText(_filePath);
                listaDeAlumnos = JsonSerializer.Deserialize<List<Alumno>>(json);


            }
            catch( Exception ex)
            {
                _logger.LogError("Error al leer el archivo JSON " + ex.Message);
                listaDeAlumnos=new List<Alumno>();
            }
        }
        else
        {

            listaDeAlumnos = new List<Alumno>();
        }

        if(!listaDeAlumnos.Any())
        {
            listaDeAlumnos.Add(new Alumno("Mario","Artoc",15,3));
            listaDeAlumnos.Add(new Alumno("Candela","Veron",18,6));
            listaDeAlumnos.Add(new Alumno("Martin","Luchetti",17,5));
            listaDeAlumnos.Add(new Alumno("Esteban","Quito",13,1));
        }

    }


    [HttpGet]

    public IEnumerable<Alumno> Get()
    {
        return listaDeAlumnos;
    }

    [HttpPost]
    public IActionResult Post(Alumno a)
    {
        listaDeAlumnos.Add(a);
        
        string json = JsonSerializer.Serialize(listaDeAlumnos,new JsonSerializerOptions {WriteIndented= true});
        System.IO.File.WriteAllText(_filePath,json);

        return Ok();
    }

    [HttpDelete("{id}")]

    public IActionResult Delete(Guid id)
    {

        Alumno a = listaDeAlumnos.FirstOrDefault(c=> c.id == id);
        
        if(a == null)
        {
            return NotFound();
        }

        listaDeAlumnos.Remove(a);

        System.IO.File.WriteAllText(_filePath, JsonSerializer.Serialize(listaDeAlumnos));
        
        return Ok();
    }




}