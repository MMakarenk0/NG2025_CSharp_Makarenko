const KEY = 'quiz-stats';

export function loadStats() {
  return JSON.parse(localStorage.getItem(KEY)) || {
    correct: 0,
    incorrect: 0,
    unanswered: 0
  };
}

export function saveStats(stats) {
  localStorage.setItem(KEY, JSON.stringify(stats));
}
