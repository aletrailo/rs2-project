<template>
  <nav>
    <router-link to="/"></router-link>
  </nav>
  <router-view :key="$route.fullPath"/>
  <notifications position="bottom right" classes="vue-notification" />
</template>

<script setup type>
import { useStore } from 'vuex';
import { onMounted } from 'vue';

const store = useStore();

onMounted(() => {
  store.dispatch('initializeAuth');
});
</script>


<style>
a {
  color: black;
  text-decoration: none;
}


.vue-notification {
  /* styling*/
  margin: 0 5px 80px;
  padding: 10px;
  font-size: 12px;
  color: #ffffff;

  /*default (blue)*/
  background: #44a4fc;
  border-left: 5px solid #187fe7;

  /* types (green, amber, red)*/

  &.success {
    background: #68cd86;
    border-left-color: #42a85f;
  }

  &.warn {
    background: #ffb648;
    border-left-color: #f48a06;
  }

  &.error {
    background: #e54d42;
    border-left-color: #b82e24;
  }
}
</style>
