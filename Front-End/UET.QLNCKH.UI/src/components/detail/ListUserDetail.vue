
<template>
  <div class="modal" :class="{ isHide: isHide }">
    <div class="dialog-mask">
      <div draggable="true" class="dialog">
        <!-- nút x thoát dialog  -->
        <button class="close-dialog" @click="cancel">
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
        <!-- tiêu đề dialog  -->
        <h3>THÔNG TIN CÁN BỘ</h3>
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
            <h4>A. THÔNG TIN CHUNG</h4>
            <hr />
            <!-- block 2 là khối chưa 2 trường nhập  -->
            <div class="block-2">
              <!-- block 1 là khối chứ 1 trường nhập  -->
              <div class="block-1">
                <div class="fieldName">Mã số cán bộ (<span>*</span>)</div>
                <!-- nhập mã nhân viên  -->
                <input
                  id="txtEmployeeCode"
                  type="text"
                />
              </div>
              <!-- Nhập họ tên  -->
              <div class="block-1">
                <div class="fieldName">Họ và tên (<span>*</span>)</div>
                <input
                  id="txtFullName"
                  fieldName="FullName"
                  class="input-required"
                  type="text"
                />
              </div>
            </div>
            <!-- Nhập ngày sinh  -->
            <div class="block-2">
              <div class="block-1">
                <div class="fieldName">Ngày sinh</div>
                <input type="date" />
              </div>
              <!-- Nhập giới tính  -->
              <div class="block-1">
                <div class="fieldName">Giới tính</div>
                <select name="gender" id="cbxGender">
                  <option value="-1">Chọn giới tính</option>
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
                <input id="txtIdentityNumber" fieldName="text" type="text" />
              </div>
              <!-- Ngầy cấp cmtnd/cc  -->
              <div class="block-1">
                <div class="fieldName">Quê quán</div>
                <input type="text" />
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Nới cấp cmtnd/cc  -->
                <div class="fieldName">Địa chỉ công tác</div>
                <textarea
                  style="margin: 0px; width: 453px; height: 107px; resize: none"
                ></textarea>
              </div>
            </div>
            <div class="block-2">
              <div class="block-1">
                <!-- Email  -->
                <div class="fieldName">Email (<span>*</span>)</div>
                <input type="email" placeholder="example@domain.com" />
              </div>
              <div class="block-1">
                <!-- Số điện thoại  -->
                <div class="fieldName">Số điện thoại (<span>*</span>)</div>
                <input id="txtPhoneNumber" type="text" />
              </div>
            </div>
            <br />
            <h4>B. THÔNG TIN CÔNG VIỆC</h4>
            <hr />
            <div class="block-2">
              <div class="block-1">
                <!-- Vị trí công việc  -->
                <div class="fieldName">Vị trí</div>
                <select id="cbxPosition" class="m-control">
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
                <select id="cbnDepartment" class="m-control">
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
                <div class="fieldName">Ghi chú</div>
                <textarea
                  style="margin: 0px; width: 453px; height: 107px; resize: none"
                ></textarea>
              </div>
            </div>
          </div>
        </div>
        <!-- footer dialog  -->
        <div class="dialog-footer">
          <div class="footer-content">
            <button id="btn-cancel" @click="cancel">Hủy</button>
            <button id="btn-save">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as axios from "axios";
import service from "../../../modules/data.js";
export default {
  name: "UserDetails",
  props: {
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
  },
  components: {},
  methods: {
    /**Sự kiện hủy */
    cancel() {
      this.$emit("outIsHide", !this.isHide);
    },
    validEmail(email) {
      var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },

    // formatPrice(value) {
    //   if (value) {
    //     var salary = value.toString();
    //     return salary.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
    //   }
    // },

    //   saveEmployee() {
    //     if (this.employee.EmployeeId == null && this.checkForm()) {
    //       axios({
    //         method: "POST",
    //         url: "http://api.manhnv.net/api/employees",
    //         data: this.employee,
    //       }).catch((e) => console.log(e));
    //       console.log(this.employee);
    //     }
    //     if (this.employee.EmployeeId != null && this.checkForm()) {
    //       axios({
    //         method: "PUT",
    //         url: "http://api.manhnv.net/api/employees",
    //         data: this.employee,
    //       }).catch((e) => console.log(e));
    //     }
    //     this.closeDialog();
    //   },
  },
  data() {
    return {
      departments: service.getDepartment(),
      positions: service.getPosition(),
    };
  },
};
</script>

<style>
.dialog {
  position: absolute;
  width: 700px;
  left: 33%;
  top: 4%;
  background-color: #fff;
}
.dialog-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
.isHide {
  display: none;
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
  margin-right: 5%;
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
}
input,
select {
  padding-left: 20px;
  margin-top: 4px;
  width: 200px;
  height: 30px;
  border-radius: 5px;
  border: 0.5px solid rgb(156, 153, 153);
  padding: 5px;
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
  background-color: Transparent !important;
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
</style>