<template>
  <div class="popup-view" v-if="isOpenPopup">
    <div class="popup">
      <div class="infomation">
        <div
          class="icon icon-popup"
          style="position: absolute; left: 30px"
        ></div>
        <div style="text-align: top; margin-left: 60px">
          <div style="font-size: 14px" v-html="rawHtml"></div>
        </div>
      </div>
      <div class="popup-footer">
        <button
          class="popup-btn btn1"
          @click="
            isOpenPopup = false;
            message = null;
          "
          v-show="btn1 != null"
        >
          {{ btn1 }}
        </button>
        <button class="popup-btn btn2" @click="closeForm" v-show="btn2 != null">
          {{ btn2 }}
        </button>
        <button
          class="popup-btn btn3"
          @click="acceptAction"
          v-show="btn3 != null"
        >
          {{ btn3 }}
        </button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      isOpenPopup: false, // hiển thị
      message: null, // nội dung thông báo
      btn1: null, // button đầu
      btn2: null, // button thứ 2
      btn3: null, // button thứ 3
      rawHtml: "<div></div>",
      eventEmit: null,
    };
  },
  methods: {
    returnHtml() {
      return;
    },
    /**
     * Xác nhận đống form từ popup trường hợp sửa dữ liệu
     * NTD 11/8/2022
     */
    closeForm() {
      this.emitter.emit("closeFormFinish");
      this.isOpenPopup = false;
    },
    /**
     * Xác nhận đóng form từ popup trường hợp thêm mới dữ liệu
     * NTD 11/8/2022
     */
    acceptAction() {
      // if(this.isAdd == true ){

      //     this.emitter.emit('closeFormFinish')
      // } else if(this.isDelete == true) {
      //     this.numberDelete = 0
      //     this.emitter.emit('deleteItem')

      // } else if(this.isFix == true) {
      //     this.emitter.emit('saveForm')

      // }
      if (this.eventEmit != null) {
        this.emitter.emit(this.eventEmit);
      }
      this.isOpenPopup = false;
      this.message = null;
    },
  },
  mounted() {
    /**
     * Mở popup thông báo, thiết lập tiêu đề và các btn hiển thị
     * NTD 11/8/2022
     */
    this.emitter.on("openPopupNotice", (message) => {
      this.rawHtml = message;
      this.btn1 = null;
      this.btn2 = null;
      this.btn3 = "Đóng";
      this.eventEmit = null;
      this.isOpenPopup = true;
    });
  },
};
</script>
<style>
@import url(../../css/layout/popup.css);
</style>