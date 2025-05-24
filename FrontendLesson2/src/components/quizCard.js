import { getRandomCharacter, getMultipleCharacters, getTotalCharacters } from '../api/api.js';
import { loadStats, saveStats } from '../utils/storage.js';
import { renderStats } from './stats.js';

const difficultyOptions = {
  easy: {
    question: 'What is the name of this character?',
    getField: character => character.name,
    async getOptions(correctCharacter, total) {
      const options = new Set([correctCharacter.name]);
      while (options.size < 4) {
        const randChar = await getRandomCharacter();
        options.add(randChar.name);
      }
      return Array.from(options);
    },
    check: (selected, character) => selected === character.name
  },

  medium: {
    question: 'What is the status of this character?',
    getField: character => character.status,
    async getOptions(correctCharacter) {
      const statuses = ['Alive', 'Dead', 'unknown'];
      const filtered = statuses.filter(s => s !== correctCharacter.status);
      const options = [correctCharacter.status, ...filtered.slice(0, 3)];
      return options.sort(() => Math.random() - 0.5);
    },
    check: (selected, character) => selected === character.status
  },

  hard: {
    question: 'What is the origin of this character?',
    getField: character => character.origin.name,
    async getOptions(correctCharacter, total) {
      const options = new Set([correctCharacter.origin.name]);
      while (options.size < 4) {
        const randChar = await getRandomCharacter();
        if (randChar.origin?.name) options.add(randChar.origin.name);
      }
      return Array.from(options);
    },
    check: (selected, character) => selected === character.origin.name
  }
};

export async function renderQuiz(container, difficulty = 'easy') {
  const main = document.createElement('div');
  main.className = 'quiz-card';

  const character = await getRandomCharacter();
  const total = await getTotalCharacters();

  const options = await difficultyOptions[difficulty].getOptions(character, total);
  const answer = difficultyOptions[difficulty].getField(character);
  const shuffled = options.sort(() => Math.random() - 0.5);

  main.innerHTML = `
    <h2>${difficultyOptions[difficulty].question}</h2>
    <img src="${character.image}" alt="Character" />
    <div class="options">
      ${shuffled.map(opt => `<button data-value="${opt}">${opt}</button>`).join('')}
    </div>
    <button id="refresh">🔁 New Question</button>
  `;

  const stats = loadStats();
  let answered = false;

  main.querySelectorAll('.options button').forEach(btn => {
    btn.addEventListener('click', () => {
      if (answered) return;
      answered = true;
      const selected = btn.dataset.value;
      if (difficultyOptions[difficulty].check(selected, character)) {
        btn.classList.add('correct');
        stats.correct++;
      } else {
        btn.classList.add('incorrect');
        stats.incorrect++;
      }
      saveStats(stats);
      renderStats(document.querySelector('#stats'));
    });
  });

  main.querySelector('#refresh').addEventListener('click', () => {
    if (!answered) {
      stats.unanswered++;
      saveStats(stats);
      renderStats(document.querySelector('#stats'));
    }
    container.innerHTML = '';
    renderQuiz(container, difficulty);
  });

  container.appendChild(main);
}

