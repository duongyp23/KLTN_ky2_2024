<template>
  <div class="category-table">
    <div
      class="group-category-type"
      v-for="item in CategoryType"
      :key="item.id"
    >
      <div class="type-name">{{ item.name }}</div>
      <hr />
      <div class="list-category">
        <div
          v-for="category in datalist.filter((x) => x.type == item.id)"
          :key="category.category_id"
          @dblclick="openFormEdit(category)"
        >
          <input type="checkbox" @click="changeCategory(category)" />
          {{ category.category_code }}
        </div>
      </div>
    </div>
  </div>
</template>
<script>
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
      isManager: this.$cookies.get("role") == 1 ? true : false,
    };
  },
  props: {
    showButton: {
      default: false,
      type: Boolean,
    },
    selectCategory: {
      default: [],
    },
  },
  components: {},
  methods: {
    openForm() {
      this.emitter.emit("addNewCategory");
    },
    changeCategory(category) {
      var index = this.selectCategory.indexOf(category);
      if (index > -1) {
        this.$props.selectCategory.splice(index, 1);
      } else {
        this.$props.selectCategory.push(category);
      }
    },
    /**
     * Lấy dữ liệu từ backend
     */
    async getNewData() {
      this.isLoader = true;
      this.datalist = [];
      let filter = [];
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
      if (this.isManager && this.showButton) {
        this.emitter.emit("updateCategory", category);
      }
    },
  },

  mounted() {
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
  watch: {},
};
</script>
<style>
@import url(./categoryTable.scss);
</style>