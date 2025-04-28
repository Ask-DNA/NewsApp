<script setup>
  import NewsItem from './NewsItem.vue'
  const props = defineProps(['news', 'appState', 'errorMessage'])
</script>

<template>
  <div id="news-list">
    <div v-if="appState == 'loading'">
      <p class="message"> Loading... </p>
    </div>
    <div v-else-if="appState == 'error'">
      <p class="message">
        If you see this message, make sure the server is running and reload the page
      </p>
    </div>
    <div v-else-if="news.length !== 0">
      <NewsItem v-for="item in news"
                :title="item.title"
                :date="item.date"
                :excerpt="item.excerpt" />
    </div>
    <div v-else>
      <p class="message"> Nothing found </p>
    </div>
  </div>
</template>

<style>
  #news-list {
    border: 1px solid black;
    margin-top: 5px;
    margin-bottom: 5px
  }

  .message {
    margin-left: 10px
  }
</style>
