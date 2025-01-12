<script setup>

import { onMounted } from 'vue';
import { useHandleUserDataStore } from '../stores/user';

const store = useHandleUserDataStore();

onMounted(async () => {
  try {
    console.log(await store.getUsers());
  }
  catch (e) {
    console.log(e)
  }
})

</script>

<template>
  <h1>GetUsersPage</h1>
</template>

<style scoped></style>
