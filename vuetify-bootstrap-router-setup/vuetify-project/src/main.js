import { registerPlugins } from "@/plugins";

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

import App from "./App.vue";

import { createApp } from "vue";

const app = createApp(App);

registerPlugins(app);

app.mount("#app");
