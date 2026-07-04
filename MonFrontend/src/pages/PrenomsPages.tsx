import React, { useEffect, useState } from 'react';

const PrenomsPage: React.FC = () => {
  const [prenoms, setPrenoms] = useState<{ nom: string }[]>([]);

const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    fetch(`${apiUrl}/api/prenoms`)
      .then(res => res.json())
         .then(data => {
      console.log('Données reçues :', data);
      setPrenoms(data);
    })
      .catch(err => console.error('Erreur lors du chargement des prénoms :', err));
  }, []);

return (
  <div>
    <h1>Liste des Prénoms</h1>
    <ul>
         {prenoms.length > 0 ? (
      prenoms.map((prenom, index) => (
        <li key={index}>{prenom.nom}</li>  // ici, on affiche la propriété 'nom'
      ))
    ) : (
      <p>Aucun prénom à afficher</p>
      )}
    </ul>
  </div>
)};

export default PrenomsPage;
