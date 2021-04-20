
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.DL.Dictionary;
using QLNCKH.DL.Interface;

namespace QLNCKH.BL.Dictionary
{
    public class MemberTopicBL : BaseBL<MemberTopic> , IMemberTopicBL
    {
        private readonly IMemberTopicDL _memberTopicDL;
        public MemberTopicBL(IBaseDL<MemberTopic> baseDL, IMemberTopicDL memberTopicDL) : base(baseDL)
        {
            _memberTopicDL = memberTopicDL;
        }

        public BaseResponse deleteMemberTopic(string UserID, string ResearchTopicID)
        {
            var param = new
            {
                UserID = UserID,
                ResearchTopicID = ResearchTopicID
            };

            var result = _memberTopicDL.deleteMemberTopic(param);

            if (result > 0)
            {
                // khởi tạo dữ liệu trả về
                // thành công ==> trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = result
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về
                // Lỗi thao tác dữ liệu trong db ==> trả về 400
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = "Xóa thất bại"
                };
                return response;
            }
        }
    }
}
