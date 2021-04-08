<template>
  <div class="formBody" :class="{ isHide: isHide }">
    <div class="myButtonDialog" style="border-bottom: solid 1px gray">
      <button class="myButton" id="buttonSave">
        <div class="iconSave"></div>
        Lưu
      </button>
      <button class="myButton" id="buttonCancel" @click="cancel">
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>

    <div class="bodyDialog">
    </div>
    <div
      class="myButtonDialog"
      style="border-top: solid 1px gray"
      id="buttonBottom"
    >
      <button class="myButton" id="buttonSave" @click.prevent="save">
        <div class="iconSave"></div>
        Lưu
      </button>
      <button class="myButton" id="buttonCancel" @click="cancel">
        <div class="iconCancel"></div>
        Hủy bỏ
      </button>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import DxNumberBox from "devextreme-vue/number-box";
export default {
  name: "ProductDetails",
  components: { DxNumberBox },

  props: {
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
  },

  data() {
    return {};
  },
  methods: {
    /**
     * Input chỉ đc nhập số
     */
    onlyNumber($event) {
      let keyCode = $event.keyCode ? $event.keyCode : $event.which;
      if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
        // 46 is dot
        $event.preventDefault();
      }
    },

    /**Sự kiện hủy */
    cancel() {
      this.closeForm();
    },
    /**Đóng form */
    closeForm() {
      this.$emit("outIsHide", !this.isHide);
    },
    /**format tiền */
    formatPrice(value) {
      let val = (value / 1).toFixed(2).replace(".", ",");
      return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
  },
  computed: {},
  async created() {},
};
</script>

<style>
.borders {
  border: 1px solid red !important;
}
.imgValidate {
  width: 16px;
  height: 16px;
  margin: 6px;
  background-size: contain;
  background-repeat: no-repeat;
  /* background-image: url("../../assets/images/exclamation.png"); */
}

.formBody {
  position: fixed;
  z-index: 10;
  top: 60px;
  height: calc(100vh - 70px);
  width: calc(100% - 170px);
  background-color: #ffffff;
}

.rowForm {
  display: flex;
  margin: 10px 10px 10px 10px;
}
.isHide {
  display: none;
}
.myButtonDialog {
  position: relative;
  display: flex;
  padding: 10px;
  height: 52px;
  width: 100%;
  align-items: center;
  justify-content: flex-start !important;
  align-items: center;
  justify-content: space-between;
  background-color: white;
}

.myButton {
  height: 35px;
  width: auto;
  margin-left: 5px;
  margin-right: 5px;
  outline: none;
  border: none;
  cursor: pointer;
  padding: 0 14px 0 14px;
  display: flex;
  border-radius: 3px;
  align-items: center;
}

.colorName {
  height: 100%;
  width: 160px;
  justify-content: center;
  padding-top: 10px;
  padding-left: 10px;
  background-color: #d3d0d0;
}

.bodyDialog {
  max-height: calc(100% - 108px);
  overflow: auto;
}
.labelForm {
  width: 250px;
}
#buttonBottom {
  position: absolute;
  bottom: 0px;
}
.boxImage {
  height: 200px;
  width: 200px;
  border-color: #636dde !important;
  border-width: 1px !important;
  border-style: dashed;
  border-radius: 5px;
  -moz-border-radius: 3px;
  -webkit-border-radius: 3px;
}

.xboxInner {
  overflow: hidden;
  position: relative;
  left: 0;
  top: 24px;
  width: 198px;
  height: 32px;
  justify-content: center !important;
}
.inputfile {
  width: 0.1px;
  height: 0.1px;
  opacity: 0;
  overflow: hidden;
  position: absolute;
  z-index: -1;
}
.inputfile + label {
  display: inline-block;
  cursor: pointer;
  background-color: #2b3173;
  color: white !important;
  width: 40px;
  height: 24px;
  right: auto;
  margin-left: 79px;
  margin-top: 4px;
  text-align: center;
  border-radius: 3px;
}
.inputNumber {
  width: 200px;
  background-color: #d3d0d0;
}
.iconPlusContain {
  width: 30px;
  height: 32px;
  background-color: #cccaca;
  padding: 9px;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
}
.gridDetail {
  max-height: 300px;
  min-width: 1450px;
  overflow: auto;
}
/* .detailBodyTable{
    border: solid 1px black !important;
  } */
.detailTable th {
  text-align: center;
  align-items: center;
  height: 32px;
  padding: 0;
  z-index: 10;
  background-color: #cccaca;
}
.detailBodyTable td {
  padding: 10px 0 10px 0 !important;
}
.detailBodyTable input {
  border: none !important;
  background-color: transparent !important;
}
.description {
  min-height: 180px;
  max-height: 300px;
  width: 438px;
  overflow: auto;
}
.imgBlock {
  padding: 0px;
  right: auto;
  left: 0px;
  top: 0px;
  margin: 0px;
  width: 198px;
  height: 146px;
}

.imgProduct {
  width: 100%;
  height: 100%;
  margin: 0 auto;
  background-image: url("https://testmisatrinhmisa.mshopkeeper.vn/backendg2/api/Image?id=12EB2AFC-16CA-45EC-8E9A-1A1B829EBFB9&type=4&mode=pad&CompanyCode=testmisatrinhmisa&w=200&h=180");
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}

.symbolPen {
  margin: -1px;
  display: flex;
  width: 100px;
  padding: 6px;
  height: 29px !important;
  margin-left: 50%;
  color: #aaaaaa;
  font-weight: bold;
  border: 1px solid #aaaaaa;
  border-radius: 4px;
}
.symbolPen:hover {
  cursor: pointer;
}
.iconPen {
  background: url("../../assets/common-icon.png") no-repeat -79px -180px;
  width: 12px;
  padding: 8px;
  margin-top: 3px;
}
.dx-texteditor-input {
  width: 300px !important;
  height: 32px !important;
  text-align: right !important;
}
.dx-widget input,
.dx-widget textarea {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  line-height: 1.35715;
}
</style>