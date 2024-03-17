<template>
  <div
    class="combobox"
    @click="changeIsOpen"
    v-click-outside="close"
    @keydown.down="setFocusDown()"
    @keydown.up="setFocusUp()"
    @keydown.enter="setValue(listItem[tmpIndex])"
    @keydown.tab="isOpen = false"
  >
    <div class="box-input" :class="{ 'box-input-select': isOpen }">
      <div :class="[icon ? 'icon icon-' + icon : '']"></div>
      <input
        type="text"
        class="input-box"
        :value="value"
        :placeholder="placeholder"
        @input="
          isOpen = true;
          $emit('update:value', $event.target.value);
        "
        ref="input"
        style="cursor: pointer"
      />
      <div
        class="icon icon-mini-down"
        style="position: absolute; right: 12px; top: 16px"
      ></div>
    </div>
    <div
      class="box-item"
      :class="{ 'box-item-display': isOpen }"
      ref="boxItem"
      :style="styleBoxItem"
    >
      <div
        class="item-box"
        v-for="(item, index) of listItem"
        :key="item.id"
        v-on:click.stop="setValue(item)"
        :class="{ highlight: index == tmpIndex || item.name == value }"
      >
        <div class="item-name">{{ item.name }}</div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      isOpen: false, // hiển thị lựa chọn
      listItem: [], // danh sách hiener thị
      tmpIndex: 0, // vị trí focus
      heightItem: 35, // chiều cao
    };
  },
  props: {
    placeholder: { type: String }, // placeholder
    value: { type: String }, // giá trị input
    icon: { type: String }, // icon
    items: { type: Array }, // danh sách
    id: { default: null, type: String }, // ID
  },
  computed: {
    /**
     * Chiều cao cho mỗi option item
     * NTD 22/8/2022
     */
    // styleItem() {
    //     return{
    //         minHeight : this.heightItem + 'px'
    //     }
    // },
    /**
     * Chiều cao cho mỗi box item tùy thuộc vào số lượng item
     * NTD 22/8/2022
     */
    styleBoxItem() {
      if (this.listItem.length < 5) {
        return {
          minHeight: this.heightItem * this.listItem.length + "px",
        };
      }
      return {
        minHeight: this.heightItem * 5 + "px",
      };
    },
  },
  methods: {
    /**
     * Thiết chọn lên xuống bằng bàn phím
     * NTD 17/8/2022
     */
    setFocusDown() {
      this.isOpen = true;
      if (this.tmpIndex < this.listItem.length - 1) {
        this.tmpIndex++;
      }
      this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
    },
    /**
     * Thiết chọn lên xuống bằng bàn phím
     * NTD 17/8/2022
     */
    setFocusUp() {
      this.isOpen = true;
      if (this.tmpIndex > 0) {
        this.tmpIndex--;
      }
      this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
    },
    /***
     * thay đổi giá trị isOpen
     * NTD
     */
    changeIsOpen() {
      this.isOpen = !this.isOpen;
      if (this.isOpen == true) {
        this.$refs.input.focus();
      }
    },
    /**
     * Thay đổi value cho input
     * NTD 8/8/2022
     */
    setValue(item) {
      if (this.isOpen == true) {
        if (item) {
          if (this.id == item.id) {
            this.$emit("update:value", null);
            this.$emit("update:id", null);
          } else {
            this.$emit("update:value", item.name);
            this.$emit("update:id", item.id);
          }
        } else {
          this.$emit("update:value", null);
          this.$emit("update:id", null);
        }
        this.isOpen = false;
      }
    },

    /**
     * Lọc theo input
     * NTD 14/8/2022
     */

    close() {
      if (this.value) {
        this.isOpen = false;
      } else {
        this.tmpIndex = 0;
        this.$refs.boxItem.scrollTo(0, 0);
        this.isOpen = false;
      }
    },
    /**
     * highlight các dòng dữ liệu khi nhập trùng
     * NTD 17/8/2022
     */
    checkInput() {
      this.listItem = [];
      if (this.value == null || this.value == "") {
        this.listItem = this.items;
      } else if (this.value) {
        this.items.forEach((element) => {
          if (element.name.toLowerCase().includes(this.value.toLowerCase())) {
            this.listItem.push(element);
          }
        });
      }
      this.tmpIndex = 0;
      this.$refs.boxItem.scrollTo(0, 0);
    },
  },
  watch: {
    /**
     * Kiểm tra khi gái trị thay đổi
     * NTD 15/9/2022
     */
    value() {
      this.checkInput();
    },
    /**
     * Kiểm tra khi id thay đổi
     * NTD 15/9/2022
     */
    id() {
      this.listItem = this.items;
      for (let index = 0; index < this.listItem.length; index++) {
        if (this.id == this.listItem[index].id) {
          this.tmpIndex = index;
          this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
        }
      }
    },
    /**
     * Kiểm tra khi danh sách thay đổi
     * NTD 15/9/2022
     */
    items() {
      this.checkInput();
    },
  },
  mounted() {
    this.listItem = this.items;
    for (let index = 0; index < this.listItem.length; index++) {
      if (this.id == this.listItem[index].id) {
        this.tmpIndex = index;
        this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
      }
    }
  },
  created() {},
};
</script>
<style>
@import url(../../css/base/combobox.scss);
</style>