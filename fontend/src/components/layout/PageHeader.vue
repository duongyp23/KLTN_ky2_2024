<template>
  <div class="header">
    <div class="left-header">Cửa hàng cho thuê quần áo</div>
    <div class="tab-list" v-if="!isManager">
      <ButtonMenu :label="'Trang chủ'" :routerPath="'/homepage'"></ButtonMenu>

      <ButtonMenu :label="'Danh mục'" :routerPath="'/products'"></ButtonMenu>
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
        v-show="!isManager"
        class="btn-img icon24"
        :style="'background-image : url(' + Images.shoppingCart + ')'"
        style="margin-left: 20px; position: relative"
        title="Tài khoản"
        @click="openOrderDetail()"
      ></button>
    </div>
  </div>
</template>
<script>
import ButtonMenu from "../base/ButtonMenu.vue";
import Images from "@/assets/icon/images";
import { apiGetWaitOrder } from "@/api/userApi";
export default {
  data() {
    return {
      isManager: this.$cookies.get("role") == 1 ? true : false,
      Images,
    };
  },
  components: { ButtonMenu },
  methods: {
    openUserInfo() {
      if (!this.isManager) {
        if (this.$cookies.get("token")) {
          this.$router.replace(this.$router.path);
          this.$router.push("/userinfo");
        } else {
          this.logout();
        }
      } else {
        this.logout();
      }
    },
    logout() {
      this.$cookies.remove("token");
      this.$cookies.remove("role");
      this.emitter.emit("reloadRole");
      this.$router.replace(this.$router.path);
      this.$router.push("/login");
    },
    async openOrderDetail() {
      if (this.$cookies.get("token")) {
        await apiGetWaitOrder(this.$cookies.get("userId"))
          .then((response) => {
            this.$router.replace(this.$router.path);
            this.$router.push(`/order/${response.data}`, {
              params: { id: response.data },
            });
          })
          .catch((ex) => {
            this.emitter.emit("openToastMessageError", ex.response.data);
          });
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
        if (!this.isManager) {
          this.$router.push("homepage");
        } else {
          this.$router.push("dashboard");
        }
      }
    },
  },
  mounted() {
    this.emitter.on("reloadRole", () => {
      this.isManager = this.$cookies.get("role") == 1 ? true : false;
    });
  },
};
</script>
<style>
@import url(../../css/layout/page.scss);
@import url(../../css/base/select.css);
@import url(../../css/base/button.scss);
@import url(../../css/base/input.css);
</style>