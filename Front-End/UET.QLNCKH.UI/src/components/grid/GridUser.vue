<template>
  <div id="app-container">
    <div class="navBar" v-if="currentRole == 'Admin'">
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Thêm User"
          type="success"
          styling-mode="contained"
          @click="btnAddOnClick"
        />
      </div>
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Sửa User"
          type="success"
          styling-mode="contained"
          @click="btnEditOnClick"
        />
      </div>
      <UserDetail
        :isHide="isHideParent"
        @outIsHide="outIsHide"
        :selectedUser="selectedUser"
        :editMode="editMode"
      />
      <div class="headerBtn">
        <DxButton
          :width="120"
          text="Xóa User"
          type="success"
          styling-mode="contained"
          @click="btnDeleteOnClick"
        />
      </div>
    </div>
    <DxLoadPanel
      :position="positionLoading"
      :visible="loadingVisible"
      :show-indicator="true"
      :show-pane="true"
      :shading="true"
      :close-on-outside-click="false"
      :on-shown="onShown"
      shading-color="rgba(0,0,0,0.4)"
    />
    <DxDataGrid
      id="dataGrid"
      :data-source="user"
      :show-borders="true"
      key-expr="userID"
      :allow-column-resizing="true"
      :column-auto-width="true"
      :allow-column-reordering="false"
      @selection-changed="selectUser"
      @exporting="exportGrid"
    >
      <DxColumn data-field="fullName" caption="Họ và Tên">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn
        :width="100"
        data-field="userCode"
        sort-order="asc"
        caption="Mã cán bộ"
      />
      <DxColumn data-field="gender" caption="Giới tính">
        <DxLookup :data-source="genders" display-expr="name" value-expr="id" />
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="position" caption="Vị trí">
        <DxLookup
          :data-source="positions"
          display-expr="name"
          value-expr="id"
        />
        <DxRequiredRule />
      </DxColumn>
      <DxColumn
        data-field="dateOfBirth"
        data-type="date"
        :width="100"
        caption="Ngày sinh"
        format="dd/MM/yyyy"
      >
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="department" :width="350" caption="Phòng ban">
        <DxRequiredRule />
        <DxLookup
          :data-source="departments"
          display-expr="name"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn data-field="businessAddress" caption="Địa chỉ công tác">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn data-field="email" c aption="Email" />
      <DxColumn data-field="phoneNumber" caption="Số điện thoại" />
      <DxColumn data-field="address" :visible="false" caption="Địa chỉ" />
      <DxColumnFixing :enabled="true" />
      <DxFilterRow :visible="true" />
      <DxSearchPanel :visible="true" />
      <DxGroupPanel
        :visible="true"
        empty-panel-text="Kéo cột muốn nhóm lại vào đây !"
      />
      <DxSelection mode="single" />
      <DxSummary>
        <DxGroupItem summary-type="count" />
      </DxSummary>
      <DxPaging :page-size="15" />
      <DxExport :enabled="true" />
    </DxDataGrid>
    <notifications position="bottom right" clean: true style="margin-bottom:
    20px"/>
    <vue-confirm-dialog></vue-confirm-dialog>
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
  DxLookup,
} from "devextreme-vue/data-grid";
import service from "../../../modules/data.js";
import * as axios from "axios";
import UserDetail from "../detail/ListUserDetail.vue";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { DxLoadPanel } from "devextreme-vue/load-panel";
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
    UserDetail,
    DxLookup,
    DxLoadPanel,
  },
  data() {
    return {
      positionLoading: { of: "#dataGrid" },
      genders: service.getGender(),
      departments: service.getDepartment(),
      positions: service.getPosition(),
      selectedUser: {},
      user: [],
      isHideParent: true,
      loadingVisible: false,
      editMode: 0,
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
    /**Hiển thị panel loading */
    showLoadPanel() {
      this.loadingVisible = true;
      this.getUserList();
      setTimeout(() => {
        // this.$router.go();
      }, 500);
    },
    /**Thời gian hiển thị loading panel */
    onShown() {
      setTimeout(() => {
        this.loadingVisible = false;
      }, 500);
    },
    btnAddOnClick() {
      // Mở form
      this.isHideParent = !this.isHideParent;
      this.selectedUser = {};
      this.editMode = 1;
    },

    /**Sự kiện nút sửa
     * Createdby VTT 09/04/21
     */
    btnEditOnClick() {
      if (this.selectedUser.userID == null) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn người dùng muốn sửa",
        });
        this.isHideParent = true;
      } else {
        this.isHideParent = !this.isHideParent;
        this.editMode = 2;
      }
    },

    /**btn xóa */
    btnDeleteOnClick() {
      if (this.selectedUser.userID == null) {
        this.$notify({
          title: "THÔNG BÁO",
          text: "Vui lòng chọn đề tài muốn xóa",
        });
      } else {
        this.$confirm({
          message:
            `Xóa người dùng đồng nghĩa với xóa tài khoản này. Bạn có chắc chắn muốn xóa người dùng ` +
            this.selectedUser.fullName +
            ` không ?`,
          button: {
            no: "Hủy",
            yes: "Xóa",
          },
          callback: (confirm) => {
            if (confirm) {
              // ... do something
              const config = {
                headers: { Authorization: `Bearer ${this.currentToken}` },
              };
              axios
                .delete(
                  "https://localhost:44323/api/User/" +
                    this.selectedUser.userID,
                  config
                )
                .then((response) => {
                  if (response.data) {
                    this.showLoadPanel();
                    this.$notify({
                      type: "success",
                      title: "THÔNG BÁO",
                      text:
                        "Xóa thành công người dùng " +
                        this.selectedUser.fullName,
                    });
                  }
                })
                .catch((e) => {
                  console.log(e);
                });
            }
          },
        });
      }
    },

    /**event đóng form từ cha */
    outIsHide(e) {
      this.isHideParent = e;
      this.showLoadPanel();
    },
    /**lấy danh sách user */
    async getUserList() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };
      await axios
        .get("https://localhost:44323/api/User", config)
        .then((response) => {
          if (response.data) {
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
              text: "Vui lòng liên hệ  để được hỗ trợ!",
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
          this.user = response.data;
        }
      })
      .catch((e) => {
        if (e.response.status == 401) {
          this.$notify({
            type: "error",
            title: "THÔNG BÁO",
            text: "Unauthorized",
          });
          setTimeout(() => {
              this.$router.push('Login');
            }, 1000);
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