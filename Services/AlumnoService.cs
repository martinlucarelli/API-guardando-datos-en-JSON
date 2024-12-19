using API_guardando_datos_en_JSON;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;


public class AlumnoService : IAlumnoService
{

    public readonly string _filePath = "F:\\Desktop\\Projects\\API guardando datos en JSON\\Inf\\Alumnos.json"; //aca va el path del archivo que queremos leer
    public readonly ILogger<AlumnoService> _logger;


    public AlumnoService(ILogger<AlumnoService> logger) { _logger = logger;}

    public IEnumerable<Alumno> Get()
    {
        List<Alumno> listaDeAlumnos = new List<Alumno>();

        if (System.IO.File.Exists(_filePath))
        {
            try
            {
                var json = System.IO.File.ReadAllText(_filePath);
                listaDeAlumnos = JsonSerializer.Deserialize<List<Alumno>>(json);


            }
            catch (Exception ex)
            {
                _logger.LogError("Error al leer el archivo JSON " + ex.Message);
                listaDeAlumnos = new List<Alumno>();
            }
        }
        else
        {

            listaDeAlumnos = new List<Alumno>();
        }

        if (!listaDeAlumnos.Any())
        {
            listaDeAlumnos.Add(new Alumno("Mario", "Artoc", 15, 3));
            listaDeAlumnos.Add(new Alumno("Candela", "Veron", 18, 6));
            listaDeAlumnos.Add(new Alumno("Martin", "Luchetti", 17, 5));
            listaDeAlumnos.Add(new Alumno("Esteban", "Quito", 13, 1));
        }

        _logger.LogInformation("SE MOSTRO CORRECTAMENTE LA LISTA DE ALUMNOS");
        return listaDeAlumnos;
        
    }

    public void Save(Alumno a)
    {
        List<Alumno> listaDeAlumnos = Get().ToList();

        listaDeAlumnos.Add(a);

        string json = JsonSerializer.Serialize(listaDeAlumnos,new JsonSerializerOptions {WriteIndented= true});
        System.IO.File.WriteAllText(_filePath,json);

        _logger.LogInformation("SE GUARDO CORRECTAMENTE LA LISTA");

    }

    public void Delete(Guid id)
    {
        List<Alumno> listaDeAlumnos= Get().ToList();

        Alumno a = listaDeAlumnos.FirstOrDefault(c=> c.id == id);

        if(a !=null)
        {
            listaDeAlumnos.Remove(a);

            string json = JsonSerializer.Serialize(listaDeAlumnos, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_filePath, json);
        }

        _logger.LogInformation("SE ELIMINO CORRECTAMENTE EL REGISTRO");

    }


}









public interface IAlumnoService
{

    IEnumerable<Alumno> Get();

    void Save(Alumno a);

    void Delete(Guid id);

}