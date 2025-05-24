const BASE_URL = 'https://rickandmortyapi.com/api';

let cachedTotal = null;

export async function getTotalCharacters() {
  if (!cachedTotal) {
    const res = await fetch(`${BASE_URL}/character`);
    const data = await res.json();
    cachedTotal = data.info.count;
  }
  return cachedTotal;
}

export async function getRandomCharacter() {
  const total = await getTotalCharacters()
  const id = Math.floor(Math.random() * total) + 1;
  const res = await fetch(`${BASE_URL}/character/${id}`);
  return res.json();
}

export async function getMultipleCharacters(ids) {
  const res = await fetch(`${BASE_URL}/character/${ids.join(',')}`);
  return res.json();
}
