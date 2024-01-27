import { createStore } from 'vuex'
import router from "@/router";

export default createStore({
  state: {
    gameState: 0,
    clickCount: 0,
    commandId: 0,
    actions : [],
    guid: 0
  },
  getters: {
  },
  mutations: {
    setState(state, gameState) {
      state.gameState = gameState
      state.clickCount = 0
      console.log("state " + gameState)
      router.push(gameState === 2 ? '/game' : gameState === 3 ? '/end' : '/')
    },
    setClickCount(state, count){
      state.clickCount = count
    },
    setCommandId(state, id) {
      state.commandId = id
    },
    setActions(state, actions) {
      state.actions = actions
    },
    setGuid(state, guid){
      state.guid = guid
    }
  },
  actions: {

  },
  modules: {
  }
})
