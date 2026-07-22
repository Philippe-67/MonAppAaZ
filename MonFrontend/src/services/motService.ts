import type { InterroItem } from '../types/interro'; // Assurez-vous que cet import est bien en haut du fichier avec les autres
import type { QuizQuestion } from '../types/quiz';


const API_URL = import.meta.env.VITE_API_URL; // Assure-toi que cette variable est bien configurée dans ton environnement

// Helper: map backend object to frontend shape
const mapBackendToFrontend = (m: any) => ({
  id: m.id ?? m.Id,
  terme: m.motFr ?? m.MotFr ?? m.MotFr ?? m.mot_fr,
  traduction: m.motEn ?? m.MotEn ?? m.mot_en,
});

// Helper: map frontend shape to backend payload
const mapFrontendToBackend = (m: { terme: string; traduction: string }) => ({
  motFr: m.terme,
  motEn: m.traduction,
});

// Récupérer tous les mots
export const getAllMots = () =>
  fetch(`${API_URL}/api/mots`).then(async res => {
    if (!res.ok) throw new Error(`Erreur ${res.status}`);
      const data = await res.json();
      const arr = Array.isArray(data) ? data : (data?.value ?? []);
      return Array.isArray(arr) ? arr.map(mapBackendToFrontend) : [];
  });

// Ajouter un nouveau mot
export const createMot = (mot: { terme: string; traduction: string }) =>
  fetch(`${API_URL}/api/mots`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(mapFrontendToBackend(mot)),
  }).then(async res => {
    if (!res.ok) throw new Error(`Erreur ${res.status}`);
    const data = await res.json();
    return mapBackendToFrontend(data);
  });

// Mettre à jour un mot existant
export const updateMot = (id: string, mot: { terme: string; traduction: string }) =>
  fetch(`${API_URL}/api/mots/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(mapFrontendToBackend(mot)),
  }).then(res => {
    if (res.status === 204) return null; // No Content, pas de corps
    return res.json(); // Si le backend retourne l'objet modifié
  });

// Supprimer un mot
export const deleteMot = (id: string) =>
  fetch(`${API_URL}/api/mots/${id}`, { method: 'DELETE' });

// Récupérer un mot par son id
export const getMotById = (id: string) =>
  fetch(`${API_URL}/api/mots/${id}`).then(async res => {
    if (!res.ok) throw new Error(`Erreur ${res.status}`);
    const data = await res.json();
    const item = data?.value ?? data;
    return mapBackendToFrontend(item);
  });

  // --- NOUVELLE FONCTION AJOUTÉE ICI (STYLE ASYNC/AWAIT) ---


export async function fetchInterroItems(count: number = 5): Promise<InterroItem[]> {
  // On récupère l'URL de base de l'API.
  // Note: votre fichier utilise API_URL, on va rester cohérent avec ça.
  const API_URL = import.meta.env.VITE_API_URL;

  // On construit l'URL complète pour notre nouvel endpoint
  const response = await fetch(`${API_URL}/api/Mots/interro?count=${count}`);
    
  // Si l'API renvoie une erreur (ex: 404, 500), on lève une exception
  // pour que le composant puisse l'attraper et afficher un message d'erreur.
  if (!response.ok) {
    const text = await response.text();
    throw new Error(`Erreur ${response.status} lors de la récupération des questions de l'interro: ${text}`);
  }

  return response.json();

}
// À ajouter à la fin de MonFrontend/src/services/motService.ts

export async function fetchQuizQuestions(count: number = 5): Promise<QuizQuestion[]> {
  const API_URL = import.meta.env.VITE_API_URL;
  const response = await fetch(`${API_URL}/api/Mots/quiz?count=${count}`);

  if (!response.ok) {
    const text = await response.text();
    throw new Error(`Erreur ${response.status} lors de la récupération des questions du quiz: ${text}`);
  }

  return response.json();
}




