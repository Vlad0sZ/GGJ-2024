<script>
  import { useSignalR } from '@dreamonkey/vue-signalr';
  import { v4 as uuidv4 } from 'uuid';
  import router from "@/router";
  import store from "@/store";
  import {ref} from "vue";

  export default {
    computed: {
      store() {
        return store
      }
    },
    setup() {
      const signalr = useSignalR();

      const bg_1 = ref(require('@/assets/UI/bg/bg_1.png'))
      const bg_2 = ref(require('@/assets/UI/bg/bg_2.png'))
      const bg_3 = ref(require('@/assets/UI/bg/bg_3.png'))
      const front_1 = ref(require('@/assets/UI/bg/front_1.png'))
      const front_2 = ref(require('@/assets/UI/bg/front_2.png'))
      const front_3 = ref(require('@/assets/UI/bg/front_3.png'))
      const front_4 = ref(require('@/assets/UI/bg/front_4.png'))
      const front_5 = ref(require('@/assets/UI/bg/front_5.png'))
      const front_6 = ref(require('@/assets/UI/bg/front_6.png'))

      let guid = localStorage.getItem("guid");
      if (guid == null) {
        guid = uuidv4();
        localStorage.setItem('guid', guid)
      }

      store.commit("setGuid", guid)

      console.log(guid)
      signalr.on("gameState", data => {
        store.commit('setState', data.state)

        signalr.invoke("getStats", guid);
        signalr.invoke("getActions")
      })

      signalr.on("stats", data => {
        // console.log(data);
        store.commit("setCommandId", data.team)
        store.commit("setClickCount", data.clickCount)
      });

      signalr.on("actions", data => {
        store.commit("setActions", data)

        // console.log(store.state.actions)
      });

      if(!signalr.connected.value) {
        signalr.invoke('login', guid);
        console.log("login")
      }

      return {bg_1, bg_2, bg_3, front_1, front_2, front_3, front_4, front_5, front_6}
    },
    data(){
      return{
        actionNames: [
          {
            id: 0,
            name: 'Action 1'
          },
          {
            id: 1,
            name: 'Action 2'
          },
          {
            id: 2,
            name: 'Action 3'
          },
          {
            id: 3,
            name: 'Action 4'
          }
        ]
      }
    }
  }
</script>

<template>
  <div class=" min-h-[100dvh] w-screen">

    <router-view></router-view>
  </div>
</template>

<style>
  * {
  color: aliceblue;
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  }
</style>
