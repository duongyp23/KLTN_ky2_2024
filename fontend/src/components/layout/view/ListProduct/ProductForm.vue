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
        <div class="img-upload">
          <div
            class="big-img"
            :style="
              product.product_image_url
                ? 'background-image: url(' + product.product_image_url + ')'
                : 'border : 2px solid #afafaf'
            "
          >
            <button
              v-if="product.product_image_url"
              class="btn-delete"
              :style="'background-image: url(' + Images.del + ')'"
              @click="deleteImage(product.product_image_url)"
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
              @click="product.product_image_url = item"
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
            <InputNumber
              :label="'Giá bán sản phẩm'"
              v-model:numberValue="product.sell_price"
            ></InputNumber>
            <div class="style-input col-flex">
              <label class="Form-Label">Trạng thái</label>
              <MsCombobox
                :items="Resource.ProductStatus"
                v-model:value="product.status_name"
                v-model:id="product.status"
              ></MsCombobox>
            </div>
          </div>
          <StyleInput
            class="description"
            :label="'Mô tả sản phẩm'"
            v-model:value="product.description"
            type="textarea"
          ></StyleInput>
          <div
            class="group-category-type"
            v-for="item in CategoryType"
            :key="item.id"
          >
            <div class="type-name">{{ item.name }}</div>
            <hr />
            <div class="list-category">
              <div
                v-for="category in categoryData.filter(
                  (x) => x.type == item.id
                )"
                :key="category.category_id"
              >
                <button
                  :class="
                    selectCategory.find(
                      (x) => x.category_id == category.category_id
                    )
                      ? 'category category-select'
                      : 'category category-not-select'
                  "
                  @click="changeCategory(category)"
                >
                  {{ category.category_code }}
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="form-footer">
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
import MsCombobox from "@/components/base/MsCombobox.vue";

import {
  apiInsertProduct,
  apiUpdateProduct,
  apiDeleteProduct,
} from "@/api/productApi";
import Images from "@/assets/icon/images";
import { apiGetAllCategory, apiGetCategoryOfProduct } from "@/api/categoryApi";
import CategoryType from "@/resource/CategoryType";

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
      list_img_url: [],
      Images,
      categoryData: [],
      selectCategory: [],
      CategoryType,
      Resource,
    };
  },
  methods: {
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
        this.product.product_image_url = this.list_img_url[0];
      } else {
        this.product.product_image_url = null;
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
            if (!this.product.product_image_url) {
              this.product.product_image_url = this.list_img_url[0];
            }
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
        this.updateProduct();
      }
    },

    /**
     * Gọi API thêm mới tài sản
     * NYD 5/9/2022
     */
    async addNewProduct() {
      this.product.status = 1;
      this.product.product_list_img_url = this.list_img_url.join(";");
      await apiInsertProduct(this.product, this.selectCategory)
        .then(() => {
          this.emitter.emit(
            "openToastMessage",
            Resource.SuccessMessage.SuccessMessageAdd
          );
          this.emitter.emit("reloadProductList");
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
    async updateProduct() {
      this.product.product_list_img_url = this.list_img_url.join(";");
      await apiUpdateProduct(this.product, this.selectCategory)
        .then(() => {
          this.emitter.emit("reloadProductList");
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
     * Lấy dữ liệu nhãn dán
     */
    async getCategoryData() {
      this.categoryData = [];
      await apiGetAllCategory([])
        .then((response) => {
          this.categoryData = response.data;
        })
        .catch(() => {});
    },
    async getDataCategoryOfProduct() {
      await apiGetCategoryOfProduct(this.product.product_id)
        .then((response) => {
          this.selectCategory = response.data;
        })
        .catch(() => {});
    },
  },
  components: { StyleInput, InputNumber, MsCombobox },
  mounted() {
    /**
     * Mở form thêm tài sản từ Tool
     * NTD 8/8/2022
     */
    this.emitter.on("addNewProduct", async () => {
      this.getCategoryData();

      this.formStatus = 1;
      this.product = {};
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
      this.getCategoryData();

      this.product = Object.assign({}, product);
      this.formStatus = 2;
      this.setLabel("Sửa nhãn dán");
      await this.getDataCategoryOfProduct();
      this.isOpen = true;
    });
  },
  watch: {},
  created() {},
};
</script>

<style>
@import url(./productForm.scss);
@import url(@/css/layout/form.scss);
</style>