<script>
  import ActionButton from "@/components/ActionButton.vue";
  import store from "@/store";
  import {ref} from "vue";
  import {useSignalR} from "@dreamonkey/vue-signalr";

  export default {
    name: 'GameView',
    data(){
      return{
        timer: null,
        signalR: null
      }
    },
    computed: {
      store() {
        return store
      }
    },
    components: {ActionButton},
    mounted() {
      this.signalR = useSignalR()
      this.timer = window.setInterval(() => {
        this.signalR.invoke("clickPull", localStorage.getItem('guid'), store.state.clickCount)
        // console.log("fetched clicks: " + store.state.clickCount)
      },1000);
    },
    unmounted() {
      window.clearInterval(this.timer)
    }
  }
</script>

<template>
  <div class="flex flex-col justify-center items-center h-[100dvh] gap-5">
    <h1 class="font-bold text-3xl">Кликай!</h1>
    <button class="bg-red-700 hover:bg-red-800 w-72 h-72 rounded-2xl
                   transition ease-in-out duration-200 hover:scale-90 active:scale-110"
            @click="store.commit('setClickCount', store.state.clickCount + 1)">
      This is fucking click!
    </button>
    <div class="text-center">
      <p>Количество кликов:</p>
      <p class="font-bold">{{store.state.clickCount}}</p>
      <p>Команда:</p>
      <p class="font-bold">{{store.state.commandId}}</p>
    </div>
    <div class="flex flex-row gap-5">
      <div v-for="action in store.state.actions">
        <action-button :disabled="store.state.clickCount < action.coast" @click="() => {
          store.commit('setClickCount', store.state.clickCount - action.coast)
          this.signalR.invoke('actionPull', store.state.guid, action.id)
        }">{{action.id}}</action-button>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>