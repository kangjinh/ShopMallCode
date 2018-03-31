using System;
namespace NH.Entity.Config
{
    public class BasicInfo
    {
        /// <summary>
        /// 支付提醒
        /// </summary>
        public string PayPrompt { get; set; }

        /// <summary>
        /// 消息中心消息图标
        /// </summary>
        public string MessageCenterIcon { get; set; }

        /// <summary>
        /// 育儿大师微信公众号
        /// </summary>
        public int WeixinAccount { get; set; }
        
        /// <summary>
        /// 3分钟免费电话咨询活动相关配置
        /// </summary>
        public ThreeMinuteOfFreePhoneCall ThreeMinuteOfFreePhoneCall { get; set; }

        /// <summary>
        /// 排班统计接口返回以本周一计算前几天（不包括本周一）
        /// </summary>
        public int SchedulingSummaryPrevDays { get; set; }
        /// <summary>
        /// 排班统计接口返回以本周一计算后几天（包括本周一）
        /// </summary>
        public int SchedulingSummaryNextDays { get; set; }

        /// <summary>
        /// 打赏金额
        /// </summary>
        public decimal RewardAmount { get; set; }
        /// <summary>
        /// 最小打赏金额
        /// </summary>
        public decimal MinRewardAmount { get; set; }
        /// <summary>
        /// 最大打赏金额
        /// </summary>
        public decimal MaxRewardAmount { get; set; }

        /// <summary>
        /// 见面解读见面地点电话沟通时长（单位：秒）
        /// </summary>
        public int IGSBookAddressTimeLimit { get; set; }


        /// <summary>
        /// IGS见面解读时间限制
        /// </summary>
        public int MeetAnswerTimeLimit { get; set; }
        /// <summary>
        /// IGS电话解读时间限制
        /// </summary>
        public int PhoneAnswerTimeLimit { get; set; }
        /// <summary>
        /// IGS报告分享链接
        /// </summary>
        public string IGSReportShareUrl { get; set; }
        /// <summary>
        /// IGS报告在线预览链接
        /// </summary>
        public string IGSReportOnlineUrl { get; set; }

        /// <summary>
        /// 开启邀请专家审核通过奖励（1.开启）
        /// </summary>
        public int OpenExpertAuthAward { get; set; }

        /// <summary>
        /// V2.5.0线下课程分享url（机构课程）
        /// </summary>
        public string OffLineCourseShareUrl { get; set; }
        /// <summary>
        /// V2.5.0线上课程分享url（育儿大讲堂）
        /// </summary>
        public string OnlineCourseShareUrl { get; set; }
        /// <summary>
        /// V2.5.0线上课程目录url（育儿大讲堂）
        /// </summary>
        public string OnlineCourseIndexUrl { get; set; }
        /// <summary>
        /// V2.5.0线上课程套餐分享url（育儿大讲堂）
        /// </summary>
        public string OnlinePackageShareUrl { get; set; }
        /// <summary>
        /// V2.5.0课程砍价URl
        /// </summary>
        public string CourseKanUrl { get; set; }

        /// <summary>
        /// 报名成功提醒
        /// </summary>
        public int ApplySuccessRemind { get; set; }

        /// <summary>
        /// FM Banner 广告编号
        /// </summary>
        public int FMBannerAdId { get; set; }
        /// <summary>
        /// FM 启动页 广告编号
        /// </summary>
        public int FMStartPageAdId { get; set; }

        /// <summary>
        /// 东莞贫血活动相关配置
        /// </summary>
        public AnemiaActicity Anemia { get; set; }
    }

    /// <summary>
    /// 3分钟免费电话咨询活动相关配置
    /// </summary>
    public class ThreeMinuteOfFreePhoneCall
    {
        /// <summary>
        /// 3分钟免费电话咨询活动马甲头像，格式：url,url,url,url,url
        /// </summary>
        public string ThreeMinuteVestAvatar { get; set; }

        /// <summary>
        /// 专家打完电话后推送中的链接
        /// </summary>
        public string AfterPhoneFinishJumpUrl { get; set; }

        /// <summary>
        /// 专家打完电话后推送的标题
        /// </summary>
        public string AfterPhoneFinishTitle { get; set; }

        /// <summary>
        /// 专家打完电话后推送的描述
        /// </summary>
        public string AfterPhoneFinishRemark { get; set; }

        /// <summary>
        /// 抢单成功推送消息中的跳转页面
        /// </summary>
        public string AfterGrabOrderJumpUrl { get; set; }

        /// <summary>
        /// 抢单成功后推送的标题
        /// </summary>
        public string AfterGrabOrderTitle { get; set; }

        /// <summary>
        /// 抢单成功后推送的描述
        /// </summary>
        public string AfterGrabOrderRemark { get; set; }

        /// <summary>
        /// 抢单成功
        /// </summary>
        public int GrabOrderRemind { get; set; }

        /// <summary>
        /// 3分钟免费咨询完毕
        /// </summary>
        public int WeixinRemind { get; set; }

        /// <summary>
        /// 到期预告
        /// </summary>
        public int ComingRemind { get; set; }

        /// <summary>
        /// 预告推送中的链接
        /// </summary>
        public string RemindJumpUrl { get; set; }

        /// <summary>
        /// 预告推送中的标题
        /// </summary>
        public string RemindTitle { get; set; }

        /// <summary>
        /// 预告推送中的内容
        /// </summary>
        public string RemindRemark { get; set; }

        /// <summary>
        /// 专家分享的活动时间(秒)
        /// </summary>
        public int ExpertShareSeconds { get; set; }
        /// <summary>
        /// 专家分享的奖励金额(元)
        /// </summary>
        public decimal ExpertShareAmount { get; set; }
    }

    /// <summary>
    /// 东莞贫血活动
    /// </summary>
    public class AnemiaActicity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 空腹时间(小时)
        /// </summary>
        public string FastingHours { get; set; }
        /// <summary>
        /// 查看检测结果天数
        /// </summary>
        public string ResultDays { get; set; }
        /// <summary>
        /// 检测地点
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 温馨提示
        /// </summary>
        public string Tips { get; set; }
        /// <summary>
        /// 幼儿园报名的检测时间
        /// </summary>
        public string KidDetection { get; set; }
        /// <summary>
        /// 幼儿园地点
        /// </summary>
        public string KidAddress { get; set; }
        /// <summary>
        /// 幼儿园报名温馨提示
        /// </summary>
        public string KidTips { get; set; }
    }
}
