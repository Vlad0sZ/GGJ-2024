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
  <div class="flex flex-col justify-center items-center h-[100dvh]">
    <div class="flex flex-col justify-center items-center bg-slate-800 gap-3 rounded-2xl p-32">
      <p class="font-bold">Ожидание...</p>
    </div>
  </div>
</template>