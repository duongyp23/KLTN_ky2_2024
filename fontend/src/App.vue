<template>
  <div class="view">
    <PageHeader></PageHeader>
    <div class="center-view">
      <router-view />
    </div>
  </div>
  <MsToastMessage></MsToastMessage>
  <MsPopup></MsPopup>
</template>

<script>
import PageHeader from "./components/layout/PageHeader.vue";
import MsToastMessage from "./components/base/MsToastMessage.vue";
import MsPopup from "@/components/base/MsPopup.vue";

export default {
  name: "App",
  components: {
    PageHeader,
    MsToastMessage,
    MsPopup,
  },
  methods() {},
  created() {},
};
</script>

<style>
@import url(./css/layout/page.scss);
</style>
