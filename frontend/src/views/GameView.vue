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
    setup() {
      const bg_1 = ref(require('../assets/UI/bg/bg_1.png'))
      const bg_2 = ref(require('../assets/UI/bg/bg_2.png'))
      const bg_3 = ref(require('../assets/UI/bg/bg_3.png'))
      const front_1 = ref(require('../assets/UI/bg/front_1.png'))
      const front_2 = ref(require('../assets/UI/bg/front_2.png'))
      const front_3 = ref(require('../assets/UI/bg/front_3.png'))
      const front_4 = ref(require('../assets/UI/bg/front_4.png'))
      const front_5 = ref(require('../assets/UI/bg/front_5.png'))
      const front_6 = ref(require('../assets/UI/bg/front_6.png'))
      const anek = ref(require('../assets/UI/anek.png'))
      const cake = ref(require('../assets/UI/cake.png'))
      const random = ref(require('../assets/UI/random.png'))
      const disco = ref(require('../assets/UI/disco.png'))
      const team0 = ref(require('../assets/UI/team0.png'))
      const team1 = ref(require('../assets/UI/team1.png'))

      return {bg_1, bg_2, bg_3, front_1, front_2, front_3, front_4, front_5, front_6,
        anek, cake, random, disco, team1, team0}
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
  <div class="flex flex-col items-center h-[100dvh] gap-5">
    <div v-if="store.state.background === 0" :style="{backgroundImage:`url(${front_1})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 0" :style="{backgroundImage:`url(${bg_1})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>

    <div v-if="store.state.background == 1" :style="{backgroundImage:`url(${front_2})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 1" :style="{backgroundImage:`url(${bg_3})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>

    <div v-if="store.state.background == 2" :style="{backgroundImage:`url(${front_3})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 2" :style="{backgroundImage:`url(${bg_2})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>

    <div v-if="store.state.background == 3" :style="{backgroundImage:`url(${front_4})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 3" :style="{backgroundImage:`url(${bg_2})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>

    <div v-if="store.state.background == 4" :style="{backgroundImage:`url(${front_5})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 4" :style="{backgroundImage:`url(${bg_3})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>

    <div v-if="store.state.background == 5" :style="{backgroundImage:`url(${front_6})`}" class="animate-pulse absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-20">

    </div>
    <div v-if="store.state.background == 5" :style="{backgroundImage:`url(${bg_1})`}" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2
    bg-cover bg-center w-screen h-screen -z-30">

    </div>
    <h1 class="font-bold text-black text-3xl mt-5 py-16">Кликай!</h1>
    <div class="text-center">
      <p class=" text-black font-medium">Количество кликов:</p>
      <p class=" text-black font-bold">{{store.state.clickCount}}</p>
    </div>
    <button class="bg-red-700 hover:bg-red-800 w-72 h-72 rounded-full mt-18
                   transition ease-in-out duration-200 hover:scale-90 active:scale-110
                   flex justify-center items-center border-8 border-black"
            @click="store.commit('setClickCount', store.state.clickCount + 1)">
      <img v-if="store.state.commandId == 0" v-bind:src="team0" alt="team 0">
      <img v-if="store.state.commandId == 1" v-bind:src="team1" alt="team 0">
    </button>
    <div class="flex flex-row gap-5">
      <div v-for="action in store.state.actions">
        <action-button :disabled="store.state.clickCount < action.coast" @click="() => {
          store.commit('setClickCount', store.state.clickCount - action.coast)
          this.signalR.invoke('actionPull', store.state.guid, action.id)
        }">
          <img class="w-[56px]" v-if="action.id === 1" v-bind:src="cake" alt="cake"/>
          <img class="w-[56px]" v-if="action.id === 2" v-bind:src="random" alt="cake"/>
          <img class="w-[56px]" v-if="action.id === 3" v-bind:src="disco" alt="cake"/>
          <img class="w-[56px]" v-if="action.id === 4" v-bind:src="anek" alt="cake"/>
        </action-button>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>