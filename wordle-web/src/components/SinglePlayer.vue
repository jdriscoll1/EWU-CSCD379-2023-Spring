<template>
  <v-app>
    <v-main>
      <v-container>
        <v-card>
          <v-card-title>
            <h1>Game!</h1>
          </v-card-title>
          <v-card-text>
            <header class="text-center">{{ displayedWord }}</header>
            <p>_</p>
            <v-text-field v-model="inputWord" label="Enter Word"></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" @click="enterWord">Enter</v-btn>
            <v-btn color="error" @click="LoseGame()">Resign</v-btn>
          </v-card-actions>
        </v-card>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { ref } from 'vue'

var displayedWord = ref('frog')//change to axios get
var inputWord = ref('')

const emits = defineEmits<{
  (event: 'WinGame'): void
  (event: 'LoseGame'): void
}>()

//---------------------random game number---------------
function generateRandomNumber(): number {
  const maxInt = Number.MAX_SAFE_INTEGER;
  const randomNumber = Math.floor(Math.random() * maxInt) + 1;
  localStorage.setItem('randomNumber', randomNumber.toString());
  return randomNumber;
}

// Usage example:
let randomNumber: number;

const storedRandomNumber = localStorage.getItem('randomNumber');

if (storedRandomNumber) {
  randomNumber = parseInt(storedRandomNumber, 10);
} else {
  randomNumber = generateRandomNumber();
}

// Clear the stored random number on page refresh
window.addEventListener('beforeunload', () => {
  localStorage.removeItem('randomNumber');
});


console.log('Random Number:', randomNumber);
//-------------------------------------------------

function enterWord() {
  let tempWord = inputWord.value.toLowerCase()
  if (isInputValid(tempWord)) {
  console.log("Input is valid!");
  displayedWord.value = tempWord
  //post word to api w/ locally stored randomly generated game id
} else {
  console.log("Invalid input. Please enter a 4-letter string without special characters or numbers.");
  displayedWord.value = "Input must be 4 letters long, and must not have special characters or numbers."
}
  
}
/*
  Axios.post('word/AddWordFromBody', {
    text: 'strin',
    isCommon: true,
    isUsed: false
  })
    .then((response) => {
      overlay.value = false
      console.log(response.data)
    })
    .catch((error) => {
      console.log(error)
    }) */

function isInputValid(input: string): boolean {
  const specialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
  const containsSpecialChars = specialCharacters.test(input);
  
  const containsNumbers = /\d/.test(input);
  
  const isFourLettersLong = input.length === 4;

  return !containsSpecialChars && !containsNumbers && isFourLettersLong;
}

function WinGame() {
  localStorage.removeItem('randomNumber');
  emits('WinGame')
}
function LoseGame() {
  localStorage.removeItem('randomNumber');
  emits('LoseGame')
}
</script>

<style scoped>
h1 {
  font-size: 24px;
  margin-bottom: 16px;
}

header {
  font-size: 32px;
}
</style>