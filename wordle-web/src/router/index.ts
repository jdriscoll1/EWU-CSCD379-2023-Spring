import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import WordleView from '../views/WordleView.vue'
import WordDay from '@/views/WordDay.vue'
import IndexView from '../views/IndexView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/wordle',
      name: 'wordle',
      component: WordleView
    },
    {
      path: '/test',
      name: 'test',
      component: () => import('../views/GameView.vue')
    },
    {
      path: '/main',
      name: 'main',
      component: () => import('../views/MainView.vue')
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
      component: WordDay
    },
    {
      path: '/pastwordoftheday',
      name: 'pastwordoftheday',
      component: WordDay
    },
    {
      path: '/index',
      name: 'index',
      component: IndexView
    },
    {
      path: '/signIn',
      name: 'signIn',
      component: () => import('../views/SignView.vue')
    }
  ]
})

export default router
