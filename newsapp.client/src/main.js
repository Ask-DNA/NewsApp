import { createApp } from 'vue'
import App from './App.vue'

const app = createApp(App)

fetch(import.meta.env.BASE_URL + 'config.json')
  .then((response) => response.json())
  .then((config) => {
    for (const key in config) {
      app.provide(key, config[key])
    }
    app.mount("#app")
  }).catch(error => {
    console.error('Error loading config:', error);
  });
