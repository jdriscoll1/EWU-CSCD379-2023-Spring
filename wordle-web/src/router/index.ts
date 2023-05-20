import { createRouter, createWebHistory } from 'vue-router'
import WordleView from '../views/WordleView.vue'
import AboutView from '../views/AboutView.vue'
import LeaderboardView from '../views/LeaderboardView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'wordle',
      component: WordleView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/leaderBoard',
      name: 'leaderBoard',
      component: () => import('../views/LeaderBoard.vue')
    },
    {
      path: '/days',
      name: 'LastTenDays',
      component: () => import('../views/DaysView.vue')
    },
    {
      path: '/wordoftheday',
      name: 'wordoftheday',
      component: WordleView
    }
  ]
})

export default router
