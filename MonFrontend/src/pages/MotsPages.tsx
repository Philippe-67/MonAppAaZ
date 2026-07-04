import React, { useEffect, useState } from 'react';

interface Mot {
  id: string;     // propriété selon votre modèle
  motFr: string;
  motEn: string;
}

const MotsPage: React.FC = () => {
  const [mots, setMots] = useState<Mot[]>([]);

  useEffect(() => {
    fetch(`${import.meta.env.VITE_API_URL}/api/mots`)
      .then(res => {
        console.log('Response', res);
        return res.json();
      })
      .then(data => {
        console.log('Data received:', data);
        setMots(data);
      })
      .catch(err => {
        console.error('Erreur lors de la récupération des mots:', err);
      });
  }, []);

  return (
    <div>
      <h1>Liste des Mots</h1>
      {mots.length > 0 ? (
        <ul>
          {mots.map((mot) => (
            <li key={mot.id}>{mot.motFr} / {mot.motEn}</li>
          ))}
        </ul>
      ) : (
        <p>Aucun mot à afficher.</p>
      )}
    </div>
  );
};

export default MotsPage;
