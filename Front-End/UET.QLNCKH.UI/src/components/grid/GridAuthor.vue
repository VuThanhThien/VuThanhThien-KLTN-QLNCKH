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
      @selection-changed="selectUser"
      @exporting="exportGrid"
    >
      <DxColumn data-field="fullName" caption="Họ và Tên">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn :calculate-cell-value="formatPosition" caption="Vị trí">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn
        data-field="dateOfBirth"
        data-type="date"
        :width="100"
        caption="Ngày sinh"
      >
        <DxRequiredRule />
      </DxColumn>
      <DxColumn
        :calculate-cell-value="formatDepartment"
        :width="350"
        caption="Phòng ban"
      >
        <DxRequiredRule />
      </DxColumn>
      <DxColumn :width="100" data-field="userCode" caption="MNV" />
      <DxColumn data-field="businessAddress" caption="Nơi công tác">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="email" caption="Email" />
      <DxColumn data-field="phoneNumber" caption="Số điện thoại" />
      <DxColumn data-field="address" :visible="false" caption="Địa chỉ" />
      <DxColumnFixing :enabled="true" />
      <DxFilterRow :visible="true" />
      <DxSearchPanel :visible="true" />
      <DxSelection mode="single" />
      <DxSummary>
        <DxGroupItem summary-type="count" />
      </DxSummary>
      <DxPaging :page-size="15" />
      <DxExport :enabled="true" />
    </DxDataGrid>
    <p id="selected-employee" v-if="selectedUser">
      Selected user: {{ selectedUser.userID }}
    </p>
  </div>
</template>

<script>
import {
  DxDataGrid,
  DxColumn,
  DxRequiredRule,
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
      selectedUser: undefined,
      user: [],
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
    /**Format phòng ban */
    formatDepartment(rowData) {
      if (rowData.department == 1) return "Ban Giám hiệu";
      if (rowData.department == 2) return "Hội đồng Khoa học và Đào tạo";
      if (rowData.department == 3) return "Khoa Công nghệ thông tin";
      if (rowData.department == 4) return "Khoa Điện tử viễn thông";
      if (rowData.department == 5)
        return "Khoa Vật lý kỹ thuật & Công Nghệ Nano";
      if (rowData.department == 6) return "Khoa Cơ học kỹ thuật & Tự động hoá";
      if (rowData.department == 7) return "Khoa Công nghệ nông nghiệp";
      if (rowData.department == 8) return "Viện Công nghệ Hàng không Vũ trụ";
      if (rowData.department == 9)
        return "Bộ môn Công nghệ Xây dựng – Giao thông";
      if (rowData.department == 10)
        return "Viện Tiên tiến về Kỹ thuật và Công nghệ";
      if (rowData.department == 11)
        return "Phòng thí nghiệm Trọng điểm Hệ thống tích hợp thông minh";
      if (rowData.department == 12)
        return "Phòng thí nghiệm Trọng điểm Công nghệ Micro & Nanô";
      if (rowData.department == 13)
        return "Trung tâm Nghiên cứu Điện tử – Viễn thông";
      if (rowData.department == 14)
        return "Trung tâm Công nghệ tích hợp liên ngành Giám sát hiện trường";
      if (rowData.department == 15) return "Phòng Đào tạo";
      if (rowData.department == 16) return "Phòng Tổ chức Cán bộ";
      if (rowData.department == 17) return "Phòng Công tác Sinh viên";
      if (rowData.department == 18) return "Phòng Hành chính – Quản trị";
      if (rowData.department == 19) return "Phòng Kế hoạch Tài chính";
      if (rowData.department == 20)
        return "Phòng Khoa học công nghệ & Hợp tác phát triển";
      if (rowData.department == 21) return "Phòng Thanh tra & Pháp chế";
      if (rowData.department == 22) return "Trung tâm Đảm bảo chất lượng";
      if (rowData.department == 23) return "Trung tâm Máy tính";
    },

    /**Format chức vụ */
    formatPosition(rowData) {
      if (rowData.position == 1) return "Hiệu trưởng";
      if (rowData.position == 2) return "Phó Hiệu trưởng";
      if (rowData.position == 3) return "Trưởng phòng";
      if (rowData.position == 4) return "Phó Trưởng phòng";
      if (rowData.position == 5) return "Chủ tịch Hội đồng nghiên cứu";
      if (rowData.position == 6) return "Cán bộ";
    },
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
    selectUser(e) {
      e.component.byKey(e.currentSelectedRowKeys[0]).done((user) => {
        if (user) {
          this.selectedUser = user;
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
  padding: 15px;
}
#app-container {
  /* margin: 10px; */
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