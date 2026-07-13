import React, { useEffect, useState } from 'react';
//import { getAllMots, createMot, updateMot, deleteMot } from '../services/motService';
import MotItem from '../components/MotItem';
import AddMotForm from '../components/AddMotForm';
import { getAllMots, createMot, deleteMot } from '../services/motService';

const GestionMots: React.FC = () => {
  const [mots, setMots] = useState<Array<{ id: string; terme: string; traduction: string }>>([]);

  useEffect(() => {
    loadMots();
  }, []);

  const loadMots = () => {
    getAllMots().then(setMots).catch(console.error);
  };

  const handleAddMot = (mot: { terme: string; traduction: string }) => {
    createMot(mot).then(newMot => setMots([...mots, newMot])).catch(console.error);
  };

  // const handleUpdateMot = (id: string, mot: { terme: string; traduction: string }) => {
  //   updateMot(id, mot).then(() => {
  //     setMots(mots.map(m => (m.id === id ? { ...m, ...mot } : m)));
  //   }).catch(console.error);
  // };

  const handleDeleteMot = (id: string) => {
    deleteMot(id).then(() => {
      setMots(mots.filter(m => m.id !== id));
    }).catch(console.error);
  };

  return (
    <div style={{ padding: '20px' }}>
      <h1>Gestion des Mots et Traductions</h1>
      <AddMotForm onAdd={handleAddMot} />
      <ul style={{ listStyle: 'none', padding: 0 }}>
        {mots.length ? (
          mots.map((m) => (
            <MotItem
              key={m.id}
              mot={m}
              //onUpdate={handleUpdateMot}
              onDelete={handleDeleteMot}
            />
          ))
        ) : (
          <p>Aucun mot à afficher</p>
        )}
      </ul>
    </div>
  );
};

export default GestionMots;
