<template>
  <div class="product-view form-view" v-show="isOpen" @keyup.esc="closeForm()">
    <div class="form">
      <div class="form-header">
        <div class="title-header">{{ product.product_name }}</div>
        <button
          class="btn-header"
          style="border: none; background: transparent"
          title="Đóng"
          @click="closeForm()"
        >
          <div class="btn-close">X</div>
        </button>
      </div>
      <div class="form-center mb-1">
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
            <div class="product-code">{{ product.product_code }}</div>
          </div>
          <div class="row-info">
            <div class="title">Giá sản phẩm</div>
            <div class="amount2">
              {{ replaceNumber(product.product_price) }}
            </div>
          </div>
          <div class="row-info">
            <div class="title">Giá thuê theo ngày</div>
            <div class="amount1">
              {{ replaceNumber(product.rental_price_day) }}
            </div>
          </div>
          <div class="row-info">
            <div class="title">Giá thuê theo tuần</div>
            <div class="amount1">
              {{ replaceNumber(product.rental_price_week) }}
            </div>
          </div>
          <div class="row-info">
            <div class="title">Giá thuê theo tháng</div>
            <div class="amount1">
              {{ replaceNumber(product.rental_price_month) }}
            </div>
          </div>
          <div class="row-info"><div class="title">Mô tả sản phẩm:</div></div>
          <div class="row-info">{{ product.description }}</div>
          <div class="flex-row wrap ml-2 mt-2">
            <label
              class="tag"
              v-for="item in selectCategory"
              :key="item.category_id"
            >
              #{{ item.category_code }}
            </label>
          </div>
          <div class="action">
            <button class="btn-add-cart" @click="addToCart">
              Thêm vào giỏ hàng
            </button>
          </div>
          <div class="row-info"><div class="title">Bình luận</div></div>
          <div></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Images from "@/assets/icon/images";
import { apiGetCategoryOfProduct } from "@/api/categoryApi";
import { apiAddProductToCart } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";

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
      selectCategory: [],
    };
  },
  methods: {
    replaceNumber,
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
          "Vui lòng đăng nhậ để thực hiện chức năng"
        );
        this.$router.replace(this.$router.path);
        this.$router.push("/login");
      }
    },
    closeForm() {
      this.isOpen = false;
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
  created() {},
};
</script>

<style>
@import url(./productForm.scss);
@import url(@/css/layout/form.scss);
</style>