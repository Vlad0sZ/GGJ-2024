<script>
  import { useSignalR } from '@dreamonkey/vue-signalr';
  import { v4 as uuidv4 } from 'uuid';
  import router from "@/router";
  import store from "@/store";

  export default {
    computed: {
      store() {
        return store
      }
    },
    setup() {
      const signalr = useSignalR();

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
  <div class="bg-gray-900 min-h-[100dvh]">
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
