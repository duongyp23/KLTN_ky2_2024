<template>
  <div class="category-table">
    <ProductTool></ProductTool>
  </div>
</template>
<script>
import ProductTool from "./ProductTool.vue";
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import Resource from "@/resource/MsResource";
import { apiGetAllCategory } from "@/api/categoryApi";
import CategoryType from "@/resource/CategoryType";

export default {
  data() {
    return {
      keyword: null, // từ cần lọc
      datalist: [], // danh sách tài sản
      CategoryType,
    };
  },
  components: { ProductTool },
  methods: {
    /**
     * Lấy dữ liệu từ backend
     */
    async getNewData() {
      this.isLoader = true;
      this.datalist = [];
      let filter = [];
      if (this.keyword != null) {
        filter.push({
          columnName: "category_code",
          filterValue: this.keyword,
          operatorValue: "=",
        });
      }
      await apiGetAllCategory(filter)
        .then((response) => {
          this.datalist = response.data;
        })
        .catch(() => {
          this.emitter.emit(
            "openPopupNotice",
            Resource.PopupNotice.ErrorGetPaging
          );
        });
    },
    openFormEdit(category) {
      this.emitter.emit("updateCategory", category);
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