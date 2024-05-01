<template>
  <div class="product-view" @keyup.esc="closeForm()">
    <div class="title-header mb-1">{{ product.product_name }}</div>
    <div class="form-center mb-1">
      <div class="img-upload">
        <div
          class="big-img"
          :style="
            product_image_url
              ? 'background-image: url(' + product_image_url + ')'
              : 'border : 2px solid #afafaf'
          "
        ></div>
        <div class="list-small-img">
          <div
            class="small-img"
            v-for="(item, index) in list_img_url"
            :key="index"
            :style="'background-image: url(' + item + ')'"
            @click="product_image_url = item"
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
        <div class="flex-row wrap ml-2">
          <div
            class="option"
            :class="{ selected: selectOption == item.option_code }"
            v-for="item in stocks"
            :key="item.stock_id"
            @click="selectOption = item.option_code"
          >
            {{ item.option_code }}
          </div>
        </div>
        <InputNumber
          class="ml-2 mr-2 mt-1"
          :label="'Số lượng thuê'"
          v-model:numberValue="numberRental"
        ></InputNumber>
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
</template>
<script>
import Images from "@/assets/icon/images";
import { apiGetCategoryOfProduct } from "@/api/categoryApi";
import { apiAddProductToCart, apiGetProduct } from "@/api/productApi";
import { replaceNumber } from "@/method/methodFormat";
import { apiGetAllStock } from "@/api/stockApi";
import InputNumber from "@/components/base/StyleInput/InputNumber.vue";
export default {
  data() {
    return {
      formStatus: 0,
      label: "", // tiêu đề
      product: {},
      list_img_url: [],
      Images,
      selectCategory: [],
      product_image_url: "",
      stocks: [],
      selectOption: "",
      numberRental: 0,
    };
  },
  components: { InputNumber },
  methods: {
    replaceNumber,
    async addToCart() {
      if (this.$cookies.get("token")) {
        if (this.selectOption != "" && this.numberRental > 0) {
          await apiAddProductToCart(
            this.product.product_id,
            this.$cookies.get("userId"),
            this.selectOption,
            this.numberRental
          )
            .then(() => {
              this.emitter.emit(
                "openToastMessage",
                "Thêm sản phẩm vào giỏ hàng thành công"
              );
              this.closeForm();
            })
            .catch(() => {});
        }
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
    async getDataById() {
      await apiGetProduct(this.$route.params.id).then((response) => {
        this.product = response.data;
        this.list_img_url = this.product.product_image_url.split(";");
        this.product_image_url = this.list_img_url[0];
      });
    },
    async getStockData() {
      let filter = [];
      filter.push({
        columnName: "product_id",
        filterValue: "'" + this.$route.params.id + "'",
        operatorValue: "=",
      });
      await apiGetAllStock(filter).then((response) => {
        this.stocks = response.data;
      });
    },
  },

  async mounted() {
    await this.getDataById();
    await this.getStockData();
  },
  watch: {},
  created() {},
};
</script>

<style>
@import url(./productView.scss);
@import url(@/css/layout/form.scss);
</style>