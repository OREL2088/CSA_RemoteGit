using ClientServe.Models;

namespace ClientServe.Storage
{
    public class MemCache : IStorage<GamingСonsoles>
    {
        private object _sync = new object();
        private List<GamingСonsoles> _Console = new List<GamingСonsoles>();
        public GamingСonsoles this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectLabDataException($"No GamingСonsoles with id {id}");

                    return _Console.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectLabDataException("Cannot request GamingСonsoles with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _Console.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<GamingСonsoles> All => _Console.Select(x => x).ToList();

        public void Add(GamingСonsoles value)
        {
            if (value.Id == Guid.Empty) throw new IncorrectLabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _Console.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _Console.RemoveAll(x => x.Id == id);
            }
        }
    }
}
