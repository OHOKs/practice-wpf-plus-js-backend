import { defineStore } from 'pinia';
import { ref } from 'vue';
import axios from 'axios';

export const useHandleUserDataStore = defineStore('HandleUserData', () => {
    const url = 'http://localhost:3000';

    const userData = ref({
        name: '',
        email: '' 
    });

    async function getUsers() {
        try {
          const response = await axios.get(`${url}/api/users`, { withCredentials: true });
          return response.data;
        } catch (error) {;
          throw new Error(`Error fetching users: ${error.response?.data?.message || error.message}`);
        }
      }
      
      async function deleteUserById(userId) {
        try {
          const response = await axios.delete(`${url}/api/users/${userId}`, { withCredentials: true });
          return response.data;
        } catch (error) {
          throw new Error(`Error deleting user: ${error.response?.data?.message || error.message}`);
        }
      }
      
      async function createUser(userData) {
        try {
          const response = await axios.post(`${url}/api/users`, userData, { withCredentials: true });
          return response.data;
        } catch (error) {
          throw new Error(`Error creating user: ${error.response?.data?.message || error.message}`);
        }
      }

      return {
        userData,
        getUsers,
        deleteUserById,
        createUser
      }
})