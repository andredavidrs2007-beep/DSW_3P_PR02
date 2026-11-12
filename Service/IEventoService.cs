using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Services;

public interface IEventoService
{
    List<Evento> ObtenerTodas();
    Evento? ObtenerPorId(int id);
    Evento Agregar(Evento nuevaEvento);
    bool Actualizar(int id, Evento actualizado);
    bool Eliminar(int id);
}