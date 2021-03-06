
const departments = [
  {
    "id": 1,
    "name": "Ban Giám hiệu"
  },
  {
    "id": 2,
    "name": "Hội đồng Khoa học và Đào tạo"
  },
  {
    "id": 3,
    "name": "Khoa Công nghệ thông tin"
  },
  {
    "id": 4,
    "name": "Khoa Điện tử viễn thông"
  },
  {
    "id": 5,
    "name": "Khoa Vật lý kỹ thuật & Công Nghệ Nano"
  },
  {
    "id": 6,
    "name": "Khoa Cơ học kỹ thuật & Tự động hoá"
  },
  {
    "id": 7,
    "name": "Khoa Công nghệ nông nghiệp"
  },
  {
    "id": 8,
    "name": "Viện Công nghệ Hàng không Vũ trụ"
  },
  {
    "id": 9,
    "name": "Bộ môn Công nghệ Xây dựng – Giao thông"
  },
  {
    "id": 10,
    "name": "Viện Tiên tiến về Kỹ thuật và Công nghệ"
  },
  {
    "id": 11,
    "name": "Phòng thí nghiệm Trọng điểm Hệ thống tích hợp thông minh"
  },
  {
    "id": 12,
    "name": "Phòng thí nghiệm Trọng điểm Công nghệ Micro & Nanô"
  },
  {
    "id": 13,
    "name": "Trung tâm Nghiên cứu Điện tử – Viễn thông"
  },
  {
    "id": 14,
    "name": "Trung tâm Công nghệ tích hợp liên ngành Giám sát hiện trường"
  },
  {
    "id": 15,
    "name": "Phòng Đào tạo"
  },
  {
    "id": 16,
    "name": "Phòng Tổ chức Cán bộ"
  },
  {
    "id": 17,
    "name": "Phòng Công tác Sinh viên"
  },
  {
    "id": 18,
    "name": "Phòng Hành chính – Quản trị"
  },
  {
    "id": 19,
    "name": "Phòng Kế hoạch Tài chính"
  },
  {
    "id": 20,
    "name": "Phòng Khoa học công nghệ & Hợp tác phát triển"
  },
  {
    "id": 21,
    "name": "Phòng Thanh tra & Pháp chế"
  },
  {
    "id": 22,
    "name": "Trung tâm Đảm bảo chất lượng"
  },
  {
    "id": 23,
    "name": "Trung tâm Máy tính"
  },

];
const positions = [
  {
    "id": 1,
    "name": "Hiệu trưởng"
  },
  {
    "id": 2,
    "name": "Phó Hiệu trưởng"
  },
  {
    "id": 3,
    "name": "Trưởng phòng"
  },
  {
    "id": 4,
    "name": "Phó trưởng phòng"
  },
  {
    "id": 5,
    "name": "Chủ tịch hội đồng nghiên cứu"
  },
  {
    "id": 6,
    "name": "Cán bộ"
  },
  {
    "id":7,
    "name": "Chủ nhiệm khoa"
  },
  {
    "id":8,
    "name":"Phó chủ nhiệm khoa"
  },
];

const statuses = [
  {
    "id": 1,
    "name": "Hoàn thành nhiệm vụ"
  },
  {
    "id": 2,
    "name": "Không hoàn thành nhiệm vụ"
  },
  {
    "id": 3,
    "name": "Bị hủy"
  },
  {
    "id": 4,
    "name": "Chưa cập nhật"
  },
];

const processArr = [
  {
    "id": 1,
    "name": "Không phê duyệt"
  },
  {
    "id": 2,
    "name": "Đang thực hiện"
  },
  {
    "id": 3,
    "name": "Đã hết hạn"
  },
  {
    "id":4,
    "name" : "Đã nghiệm thu"
  },
  {
    "id":5,
    "name" : "Đề xuất"
  }
];
const genders = [
  {
    "id": 1,
    "name": "Nữ"
  },
  {
    "id":2,
    "name":"Nam"
  },
  {
    "id" :3,
    "name": "Khác"
  }
];

const options =[
  {
    "id" : 3,
    "name" : "Đề tài của tôi",
    "route":"MyTopic"
  },
  // {
  //   "id" : 2,
  //   "name" : "Đề tài mới",
  //   "route":"NewTopic"
  // },
  {
    "id" : 1,
    "name" : "Tất cả đề tài",
    "route":"ListTopic"
  },
  {
    "id" : 5,
    "name" : "Tất cả tác giả",
    "route":"ListAuthor"
  },
  {
    "id" : 6,
    "name" : "Thống kê",
    "route":"ChartExpire"
  },
  // {
  //   "id" : 4,
  //   "name" : "Đề tài sắp nghiệm thu",
  //   "route":"Expiring"
  // },
  {
    "id" : 7,
    "name" : "Thiết lập gửi Mail tự động",
    "route":"SetCronJob"
  },
  {
    "id" : 8,
    "name" : "Gửi Email",
    "route":"Email"
  },
  {
    "id": 9,
    "name": "Thông tin của tôi",
    "route":"MyInfo"
  }
];
const optionsForUser =[
  {
    "id" : 3,
    "name" : "Đề tài của tôi",
    "route":"MyTopic"
  },
  {
    "id" : 1,
    "name" : "Tất cả đề tài",
    "route":"ListTopic"
  },
  {
    "id": 9,
    "name": "Thông tin của tôi",
    "route":"MyInfo"
  }
]
const myRouteInName = ['MyTopic','MyInfo', 'about'];
export default {
  getDepartment() {
    return departments;
  },
  getPosition(){
    return positions;
  },
  getStatus(){
    return statuses;
  },
  getProcess(){
    return processArr;
  },
  getGender(){
    return genders;
  },
  getRoute(){
    return myRouteInName;
  },
  getOption(){
    return options;
  },
  getOptionsForUser(){
    return optionsForUser;
  }
};