<script setup>
  defineProps({
    currentPage: Number,
    totalPages: Number,
    appState: String
  })
  const emit = defineEmits(['pageChanged'])
</script>

<template>
  <nav class="paging-nav">
    <div v-if="currentPage != 1 && appState == 'ready'">
      <button @click="emit('pageChanged', 1)"> First </button>
      <button @click="emit('pageChanged', currentPage - 1)"> Prev </button>
    </div>
    <div v-else>
      <button disabled> First </button>
      <button disabled> Prev </button>
    </div>
    <p>{{currentPage}} / {{totalPages}}</p>
    <div v-if="currentPage != totalPages && appState == 'ready'">
      <button @click="emit('pageChanged', currentPage + 1)"> Next </button>
      <button @click="emit('pageChanged', totalPages)"> Last </button>
    </div>
    <div v-else>
      <button disabled> Next </button>
      <button disabled> Last </button>
    </div>
  </nav>
</template>

<style>
  .paging-nav * {
    display: inline;
    margin: 0px 5px 0px 5px;
  }

  .paging-nav button {
    width: 45px
  }
</style>
