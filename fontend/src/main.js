import { createApp } from 'vue'
import App from './App.vue'
import mitt from 'mitt';
import vClickOutside from "click-outside-vue3"
import router from "./router";
import VueCookies from 'vue-cookies';
import vToolTip from 'v-tooltip';

const emitter = mitt();

const app = createApp(App)
app.config.globalProperties.emitter = emitter;
app.use(vClickOutside);
app.use(router);
app.use(VueCookies);
app.use(vToolTip);

app.mount('#app');



