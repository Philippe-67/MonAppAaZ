import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { getMotById, updateMot, deleteMot } from '../services/motService';

const EditMotPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { register, handleSubmit, reset, formState: { errors, isSubmitting } } = useForm<{ terme: string; traduction: string }>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    getMotById(id)
      .then(data => {
        reset({ terme: data.terme, traduction: data.traduction });
      })
      .catch(err => {
        console.error(err);
        alert('Impossible de charger le mot');
      })
      .finally(() => setLoading(false));
  }, [id, reset]);

  const onSubmit = async (data: { terme: string; traduction: string }) => {
    if (!id) return;
    try {
      await updateMot(id, data);
      navigate('/mots');
    } catch (err) {
      console.error(err);
      alert('Erreur lors de la mise à jour');
    }
  };

  const handleDelete = async () => {
    if (!id) return;
    if (!window.confirm('Supprimer ce mot ?')) return;
    try {
      await deleteMot(id);
      navigate('/mots');
    } catch (err) {
      console.error(err);
      alert('Impossible de supprimer');
    }
  };

  if (loading) return <div style={{ padding: '20px' }}>Chargement...</div>;

  return (
    <div style={{ padding: '20px' }}>
      <h2>Voir / Modifier le mot</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div style={{ marginBottom: '10px' }}>
          <label>Mot</label><br />
          <input {...register('terme', { required: 'Terme requis' })} />
          {errors.terme && <p style={{ color: 'red' }}>{errors.terme.message}</p>}
        </div>
        <div style={{ marginBottom: '10px' }}>
          <label>Traduction</label><br />
          <input {...register('traduction', { required: 'Traduction requise' })} />
          {errors.traduction && <p style={{ color: 'red' }}>{errors.traduction.message}</p>}
        </div>
        <button type="submit" disabled={isSubmitting}>Enregistrer</button>
        <button type="button" onClick={() => navigate('/mots')} style={{ marginLeft: '10px' }}>Annuler</button>
        <button type="button" onClick={handleDelete} style={{ marginLeft: '10px', color: 'red' }}>Supprimer</button>
      </form>
    </div>
  );
};

export default EditMotPage;
