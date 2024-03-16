<template>
  <div class="header">
    <div class="left-header">Cửa hàng cho thuê quần áo</div>
    <div class="tab-list">
      <ButtonMenu :label="'Trang chủ'" :routerPath="'/homepage'"></ButtonMenu>

      <ButtonMenu
        :label="'Danh mục sản phẩm'"
        :routerPath="'/dictionary'"
      ></ButtonMenu>

      <ButtonMenu
        :label="'Tra cứu sản phẩm'"
        :routerPath="'/search'"
      ></ButtonMenu>

      <ButtonMenu :label="'Báo cáo'" :routerPath="'/report'"></ButtonMenu>
    </div>

    <div class="right-header">
      <button
        class="btn-header icon icon-avartar"
        style="margin-left: 20px; position: relative"
        title="Tài khoản"
      ></button>
    </div>
  </div>
</template>
<script>
import Cookies from "js-cookie";
import ButtonMenu from "../base/ButtonMenu.vue";
export default {
  data() {
    return {
      userName: Cookies.get("userName"),
      isOpen: false,
      count: 2022, // mặc định năm
    };
  },
  components: { ButtonMenu },
  methods: {},
  watch: {
    /**
     * Kiểm tra sự thay đổi router và check cookies
     * NTD 29/9/2022
     */
    async $route() {
      if (this.$route.path == "/") {
        this.$router.push("dictionary");
      }
    },
  },
};
</script>
<style>
@import url(../../css/layout/page.scss);
@import url(../../css/base/select.css);
@import url(../../css/base/button.scss);
@import url(../../css/base/input.css);
</style>