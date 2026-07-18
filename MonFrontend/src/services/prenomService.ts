// import type { Prenom } from '../Types/Prenom';

// const API_URL = 'http://localhost:5270/api/prenoms';

// type PrenomPayload = Pick<Prenom, 'nom'>;

// // GET ALL
// export const getPrenoms = async (): Promise<Prenom[]> => {
//   const response = await fetch(API_URL);
//   if (!response.ok) throw new Error('Erreur lors de la récupération');
//   return response.json();
// };

// // GET ONE
// export const getPrenomById = async (id: string): Promise<Prenom> => {
//   const response = await fetch(`${API_URL}/${id}`);
//   if (!response.ok) throw new Error('Prénom introuvable');
//   return response.json();
// };

// // CREATE
// export const createPrenom = async (prenom: PrenomPayload): Promise<Prenom> => {
//   const response = await fetch(API_URL, {
//     method: 'POST',
//     headers: { 'Content-Type': 'application/json' },
//     body: JSON.stringify(prenom),
//   });
//   if (!response.ok) throw new Error('Erreur lors de la création');
//   return response.json();
// };

// // UPDATE
// export const updatePrenom = async (id: string, prenom: PrenomPayload): Promise<void> => {
//   const response = await fetch(`${API_URL}/${id}`, {
//     method: 'PUT',
//     headers: { 'Content-Type': 'application/json' },
//     body: JSON.stringify(prenom),
//   });
//   if (!response.ok) throw new Error('Erreur lors de la mise à jour');
// };

// // DELETE
// export const deletePrenom = async (id: string): Promise<void> => {
//   const response = await fetch(`${API_URL}/${id}`, {
//     method: 'DELETE',
//   });
//   if (!response.ok) throw new Error('Erreur lors de la suppression');
// };
