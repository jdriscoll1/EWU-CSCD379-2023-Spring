import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { mdi } from 'vuetify/iconsets/mdi'

import './assets/main.css'
import '@mdi/font/css/materialdesignicons.css'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'mdi',
    sets: {
      mdi
    }
  },
  theme: {
    defaultTheme: 'dark',
    themes: {
      dark: {
        colors: {
          correct: '#4CAF50',
          misplaced: '#FFC107',
          wrong: '#F44336'
        }
      },
      inverse: {
        colors: {
          correct: '#F44336',
          misplaced: '#FFC107',
          wrong: '#4CAF50'
        }
      },
      oceandark: {
        dark: true,
        colors: {
          background: '#0277BD',
          surface: '#26C6DA',
          primary: '#FFFF8D',
          secondary: '#9FA8DA'
        }
      },
      oceanlight: {
        dark: false,
        colors: {
          background: '#0277BD',
          surface: '#26C6DA',
          primary: '#FFFF8D',
          secondary: '#9FA8DA'
        }
      },
      forestlight: {
        dark: false,
        colors: {
          background: '#2E7D32',
          surface: '#B2FF59',
          primary: '#C0CA33',
          secondary: '#6D4C41'
        }
      },
      forestdark: {
        dark: true,
        colors: {
          background: '#2E7D32',
          surface: '#B2FF59',
          primary: '#C0CA33',
          secondary: '#6D4C41'
        }
      },
    }
  }
})

const app = createApp(App)

app.use(vuetify)

app.use(router)

app.mount('#app')
