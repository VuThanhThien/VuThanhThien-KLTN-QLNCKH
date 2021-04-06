<template>
  <div class="GridTopic">
    <DxDataGrid :data-source="sales" :show-borders="true" key-expr="orderId" @selection-changed="selectSale">
      <DxColumn :width="90" data-field="orderId" caption="Order ID" />
      <DxColumn data-field="city" />
      <DxColumn :width="180" data-field="country" />
      <DxColumn data-field="region" />
      <DxColumn data-field="date" data-type="date" />
      <DxColumn :width="90" data-field="amount" format="currency" />
      <DxPaging :page-size="15" />
      <DxSelection mode="single" />
      <DxFilterRow :visible="true" />
    </DxDataGrid>
    <p id="selected-employee" v-if="selectedSale">
      Selected : {{ selectedSale.city }}
    </p>
  </div>
</template>
<script>
import { sales } from "../../../modules/data.js";
import { DxSelectBox } from "devextreme-vue/select-box";
import {
  DxDataGrid,
  DxColumn,
  DxPaging,
  DxSelection,
  DxFilterRow,
  DxEditing,
  DxFormItem,
} from "devextreme-vue/data-grid";

export default {
  components: {
    DxSelectBox,
    DxDataGrid,
    DxColumn,
    DxPaging,
    DxSelection,
    DxFilterRow,
    DxEditing,
    DxFormItem,
  },
  computed: {
    currentRole() {
      return this.$store.getters.currentRole;
    },
  },
  methods:{
    selectSale(e) {
      e.component.byKey(e.currentSelectedRowKeys[0]).done((sale) => {
        if (sale) {
          this.selectedSale = sale;
        }
      });
    },
  },
  data() {
    return {
      allMode: "page",
      checkBoxesMode: "always",
      sales,
      selectedSale: undefined,
    };
  },
};
</script>
<style scoped>
.GridTopic {
  margin: 10px;
}
.options {
  margin-top: 20px;
  padding: 20px;
  background-color: rgba(191, 191, 191, 0.15);
  position: relative;
}

.caption {
  font-size: 18px;
  font-weight: 500;
}

.option {
  margin-top: 10px;
}

.checkboxes-mode {
  position: absolute;
  right: 20px;
  bottom: 20px;
}

.option > .dx-selectbox {
  width: 150px;
  display: inline-block;
  vertical-align: middle;
}

.option > span {
  margin-right: 10px;
}
</style>
