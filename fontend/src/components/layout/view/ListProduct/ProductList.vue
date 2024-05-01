<template>
  <div class="dictionary-page">
    <CategoryTable class="left-page" :show-button="true"></CategoryTable>

    <div class="product-list right-page">
      <product-tool class="ml-2 mr-2"></product-tool>
      <div class="list-item mt-1">
        <StyleProduct
          v-for="item in datalist"
          :key="item.product_id"
          :product="item"
          @click="viewProduct(item)"
        ></StyleProduct>
      </div>
      <div class="paging mt-2"></div>
    </div>
  </div>
</template>
<script>
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import Resource from "@/resource/MsResource";
import { apiGetPagingProduct } from "@/api/productApi";
import StyleProduct from "@/components/base/StyleProduct/StyleProduct.vue";
import ProductTool from "./ProductTool.vue";
import CategoryTable from "../ListCategory/CategoryTable.vue";

export default {
  setup() {},
  data() {
    return {
      keyword: null, // từ cần lọc
      datalist: [], // danh sách tài sản
      pageNumber: 1,
      isShowListCategory: true,
    };
  },
  components: { StyleProduct, ProductTool, CategoryTable },
  methods: {
    /**
     * Lấy dữ liệu từ backend
     */
    async getProductData() {
      let filter = [];
      if (this.keyword) {
        filter.push({
          columnName: "product_name",
          filterValue: "'%" + this.keyword + "%'",
          operatorValue: "LIKE",
        });
      }
      await apiGetPagingProduct(filter, 20, this.pageNumber)
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
      this.$router.replace(this.$router.path);
      this.$router.push(`/products/${product.product_id}`, {
        params: { id: product.product_id },
      });
    },
  },

  mounted() {
    /**
     * Lọc tài sản theo tên
     * NTD 14/8/2022
     */
    this.emitter.on("searchProduct", (valueSearch) => {
      this.keyword = valueSearch;
    }),
      /***
       * load lại page
       * NTD 5/9/2022
       */
      this.emitter.on("reloadProductList", () => {
        this.getProductData();
      });
  },
  created() {
    this.getProductData();
  },
  watch: {
    /***
     * Kiểm tra thay đổi giá trị để phân trang
     * NTD 5/9/2022
     */
    keyword() {
      this.getProductData();
    },
  },
};
</script>
<style>
@import url(./productList.scss);
</style>