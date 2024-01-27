<script>
  import ActionButton from "@/components/ActionButton.vue";

  export default {
    name: 'GameView',
    components: {ActionButton},
    data(){
      return {
        clickCount: 0,
        actions: [
          {
            id: 0,
            price: 1,
            name: 'Action 1'
          },
          {
            id: 1,
            price: 3,
            name: 'Action 2'
          }
        ]
      }
    },
    methods: {
      addClick()
      {
        this.clickCount++
      },
    }
  }
</script>

<template>
  <div class="flex flex-col justify-center items-center h-[100svh] gap-5">
    <h1 class="font-bold text-3xl">Кликай!</h1>
    <button class="bg-red-700 hover:bg-red-800 focus:bg-red-900 px-10 py-3 rounded-2xl" @click="addClick">
      This is fucking click!
    </button>
    <div class="text-center">
      <p>Количество кликов:</p>
      <p class="font-bold">{{clickCount}}</p>
    </div>
    <div class="flex flex-row gap-5">
      <div v-for="action in actions">
        <action-button :disabled="this.clickCount < action.price" @click="() => {this.clickCount -= action.price}">{{action.name}}</action-button>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>