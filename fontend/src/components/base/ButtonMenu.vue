<template>
  <router-link class="link" :to="routerPath" @click="selectRow">
    <div>
      <button class="btn-menu" :class="{ selected: isSelected }">
        <div class="btn-menu-name">{{ label }}</div>
      </button>
    </div>
  </router-link>
</template>
<script>
export default {
  data() {
    return {
      isSelected: false, // hiển thị
    };
  },
  props: {
    routerPath: { type: String, default: "/" }, // routerlink
    label: { required: true, type: String }, // tiêu đề
    selectList: { type: Array },
    isOpen: { type: Boolean },
  },
  computed: {},
  methods: {
    /**
     * Bỏ chọn tất cả menu rồi chọn chính nó
     * NTD 11/8/2022
     */
    selectRow() {
      if (this.isSelected == false) {
        this.isSelected = !this.isSelected;
      }
    },
  },
  mounted() {},
  watch: {
    /**
     * Kiểm tra khi router thay đổi
     * NTD 15/9/2022
     */
    $route() {
      if (this.$route.path.includes(this.routerPath)) {
        this.isSelected = true;
      } else {
        this.isSelected = false;
      }
    },
  },
};
</script>
<style>
@import url(../../css/base/button.scss);
@import url(../../css/layout/menu.css);
</style>