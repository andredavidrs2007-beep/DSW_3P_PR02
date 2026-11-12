using DSW_3P_PR02.Models;
using System.Text.Json;

namespace DSW_3P_PR02.Repositories;

public class EventoJsonRepository : IEventoRepository
{
    private readonly string _ruta;
    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public EventoJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data", "Eventoes.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    private List<Evento> Leer()
    {
        string Json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Evento>>(Json) ?? new List<Evento>();
    }

    private void Guardar(List<Evento> datos)
    {
        string Json = JsonSerializer.Serialize(datos, _opciones);
        
        File.WriteAllText(_ruta, Json);
    }

    public List<Evento> ObtenerTodos()
    {
        return Leer();
    }

    public Evento? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(d => d.Id == id);
    }

    public Evento Agregar(Evento nueva)
    {
        var datos = Leer();

        nueva.Id = datos.Count == 0 ? 1 : datos.Max(d => d.Id + 1);
        datos.Add(nueva);
        Guardar(datos);

        return nueva;
    }

    public bool Actualizar(int id, Evento actualizada)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(d => d.Id == id);

        if(existente == null)
        {
            return false;
        }
        existente.Nombre = actualizada.Nombre;
        existente.Categoria = actualizada.Categoria;
        existente.Fecha = actualizada.Fecha;
        existente.Lugar = actualizada.Lugar;
        existente.CupoMaximo = actualizada.CupoMaximo;
        existente.Activo = actualizada.Activo;

        Guardar(datos);

        return true;
    }

    public bool Eliminar(int id)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(d => d.Id == id);

        if(existente == null)
        {
            return false;
        }
        datos.Remove(existente);
        Guardar(datos);

        return true;
    }
}