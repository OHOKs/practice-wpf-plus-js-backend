import {createRouter, createWebHistory} from 'vue-router'

import HelloWorld from '../components/HelloWorld.vue'
import CreateUser from '../components/CreateUser.vue'
import GetUsers from '../components/GetUsers.vue'
import DeleteUser from '../components/DeleteUser.vue'

const routes = [
    { path: '/', component: () => import("../components/HelloWorld.vue") },
    { path: '/create', component: () => import("../components/CreateUser.vue") },
    { path: '/delete', component: () => import("../components/DeleteUser.vue") },
    { path: '/get', component: () => import("../components/GetUsers.vue") },
]

const router = createRouter({
    history: createWebHistory('/'),
    routes
})

export default router;