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
  <button :disabled="$props.disabled" class="bg-green-700 hover:bg-green-800 focus:bg-green-900 disabled:bg-red-950 p-3 rounded">
    <slot></slot>
  </button>
</template>