<template>
  <div class="toast-message" v-if="isOpen">
    {{ message }}
  </div>
</template>
<script>
export default {
  data() {
    return {
      isOpen: false, // hiển thị
      message: { default: null, type: String }, // đoạn thông báo
      error: false, // hiện lỗi
    };
  },
  mounted() {
    /**
     * Nhận lệnh tạo toast messsage
     * NTD 5/9/2022
     */
    this.emitter.on("openToastMessage", async (message) => {
      this.error = false;
      this.isOpen = true;
      this.message = message;
      await setTimeout(() => {
        this.isOpen = false;
      }, 3000);
    });
    /***
     * Nhận lệnh tạo toast mesg thông báo lỗi
     * NTD 5/9/2022
     */
    this.emitter.on("openToastMessageError", async (message) => {
      this.error = true;
      this.isOpen = true;
      this.message = message;
      await setTimeout(() => {
        this.isOpen = false;
      }, 3000);
    });
  },
};
</script>
<style>
@import url(../../css/base/toastmessage.css);
</style>