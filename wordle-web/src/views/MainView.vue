<template>
  <div style="display: flex; justify-content: center; align-items: center; height: 100vh">
    <v-card class="mx-auto" max-width="344" variant="outlined">
      <v-card-item>
        <div>
          <div class="text-h6 mb-1">Main Page</div>
          <div class="text-caption">
            <router-link to="/game">
              <v-btn color="primary" block class="mt-4">Single Player</v-btn>
            </router-link>
            <router-link to="/about">
              <v-btn color="primary" block class="mt-4">How to Play</v-btn>
            </router-link>
          </div>
        </div>
      </v-card-item>
    </v-card>
  </div>

  <div></div>

</template>

<script setup lang="ts"></script>

<style>
@media (min-width: 1024px) {
  .main {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>