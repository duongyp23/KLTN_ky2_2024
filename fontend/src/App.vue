<template>
    <MsMenu></MsMenu>
    <div class="view">
      <MsHeader></MsHeader>
      <div class="center-view">
        <router-view />
      </div>
    </div>
    <MsToastMessage></MsToastMessage>
    <MsPopup></MsPopup>
</template>

<script>
import MsHeader from "./components/layout/MsHeader.vue";
import MsMenu from "./components/layout/MsMenu.vue";
import MsToastMessage from "./components/base/MsToastMessage.vue";
import MsPopup from "@/components/base/MsPopup.vue"


export default {
  name: 'App',
  components: {
    MsHeader,
    MsMenu,
    MsToastMessage,
    MsPopup
},
  methods(){
   
  },
  created() {
    
  }
}
</script>

<style>
@import url(./css/layout/page.css);
</style>
