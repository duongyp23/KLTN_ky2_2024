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
          @click="changeCategory(category)"
        >
          <input
            type="checkbox"
            :value="category.category_id"
            v-model="listSelectedId"
          />
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
      listSelectedId: this.selectCategory.map((item) => item.category_id),
    };
  },
  props: {
    selectCategory: {
      default: [],
    },
  },
  components: {},
  methods: {
    checkvalue(category) {
      let item = this.selectCategory.find(
        (x) => x.category_id == category.category_id
      );
      if (item) {
        return true;
      }
      return false;
    },
    openForm() {
      this.emitter.emit("addNewCategory");
    },
    changeCategory(category) {
      let item = this.$props.selectCategory.find(
        (x) => x.category_id == category.category_id
      );
      let newList = [];
      if (item) {
        newList = this.$props.selectCategory.filter(
          (x) => x.category_id != category.category_id
        );
        this.$emit("update:selectCategory", newList);
      } else {
        this.$props.selectCategory.push(category);
        this.$emit("update:selectCategory", this.$props.selectCategory);
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
  watch: {
    selectCategory: {
      handler: function () {
        this.listSelectedId = this.selectCategory.map(
          (item) => item.category_id
        );
      },
      deep: true,
    },
  },
};
</script>
<style>
@import url(./categoryTable.scss);
</style>