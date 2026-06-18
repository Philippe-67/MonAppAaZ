import { useEffect, useState } from 'react';

function App() {
  const [prenoms, setPrenoms] = useState<string[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_URL}/prenoms`)
      .then(res => {
        if (!res.ok) throw new Error('Erreur API');
        return res.json();
      })
      .then(data => {
        setPrenoms(data);
        setLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  return (
    <div>
      <h1>Liste des prénoms</h1>
      {loading && <p>Chargement...</p>}
      {error && <p style={{ color: 'red' }}>Erreur : {error}</p>}
      <ul>
        {prenoms.map((prenom, idx) => (
          <li key={idx}>{prenom}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
