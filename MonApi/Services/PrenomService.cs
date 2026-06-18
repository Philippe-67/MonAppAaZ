namespace MonApi.Services
{
    public class PrenomService
    {
        private readonly List<string> _prenoms = new() { "Philippe", "Christelle", "Guillaume","Aurore","Margaux","Hugo" };

        public IEnumerable<string> GetAll() => _prenoms;
        public string? Get(int id) => (id >= 0 && id < _prenoms.Count) ? _prenoms[id] : null;
        public int Add(string prenom) { _prenoms.Add(prenom); return _prenoms.Count - 1; }
        public bool Update(int id, string prenom)
        {
            if (id < 0 || id >= _prenoms.Count) return false;
            _prenoms[id] = prenom;
            return true;
        }
        public bool Delete(int id)
        {
            if (id < 0 || id >= _prenoms.Count) return false;
            _prenoms.RemoveAt(id);
            return true;
        }
    }
}
