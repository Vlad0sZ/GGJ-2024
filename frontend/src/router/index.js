import { createRouter, createWebHistory } from 'vue-router'
import StartView from "@/views/StartView.vue";
import GameView from "@/views/GameView.vue";
import store from "@/store";
import EndGameView from "@/views/EndGameView.vue";

const routes = [
  {
    path: '/',
    name: 'start',
    component: StartView
  },
  {
    path: '/game',
    name: 'game',
    component: GameView
  },
  {
    path: '/end',
    name: 'end',
    component: EndGameView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
