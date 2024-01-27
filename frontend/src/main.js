import { createApp } from 'vue'
import App from './App.vue'
import router from "@/router";
import store from "@/store";
import './index.css'
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';

const connection = new HubConnectionBuilder()
    .withUrl('http://158.160.131.200:5555/hub')
    .build();


createApp(App)
    .use(VueSignalR, { connection })
    .use(router)
    .use(store)
    .mount('#app')
