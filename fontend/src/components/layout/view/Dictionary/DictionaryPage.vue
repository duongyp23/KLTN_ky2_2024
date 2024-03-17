<template>
  <div class="dictionary-page">
    <div class="dictionary-menu">
      <ButtonMenu
        :label="'Sản phẩm'"
        :routerPath="'/dictionary/product'"
      ></ButtonMenu>

      <ButtonMenu
        :label="'Nhãn dán'"
        :routerPath="'/dictionary/category'"
      ></ButtonMenu>
    </div>
    <router-view />
  </div>
</template>
<script>
import ButtonMenu from "@/components/base/ButtonMenu.vue";
export default {
  data() {
    return {};
  },
  components: { ButtonMenu },
};
</script>
<style>
@import url(./dictionaryPage.scss);
</style>