import { useEffect, useState } from 'react';
import type { Prenom } from './Types/Prenom';

function App() {
  const [prenoms, setPrenoms] = useState<Prenom[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_URL}/api/prenoms`)
      .then((res) => {
        if (!res.ok) throw new Error('Erreur API');
        return res.json();
      })
      .then((data: Prenom[]) => { // Indiquez le type ici
        setPrenoms(data);
        setLoading(false);
      })
      .catch((err) => {
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
        {prenoms.map((prenom) => (
          <li key={prenom.id}>{prenom.nom}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
