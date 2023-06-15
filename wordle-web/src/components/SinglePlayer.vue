<template>
  
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
   
</template>

<script setup lang="ts">
import Axios from 'axios'
import { ref } from 'vue'

var displayedWord = ref('') 
var currGameId = -1; 
var inputWord = ref('')

const emits = defineEmits<{
  (event: 'WinGame'): void
  (event: 'LoseGame'): void
}>()


  Axios.get('/api/GameController/StartGame')
  .then((response) => {
    
    displayedWord.value = response.data.Word
    currGameId = response.data.GameId
    console.log("Curr Game Id: " + currGameId + " First Word: " + displayedWord.value); 
    
  })
  .catch((error) => {
    console.log(error)
  }) 

function enterWord() {
  let tempWord = inputWord.value.toLowerCase()
  if (isInputValid(tempWord)) {
    console.log('Input is valid!')
    displayedWord.value = tempWord
    //post word to api
    Axios.post('api/GameController/AcceptInput', {
    word: tempWord,
    gameId: currGameId,
  })
    .then((response) => {
      //time out, wait a moment, overlay?
      //overlay.value = false
      console.log(response.data)
      if(response.data.Word != "Game Over"){
        displayedWord.value = response.data.Word
      }else{
         WinGame()
      }

    })
    .catch((error) => {
      console.log(error)
    }) 
  } else {
    console.log(
      'Invalid input. Please enter a 4-letter string without special characters or numbers.'
    )
    displayedWord.value =
      'Input must be 4 letters long, and must not have special characters or numbers.'
  }
}

function isInputValid(input: string): boolean {
  const specialCharacters = /[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]+/
  const containsSpecialChars = specialCharacters.test(input)

  const containsNumbers = /\d/.test(input)

  const isFourLettersLong = input.length === 4

  return !containsSpecialChars && !containsNumbers && isFourLettersLong
}

function WinGame() {
  localStorage.removeItem('randomNumber')
  emits('WinGame')
}
function LoseGame() {
  localStorage.removeItem('randomNumber')
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
