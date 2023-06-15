<template>
  <v-card>
    <v-layout>
      <v-app-bar>
        <template v-slot:image> </template>
        <template v-slot:prepend>
          <router-link to="/"
            ><v-app-bar-nav-icon icon="mdi-puzzle"></v-app-bar-nav-icon
          ></router-link>
          <router-link to="/"
            ><v-app-bar-title class="primary" color="primary">Last Word Standing</v-app-bar-title></router-link
          >
        </template>
        <v-spacer></v-spacer>

        <v-btn>
          <SettingsDialogVue />
        </v-btn>

        <template v-slot:append>
          <v-menu>
            <template v-slot:activator="{ props }">
              <v-btn v-bind="props" icon="mdi-hamburger"> </v-btn>
            </template>

            <v-list>
              <router-link to="/"><v-list-item-title>Main</v-list-item-title></router-link>
              <router-link to="/game"><v-list-item-title>Single Player</v-list-item-title></router-link>
              <router-link to="/about"><v-list-item-title>About</v-list-item-title></router-link>

            </v-list>
          </v-menu>
        </template>
      </v-app-bar>
    </v-layout>
  </v-card>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'
import SettingsDialogVue from './SettingsDialog.vue'

</script>
