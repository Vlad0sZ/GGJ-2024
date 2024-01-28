<script>
  export default {
    props: {
      disabled :{
        type: Boolean,
        required: true,
      }
    }
  }
</script>

<template>
  <button :disabled="$props.disabled"
          class="bg-[#90BB0C] hover:bg-green-800 active:bg-[#86107E] disabled:bg-[#FFD105]
          h-[70px] w-[70px] flex justify-center items-center rounded-2xl border-black border-4">
    <slot></slot>
  </button>
</template>