<template>
  <div class="row-flex">
    <div class="form-small-div col-flex">
      <label class="Form-Label"
        >{{ label1 }} <span style="color: red">*</span></label
      >
      <div
        class="combobox"
        @click="changeIsOpen()"
        v-click-outside="close"
        @keydown.down="setFocusDown()"
        @keydown.up="setFocusUp()"
        @keydown.enter="setValue(listItem[tmpIndex])"
        @keydown.tab="close()"
      >
        <div
          class="box-input"
          :class="{ 'box-input-select': isOpen, error: check }"
          style="padding-left: 14px"
        >
          <input
            type="text"
            class="input-box form-placeholder"
            :value="inputCode"
            :placeholder="placeholder"
            @input="
              isOpen = true;
              $emit('update:inputCode', $event.target.value);
            "
            ref="input"
            style="cursor: pointer"
          />
          <div
            class="icon icon-mini-down"
            style="position: absolute; right: 12px; top: 15px"
          ></div>
        </div>
        <div
          class="box-item"
          :class="{ 'box-item-display': isOpen }"
          ref="boxItem"
          :style="styleBoxItem"
        >
          <div class="item-box item-header">
            <b class="item-code">Mã</b>
            <b class="item-name">Tên</b>
          </div>
          <div
            class="item-box"
            v-for="(item, index) of listItem"
            :key="item.id"
            @click.stop="setValue(item)"
            :class="{
              highlight: index == tmpIndex,
              selected: item.name === inputCode,
            }"
          >
            <div class="item-code">
              {{ item.code }}
            </div>
            <div class="item-name">
              {{ item.name }}
            </div>
          </div>
        </div>
      </div>
      <label v-if="check" style="color: red; font-size: 11px"
        >Cần phải nhập thông tin
      </label>
    </div>

    <div class="form-big-div col-flex">
      <label class="Form-Label">{{ label2 }}</label>
      <input
        type="text"
        class="form-input"
        style="background-color: rgb(245, 245, 245)"
        readonly
        :value="inputName"
        disabled
      />
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      listItem: [], // danh sách hiển thị
      isOpen: false, // hiển thị
      tmpIndex: 0, // vị trí focus
      heightItem: 35, // chiều cao
    };
  },
  props: {
    placeholder: { type: String }, // placeholder
    label1: { type: String }, //mã
    label2: { type: String }, // tên
    inputCode: { type: String }, // giá trị mã
    inputName: { type: String }, // giá trị têm
    items: { type: Array }, // danh sách
    check: { type: Boolean }, //kiểm tra
    selectValue: { type: Object }, // gái trị được chọn
  },
  computed: {
    /**
     * Chiều cao cho mỗi option item
     * NTD 22/8/2022
     */
    styleItem() {
      return {
        minHeight: this.heightItem + "px",
      };
    },
    /**
     * Chiều cao cho mỗi box item tùy thuộc vào số lượng item
     * NTD 22/8/2022
     */
    styleBoxItem() {
      if (this.listItem.length < 4) {
        return {
          height: this.heightItem * (this.listItem.length + 1) + "px",
        };
      }
      return {
        height: this.heightItem * 5 + "px",
      };
    },
  },
  methods: {
    /**
     * Focus vào input
     * NTD 27/9/2022
     */
    setFocusInput() {
      this.$refs.input.focus();
    },
    /**
     * Sự kiện thi focus ra ngoài
     * NTD 27/9/2022
     */
    focusoutInput() {
      if (this.inputCode) {
        this.setValue(this.listItem[this.tmpIndex]);
      }
      this.isOpen = false;
    },
    /**
     * Thay đổi value cho input
     * NTD 8/8/2022
     */
    setValue(item) {
      if (this.isOpen == true) {
        if (item) {
          this.$emit("update:inputCode", item.code);

          this.$emit("update:selectValue", item);
        } else {
          this.$emit("update:inputCode", null);
          this.$emit("update:inputName", null);
        }
        this.isOpen = false;
      }
    },
    /**
     * thay đổi giá trị bằng cách nhập
     * NTD 14/8/2022
     */
    // setValueInput() {
    //     var check = false
    //     var index = 0
    //     if(this.inputCode && this.tmpIndex == null ){
    //         console.log('ok');
    //         this.items.forEach(element => {
    //             if(element.code.toLowerCase() == this.inputCode.toLowerCase() || element.name.toLowerCase() == this.inputCode.toLowerCase()) {
    //                 this.setValue(element, index)
    //                 check = true
    //                 console.log('ok2');
    //             }
    //             index++
    //         });
    //     }
    //     if(check == false && this.inputCode && this.tmpIndex == null) {
    //         this.emitter.emit("openPopupNotice", this.label1 + " không tồn tại")
    //         this.$emit('update:inputCode', null)
    //         this.$emit('update:inputName', null)
    //     }
    // },
    /**
     * Kiểm tra các giá trị trùng với input
     * NTD 14/8/2022
     */
    checkInput() {
      this.listItem = [];
      if (this.inputCode == null || this.inputCode == "") {
        this.listItem = this.items;
      } else if (this.inputCode) {
        this.items.forEach((element) => {
          if (
            element.name.toLowerCase().includes(this.inputCode.toLowerCase()) ||
            element.code.toLowerCase().includes(this.inputCode.toLowerCase())
          ) {
            this.listItem.push(element);
          }
        });
      }
      this.tmpIndex = 0;
      this.$refs.boxItem.scrollTo(0, 0);
    },
    /**
     * Sự kiện di chuyển xuống
     * NTD 14/8/2022
     */
    setFocusDown() {
      if (this.isOpen == false) {
        this.isOpen = true;
      } else {
        if (this.tmpIndex < this.listItem.length - 1) {
          this.tmpIndex++;
        }
      }
      this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
    },
    /***
     * sự kiện khi bấm nút di chuyển lên
     * NTD 14/8/2022
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
     * NTD 14/8/2022
     */
    changeIsOpen() {
      this.isOpen = !this.isOpen;
      if (this.isOpen == true) {
        this.$refs.input.focus();
      }
    },
    /**
     * Đóng combobox
     * NTD 14/8/2022
     */
    close() {
      this.isOpen = false;
    },
  },
  watch: {
    /**
     *  kiểm tra khi mã thay đổi
     * NTD 15/9/2022
     */
    inputCode() {
      this.checkInput();
    },
    /**
     * Kiểm tra khi danh sách thay đổi
     * NTD 15/9/2022
     */
    items() {
      this.checkInput();
    },
    /**
     * Kiểm tra khi giá trị duduocj chọn thay đổi
     * NTD 15/9/2022
     */
    selectValue() {
      this.listItem = this.items;
      for (let index = 0; index < this.listItem.length; index++) {
        if (this.selectValue.id == this.listItem[index].id) {
          this.tmpIndex = index;
          this.$refs.boxItem.scrollTo(0, this.tmpIndex * 35);
        }
      }
    },
  },
};
</script>
<style>
@import url(../../css/layout/form.scss);
@import url(../../css/base/combobox.scss);
</style>