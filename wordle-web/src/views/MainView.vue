<template>
  <v-container class="d-flex align-center justify-center" style="height: 100vh">
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="6" lg="4">
        <v-card class="text-center">
          <v-card-title>Play Game</v-card-title>
          <v-card-text>
            <router-link to="/test">
              <v-btn color="primary" block class="mt-4">Single Player</v-btn>
            </router-link>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts"></script>