<script setup>
  import { computed } from 'vue'

  const props = defineProps(['title', 'date', 'excerpt'])

  const excerptDecoded = computed(() => {
    return decodeHtmlEntity(props.excerpt)
  })

  function decodeHtmlEntity(str) {
    var e = document.createElement('div');
    e.innerHTML = str;
    return e.childNodes.length === 0 ? "" : e.childNodes[0].nodeValue;
  }
</script>

<template>
  <div class="news-item">
    <div class="news-item-header">
      <p class="news-item-title">
        {{title}}
      </p>
      <p class="news-item-date">
        {{date}}
      </p>
    </div>
    <hr>
    <p class="news-item-excerpt">
      {{excerptDecoded}}
    </p>
  </div>
</template>

<style>
  .news-item {
    border: 1px solid black;
    margin: 10px 5px 10px 5px;
  }

  .news-item-header * {
    margin-left: 10px;
  }

  .news-item-title {
    margin-top: 10px;
    font-weight: 700;
  }

  .news-item-date {
    margin-bottom: 0px;
  }

  .news-item-excerpt {
    margin-left: 10px;
  }
</style>
