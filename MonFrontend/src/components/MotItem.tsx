import React from 'react';
import { Link } from 'react-router-dom';

interface Mot {
  id: string;
  terme: string;
  traduction: string;
}

interface MotItemProps {
  mot: Mot;
  onDelete: (id: string) => void;
}

const MotItem: React.FC<MotItemProps> = ({ mot, onDelete }) => {
  return (
    <li>
      <strong>{mot.terme}</strong> — {mot.traduction}
      <Link to={`/mots/${mot.id}`} style={{ marginLeft: '10px' }}>Voir / Modifier</Link>
      <button onClick={() => onDelete(mot.id)} style={{ marginLeft: '10px' }}>Supprimer</button>
    </li>
  );
};

export default MotItem;
