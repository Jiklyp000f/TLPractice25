import { JsonDiff } from "./json-diff.js";

const form = document.querySelector(`.main-form`);
const textareaOld = document.querySelector(`#oldJson`);
const textareaNew = document.querySelector(`#newJson`);
const resultBlock = document.querySelector(`.result`);
const button = document.querySelector(`.main-form button`);

form.addEventListener(`submit`, async (event) => {
  console.log(`submit`);
  event.preventDefault();
  const defualtButtonHtml = button.innerHTML;
  button.innerHTML = `Loading...`;
  button.disabled = true;
  const oldValue = JSON.parse(textareaOld.value);
  const newValue = JSON.parse(textareaNew.value);
  const result = await JsonDiff.create(oldValue, newValue);
  button.innerHTML = defualtButtonHtml;
  button.disabled = false;
  const jsonResult = JSON.stringify(result, undefined, 2);
  resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
  resultBlock.classList.add(`result__visible`);
});


class AuthModule {
  constructor() {
    this.user = null;
    this.initAuth();
  }

  initAuth() {
    this.createModal();
    this.authButton = document.querySelector('.header h2'); // Исправленный селектор
    this.startButton = document.createElement('div');
    this.mainForm = document.querySelector('.main-form');
    this.modal = document.querySelector('.auth-modal');
    
    // Изначально скрываем форму
    this.mainForm.style.display = 'none';
    this.updateUI();
    this.setupEventListeners();
  }

  createModal() {
    const modalHTML = `
      <div class="auth-modal" style="display: none;">
        <div class="auth-content">
          <input type="text" placeholder="Username" id="username">
          <button id="login-btn">Sign In</button>
        </div>
      </div>
    `;
    document.body.insertAdjacentHTML('beforeend', modalHTML);
  }

  setupEventListeners() {
    // Обработчик для кнопки Log In/Out
    this.authButton.addEventListener('click', (e) => {
      e.preventDefault();
      if (this.user) {
        this.logout();
      } else {
        this.showModal();
      }
    });

    // Обработчик для кнопки в модалке
    document.getElementById('login-btn').addEventListener('click', (e) => {
      e.preventDefault();
      const username = document.getElementById('username').value;
      if (username.trim()) this.login(username);
    });

    // Обработчик для кнопки Start
    this.startButton.addEventListener('click', () => {
      this.mainForm.style.display = 'block';
      this.startButton.style.display = 'none';
    });
  }

  login(username) {
    this.user = username;
    document.querySelector('.auth-modal').style.display = 'none';
    this.updateUI();
  }

  logout() {
    this.user = null;
    this.mainForm.style.display = 'none';
    this.updateUI();
  }

  showModal() {
    document.querySelector('.auth-modal').style.display = 'flex';
    document.getElementById('username').value = '';
  }

  updateUI() {
    // Обновление кнопки Log In/Out
    this.authButton.textContent = this.user ? 'Log out' : 'Log in';
    
    // Обновление приветствия
    let greeting = document.querySelector('.greeting');
    if (!greeting) {
      greeting = document.createElement('span');
      greeting.className = 'greeting';
      this.authButton.after(greeting);
    }
    greeting.textContent = this.user ? `Hello, ${this.user}!` : '';
    
    // Управление кнопкой Start
    if (this.user) {
      this.startButton.textContent = 'Start';
      this.startButton.className = 'start-button';
      document.querySelector('.container').appendChild(this.startButton);
    } else {
      if (this.startButton.parentElement) {
        this.startButton.parentElement.removeChild(this.startButton);
      }
    }
  }
}

// Инициализация системы авторизации
const auth = new AuthModule();

// Обработчик формы
document.querySelector('.main-form').addEventListener('submit', async (e) => {
  e.preventDefault();
  if (!auth.user) return;
  
  const button = e.target.querySelector('button');
  const defaultText = button.innerHTML;
  
  try {
    button.innerHTML = 'Loading...';
    button.disabled = true;
    
    const oldValue = JSON.parse(document.getElementById('oldJson').value);
    const newValue = JSON.parse(document.getElementById('newJson').value);
    const result = await JsonDiff.create(oldValue, newValue);
    
    document.querySelector('.result').innerHTML = 
      `<pre>${JSON.stringify(result, null, 2)}</pre>`;
    document.querySelector('.result').classList.add('result__visible');
  } catch (error) {
    console.error('Error:', error);
  } finally {
    button.innerHTML = defaultText;
    button.disabled = false;
  }
});