using Microsoft.AspNetCore.Http;
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.Common.NotificationCenter;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QLNCKH.BL.Dictionary
{
    public class ResearchTopicBL : BaseBL<ResearchTopic> , IResearchTopicBL
    {
        private readonly IResearchTopicDL _researchTopicDL;
        private readonly ISlackNotification _slack;

        public ResearchTopicBL(IBaseDL<ResearchTopic> baseDL, IResearchTopicDL researchTopicDL, ISlackNotification slack) : base(baseDL)
        {
            _researchTopicDL = researchTopicDL;
            _slack = slack;
        }

        /// <summary>
        /// Thêm mới đề tài
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public override BaseResponse Insert(ResearchTopic topic)
        {
            topic.ResearchID = Guid.NewGuid();
            // kiểm tra trùng mã SKU không
            var researchByCode = _baseDL.GetByCode(topic.ResearchCode);

            if(researchByCode != null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = "Đã trùng mã nghiên cứu"
                };
                return response;
            }
            var result = base.Insert(topic);
            result.Data = topic;
            var attach = new List<SlackAttach>()
            {
                new SlackAttach("Chủ nhiệm: ", topic.Fullname, true),
                new SlackAttach("Email: ", topic.Email, true),
                new SlackAttach("Mã đề tài: ", topic.ResearchCode, true),
                new SlackAttach("Mã loại đề tài: ", topic.SpecializeCode, true),
                new SlackAttach("Tên đề tài: ", topic.ResearchName),
            };

            _ = Task.Run(() => _slack.SendSlackNotification(content: "Thêm một đề tài nghiên cứu được đăng ký!", slackAttachs: attach));
            return result;
        }

        public override BaseResponse Update(Guid id, ResearchTopic topic)
        {
            if (!topic.Status.HasValue)
            {
                topic.Status = 0;
            }
            if (!topic.Process.HasValue)
            {
                topic.Status = 0;
            }

            var attach = new List<SlackAttach>()
            {
                new SlackAttach("Tên đề tài : ", topic.ResearchName),
                new SlackAttach("Kết quả : ", ConvertStatus(topic.Status.Value), true),
                new SlackAttach("Trạng thái đề tài: ", ConvertProcess(topic.Process.Value), true),
            };

            _ = Task.Run(() => _slack.SendSlackNotification(content: "Cập nhật đề tài thành công!", slackAttachs: attach));

            return base.Update(id, topic);
        }
        /// <summary>
        /// Lấy đề tài theo người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public BaseResponse GetTopicByUserID(string userID)
        {
            var param = new
            {
                userID = userID
            };
            var result = _researchTopicDL.GetTopicByUserID(param);
            // danh sách rỗng => trả vè mã 204
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.No_ConTent,
                    Data = null
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
        }
        /// <summary>
        /// Tạo tên file mới
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GenFileName(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            var fileName = file.FileName.Substring(0, 2) + DateTime.Now.ToString("yyyyMMddhhmm") + extension;

            return fileName;
        }

        public async Task<bool> WriteFile(IFormFile file, string fileName, string folderName)
        {
            bool isSaveSuccess = false;
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\" + folderName);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\" + folderName, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return isSaveSuccess;
        }

        public List<ResearchTopic> GetAllEmailOfExpreTopic(int perDay)
        {
            return _researchTopicDL.GetAllEmailOfExpreTopic(perDay);
        }

        private string ConvertStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "Hoàn thành nhiệm vụ";
                case 2:
                    return "Không hoàn thành nhiệm vụ";
                case 3:
                    return "Bị hủy";
                case 4:
                    return "Chưa cập nhật";
                default:
                    return "";
            }
        }

        private string ConvertProcess(int process)
        {
            switch (process)
            {
                case 1:
                    return "Không phê duyệt";
                case 2:
                    return "Đang thực hiện";
                case 3:
                    return "Đã hết hạn";
                case 4:
                    return "Đã nghiệm thu";
                case 5:
                    return "Bị hủy";
                default:
                    return "";
            }
        }
    }
}
