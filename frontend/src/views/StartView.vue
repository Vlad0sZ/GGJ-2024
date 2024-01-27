<script>
  import router from "@/router";

  export default {
    name: 'StartView',
    methods: {
      goToGame() {
        router.push('game')
      }
    }
  }
</script>

<template>
  <div class="flex flex-col justify-center items-center h-[100svh]">
    <div class="flex flex-col justify-center items-center bg-slate-800 gap-3 rounded-2xl p-32">
      <p class="font-bold">Ожидание...</p>
      <button class="bg-fuchsia-700 hover:bg-fuchsia-800 focus:bg-fuchsia-900 py-3 px-10 rounded-2xl" @click="goToGame">Старт</button>
    </div>
  </div>
</template>