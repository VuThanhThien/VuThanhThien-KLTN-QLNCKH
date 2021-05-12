<template>
  <div class="login-form">
    <h1 class="login-heading">Gửi email tới cán bộ nghiên cứu</h1>
    <form>
      <div class="form-control">
        <label for="email">Gửi tới:</label>
        <input-tag v-model="param.sendTo"></input-tag>
      </div>

      <div class="form-control mb-more">
        <label for="TitleEmail">Tiêu đề:</label>
        <input
          type="text"
          name="TitleEmail"
          id="TitleEmail"
          class="login-input"
          v-model="param.title"
        />
      </div>

      <div class="form-control mb-more">
        <label for="Content">Nội dung:</label>
        <textarea
          class="login-input"
          style="height: 200px; resize: none"
          v-model="param.content"
        />
      </div>

      <div class="form-control">
        <button type="submit" class="btn-submit" @click="sendEmail">Xác nhận</button>
      </div>
    </form>
    <notifications position="bottom right" clean: true style="margin-bottom:
    20px"/>
  </div>
</template>

<script>
import * as axios from "axios";
export default {
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
  },
  data() {
    return {
      tags: [],
      param: {
        sendTo: [],
        title: "",
        content: "",
      },
    };
  },
  methods: {
    sendEmail() {
      const config = {
        headers: { Authorization: `Bearer ${this.currentToken}` },
      };
      const bodyParameters = this.param;
      axios
        .post(
          "https://localhost:44323/api/NotificationCenter/send-email",
          bodyParameters,
          config
        )
        .catch((e) => {
          if (e.response.status == 200) {
            this.$notify({
              type: "success",
              title: "THÔNG BÁO",
              text: "Bạn đã gửi mail thành công",
            });
          }

          if (e.response.status == 400) {
            this.$notify({
              type: "error",
              title: "THÔNG BÁO",
              text: "Có lỗi xảy ra",
            });
          }

          if (e.response.status == 401) {
            this.$notify({
              type: "errpr",
              title: "THÔNG BÁO",
              text: "Vui lòng đăng nhập lại",
            });
          }
        });
    },
  },
};
</script>

<style scoped>
input {
  height: 38px;
  padding: 4px 4px 4px 0;
}
.login-input {
  padding: 4px 0 0 4px;
}
.btn-submit {
  width: 100%;
  padding: 14px 12px;
  font-size: 18px;
  font-weight: bold;
  background: #60bd4f;
  color: white;
  border-radius: 3px;
  cursor: pointer;
  outline: none;
  border: none;
}
</style>