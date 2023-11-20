using System;

namespace Blog.MVP.Blazor.SSR.Models
{
    public class BlogArticleVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string bID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string bsubmitter { get; set; }

        /// <summary>
        /// 标题blog
        /// </summary>
        public string btitle { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string bcategory { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string bcontent { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int btraffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int bcommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public string bUpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string bCreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bRemark { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool? IsDeleted { get; set; }


        public string Token { get; set; }

    }

}
