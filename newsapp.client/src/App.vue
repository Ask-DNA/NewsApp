<script setup>
  import { ref, computed, inject } from 'vue'
  import SearchForm from './components/SearchForm.vue'
  import PageNav from './components/PageNav.vue'
  import NewsList from './components/NewsList.vue'

  const apiPath = inject('ApiPath')
  const newsPerPage = 5

  const currentPage = ref(1)
  const appState = ref('loading')
  const allNews = ref([])

  const displayedNews = computed(() => {
    let sliceStart = (currentPage.value - 1) * newsPerPage
    let sliceEnd = sliceStart + newsPerPage
    if (sliceEnd > allNews.value.length) {
      sliceEnd = allNews.value.length
    }
    return allNews.value.slice(sliceStart, sliceEnd)
  })

  const totalPages = computed(() => {
    let allNewsLen = allNews.value.length
    if (allNewsLen == 0) { return 1 }
    let tmp = (allNewsLen - allNewsLen % newsPerPage) / newsPerPage
    return allNewsLen % newsPerPage == 0 ? tmp : tmp + 1
  })

  function onPageNavigation(n) {
    currentPage.value = n
  }

  async function onSearch(searchString, orderingOption) {
    appState.value = 'loading'
    currentPage.value = 1
    await fetchData(buildUrl(searchString, orderingOption))
  }

  function buildUrl(searchString, orderingOption) {
    let uri = apiPath + '/Api/News/Get'
    let uriOptions = []
    if (searchString.length != 0) { uriOptions.push('SearchString=' + searchString) }
    if (orderingOption.length != 0) { uriOptions.push('OrderBy=' + orderingOption) }
    if (uriOptions.length > 0) {
      uriOptions.forEach(function (item, i, uriOptions) {
        uri += (i == 0) ? '?' : '&'
        uri += item
      })
    }
    return encodeURI(uri)
  }

  async function fetchData(url) {
    try {
      let response = await fetch(url)
      if (response.ok) {
        let result = await response.json()
        allNews.value = result.news
        appState.value = 'ready'
      } else {
        throw new Error(response.status);
      }
    } catch (error) {
      appState.value = 'error'
    }
  }

  fetchData(apiPath + '/Api/News/Get')
</script>

<template>
  <h2>Test task for DM365</h2>
  <hr>
  <SearchForm :appState="appState" @submit="(x,y) => onSearch(x,y)" />
  <hr>
  <PageNav :currentPage="currentPage"
           :totalPages="totalPages"
           :appState="appState"
           @pageChanged="(n) => onPageNavigation(n)"
           style="display:flex; justify-content: center" />
  <NewsList :news="displayedNews" :appState="appState" />
  <PageNav :currentPage="currentPage"
           :totalPages="totalPages"
           :appState="appState"
           @pageChanged="(n) => onPageNavigation(n)"
           style="display:flex; justify-content: center" />
</template>
