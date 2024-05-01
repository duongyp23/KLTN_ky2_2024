<template>
  <div class="homepage">
    <div class="banner">
      <img
        src="https://i.pinimg.com/564x/16/05/fe/1605feca3b5e85f5f70493bb6b7b015a.jpg"
        alt=""
        class="img-banner"
      />
    </div>
    <div class="group">
      <h2 class="title">Sản phẩm mới về</h2>
      <div class="list-item">
        <StyleProduct
          v-for="item in datalist"
          :key="item.product_id"
          :product="item"
        ></StyleProduct>
      </div>
    </div>
    <div class="group">
      <h2 class="title">Sản phẩm được thuê nhiều</h2>
      <div class="list-item">
        <StyleProduct
          v-for="item in datalist"
          :key="item.product_id"
          :product="item"
        ></StyleProduct>
      </div>
    </div>
    <div class="group">
      <h2 class="title">Sản phẩm được ưa thích nhất</h2>
      <div class="list-item">
        <StyleProduct
          v-for="item in datalist"
          :key="item.product_id"
          :product="item"
        ></StyleProduct>
      </div>
    </div>
    <div class="group">
      <h2 class="title">Loại sản phẩm</h2>
      <div class="list-item"></div>
    </div>
    <div class="group">
      <h2 class="title">Thuê đồ sự kiện</h2>
      <div class="list-item"></div>
    </div>
  </div>
</template>
<script>
import { apiGetPagingProduct } from "@/api/productApi";
import StyleProduct from "@/components/base/StyleProduct/StyleProduct.vue";
export default {
  data() {
    return {
      datalist: [],
    };
  },
  components: { StyleProduct },
  methods: {
    /**
     * Lấy dữ liệu từ backend
     */
    async getProductData() {
      let filter = [];
      await apiGetPagingProduct(filter, 20, 1).then((response) => {
        this.datalist = response.data.data;
      });
    },
  },
  created() {
    this.getProductData();
  },
};
</script>
<style>
@import url(./homePage.scss);
</style>