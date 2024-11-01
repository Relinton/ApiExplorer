namespace ApiExplorer.API_KEY__Criacao.Models.Interfaces
{
    public interface IReservaRepository
    {
        Reserva this[int id] { get; }
        IEnumerable<Reserva> Reservas { get; }
        Reserva AddReserva(Reserva reserva);
        Reserva UpdateReserva(Reserva reserva);
        void DeleteReserva(int id);
    }
}
