
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.DL.Dictionary;
using QLNCKH.DL.Interface;
using System;

namespace QLNCKH.BL.Dictionary
{
    public class MemberTopicBL : BaseBL<MemberTopic> , IMemberTopicBL
    {
        private readonly IMemberTopicDL _memberTopicDL;
        public MemberTopicBL(IBaseDL<MemberTopic> baseDL, IMemberTopicDL memberTopicDL) : base(baseDL)
        {
            _memberTopicDL = memberTopicDL;
        }

        public BaseResponse GetMemberTopicByMemberID(Guid memberId)
        {
            var result = _memberTopicDL.GetMemberTopicByMemberID(memberId);
            // danh sách rỗng => trả vè lỗi 400
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = "Not Found"
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

        public BaseResponse GetMemberTopicByTopicID(Guid id)
        {
            var result = _memberTopicDL.GetMemberTopicByTopicID(id);
            // danh sách rỗng => trả vè lỗi 400
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = "Not Found"
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
    }
}
