using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// Banner
    /// </summary>
    public class Banner
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public virtual string ImgUrl { get; set; }
        /// <summary>
        /// 类别:--0：跳转至 h5 页面；1：跳转至店铺详情页；2：跳转至商品详情页；3：跳PC页面
        /// </summary>
        public virtual int RecordType { get; set; }
        /// <summary>
        /// 类别记录ID:店铺id/商品id【当RecordType=1 or RecordType=2】
        /// </summary>
        public virtual int RecordID { get; set; }
        /// <summary>
        /// 跳转url
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// Banner类型
        /// </summary>
        public virtual int BannerType { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public virtual int CityID { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }

        public Banner()
        {
            this.ID = 0;
            this.ImgUrl = "";
            this.RecordType = 0;
            this.RecordID = 0;
            this.Url = "";
            this.BannerType = 0;
            this.CityID = 0;
            this.IsLocked = false;
        }
    }
}
