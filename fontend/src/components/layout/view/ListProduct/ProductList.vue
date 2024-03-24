<template>
  <div class="product-list">
    <ProductTool></ProductTool>
    <div class="list-item">
      <StyleProduct
        v-for="item in datalist"
        :key="item.product_id"
        :product="item"
        @click="viewProduct(item)"
      ></StyleProduct>
    </div>
    <div class="paging"></div>
    <ProductView></ProductView>
  </div>
</template>
<script>
import ProductTool from "./ProductTool.vue";
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import Resource from "@/resource/MsResource";
import { apiGetPagingProduct } from "@/api/productApi";
import StyleProduct from "@/components/base/StyleProduct/StyleProduct.vue";
import ProductView from "./ProductView.vue";

export default {
  setup() {},
  data() {
    return {
      keyword: null, // từ cần lọc
      datalist: [], // danh sách tài sản
      pagaSize: 20,
      pageNumber: 1,
      isShowListCategory: true,
    };
  },
  components: { ProductTool, StyleProduct, ProductView },
  methods: {
    /**
     * Lấy dữ liệu từ backend
     */
    async getNewData() {
      await apiGetPagingProduct([], this.pagaSize, this.pageNumber)
        .then((response) => {
          this.datalist = response.data.data;
        })
        .catch(() => {
          this.emitter.emit(
            "openPopupNotice",
            Resource.PopupNotice.ErrorGetPaging
          );
        });
    },
    viewProduct(product) {
      if (this.$cookies.get("role") == 1) {
        this.emitter.emit("updateProduct", product);
      } else {
        this.emitter.emit("viewProduct", product);
      }
    },
  },

  mounted() {
    /**
     * Lọc tài sản theo tên
     * NTD 14/8/2022
     */
    this.emitter.on("searchItemInList", (valueSearch) => {
      this.keyword = valueSearch;
    }),
      /***
       * load lại page
       * NTD 5/9/2022
       */
      this.emitter.on("loadDataCategory", () => {
        this.getNewData();
      });
  },
  created() {
    /***
     * Lấy dữ liệu khi tạo component
     * NTD 25/8/2022
     */
    this.getNewData();
  },
  watch: {
    /***
     * Kiểm tra thay đổi giá trị để phân trang
     * NTD 5/9/2022
     */
    keyword() {
      if (this.pageNumber != 1) {
        this.pageNumber = 1;
      } else {
        this.getNewData();
      }
    },
  },
};
</script>
<style>
@import url(./productList.scss);
</style>