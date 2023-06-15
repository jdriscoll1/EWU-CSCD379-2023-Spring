<template>
  <div style="display: flex; justify-content: center; align-items: center; height: 100vh">
    <v-card class="mx-auto" max-width="344" variant="outlined">
      <v-card-item>
        <div>
          <div class="text-overline mb-1">Four Letter Word Game</div>
          <div class="text-h6 mb-1">About</div>
          <div class="text-caption">
            In this game you will be competing with a computer to list the last word. You will start with a four letter word, 
            and must come up with a new word that has not been listed yet that is one letter away from the previous word.
            The game ends when you give up, or when the computer can't come up with a connected word.
          </div>
        </div>
      </v-card-item>

      <v-card-actions>
        <v-btn density="compact" variant="outlined" @click="goBack"> Back </v-btn>
      </v-card-actions>
    </v-card>
  </div>

  <div></div>
</template>

<script setup lang="ts">
import $router from '../router/index'

function goBack() {
  $router.go(-1)
}
</script>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
