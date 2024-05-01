<template>
  <div class="product-form">
    <div class="header-label">{{ label }}</div>
    <div class="middle-view mt-2">
      <div class="top flex-row">
        <div class="img-upload">
          <div
            class="big-img"
            :style="
              this.product_image_url
                ? 'background-image: url(' + this.product_image_url + ')'
                : 'border : 2px solid #afafaf'
            "
          >
            <button
              v-if="this.product_image_url"
              class="btn-delete"
              :style="'background-image: url(' + Images.del + ')'"
              @click="deleteImage(this.product_image_url)"
            ></button>
          </div>
          <div class="list-small-img">
            <button
              class="small-img"
              @click="openUploadWidget()"
              :style="'background-image: url(' + Images.addP + ')'"
            ></button>
            <div
              class="small-img"
              v-for="(item, index) in list_img_url"
              :key="index"
              :style="'background-image: url(' + item + ')'"
              @click="this.product_image_url = item"
            ></div>
          </div>
        </div>

        <div class="information">
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

          <!-- <div class="row-flex mt-1">
            <InputNumber
              :label="'Số lượng sản phẩm'"
              v-model:numberValue="product.total_quantity"
              :disabled="true"
            ></InputNumber>
            <InputNumber
              :label="'Số lượng sản phẩm trong kho'"
              v-model:numberValue="product.quantity_remain"
              :disabled="true"
            ></InputNumber>
          </div>
          <div class="row-flex mt-1">
            <InputNumber
              :label="'Số lượng sản phẩm đã bán'"
              v-model:numberValue="product.quantity_sold"
              :disabled="true"
            ></InputNumber>
            <InputNumber
              :label="'Số lượng sản phẩm đang thuê'"
              v-model:numberValue="product.quantity_rental"
              :disabled="true"
            ></InputNumber>
          </div> -->
          <div class="row-flex mt-1">
            <InputNumber
              :label="'Giá sản phẩm (đ)'"
              v-model:numberValue="product.product_price"
            ></InputNumber>
            <InputNumber
              v-model:numberValue="product.rental_price_day"
              :label="'Giá thuê sản phẩm theo ngày(đ)'"
            ></InputNumber>
          </div>
          <div class="row-flex mt-1">
            <InputNumber
              v-model:numberValue="product.rental_price_week"
              :label="'Giá thuê sản phẩm theo tuần(đ)'"
            ></InputNumber>
            <InputNumber
              v-model:numberValue="product.rental_price_month"
              :label="'Giá thuê sản phẩm theo tháng(đ)'"
            ></InputNumber>
          </div>
          <StyleInput
            class="description mt-1"
            :label="'Mô tả sản phẩm'"
            v-model:value="product.description"
            type="textarea"
          ></StyleInput>
        </div>
      </div>
      <category-table
        class="flex-row"
        v-model:selectCategory="selectCategory"
      ></category-table>
    </div>
    <div class="flex-row flex-end mt-2">
      <div class="flex-row flex-end">
        <button class="form-btn btn1" v-on:click="closeForm">Hủy</button>
        <button
          class="form-btn btn2"
          @click="deleteProduct"
          v-if="formStatus == 2"
        >
          Xóa
        </button>
        <button class="form-btn btn3" @click="saveForm">Lưu</button>
      </div>
    </div>
  </div>
</template>
<script>
import Resource from "@/resource/MsResource";
import StyleInput from "@/components/base/StyleInput/StyleInput.vue";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";

import {
  apiInsertProduct,
  apiUpdateProduct,
  apiDeleteProduct,
} from "@/api/productApi";
import Images from "@/assets/icon/images";
import { apiGetCategoryOfProduct } from "@/api/categoryApi";
import CategoryType from "@/resource/CategoryType";
import { datetimeToDate } from "@/method/methodFormat";
import CategoryTable from "../../ListCategory/CategoryTable.vue";
import { apiGetProduct } from "@/api/productApi";

/**
 * Khởi tạo 1 Item với giá trị ban đầu là null
 * NTD 22/8/2022
 */
export default {
  data() {
    return {
      formStatus: 0,
      label: "", // tiêu đề
      product: {},
      list_img_url: [],
      product_image_url: "",
      Images,
      selectCategory: [],
      CategoryType,
      Resource,
    };
  },
  methods: {
    datetimeToDate,
    async deleteProduct() {
      await apiDeleteProduct(this.product.product_id).then(() => {
        this.emitter.emit("reloadProductList");
        this.closeForm();
      });
    },
    changeCategory(category) {
      var index = this.selectCategory.findIndex(
        (item) => item.category_id == category.category_id
      );
      if (index > -1) {
        this.selectCategory.splice(index, 1);
      } else {
        this.selectCategory.push(category);
      }
    },
    deleteImage(img_url) {
      var index = this.list_img_url.indexOf(img_url);
      if (index > -1) {
        this.list_img_url.splice(index, 1);
      }

      if (this.list_img_url.length > 0) {
        this.product_image_url = this.list_img_url[0];
      } else {
        this.product_image_url = null;
      }
    },
    openUploadWidget() {
      const myWidget = window.cloudinary.createUploadWidget(
        {
          cloudName: "dmci423da",
          uploadPreset: "kltn-image",
        },
        (error, result) => {
          if (!error && result && result.event === "success") {
            console.log("Done! Here is the image info: ", result.info);
            this.list_img_url.push(result.info.url);
            if (!this.product_image_url) {
              this.product_image_url = this.list_img_url[0];
            }
          }
        }
      );

      myWidget.open();
    },
    closeForm() {
      this.$router.replace(this.$router.path);
      this.$router.push(`/manageproduct`);
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
        this.updateProduct();
      }
    },

    /**
     * Gọi API thêm mới tài sản
     * NYD 5/9/2022
     */
    async addNewProduct() {
      this.product.status = 1;
      this.product.product_image_url = this.list_img_url.join(";");
      await apiInsertProduct(this.product, this.selectCategory)
        .then(() => {
          this.emitter.emit(
            "openToastMessage",
            Resource.SuccessMessage.SuccessMessageAdd
          );
          this.closeForm();
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
    async updateProduct() {
      this.product.product_image_url = this.list_img_url.join(";");
      await apiUpdateProduct(this.product, this.selectCategory)
        .then(() => {
          this.closeForm();
        })
        .catch(() => {
          this.emitter.emit(
            "openToastMessageError",
            Resource.ErrorMessage.ErrorMessageAdd
          );
        });
    },
    async getDataCategoryOfProduct() {
      this.selectCategory = [];
      await apiGetCategoryOfProduct(this.$route.params.id)
        .then((response) => {
          this.selectCategory = response.data;
        })
        .catch(() => {});
    },
    async getDataById() {
      await apiGetProduct(this.$route.params.id).then((response) => {
        this.product = response.data;
        this.list_img_url = this.product.product_image_url.split(";");
        this.product_image_url = this.list_img_url[0];
      });
    },
  },
  components: { StyleInput, InputNumber, CategoryTable },
  mounted() {
    /**
     * Mở form thêm tài sản từ Tool
     * NTD 8/8/2022
     */
    this.emitter.on("addNewProduct", async () => {
      this.formStatus = 1;
      this.product = {
        total_quantity: 0,
        quantity_rental: 0,
        quantity_remain: 0,
        quantity_sold: 0,
      };
      this.selectCategory = [];
      this.list_img_url = [];
      this.setLabel("Thêm sản phẩm");
      this.isOpen = true;
    });
    /**
     * Mở form sửa tài sản từ Row trong table
     * NTD 8/8/2022
     */
    this.emitter.on("updateProduct", async (product) => {
      this.product = Object.assign({}, product);
      this.list_img_url = product.product_image_url.split(";");
      this.product_image_url = this.list_img_url[0];
      this.formStatus = 2;
      this.setLabel("Sửa sản phẩm");
      await this.getDataCategoryOfProduct();
      this.isOpen = true;
    });
  },
  watch: {},
  async created() {
    if (this.$route.path.includes("addproduct")) {
      this.formStatus = 1;
      this.product = {
        total_quantity: 0,
        quantity_rental: 0,
        quantity_remain: 0,
        quantity_sold: 0,
      };
      this.selectCategory = [];
      this.list_img_url = [];
      this.setLabel("Thêm sản phẩm");
    } else if (this.$route.path.includes("editproduct")) {
      await this.getDataById();
      this.formStatus = 2;
      this.setLabel("Sửa sản phẩm");
      await this.getDataCategoryOfProduct();
    }
  },
};
</script>

<style>
@import url(./productForm.scss);
@import url(@/css/layout/form.scss);
</style>