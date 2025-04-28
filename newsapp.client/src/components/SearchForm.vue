<script setup>
import { ref } from 'vue'

defineProps({
  appState: String
})

const searchString = ref('')
const orderingOption = ref('')
const emit = defineEmits(['submit'])
</script>

<template>
  <div>
    <div style="display: flex">
      <input type="text"
             v-model="searchString"
             id="search-field"
             placeholder="Search by Title">
      <button v-if="appState != 'loading'"
              @click="emit('submit', searchString, orderingOption)"
              class="find-button">
        Find
      </button>
      <button v-else class="find-button" disabled>Find</button>
    </div>
    <label for="pet-select">Ordering options: </label>
    <select name="pets" v-model="orderingOption" id="pet-select">
      <option value="keep original">Keep original order</option>
      <option value="date asc">By date ascending</option>
      <option value="date desc">By date descending</option>
    </select>
  </div>
</template>

<style>
  #search-field {
    flex-grow: 1
  }

  #pet-select {
    margin-top: 10px
  }

  .find-button {
    margin-left: 5px;
    width: 10%
  }
</style>
