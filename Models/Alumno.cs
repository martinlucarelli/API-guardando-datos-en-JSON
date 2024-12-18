namespace API_guardando_datos_en_JSON;

public class Alumno
{

    public Guid id{get;set;}
    public string? nombre{get;set;}
    public string? apellido{get;set;}
    public int edad{get;set;}
    public int anio{get;set;}



    
    public Alumno(string nombre,string apellido, int edad, int anio)
    {
        this. id= Guid.NewGuid();
        this.nombre=nombre;
        this.apellido=apellido;
        this.edad=edad;
        this.anio=anio;
    }


}