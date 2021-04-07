<template>
  <div id="app-container">
    <div class="navBar" v-if="currentRole == 'Admin'">
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Thêm mới"
          type="default"
          styling-mode="contained"
        />
      </div>
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Sửa"
          type="default"
          styling-mode="contained"
        />
      </div>

      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Xóa"
          type="default"
          styling-mode="contained"
        />
      </div>
    </div>

    <DxDataGrid
      id="dataGrid"
      :data-source="user"
      key-expr="userID"
      :allow-column-resizing="true"
      :column-auto-width="true"
      :allow-column-reordering="true"
      @selection-changed="selectEmployee"
      @exporting="exportGrid"
    >
      <DxColumn data-field="fullName" >
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="position">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="dateOfBirth" data-type="date" :width="100">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="department" :width="100">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="userCode" />
      <DxColumn data-field="businessAddress">
        <!-- :group-index="0" sort-order="asc" -->
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="email" />
      <DxColumn data-field="phoneNumber" />
      <DxColumn data-field="address" :visible="false" />
      <DxColumnChooser :enabled="true" />
      <DxColumnFixing :enabled="true" />
      <DxFilterRow :visible="true" />
      <DxSearchPanel :visible="true" />
      <DxGroupPanel :visible="true" />
      <DxSelection mode="single" />
      <DxSummary>
        <DxGroupItem summary-type="count" />
      </DxSummary>
      <DxPaging :page-size="15" />
      <DxExport :enabled="true" />
    </DxDataGrid>
    <p id="selected-employee" v-if="selectedEmployee">
      Selected employee: {{ selectedEmployee.FullName }}
    </p>
  </div>
</template>

<script>
import {
  DxDataGrid,
  DxColumn,
  DxRequiredRule,
  DxColumnChooser,
  DxColumnFixing,
  DxFilterRow,
  DxSearchPanel,
  DxGroupPanel,
  DxSelection,
  DxSummary,
  DxGroupItem,
  DxMasterDetail,
  DxExport,
  DxPaging,
} from "devextreme-vue/data-grid";
import * as axios from "axios";
import service from "../../../modules/employees.service";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { exportDataGrid } from "devextreme/excel_exporter";
import DxButton from "devextreme-vue/button";
export default {
  name: "App",
  components: {
    DxDataGrid,
    DxColumn,
    DxRequiredRule,
    DxColumnChooser,
    DxColumnFixing,
    DxFilterRow,
    DxSearchPanel,
    DxGroupPanel,
    DxSelection,
    DxSummary,
    DxGroupItem,
    DxMasterDetail,
    DxExport,
    DxPaging,
    DxButton,
  },
  data() {
    return {
      employees: service.getEmployees(),
      selectedEmployee: undefined,
      user: {},
    };
  },
  computed: {
    /**Role hiện tại */
    currentRole() {
      return this.$store.getters.currentRole;
    },
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
    currentToken() {
      return this.$store.getters.currentToken;
    },
  },
  methods: {
    async getAuthorList() {
      const config = {
        headers: { Authorization: `Bearer ${currentToken}` },
      };
      await axios
        .get("https://localhost:44323/api/User", config)
        .then((response) => {
          if (response.data) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text: "Cập nhật thành công ",
            });
          }
          this.user = response.data;
          console.log(this.user);
        })
        .catch((e) => {
          if (e.response.status == 401) {
            this.$notify({
              // bad request
              type: "error",
              title: "THÔNG BÁO",
              text: "Unauthorized",
            });
          }

          if (e.response.status == 500) {
            this.$notify({
              //Lỗi server
              type: "error",
              title: "THÔNG BÁO",
              text: "Vui lòng liên hệ MISA để được hỗ trợ!",
            });
          }
        });
    },
    /**Chọn dòng */
    selectEmployee(e) {
      e.component.byKey(e.currentSelectedRowKeys[0]).done((employee) => {
        if (employee) {
          this.selectedEmployee = employee;
        }
      });
    },
    /**Xuất file */
    exportGrid(e) {
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet("Main sheet");
      exportDataGrid({
        worksheet: worksheet,
        component: e.component,
      }).then(function () {
        workbook.xlsx.writeBuffer().then(function (buffer) {
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "DataGrid.xlsx"
          );
        });
      });
      e.cancel = true;
    },
  },
  async created() {
    debugger;
    await axios
      .get("https://localhost:44323/api/User", {
        headers: {
          Authorization: `Bearer ${this.currentToken}`,
        },
      })
      .then((response) => {
        if (response.data) {
          this.$notify({
            type: "success",
            title: "THÔNG BÁO",
            text: "Cập nhật thành công ",
          });
          this.user = response.data;
          console.log(this.user);
        }
      })
      .catch((e) => {
        if (e.response.status == 401) {
          this.$notify({
            // bad request
            type: "error",
            title: "THÔNG BÁO",
            text: "Unauthorized",
          });
        }

        if (e.response.status == 500) {
          this.$notify({
            //Lỗi server
            type: "error",
            title: "THÔNG BÁO",
            text: "Vui lòng liên hệ MISA để được hỗ trợ!",
          });
        }
      });
  },
};
</script>

<style>
#dataGrid {
  /* height: 500px; */
  padding: 15px;
}
#app-container {
  /* width: 900px; */
  position: relative;
}
#selected-employee {
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0);
}
.navBar {
  display: flex;
  list-style: none;
  padding: 15px 0;
  margin: 0;
  justify-content: flex-end;
  background: #f5f8fa;
  border-bottom: 1px solid lightgrey;
}
.headerBtn {
  margin-right: 15px;
}
</style>