import React from 'react';
import { useForm } from 'react-hook-form';

interface AddMotFormProps {
  onAdd: (mot: { terme: string; traduction: string }) => void;
}

const AddMotForm: React.FC<AddMotFormProps> = ({ onAdd }) => {
  const { register, handleSubmit, reset, formState: { errors } } = useForm<{ terme: string; traduction: string }>();

  const onSubmit = (data: { terme: string; traduction: string }) => {
    onAdd(data);
    reset(); // vide le formulaire après soumission
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} style={{ marginBottom: '20px' }}>
      <div style={{ marginBottom: '10px' }}>
        <input
          {...register('terme', { required: 'Ce champ est requis' })}
          placeholder="Mot"
        />
        {errors.terme && <p style={{ color: 'red' }}>{errors.terme.message}</p>}
      </div>
      <div style={{ marginBottom: '10px' }}>
        <input
          {...register('traduction', { required: 'Ce champ est requis' })}
          placeholder="Traduction"
        />
        {errors.traduction && <p style={{ color: 'red' }}>{errors.traduction.message}</p>}
      </div>
      <button type="submit">Ajouter</button>
    </form>
  );
};

export default AddMotForm;
