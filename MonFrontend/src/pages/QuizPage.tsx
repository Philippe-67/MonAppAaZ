// MonFrontend/src/pages/QuizPage.tsx

import { useState, useEffect } from 'react';
import { fetchQuizQuestions } from '../services/motService';
import type { QuizQuestion } from '../types/quiz';

export function QuizPage() {
  // Helper: choose black or white text depending on background hex color for contrast
  const getContrastColor = (hex: string) => {
    try {
      // Normalize hex
      const h = hex.replace('#', '');
      const r = parseInt(h.length === 3 ? h[0] + h[0] : h.slice(0, 2), 16);
      const g = parseInt(h.length === 3 ? h[1] + h[1] : h.slice(2, 4), 16);
      const b = parseInt(h.length === 3 ? h[2] + h[2] : h.slice(4, 6), 16);
      // Perceived luminance
      const lum = 0.2126 * r + 0.7152 * g + 0.0722 * b;
      return lum > 150 ? '#000' : '#fff';
    } catch {
      return '#000';
    }
  };
  // --- ÉTATS ---
  const [questions, setQuestions] = useState<QuizQuestion[]>([]);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState<string | null>(null);
  const [score, setScore] = useState(0);
  const [showScore, setShowScore] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [answered, setAnswered] = useState(false);

  // --- LOGIQUE MÉTIER ---

  const loadQuestions = () => {
    setLoading(true);
    setError(null);
    setShowScore(false);
    setCurrentQuestionIndex(0);
    setScore(0);
    setSelectedAnswer(null);
    setAnswered(false);

    fetchQuizQuestions(5)
      .then(setQuestions)
      .catch(() => setError("Impossible de charger le quiz."))
      .finally(() => setLoading(false));
  };

  useEffect(() => {
    loadQuestions();
  }, []);

  const handleAnswerClick = (option: string) => {
    // Si on a déjà répondu, on ne peut pas changer
    if (answered) return;

    setSelectedAnswer(option);
    setAnswered(true);

    // Vérifier si la réponse est correcte
    if (option === questions[currentQuestionIndex].correctAnswer) {
      setScore(prevScore => prevScore + 1);
    }
  };

  const handleNextQuestion = () => {
    const nextIndex = currentQuestionIndex + 1;
    
    if (nextIndex < questions.length) {
      setCurrentQuestionIndex(nextIndex);
      setSelectedAnswer(null);
      setAnswered(false);
    } else {
      setShowScore(true);
    }
  };

  // --- AFFICHAGE ---

  if (loading) {
    return <div>Chargement du quiz...</div>;
  }

  if (error) {
    return (
      <div>
        <p>{error}</p>
        <button onClick={loadQuestions} style={{ padding: '10px 20px', fontSize: '16px' }}>
          Réessayer
        </button>
      </div>
    );
  }

  return (
    <div style={{ maxWidth: '600px', margin: '50px auto' }}>
      {showScore ? (
        // Affichage du score final
        <div style={{ textAlign: 'center' }}>
          <h2>Quiz Terminé!</h2>
          <p>
            Votre score : {score} / {questions.length}
          </p>
          <button onClick={loadQuestions} style={{ padding: '10px 20px', fontSize: '16px' }}>
            Rejouer
          </button>
        </div>
      ) : (
        // Vue principale : Affichage de la question
        questions.length > 0 && (
          <div>
            <p>
              Question {currentQuestionIndex + 1} / {questions.length}
            </p>
            <h3>Traduire : "{questions[currentQuestionIndex].wordToTranslate}"</h3>

            {/* Affichage des options sous forme de boutons */}
            <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginBottom: '20px' }}>
              {questions[currentQuestionIndex].options.map((option, index) => {
                const isSelected = selectedAnswer === option;
                const isCorrect = option === questions[currentQuestionIndex].correctAnswer && answered;
                let bgColor = '#f0f0f0';
                if (selectedAnswer === null) bgColor = '#4c4ca3';
                else if (isSelected) bgColor = option === questions[currentQuestionIndex].correctAnswer ? '#90EE90' : '#FFB6C6';
                else if (isCorrect) bgColor = '#90EE90';

                const textColor = getContrastColor(bgColor);

                return (
                  <button
                    key={index}
                    onClick={() => handleAnswerClick(option)}
                    disabled={answered}
                    style={{
                      padding: '12px 20px',
                      fontSize: '16px',
                      cursor: answered ? 'not-allowed' : 'pointer',
                      backgroundColor: bgColor,
                      color: textColor,
                      border: isSelected ? '2px solid #333' : '1px solid #ccc',
                      borderRadius: '4px',
                      transition: 'all 0.3s ease',
                      textAlign: 'left',
                    }}
                  >
                    {option}
                  </button>
                );
              })}
            </div>

            {/* Bouton "Suivant" qui apparaît après avoir répondu */}
            {answered && (
              <button
                onClick={handleNextQuestion}
                style={{
                  padding: '12px 24px',
                  fontSize: '16px',
                  backgroundColor: '#4CAF50',
                  color: 'white',
                  border: 'none',
                  borderRadius: '4px',
                  cursor: 'pointer',
                }}
              >
                {currentQuestionIndex === questions.length - 1 ? 'Voir le score' : 'Question suivante'}
              </button>
            )}
          </div>
        )
      )}
    </div>
  );
}
