// MonFrontend/src/pages/InterroPage.tsx

import { useState, useEffect } from 'react';
import { fetchInterroItems } from '../services/motService';
import type { InterroItem } from '../types/interro';

export function InterroPage() {
  // --- ÉTATS (STATES) ---
  // La "mémoire" de notre composant

  // La liste des questions chargées depuis l'API
  const [items, setItems] = useState<InterroItem[]>([]);

  // L'index de la question actuellement affichée
  const [currentItemIndex, setCurrentItemIndex] = useState(0);

  // La valeur actuelle du champ de saisie de l'utilisateur
  const [userAnswer, setUserAnswer] = useState('');

  // Le score de l'utilisateur
  const [score, setScore] = useState(0);

  // Un booléen pour savoir si on doit afficher le score final
  const [showScore, setShowScore] = useState(false);

  // Un booléen pour afficher un message pendant que les données chargent
  const [loading, setLoading] = useState(true);
  
  // Pour stocker un message d'erreur s'il y en a un
  const [error, setError] = useState<string | null>(null);

  // --- LOGIQUE MÉTIER ---

  // Fonction pour charger (ou recharger) les questions
  const loadQuestions = () => {
    setLoading(true);
    setError(null);
    setShowScore(false);
    setCurrentItemIndex(0);
    setScore(0);
    
    fetchInterroItems(5) // On demande 5 questions à notre service
      .then((data) => {
        setItems(data);
      })
      .catch((err) => {
        console.error(err);
        setError("Impossible de charger l'interro. L'API est-elle bien lancée ?");
      })
      .finally(() => {
        setLoading(false);
      });
  };

  // Effet de bord : se déclenche une seule fois au montage du composant
  useEffect(() => {
    loadQuestions();
  }, []); // Le tableau vide [] signifie "ne s'exécute qu'une fois"

  // Gère la soumission du formulaire
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault(); // Empêche la page de se recharger
    if (!userAnswer) return; // Ne rien faire si le champ est vide

    const currentItem = items[currentItemIndex];
    
    // On compare la réponse (insensible à la casse et aux espaces)
    if (userAnswer.trim().toLowerCase() === currentItem.correctAnswer.toLowerCase()) {
      setScore(prevScore => prevScore + 1);
    }

    setUserAnswer(''); // On vide le champ de saisie

    const nextIndex = currentItemIndex + 1;
    if (nextIndex < items.length) {
      // S'il reste des questions, on passe à la suivante
      setCurrentItemIndex(nextIndex);
    } else {
      // Sinon, on affiche l'écran de score
      setShowScore(true);
    }
  };

  // --- AFFICHAGE (RENDU JSX) ---

  // Rendu conditionnel : on affiche un message de chargement si besoin
  if (loading) return <p>Chargement de l'interro...</p>;
  
  // Rendu conditionnel : on affiche un message d'erreur si l'API a échoué
  if (error) return <p style={{ color: 'red' }}>{error}</p>;

  // Rendu principal
  return (
    <div>
      <h2>Interro</h2>

      {showScore ? (
        // Vue n°1 : Affichage du score final
        <div>
          <h3>Votre score est de : {score} / {items.length}</h3>
          <button onClick={loadQuestions}>Rejouer</button>
        </div>
      ) : (
        // Vue n°2 : Affichage de la question actuelle
        items.length > 0 && ( // On s'assure qu'il y a bien des questions avant d'essayer de les afficher
          <div>
            <p>
              Question {currentItemIndex + 1} / {items.length}
            </p>
            <h3>Traduire : "{items[currentItemIndex].wordToTranslate}"</h3>
            
            <form onSubmit={handleSubmit}>
              <input
                type="text"
                value={userAnswer}
                onChange={(e) => setUserAnswer(e.target.value)}
                autoFocus // Met le focus automatiquement sur le champ
                placeholder="Votre réponse..."
              />
              <button type="submit">Valider</button>
            </form>
          </div>
        )
      )}
    </div>
  );
}

