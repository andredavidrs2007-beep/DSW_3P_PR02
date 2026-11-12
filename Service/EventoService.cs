using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ArgumentException("El nombre no puede estar vacio");
        if (evento.Categoria == null)
            throw new ArgumentException("La categoria no puede estar vacia");
        if (evento.Fecha < DateTime.Now)
            throw new ArgumentException("La fecha no puede ser anterior a la fecha actual");
        if (evento.CupoMaximo <= 0)
            throw new ArgumentException("El cupo máximo no puede ser negativo o cero");
    }

    public Evento? ObtenerPorId(int id)
    {
        var Evento = _repository.ObtenerPorId(id);

        return Evento;
    }

    public List<Evento> ObtenerTodas()
    {
        return _repository.ObtenerTodos();
    }

    public Evento Agregar(Evento nuevaEvento)
    {
        Validar(nuevaEvento);

        return _repository.Agregar(nuevaEvento);
    }

    public bool Actualizar(int id, Evento Evento)
    {
        Validar(Evento);

        return _repository.Actualizar(id, Evento);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}