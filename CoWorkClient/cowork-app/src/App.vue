<template>
  <nav>
    <router-link to="/"></router-link>
  </nav>
  <router-view :key="$route.fullPath"/>
</template>

<script setup type>
import { useStore } from 'vuex';
import { onMounted } from 'vue';

const store = useStore();

onMounted(() => {
  store.dispatch('auth/initializeAuth');
});
</script>


<style>

</style>
