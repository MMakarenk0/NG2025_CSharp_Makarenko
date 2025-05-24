import { loadStats } from '../utils/storage.js';

export function renderStats(container) {
  const stats = loadStats();
  container.innerHTML = `
    <div class="stats">
      ✅ Correct: ${stats.correct} | ❌ Incorrect: ${stats.incorrect} | ❓ Unanswered: ${stats.unanswered}
    </div>
  `;
}
