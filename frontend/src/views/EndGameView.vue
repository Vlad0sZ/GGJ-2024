<script>
import ActionButton from "@/components/ActionButton.vue";
import {ref} from "vue";
import store from "@/store";


export default {
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

    return {bg_1, bg_2, bg_3, front_1, front_2, front_3, front_4, front_5, front_6}
  },
  name: 'GameView',
  components: {ActionButton},
}
</script>

<template>
  <div class="flex flex-col justify-center items-center h-[100dvh] gap-5">
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

    <h1 class="text-black text-3xl font-medium uppercase">Игра окончена</h1>
  </div>
</template>

<style scoped>

</style>