import React, { useEffect, useState } from 'react';
//import type { Prenom } from '../types/Prenom';
import {
  getPrenoms,
  createPrenom,
  updatePrenom,
  deletePrenom,
} from '../services/prenomService';
import type { Prenom } from '../Types/Prenom';

const PrenomList: React.FC = () => {
  const [prenoms, setPrenoms] = useState<Prenom[]>([]);
  const [formData, setFormData] = useState<{ nom: string }>({ nom: '' });
  const [editingId, setEditingId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  // ✅ Chargement initial
  useEffect(() => {
    fetchPrenoms();
  }, []);

  const fetchPrenoms = async () => {
    try {
      const data = await getPrenoms();
      setPrenoms(data);
    } catch (err) {
      setError('Erreur lors du chargement');
    }
  };

  // ✅ Soumission formulaire (Create ou Update)
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingId) {
        await updatePrenom(editingId, formData);
        setEditingId(null);
      } else {
        await createPrenom(formData);
      }
      setFormData({ nom: ''});
      fetchPrenoms();
    } catch (err) {
      setError('Erreur lors de la sauvegarde');
    }
  };

  // ✅ Préparer l'édition
  const handleEdit = (prenom: Prenom) => {
    setEditingId(prenom.id!);
    setFormData({ nom: prenom.nom
     });
  };

  // ✅ Suppression
  const handleDelete = async (id: string) => {
    try {
      await deletePrenom(id);
      fetchPrenoms();
    } catch (err) {
      setError('Erreur lors de la suppression');
    }
  };

  return (
    <div>
      <h1>Gestion des Prénoms</h1>

      {error && <p style={{ color: 'red' }}>{error}</p>}

      {/* Formulaire Create / Update */}
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nom"
          value={formData.nom}
          onChange={(e) => setFormData({ ...formData, nom: e.target.value })}
          required
        />
       
        
        <button type="submit">{editingId ? 'Modifier' : 'Ajouter'}</button>
        {editingId && (
          <button type="button" onClick={() => {
            setEditingId(null);
            setFormData({ nom: ''});
          }}>
            Annuler
          </button>
        )}
      </form>

      {/* Liste des prénoms */}
      <ul>
        {prenoms.map((prenom) => (
          <li key={prenom.id}>
            <strong>{prenom.nom}</strong> 
                <button onClick={() => handleEdit(prenom)}>✏️ Modifier</button>
            <button onClick={() => handleDelete(prenom.id!)}>🗑️ Supprimer</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PrenomList;
