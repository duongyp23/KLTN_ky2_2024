<template>
  <div class="left-menu">
    <ButtonLeftMenuVue
      :label="'Tổng quan'"
      :routerPath="'/dashboard'"
    ></ButtonLeftMenuVue>
    <ButtonLeftMenuVue
      :label="'Người dùng'"
      :routerPath="'/manageuser'"
    ></ButtonLeftMenuVue>
    <ButtonLeftMenuVue
      :label="'Đơn hàng'"
      :routerPath="'/manageorder'"
    ></ButtonLeftMenuVue>
    <ButtonLeftMenuVue
      :label="'Kho hàng'"
      :routerPath="'/managestock'"
    ></ButtonLeftMenuVue>
    <ButtonLeftMenuVue
      :label="'Sản phẩm'"
      :routerPath="'/manageproduct'"
    ></ButtonLeftMenuVue>
    <ButtonLeftMenuVue
      :label="'Nhãn dán'"
      :routerPath="'/managecategory'"
    ></ButtonLeftMenuVue>
  </div>
</template>
<script>
import ButtonLeftMenuVue from "../../../../base/ButtonLeftMenu.vue";
export default {
  data() {
    return {};
  },
  components: { ButtonLeftMenuVue },
};
</script>
<style>
</style>