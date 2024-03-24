<template>
  <div class="header">
    <div class="left-header">Cửa hàng cho thuê quần áo</div>
    <div class="tab-list">
      <ButtonMenu :label="'Trang chủ'" :routerPath="'/homepage'"></ButtonMenu>

      <ButtonMenu :label="'Danh mục'" :routerPath="'/dictionary'"></ButtonMenu>
    </div>

    <div class="right-header">
      <button
        class="btn-img icon24"
        :style="'background-image : url(' + Images.user + ')'"
        style="margin-left: 20px; position: relative"
        title="Tài khoản"
        @click="openUserInfo()"
      ></button>
      <button
        class="btn-img icon24"
        :style="'background-image : url(' + Images.shoppingCart + ')'"
        style="margin-left: 20px; position: relative"
        title="Tài khoản"
        @click="openUserInfo()"
      ></button>
    </div>
  </div>
</template>
<script>
import Cookies from "js-cookie";
import ButtonMenu from "../base/ButtonMenu.vue";
import Images from "@/assets/icon/images";
export default {
  data() {
    return {
      userName: Cookies.get("userName"),
      isOpen: false,
      count: 2022, // mặc định năm
      Images,
    };
  },
  components: { ButtonMenu },
  methods: {
    openUserInfo() {
      if (this.$cookies.get("token")) {
        this.$router.replace(this.$router.path);
        this.$router.push("/userinfo");
      } else {
        this.$router.replace(this.$router.path);
        this.$router.push("/login");
      }
    },
  },
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