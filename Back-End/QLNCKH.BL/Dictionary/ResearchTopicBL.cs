﻿using Microsoft.AspNetCore.Http;
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.DL.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QLNCKH.BL.Dictionary
{
    public class ResearchTopicBL : BaseBL<ResearchTopic> , IResearchTopicBL
    {
        private readonly IResearchTopicDL _researchTopicDL;
        public ResearchTopicBL(IBaseDL<ResearchTopic> baseDL, IResearchTopicDL researchTopicDL) : base(baseDL)
        {
            _researchTopicDL = researchTopicDL;
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
            return result;
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

        public string GenFileName(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            var fileName = DateTime.Now.Ticks + extension;

            return fileName;
        }

        public async Task<bool> WriteFile(IFormFile file, string fileName)
        {
            bool isSaveSuccess = false;
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }
    }
}
