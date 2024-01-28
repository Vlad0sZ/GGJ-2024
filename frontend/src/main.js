import { createApp } from 'vue'
import App from './App.vue'
import router from "@/router";
import store from "@/store";
import './index.css'
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { initializeApp } from "firebase/app";

const connection = new HubConnectionBuilder()
    .withUrl('https://gorbulka.ru/hub')
    .build();

// Import the functions you need from the SDKs you need
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyBeTl3RVzY5rO1UmzjIv3kzMLwIJUh-HRY",
    authDomain: "make-me-laugh-front.firebaseapp.com",
    projectId: "make-me-laugh-front",
    storageBucket: "make-me-laugh-front.appspot.com",
    messagingSenderId: "903708519860",
    appId: "1:903708519860:web:5f08a499545f0264b916e1"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

createApp(App)
    .use(VueSignalR, { connection })
    .use(router)
    .use(store)
    .mount('#app')
