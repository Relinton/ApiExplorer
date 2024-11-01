using ApiExplorer.API_KEY__Criacao.Models.Interfaces;

namespace ApiExplorer.API_KEY__Criacao.Models.Repository
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly Dictionary<int, Reserva> items;

        public ReservaRepository()
        {
            items = [];

            new List<Reserva>
            {
                new() { ReservaId = 1, Nome = "Relinton", InicioLocacao = "Porto Velho", FimLocacao = "Ribeirão Preto"},
                new() { ReservaId = 2, Nome = "Lucas", InicioLocacao = "Curitiba", FimLocacao = "Porto Velho" },
                new() { ReservaId = 3, Nome = "Francisco", InicioLocacao = "Campinas", FimLocacao = "Porto Velho" }
            }.ForEach(r => AddReserva(r));
        }

        public Reserva this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Reserva> Reservas => items.Values;

        public Reserva AddReserva(Reserva reserva)
        {
            if (reserva.ReservaId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; };
                reserva.ReservaId = key;
            }
            items[reserva.ReservaId] = reserva;
            return reserva;
        }

        public Reserva UpdateReserva(Reserva reserva)
        {
            AddReserva(reserva);
            return reserva;
        }

        public void DeleteReserva(int id)
        {
            items.Remove(id);
        }
    }
}
