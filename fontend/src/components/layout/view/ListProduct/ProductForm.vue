<template>
  <div class="product-form form-view" v-show="isOpen" @keyup.esc="closeForm()">
    <div class="form">
      <div class="form-header">
        <div>{{ label }}</div>
        <button
          class="btn-header"
          style="border: none; background: transparent"
          title="Đóng"
          @click="closeForm()"
        >
          <div class="btn-close">X</div>
        </button>
      </div>
      <div class="form-center">
        <div class="row-flex">
          <StyleInput
            :label="'Mã sản phẩm'"
            v-model:value="product.product_code"
          ></StyleInput>

          <StyleInput
            :label="'Tên sản phẩm'"
            v-model:value="product.product_name"
          ></StyleInput>
        </div>
        <div class="row-flex">
          <InputNumber
            :label="'Giá sản phẩm'"
            v-model:numberValue="product.product_price"
          ></InputNumber>
          <InputNumber
            v-model:numberValue="product.rental_price"
            :label="'Giá thuê sản phẩm'"
          ></InputNumber>
        </div>
        <div class="row-flex">
          <StyleInput
            class="description"
            :label="'Diễn giải'"
            v-model:value="product.description"
          ></StyleInput>
        </div>
        <div class="row-flex">
          <button class="upload-file" @click="openUploadWidget()"></button>
        </div>
      </div>
      <div class="form-footer">
        <button class="form-btn" v-on:click="closeForm">Hủy</button>
        <button
          class="form-btn"
          style="
            background-color: rgba(26, 164, 200);
            color: #fff;
            margin-left: 10px;
          "
          @click="saveForm"
        >
          Lưu
        </button>
      </div>
    </div>
  </div>
</template>
<script>
import Resource from "@/resource/MsResource";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
import { apiInsertProduct } from "@/api/productApi";

/**
 * Khởi tạo 1 Item với giá trị ban đầu là null
 * NTD 22/8/2022
 */
export default {
  data() {
    return {
      formStatus: 0,
      isOpen: false, // hiển thị
      label: "", // tiêu đề
      product: {},
    };
  },
  methods: {
    openUploadWidget() {
      const myWidget = window.cloudinary.createUploadWidget(
        {
          cloudName: "dmci423da",
          uploadPreset: "kltn-image",
        },
        (error, result) => {
          if (!error && result && result.event === "success") {
            console.log("Done! Here is the image info: ", result.info);
            this.product.product_image_url = result.info.url;
          }
        }
      );

      myWidget.open();
    },
    closeForm() {
      this.isOpen = false;
    },
    /**
     *  Thay đổi tiêu đề form
     *  NTD 8/8/2022
     */
    setLabel(newLabel) {
      this.label = newLabel;
    },

    /**
     * Lưu form, kiểm tra các trường đã được nhập dữ liệu hay chưa khi click
     */
    saveForm() {
      if (this.formStatus == 1) {
        this.addNewProduct();
      } else if (this.formStatus == 2) {
        this.updateCategory();
      }
    },

    /**
     * Gọi API thêm mới tài sản
     * NYD 5/9/2022
     */
    async addNewProduct() {
      await apiInsertProduct(this.product)
        .then(() => {
          this.emitter.emit(
            "openToastMessage",
            Resource.SuccessMessage.SuccessMessageAdd
          );
          this.emitter.emit("loadDataCategory");
          this.isOpen = false;
        })
        .catch(() => {
          this.emitter.emit(
            "openToastMessageError",
            Resource.ErrorMessage.ErrorMessageAdd
          );
        });
    },
    /**
     * gọi API Sửa tài sản
     * NTD 5/9/2022
     */
    async updateCategory() {},
  },
  components: { StyleInput, InputNumber },
  mounted() {
    /**
     * Mở form thêm tài sản từ Tool
     * NTD 8/8/2022
     */
    this.emitter.on("addNewProduct", async () => {
      this.formStatus = 1;
      this.setLabel("Thêm sản phẩm");
      this.isOpen = true;
    });
    /**
     * Đóng form sau khi xác nhận
     * NTD 8/8/2022
     */
    this.emitter.on("closeFormFinish", () => {
      this.isOpen = false;
      this.emitter.emit("focusTable");
    });
    /**
     * Mở form sửa tài sản từ Row trong table
     * NTD 8/8/2022
     */
    this.emitter.on("updateCategory", async (product) => {
      this.product = Object.assign({}, product);
      this.formStatus = 2;
      this.setLabel("Sửa nhãn dán");
      this.isOpen = true;
    });
  },
  watch: {},
};
</script>

<style>
@import url(./productForm.scss);
@import url(@/css/layout/form.scss);
</style>