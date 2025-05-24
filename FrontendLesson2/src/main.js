import './assets/style.css';
import { renderQuiz } from './components/quizCard.js';
import { renderStats } from './components/stats.js';

const app = document.querySelector('#app');

const stats = document.createElement('div');
stats.id = 'stats';

const difficultySelect = document.createElement('select');
['easy', 'medium', 'hard'].forEach(level => {
  const option = document.createElement('option');
  option.value = level;
  option.textContent = `Difficulty: ${level.charAt(0).toUpperCase() + level.slice(1)}`;
  difficultySelect.appendChild(option);
});
difficultySelect.id = 'difficulty';

const quiz = document.createElement('div');
quiz.id = 'quiz';

difficultySelect.addEventListener('change', () => {
  quiz.innerHTML = '';
  renderQuiz(quiz, difficultySelect.value);
});

app.appendChild(stats);
app.appendChild(difficultySelect);
app.appendChild(quiz);

renderStats(stats);
renderQuiz(quiz, 'easy');
