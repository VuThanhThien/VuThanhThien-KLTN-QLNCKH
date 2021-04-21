using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.Dictionary
{
    public class ResearchTopicDto
    {
        /// <summary>
        /// ID đề tài
        /// </summary>
        public Guid ResearchID { get; set; }

        /// <summary>
        /// Mã đề tài
        /// </summary>
        public string ResearchCode { get; set; }

        /// <summary>
        /// Tên đề tài
        /// </summary>
        public string ResearchName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Trạng thái(  1- hoàn thành nhiệm vụ, 2- không hoàn thành nhiệm vụ, 3- bị hủy)
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Quá trình làm (1- đợi xét chọn, 2- đang làm, 3- đã hết hạn)
        /// </summary>
        public int? Process { get; set; }

        /// <summary>
        /// ID tác giả
        /// </summary>
        public Guid UserID { get; set; }


        /// <summary>
        /// Tạo ngày
        /// </summary>
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Chỉnh sửa bởi
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Kinh phí thực sự
        /// </summary>
        public decimal? Expense { get; set; }

        /// <summary>
        /// Ngày dự kiến kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// Tiền yêu cầu VNU tài trợ
        /// </summary>
        public decimal? SponsorMoney { get; set; }

        /// <summary>
        /// Mã chuyên ngành
        /// </summary>
        public string SpecializeCode { get; set; }

        /// <summary>
        /// Loại nghiên cứu
        /// </summary>
        public string TypeResearch { get; set; }

        /// <summary>
        /// Cơ sở vật chất
        /// </summary>
        public string Infrastructure { get; set; }

        /// <summary>
        /// Thuyết minh đề cương
        /// </summary>
        public string Present { get; set; }

        /// <summary>
        /// Số bài báo ISI
        /// </summary>
        public int? ISI { get; set; }

        /// <summary>
        /// Số tạp chí quốc tế
        /// </summary>
        public int? Journal { get; set; }

        /// <summary>
        /// Công bố phát minh sáng chế / Giải pháp hữu ích
        /// </summary>
        public int? UsefullSolution { get; set; }

        /// <summary>
        /// Sản phẩm công nghệ chuyển giao
        /// </summary>
        public int? TransferProduct { get; set; }

        /// <summary>
        /// Báo cáo tư vấn chính sách cho nhà nước
        /// </summary>
        public int? ReportToState { get; set; }

        /// <summary>
        /// Sách chuyên khảo
        /// </summary>
        public int? Monograph { get; set; }

        /// <summary>
        /// Công bố khác
        /// </summary>
        public int? OtherResult { get; set; }
        /// <summary>
        /// Số ng tham gia nghiên cứu
        /// </summary>
        public int? NumberResearcher { get; set; } = 1;

        public List<MemberTopic> NewOrEditMembers { get; set; }
        public List<MemberTopic> DeleteMembers { get; set; }


    }
}
