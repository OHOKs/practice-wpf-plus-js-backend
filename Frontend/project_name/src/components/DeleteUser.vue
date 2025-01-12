<script setup>
import { useHandleUserDataStore } from '../stores/user';

const id = '';

const store = useHandleUserDataStore();
</script>

<template>
  <h1>DeleteUserPage</h1>

  <input type="number" name="" v-model="id" id="">

  <button @click="store.deleteUserById(id)">Create User</button>
</template>

<style scoped></style>
