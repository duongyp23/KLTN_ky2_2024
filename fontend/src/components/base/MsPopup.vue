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
import Resource from "@/resource/MsResource";

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
     * Mở popup khi xóa, thiết lập tiêu đề và các btn hiển thị
     * NTD 11/8/2022
     */
    this.emitter.on("openPopupDeleteListProperty", (list) => {
      if (list.length == 0) {
        this.rawHtml = "Không có tài sản nào được chọn";
        this.btn1 = null;
        this.btn2 = null;
        this.btn3 = "Đóng";
        this.eventEmit = null;
      } else if (list.length == 1) {
        if (list[0].isActive == Resource.IsActive.Active) {
          this.rawHtml =
            "Tài sản có mã <b>" +
            list[0].propertyCode +
            "</b> đã phát sinh chứng từ ghi tăng";
          this.btn1 = null;
          this.btn2 = null;
          this.btn3 = "Đóng";
          this.eventEmit = null;
        } else {
          this.rawHtml =
            "Bạn muốn xóa tài sản <b>" +
            list[0].propertyCode +
            " " +
            list[0].propertyName +
            "</b> ?";
          this.btn1 = "Không";
          this.btn2 = null;
          this.btn3 = "Xóa";
          this.eventEmit = "deleteSelectList";
        }
      } else if (list.length > 1) {
        var isError = false;
        list.forEach((element) => {
          if (
            element.isActive == Resource.IsActive.Active &&
            isError == false
          ) {
            this.rawHtml =
              "Tài sản có mã <b>" +
              element.propertyCode +
              "</b> đã phát sinh chứng từ ghi tăng";
            isError = true;
            this.btn1 = null;
            this.btn2 = null;
            this.btn3 = "Đóng";
            this.eventEmit = null;
          }
        });
        if (isError == false) {
          if (list.length < 10) {
            this.rawHtml =
              "<b>0" +
              list.length +
              "</b> tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?";
          } else {
            this.rawHtml =
              "<b>" +
              list.length +
              "</b> tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?";
          }
          this.btn1 = "Không";
          this.btn2 = null;
          this.btn3 = "Xóa";
          this.eventEmit = "deleteSelectList";
        }
      }

      this.isOpenPopup = true;
    });

    /**
     * Mở popup khi xóa, thiết lập tiêu đề và các btn hiển thị
     * NTD 11/8/2022
     */
    this.emitter.on("openPopupDeleteProperty", (property) => {
      if (property) {
        if (property.isActive == Resource.IsActive.Active) {
          this.rawHtml =
            "Tài sản có mã <b>" +
            property.propertyCode +
            "</b> đã phát sinh chứng từ ghi tăng";
          this.btn1 = null;
          this.btn2 = null;
          this.btn3 = "Đóng";
          this.eventEmit = null;
        } else {
          this.rawHtml =
            "Bạn muốn xóa tài sản <b>" +
            property.propertyCode +
            " " +
            property.propertyName +
            "</b> ?";
          this.btn1 = "Không";
          this.btn2 = null;
          this.btn3 = "Xóa";
          this.eventEmit = "deleteProperty";
        }
      }

      this.isOpenPopup = true;
    });

    /**
     * Mở popup khi hủy sửa, thiết lập tiêu đề và các btn hiển thị
     * NTD 11/8/2022
     */
    this.emitter.on("openPopupCancelFix", () => {
      this.rawHtml =
        "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?";
      this.btn1 = "Hủy bỏ";
      this.btn2 = "Không lưu";
      this.btn3 = "Lưu";
      this.eventEmit = "saveForm";
      this.isOpenPopup = true;
    });
    /**
     * Mở popup khi hủy thêm mới, thiết lập tiêu đề và các btn hiển thị
     * NTD 11/8/2022
     */
    this.emitter.on("openPopupCancelAdd", () => {
      this.rawHtml = "Bạn có muốn hủy bỏ khai báo tài sản này?";
      this.btn1 = "Không";
      this.btn2 = null;
      this.btn3 = "Hủy bỏ";
      this.eventEmit = "closeFormFinish";
      this.isOpenPopup = true;
    });
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
    /**
     * Popup thông báo xóa voucher
     * NTD 23/10/2022
     */
    this.emitter.on("openPopupDeleteVoucher", (voucher) => {
      this.rawHtml =
        "Bạn có muốn xóa chứng từ có mã <b>" + voucher.voucherCode + "</b>?";
      this.btn1 = "Không";
      this.btn2 = null;
      this.btn3 = "Xóa";
      this.eventEmit = "deleteVoucher";
      this.isOpenPopup = true;
    });
    /**
     * Popup thông báo xóa voucher list
     * NTD 23/10/2022
     */
    this.emitter.on("openPopupDeleteVoucherList", (list) => {
      if (list.length < 10) {
        this.rawHtml =
          "<b>0" +
          list.length +
          "</b> chứng từ đã được chọn. Bạn có muốn xóa các chứng từ này khỏi danh sách?";
      } else {
        this.rawHtml =
          "<b>" +
          list.length +
          "</b> chứng từ đã được chọn. Bạn có muốn xóa các chứng từ này khỏi danh sách?";
      }
      this.btn1 = "Không";
      this.btn2 = null;
      this.btn3 = "Xóa";
      this.eventEmit = "deleteVoucherList";
      this.isOpenPopup = true;
    });
  },
};
</script>
<style>
@import url(../../css/layout/popup.css);
</style>