import { createRouter, createWebHistory } from 'vue-router'
import StartView from "@/views/StartView.vue";
import GameView from "@/views/GameView.vue";
import store from "@/store";

const routes = [
  {
    path: '/',
    name: 'start',
    component: store.state.isGame ? GameView : StartView
  },
  {
    path: '/game',
    name: 'game',
    component: GameView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
