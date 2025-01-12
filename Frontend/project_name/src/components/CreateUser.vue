<script setup>
import { useHandleUserDataStore } from '../stores/user';

const store = useHandleUserDataStore();
</script>

<template>
  <h1>CreateUserPage</h1>

  <input type="text" name="" v-model="store.userData.name" id="">
  <input type="text" name="" v-model="store.userData.email" id="">

  <button @click="store.createUser(store.userData)">Create User</button>
</template>

<style scoped></style>
