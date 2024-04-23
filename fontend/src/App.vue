<template>
  <div class="view">
    <PageHeader></PageHeader>
    <div class="center-view" v-if="!isManager">
      <router-view />
    </div>
    <div v-else class="view-admin">
      <LeftMenuVue></LeftMenuVue>
      <div class="right-view">
        <router-view />
      </div>
    </div>
    <MsToastMessage></MsToastMessage>
    <MsPopup></MsPopup>
  </div>
</template>

<script>
import PageHeader from "./components/layout/PageHeader.vue";
import MsToastMessage from "./components/base/MsToastMessage.vue";
import MsPopup from "@/components/base/MsPopup.vue";
import LeftMenuVue from "./components/layout/view/Admin/LeftMenu/LeftMenu.vue";

export default {
  name: "App",
  data() {
    return {
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  components: {
    PageHeader,
    MsToastMessage,
    MsPopup,
    LeftMenuVue,
  },
  methods() {},
  mounted() {
    this.emitter.on("reloadRole", () => {
      this.isManager = this.$cookies.get("role") == 1 ? true : false;
    });
  },
  created() {},
};
</script>

<style>
@import url(./css/layout/page.scss);
</style>
