import { JsonDiff } from "./json-diff.js";

const form = document.querySelector(`.main-form`);
const textareaOld = document.querySelector(`#oldJson`);
const textareaNew = document.querySelector(`#newJson`);
const resultBlock = document.querySelector(`.result`);
const button = document.querySelector(`.main-form button`);
const mainContainer = document.querySelector('.container');

form.addEventListener(`submit`, async (event) => {
  event.preventDefault();
  const defualtButtonHtml = button.innerHTML;
  
  try {
    // Сброс предыдущих ошибок
    document.querySelectorAll('.error-message').forEach(el => el.remove());
    resultBlock.classList.remove(`result__visible`);
    
    // Валидация полей
    let hasErrors = false;
    if (!textareaOld.value.trim()) {
      showError(textareaOld, 'Please enter old JSON');
      hasErrors = true;
    }
    if (!textareaNew.value.trim()) {
      showError(textareaNew, 'Please enter new JSON');
      hasErrors = true;
    }
    if (hasErrors) return;

    button.innerHTML = `Loading...`;
    button.disabled = true;

    // Парсинг JSON с обработкой ошибок
    const oldValue = parseJSON(textareaOld.value, textareaOld);
    const newValue = parseJSON(textareaNew.value, textareaNew);
    
    const result = await JsonDiff.create(oldValue, newValue);
    const jsonResult = JSON.stringify(result, undefined, 2);
    resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
    resultBlock.classList.add(`result__visible`);
  } catch (error) {
    console.error('Error:', error);
    resultBlock.innerHTML = `<div class="error">${error.message}</div>`;
    resultBlock.classList.add(`result__visible`);
  } finally {
    button.innerHTML = defualtButtonHtml;
    button.disabled = false;
  }
});

function parseJSON(jsonString, textarea) {
  try {
    return JSON.parse(jsonString);
  } catch (error) {
    showError(textarea, 'Invalid JSON format');
    throw new Error(`Invalid JSON in ${textarea.id}`);
  }
}

function showError(inputElement, message) {
  const error = document.createElement('div');
  error.className = 'error-message';
  error.textContent = message;
  error.style.color = 'red';
  error.style.fontSize = '0.8em';
  inputElement.parentNode.insertBefore(error, inputElement.nextSibling);
}


class AuthModule {
  constructor() {
    this.user = null;
    this.initAuth();
  }

  initAuth() {
    this.createAuthForm();
    this.authButton = document.querySelector('.header h2');
    this.startButton = document.createElement('h2');
    this.mainForm = document.querySelector('.main-form');
    this.authForm = document.querySelector('.auth-form');
    
    this.mainForm.style.display = 'none';
    this.updateUI();
    this.setupEventListeners();
  }

  createAuthForm() {
    const authHTML = `
      <div class="auth-form">
        <div class="auth-content">
          <input type="text" placeholder="Username" id="username">
          <button id="login-btn">Sign In</button>
        </div>
      </div>
    `;
    mainContainer.insertAdjacentHTML('afterbegin', authHTML);
  }

  setupEventListeners() {
    this.authButton.addEventListener('click', (e) => {
      e.preventDefault();
      if (this.user) {
        this.logout();
      } else {
        this.showAuthForm();
      }
    });

    document.getElementById('login-btn').addEventListener('click', (e) => {
      e.preventDefault();
      const username = document.getElementById('username').value;
      if (username.trim()) this.login(username);
    });

    this.startButton.addEventListener('click', () => {
      this.mainForm.style.display = 'grid';
      this.startButton.style.display = 'none';
      mainContainer.querySelector('.title-block').style.display = 'none';
    });
  }

  login(username) {
    this.user = username;
    document.querySelector('.auth-form').style.display = 'none';
    this.updateUI();
  }

  logout() {
    this.user = null;
    this.mainForm.style.display = 'none';
    document.querySelector('.auth-form').style.display = 'block';
    mainContainer.querySelector('.title-block').style.display = 'block';
    this.updateUI();
  }

  showAuthForm() {
    document.querySelector('.auth-form').style.display = 'flex';
    document.getElementById('username').value = '';
  }

  updateUI() {
    this.authButton.textContent = this.user ? 'Log out' : 'Log in';
    
    let greeting = document.querySelector('.greeting');
    if (!greeting) {
      greeting = document.createElement('span');
      greeting.className = 'greeting';
      this.authButton.after(greeting);
    }
    greeting.textContent = this.user ? `Hello, ${this.user}!` : '';
    
    if (this.user) {
      this.startButton.textContent = 'Start';
      this.startButton.className = 'start-heading';
      mainContainer.appendChild(this.startButton);
    } else {
      if (this.startButton.parentElement) {
        this.startButton.parentElement.removeChild(this.startButton);
      }
    }
  }
}

const auth = new AuthModule();

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
    button.innerHTML = 'Show difference';
    button.disabled = false;
  }
});