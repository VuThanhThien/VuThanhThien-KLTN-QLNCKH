
<template>
  <div class="modal">
    <div class="dialog-mask">
      <div draggable="true" class="dialog">
        <!-- nút x thoát dialog  -->
        <router-link :to="{ name: 'ListAuthor' }">
          <button class="close-dialog">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
              class="feather feather-x"
            >
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
        </router-link>
        <!-- tiêu đề dialog  -->
        <h3>THÔNG TIN CỦA TÔI</h3>
        <!-- body dialog  -->
        <div class="dialog-content">
          <!-- khối avatar  -->
          <div class="block-ava">
            <img class="dl-avatar" />
            <div class="avt-subtitle">
              (Vui lòng chọn ảnh có định dạng <br />
              <span>.jpg, .jpeg, .png, .gif</span>)
            </div>
          </div>
          <!-- Khối điền thông tin  -->
          <div class="block-right">
            <h4>-- THÔNG TIN CHUNG --</h4>
            <hr />
            <!-- block 2 là khối chưa 2 trường nhập  -->
            <div class="block-2">
              <!-- block 1 là khối chứ 1 trường nhập  -->
              <div class="block-1">
                <div class="fieldName">Mã số cán bộ (<span>*</span>)</div>
                <!-- nhập mã nhân viên  -->
                <input type="text" v-model="user.userCode" />
              </div>
              <!-- Nhập họ tên  -->
              <div class="block-1">
                <div class="fieldName">Họ và tên (<span>*</span>)</div>
                <input id="txtFullName" type="text" v-model="user.fullName" />
              </div>
            </div>
            <!-- Nhập ngày sinh  -->
            <div class="block-2">
              <div class="block-1">
                <div class="fieldName">Ngày sinh</div>
                <DxDateBox
                  v-model="user.dateOfBirth"
                  type="date"
                  display-format="dd/MM/yyyy"
                />
              </div>
              <!-- Nhập giới tính  -->
              <div class="block-1">
                <div class="fieldName">Giới tính</div>
                <select name="gender" v-model="user.gender">
                  <option value="0" disabled>Chọn giới tính</option>
                  <option value="1">Nữ</option>
                  <option value="2">Nam</option>
                  <option value="3">Khác</option>
                </select>
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Nhập cmtnd/ căn cước  -->
                <div class="fieldName">Số CMTND/ CCCD (<span>*</span>)</div>
                <input type="text" v-model="user.identityCode" />
              </div>
              <!-- Ngầy cấp cmtnd/cc  -->
              <div class="block-1">
                <div class="fieldName">Quê quán</div>
                <input type="text" v-model="user.address" />
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Nới cấp cmtnd/cc  -->
                <div class="fieldName">Địa chỉ công tác</div>
                <DxTextArea :width="450" v-model="user.businessAddress" />
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Số điện thoại  -->
                <div class="fieldName">Số điện thoại (<span>*</span>)</div>
                <input type="text" v-model="user.phoneNumber" />
              </div>
              <div class="block-1">
                <!-- Số điện thoại  -->
                <div class="fieldName">Học vị</div>
                <input type="text" v-model="user.degree" />
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Vị trí công việc  -->
                <div class="fieldName">Vị trí</div>
                <select
                  id="cbxPosition"
                  class="m-control"
                  v-model="user.position"
                >
                  <option value="0" disabled>Chọn một vị trí</option>

                  <option
                    v-for="position in positions"
                    :key="position.id"
                    :value="position.id"
                  >
                    {{ position.name }}
                  </option>
                </select>
              </div>
              <div class="block-1">
                <!-- Phòng ban  -->
                <div class="fieldName">Phòng ban</div>
                <select v-model="user.department">
                  <option value="0" disabled>Chọn một phòng ban</option>

                  <option
                    v-for="department in departments"
                    :key="department.id"
                    :value="department.id"
                  >
                    {{ department.name }}
                  </option>
                </select>
              </div>
            </div>

            <div class="block-2">
              <div class="block-1">
                <div class="fieldName">Thành tích</div>
                <DxTextArea :width="450" v-model="user.achievements" />
              </div>
            </div>
          </div>
        </div>
        <!-- footer dialog  -->
        <div class="dialog-footer">
          <div class="footer-content">
            <router-link :to="{ name: 'ListAuthor' }">
              <button id="btn-cancel">Hủy</button>
            </router-link>
            <button id="btn-save" @click="btnSaveOnclick">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import DxDateBox from "devextreme-vue/date-box";
import DxTextArea from "devextreme-vue/text-area";
import service from "../../../modules/data.js";
export default {
  name: "MyInfo",
  props: {},
  components: { DxDateBox, DxTextArea },

  computed: {
    currentRole() {
      return this.$store.getters.currentRole;
    },
    loggedIn() {
      return this.$store.getters.loggedIn;
    },
    currentToken() {
      return this.$store.getters.currentToken;
    },
    currenID() {
      return this.$store.getters.currentID;
    },

    /**validate thông tin chung */
    validateInfo() {
      let checkInfoVal = {
        error: false,
        msg: "",
        typeError: "",
      };
      //code
      if (this.user.userCode == "" || this.user.userCode == null) {
        checkInfoVal = {
          error: true,
          msg: "Vui lòng nhập mã cán bộ",
          typeError: "userCode",
        };
      }
      //tên
      if (this.user.fullName == "" || this.user.fullName == null) {
        checkInfoVal = {
          error: true,
          msg: "Vui lòng nhập mã cán bộ",
          typeError: "fullName",
        };
      }
      //sđt
      if (this.user.phoneNumber == "" || this.user.phoneNumber == null) {
        checkInfoVal = {
          error: true,
          msg: "Vui lòng nhập số điện thoại",
          typeError: "phoneNumber",
        };
      }
      return checkInfoVal;
    },
  },

  methods: {
    /**Hàm sửa */
    async putUser() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };

      const bodyParameters = this.user;
      await axios
        .put(
          "https://localhost:44323/api/User/MyInfo/" + this.user.userID,
          bodyParameters,
          config
        )
        .then((response) => {
          if (response.data) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text:
                "Cập nhật thành công thông tin cán bộ " + this.user.fullName,
            });
            this.$router.push('ListAuthor');
          }
        })
        .catch((e) => {
          if (e.response.status == 403) {
            this.$notify({
              // bad request
              type: "error",
              title: "THÔNG BÁO",
              text: "Bạn không đủ quyền để chỉnh sửa, vui lòng liên hệ Admin!",
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
          if (e.response.status == 401) {
            this.$notify({
              //Lỗi server
              type: "error",
              title: "THÔNG BÁO",
              text: "Vui lòng đăng nhập lại",
            });
            
            setTimeout(() => {
              this.$router.push('Login');
            }, 1000);
          }
        });
    },

    /**event btn save
     * createdby VTTHien 11/04/21
     */
    btnSaveOnclick() {
      if (this.validateInfo.error) {
        this.$notify({
          title: "THÔNG BÁO",
          type: "warn",
          text: this.validateInfo.msg,
        });
      } else {
        this.putUser();
      }
    },
  },
  data() {
    return {
      departments: service.getDepartment(),
      positions: service.getPosition(),
      user: [],
    };
  },

  async created() {
    await axios
      .get("https://localhost:44323/api/User/" + this.currenID, {
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
        console.log(e);
      });
  },
};
</script>

<style scoped>
.dx-datebox {
  max-width: 85% !important;
}
.dialog {
  padding: 10px;
  position: absolute;
  width: 700px;
  left: 33%;
  top: 4%;
  background-color: #fff;
}
.dialog-mask {
  position: fixed;
  z-index: 3;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
h3 {
  padding: 20px;
  font-weight: bold;
  text-transform: uppercase;
}
h4 {
  padding-top: 10px;
  margin-left: 14px;
}

hr {
  width: 450px;
  margin-left: 0;
  border: 2px solid #5cb85c;
  background-color: #5cb85c;
  margin-left: 14px;
}
.close-dialog {
  margin: 5px;
  float: right;
  top: 0;
  border: none;
  background-color: #fff !important;
  cursor: pointer;
}
.dialog-content {
  display: flex;
  justify-content: space-between;
}
.block-ava {
  width: 24%;
  height: calc(100% - 2px);
  margin-left: 5px;
  margin-top: 30px;
}

.dl-avatar {
  width: 160px;
  margin-left: 5%;
  margin-right: 6%;
  height: 160px;
  background-position: center;
  background-repeat: no-repeat;
  border-radius: 50%;
  cursor: pointer;
  align-items: center;
  justify-content: center;
  outline: 1px black;
  border: 1px solid #ccc;
  background-image: url("../../assets/default-avatar.jpg");
  background-size: contain;
  background-repeat: no-repeat;
}

.avt-subtitle {
  font-size: 10px;
  width: 100%;
  height: 20%;
  text-align: center;
}

.avt-subtitle span {
  font-weight: bold;
}

.block-right {
  display: block;
}
.block-2 {
  display: flex;
  justify-content: space-between;
  width: 500px;
  align-items: center;
}
.block-1 {
  width: 250px;
  height: calc(100% - 2px);
  margin-left: 15px;
  margin-top: 10px;
}
.block-1 span {
  color: #ff0000;
}
.fieldName {
  font-size: 13px;
  font-weight: bold;
  margin-bottom: 2px;
  margin-top: 5px;
}
input,
select {
  padding-left: 20px;
  margin-top: 4px;
  width: 200px;
  height: 30px;
  border-radius: 5px;
  border: 0.5px solid rgb(204, 204, 204);
  padding: 5px;
}
input:hover,
select:hover {
  border: 0.5px solid rgb(183, 192, 245);
}
.dialog-footer {
  margin-top: 20px;
  width: 100%;
  height: 43px;
  background-color: #e9ebee;
  border-radius: 0 0 5px 5px;
  display: flex;
  align-items: center;
  justify-items: right;
  border-radius: 2px;
}

.dialog-footer .footer-content {
  position: absolute;
  right: 0px;
  margin-right: 9px;
}

.dialog-footer button {
  margin-right: 20px;
  height: 31px;
  padding-left: 10px;
  padding-right: 10px;
  background-color: #5cb85c;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

#btn-save {
  width: 70px;
  border: solid 1px #ffffff;
}

#btn-cancel {
  border: solid 1px #ffffff;
  width: 70px;
}

.currency-for-input {
  position: absolute;
  right: 40px;
  line-height: 40px;
  font-size: xx-small;
  font-style: italic;
  color: black !important;
}

.dialog-footer button:hover {
  background-color: azure;
}
option {
  text-overflow: ellipsis;
}
</style>