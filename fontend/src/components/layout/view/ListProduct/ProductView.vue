<template>
  <div class="product-form form-view" v-show="isOpen" @keyup.esc="closeForm()">
    <div class="form">
      <div class="form-header">
        <div>{{ product.product_name }}</div>
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
          ></div>
          <div class="list-small-img">
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
          <div class="row-info">
            <div class="title">Mã sản phẩm</div>
            <div class="">{{ product.product_code }}</div>
          </div>
          <div class="row-info">
            <div class="title">Giá thuê</div>
            <div class="">{{ product.rental_price }}</div>
          </div>
          <div class="row-info">
            <div class="title">Giá sản phẩm</div>
            <div class="">{{ product.product_price }}</div>
          </div>
          <div>Mô tả sản phẩm:</div>
          <div>{{ product.description }}</div>
          <div class="list-category">
            <div
              v-for="item in categoryData"
              :key="item.category_id"
              :class="
                selectCategory.find((x) => x.category_id == item.category_id)
                  ? 'category-select'
                  : 'category-not-select'
              "
            >
              {{ item.category_code }}
            </div>
          </div>
          <div class="action">
            <button class="btn-add-cart" @click="addToCart">
              Thêm vào giỏ hàng
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Images from "@/assets/icon/images";
import { apiGetAllCategory, apiGetCategoryOfProduct } from "@/api/categoryApi";
import { apiAddProductToCart } from "@/api/productApi";

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
    };
  },
  methods: {
    async addToCart() {
      if (this.$cookies.get("token")) {
        await apiAddProductToCart(
          this.product.product_id,
          this.$cookies.get("userId")
        )
          .then(() => {
            this.emitter.emit(
              "openToastMessage",
              "Thêm sản phẩm vào giỏ hàng thành công"
            );
            this.closeForm();
          })
          .catch(() => {});
      } else {
        this.emitter.emit(
          "openToastMessageError",
          "Thêm sản phẩm vào giỏ hàng không thành công"
        );
      }
    },
    closeForm() {
      this.isOpen = false;
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
  components: {},
  mounted() {
    /**
     * Mở form sửa tài sản từ Row trong table
     * NTD 8/8/2022
     */
    this.emitter.on("viewProduct", async (product) => {
      this.product = Object.assign({}, product);
      this.list_img_url = this.product.product_list_img_url.split(";");
      await this.getDataCategoryOfProduct();
      this.isOpen = true;
    });
  },
  watch: {},
  created() {
    this.getCategoryData();
  },
};
</script>

<style>
@import url(./productForm.scss);
@import url(@/css/layout/form.scss);
</style>