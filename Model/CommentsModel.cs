using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GrapecityAssignment.Model
{
    [DataContract, Serializable]
    public class CommentsModel
    {
        private int _id;
        [DataMember]
        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _postId;
        [DataMember]
        public int PostId
        {
            get { return _postId; }
            set { _postId = value; }
        }
        private string _content;
        [DataMember]
        [Required]
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string _commentStatus;
        [DataMember]
        [Required]
        public string CommentStatus
        {
            get { return _commentStatus; }
            set { _commentStatus = value; }
        }

        private DateTime _createdDate;
        [DataMember]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private DateTime _modifiedDate;
        [DataMember]
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
        private CrudOperationType _crudOperationType;
        [NotMapped]
        public CrudOperationType CrudOperationType
        {
            get { return _crudOperationType; }
            set { _crudOperationType = value; }
        }
        
        [DataMember]
        [ForeignKey("PostId")]
        public virtual PostModel Post { get; set; }
    }
}
