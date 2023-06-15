<template>
  <SinglePlayer
    @WinGame=";(dialog = true), (gameResult = 'win')"
    @LoseGame=";(dialog = true), (gameResult = 'Lose')"
  />
  <div class="text-right">
    <v-dialog
      :model-value="dialog"
      @update:model-value="dialog = $event"
      transition="dialog-bottom-transition"
      persistent
    >
      <template v-slot:default>
        <v-row align="center" justify="center">
          <v-col cols="1">
            <v-card>
              <v-toolbar class="text-center" style="font-size: 20px"
                >-----{{ gameResult }}--------</v-toolbar
              >
              <v-col>
                <router-link to="/">
                  <v-btn variant="text" @click="dialog = false">Home</v-btn>
                </router-link>
              </v-col>
            </v-card>
          </v-col>
        </v-row>
      </template>
    </v-dialog>
  </div>
</template>

<script setup lang="ts">
import SinglePlayer from '../components/SinglePlayer.vue'

import { ref } from 'vue'

var dialog = ref(false)
var gameResult = ref('')
</script>
